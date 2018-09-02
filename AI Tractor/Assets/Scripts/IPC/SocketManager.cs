using System;
using UnityEngine;


public class SocketManager
{
	static string HOST = "127.0.0.1";
	static int PORT = 8000;

	System.Net.Sockets.TcpClient socket;
	System.IO.Stream stream;
	System.IO.StreamReader reader;
	System.IO.StreamWriter writer;

	public SocketManager ()
	{
		this.socket = new System.Net.Sockets.TcpClient(HOST, PORT);
		this.stream = this.socket.GetStream ();
		this.reader = new System.IO.StreamReader (stream);
		this.writer = new System.IO.StreamWriter (stream);
	}

	public String GetInfo(String msg){
		this.writer.Write(msg + "\n");
		this.writer.Flush ();
		return this.reader.ReadLine ();
	}
}

