using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ClientSession : Gamnet.StreamSession
{
    public string host;
    public int port;
    
    // Use this for initialization
    void Start()
    {
		//Connect(host, port);
    }

    public override void OnConnect()
    {
    }
    public override void OnReconnect()
    {
    }
    public override void OnClose()
    {
    }
    public override void OnError(System.Exception e)
    {
    }
}

