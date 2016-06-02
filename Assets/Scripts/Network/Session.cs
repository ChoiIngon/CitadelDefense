﻿using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Gamnet
{
	public abstract class Session : MonoBehaviour
	{
        public static T CreateSession<T>() where T :  Session
        {
            GameObject go = new GameObject();
            go.name = typeof(T).ToString();
            DontDestroyOnLoad(go);
            return (T)go.AddComponent<T>();
        }

        public enum State
        {
            Disconnected,
            OnConnecting,
            Connected
        }
        public abstract class NetworkEvent
        {
            public Session session;
            public abstract void Event();
        };
        public class ConnectEvent : NetworkEvent
        {
            public override void Event()
            {
                session.state = State.Connected;
                session.sendQueue.Clear();
                session.OnConnect();
            }
        }
        public class ReconnectEvent : NetworkEvent
        {
            public override void Event()
            {
                session.state = State.Connected;
                session.OnReconnect();
                while (0 < session.sendQueue.GetCount())
                {
                    session.PostSend(session.sendQueue.Dequeue());
                }
            }
        }
        public class ErrorEvent : NetworkEvent
        {
            public System.Exception error;
            public override void Event()
            {
                session.OnError(error);
            }
        }
        public class ReceiveEvent : NetworkEvent
        {
            public Gamnet.Buffer buffer;
            public override void Event()
            {
                session.OnReceive(buffer);
            }
        }
        public class CloseEvent : NetworkEvent
        {
            public override void Event()
            {
                session.state = State.Disconnected;
                session.OnClose();
            }
        }
        
        public class SendStateObject
        {
            public Socket socket = null;
            public Gamnet.Buffer buffer = null;
        }
        public class ReceiveStateObject
        {
            public Socket socket = null;
            public Gamnet.Buffer buffer = null;
        }
        public class SyncQueue<T> : Queue<T>
        {
            private object _syncObject = new object();

            public int GetCount()
            {
                lock (_syncObject)
                    return base.Count;
            }

            public void Enqueue(T container)
            {
                lock (_syncObject)
                {
                    base.Enqueue(container);
                }
            }

            public T Dequeue()
            {
                lock (_syncObject)
                    return base.Dequeue();
            }

            public void Clear()
            {
                lock (_syncObject)
                    base.Clear();
            }
        };
        
        public abstract void OnConnect();
        public abstract void OnReconnect();
        public abstract void OnClose();
        public abstract void OnError(System.Exception e);
        public abstract void OnReceive(Gamnet.Buffer buf);

        private Socket _socket = null;
        private IPEndPoint _endPoint = null;
        public  SyncQueue<NetworkEvent> eventQueue = new SyncQueue<NetworkEvent>();
        private SyncQueue<byte[]> sendQueue = new SyncQueue<byte[]>();

        public State state = State.Disconnected;

		public    void Connect(string host, int port)
		{
			try {
                state = State.OnConnecting;
                IPAddress ip = null;
                try
                {
                    ip = IPAddress.Parse(host);
                }
                catch(System.FormatException)
                {
                    IPHostEntry hostEntry = Dns.GetHostEntry(host);
                    if(hostEntry.AddressList.Length > 0)
                    {
                        ip = hostEntry.AddressList[0];
                    }
                }
				_endPoint = new IPEndPoint(ip, port);
				Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				socket.BeginConnect(_endPoint, new AsyncCallback(Callback_OnConnect), socket);
			}
			catch (System.Exception error)
			{
                Error(error);
			}
		}
        private   void Reconnect()
        {
            try
            {
                if(State.Disconnected != state)
                {
                    return;
                }
                state = State.OnConnecting;
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.BeginConnect(_endPoint, new AsyncCallback(Callback_OnConnect), socket);
            }
            catch(System.Exception error)
            {
                Error(error);
            }
        }
		private   void Receive()
		{
			try
			{
				ReceiveStateObject obj = new ReceiveStateObject();
				obj.socket = _socket;
				obj.buffer = new Gamnet.Buffer();
				_socket.BeginReceive(obj.buffer.buffer, 0, Gamnet.Buffer.BUFFER_SIZE, 0, new AsyncCallback(Callback_OnReceive), obj);
			}
			catch (System.Exception error)
			{
                Error(error);
                Close();
			}
		}
        protected void Error(System.Exception e)
        {
            ErrorEvent evt = new ErrorEvent();
            evt.session = this;
            evt.error = e;
            eventQueue.Enqueue(evt);
        }
        public    void Close()
        {
            try
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.BeginDisconnect(false, new AsyncCallback(Callback_OnClose), null);
            }
            catch (System.Exception error)
            {
                Error(error);
            }
        }
        private   void Callback_OnConnect(IAsyncResult result)
        {
            try
            {
                bool isReconnect = false;
                if (null != _socket)
                {
                    isReconnect = true;
                }
                _socket = (Socket)result.AsyncState;
                _socket.EndConnect(result);
                _socket.ReceiveBufferSize = Gamnet.Buffer.BUFFER_SIZE;
                _socket.SendBufferSize = Gamnet.Buffer.BUFFER_SIZE;
                if (true == isReconnect)
                {
                    ReconnectEvent evt = new ReconnectEvent();
                    evt.session = this;
                    eventQueue.Enqueue(evt);
                }
                else
                {
                    ConnectEvent evt = new ConnectEvent();
                    evt.session = this;
                    eventQueue.Enqueue(evt);
                }
                Receive();
            }
            catch (System.Exception error)
            {
                Error(error);
                Close();
            }
        }
        private   void Callback_OnReceive(IAsyncResult result)
		{
			try
			{
				ReceiveStateObject obj = (ReceiveStateObject)result.AsyncState;
				int recvBytes = _socket.EndReceive(result);

				if(0 == recvBytes)
				{
                    Close();
					return;
				}
                obj.buffer.writeIndex += recvBytes;

				ReceiveEvent evt = new ReceiveEvent();
				evt.session = this;
				evt.buffer = obj.buffer;
				eventQueue.Enqueue(evt);
				Receive();
			}
			catch (System.Exception error)
			{
                Error(error);
			}
		}
        private   void Callback_OnSend(IAsyncResult result)
        {
            try
            {
                SendStateObject obj = (SendStateObject)result.AsyncState;
                int writedBytes = _socket.EndSend(result);
                obj.buffer.readIndex += writedBytes;
                if (obj.buffer.Size() > 0)
                {
                    Gamnet.Buffer newBuffer = new Gamnet.Buffer(obj.buffer);
                    AsyncSend(newBuffer.buffer);
                }
            }
            catch (System.Exception error)
            {
                Error(error);
                Close();
            }
        }
        private   void Callback_OnClose(IAsyncResult result)
        {
            try
            {
                _socket.EndDisconnect(result);
                _socket.Close();
                _socket = null;
                CloseEvent evt = new CloseEvent();
                evt.session = this;
                eventQueue.Enqueue(evt);
            }
            catch (System.Exception error)
            {
                Error(error);
            }
        }

        public    void AsyncSend(byte[] buf)
        {
            try
            {
                Reconnect();
                PostSend(buf);
            }
            catch (System.Exception error)
            {
                Error(error);
                Close();
            }
        }		
        private   void PostSend(byte[] buf)
        {
            if (State.Connected == state)
            {
                SendStateObject sendObj = new SendStateObject();
                sendObj.socket = _socket;
                sendObj.buffer = new Gamnet.Buffer();
                sendObj.buffer.Copy(buf);
                _socket.BeginSend(buf, 0, buf.Length, 0, new AsyncCallback(Callback_OnSend), sendObj);
            }
            else
            {
                sendQueue.Enqueue(buf);
            }
        }
		
        public void Update()
        {
            while (0 < eventQueue.GetCount())
            {
                NetworkEvent evt = eventQueue.Dequeue();
                evt.Event();
            }
        }
    }
}
/*
7da8f495-b9f7-4a07-9c96-39c64d0f125a1b8d0d30-efc0-4b69-9bb7-9ee8478e56f5*/