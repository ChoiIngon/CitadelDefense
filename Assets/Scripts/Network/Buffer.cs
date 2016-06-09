using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Gamnet {
	public class Buffer
	{
        public static int BUFFER_SIZE = 65535;
		public byte[] buffer = null;
		
		public int readIndex = 0;
		public int writeIndex = 0;
		public Buffer() 
		{
			buffer = new byte[BUFFER_SIZE];
		}
        public Buffer(byte[] src)
		{
			buffer = new byte[BUFFER_SIZE];
            Copy(src);
		}
        public Buffer(System.IO.MemoryStream ms)
        {
            buffer = new byte[BUFFER_SIZE];
            Copy(ms);
        }
        public Buffer(Buffer src)
        {
            buffer = new byte[BUFFER_SIZE];
            Copy(src);
        }        
		public int Size()
		{
			return writeIndex - readIndex;
		}
		public int Available()
		{
			return BUFFER_SIZE - writeIndex;
		}
		public void Copy(byte[] src)
		{
			System.Buffer.BlockCopy (src, 0, buffer, 0, src.Length); 
			writeIndex = src.Length;
			readIndex = 0;
		}
		public void Copy(System.IO.MemoryStream ms)
		{
			System.Array.Copy(ms.GetBuffer(), 0, buffer, readIndex, ms.GetBuffer().Length);
		}
		public void Copy(Buffer src)
		{
            if(0 == src.Size())
            {
                return;
            }
			System.Buffer.BlockCopy(src.buffer, src.readIndex, buffer, 0, src.Size());
			writeIndex = src.Size ();
			readIndex = 0;
		}
		public void Append(Buffer src)
		{
			System.Buffer.BlockCopy(src.buffer, src.readIndex, buffer, writeIndex, src.Size());
			writeIndex += src.Size ();
		}
		public static void BlockCopy (Buffer src, Buffer dest, int size)
		{
			System.Buffer.BlockCopy (src.buffer, src.readIndex, dest.buffer, 0, size);
			dest.readIndex = 0;
			dest.writeIndex = size;
		}
		public static implicit operator System.IO.MemoryStream(Gamnet.Buffer src)  // explicit byte to digit conversion operator
		{
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			ms.Write(src.buffer, src.readIndex, src.Size());
			ms.Seek(0, System.IO.SeekOrigin.Begin);
			return ms;
		}
        public static implicit operator byte[](Gamnet.Buffer src)
		{
			byte [] dest = new byte[src.Size ()];
			System.Buffer.BlockCopy (src.buffer, src.readIndex, dest, 0, src.Size ());
			return dest;
		}
        public static Gamnet.Buffer operator + (Gamnet.Buffer lhs, Gamnet.Buffer rhs)
        {
            Buffer buffer = new Buffer();
            buffer.Append(lhs);
            buffer.Append(rhs);
            return buffer;
        }
	}
}