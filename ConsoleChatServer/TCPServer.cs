using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

class TCPServer
{
	public static void Main0()
	{

		TcpListener server = null;
		try
		{
			// Set the TCP IP address and port number for the server
			IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
			int port = 8888;

			// Create a TcpListener to listen for incoming connections
			server = new TcpListener(ipAddress, port);

			// Start listening for client requests
			server.Start();

			// Buffer for reading data
			byte[] bytes = new byte[1024];
			string data;

			// Enter the listening loop
			while (true)
			{
				Console.WriteLine("Waiting for a connection...");

				// Perform a blocking call to accept requests
				TcpClient client = server.AcceptTcpClient();
				Console.WriteLine("Connected!");

				// Get a stream object for reading and writing
				NetworkStream stream = client.GetStream();

				int i;

				// Loop to receive all the data sent by the client
				while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
				{
					// Translate data bytes to a ASCII string
					data = Encoding.ASCII.GetString(bytes, 0, i);
					Console.WriteLine("Received: {0}", data);

					//aaa;tal1;msg;hello
					string[] sss = data.Split(';');
					byte[] msg = null;

					if (sss[1]=="srv" && sss[2]=="online")
					{
						// Add sss[0] to online user list
						// Send all users the updated list
						data = "srv;;online;aaa,tasl1";

						msg = Encoding.ASCII.GetBytes(data);
						stream.Write(msg, 0, msg.Length);
						Console.WriteLine("Sent: {0}", data);

					}

					if (sss[2] == "msg")
					{
						// Find user sss[1]
						// send sss[1] data
						
					}
						msg = Encoding.ASCII.GetBytes(data);
						stream.Write(msg, 0, msg.Length);
						Console.WriteLine("Sent: {0}", data);

					// Echo back to client
					// Send back a response

				}
				Thread.Sleep(1000);
				// Shutdown and end connection
				client.Close();
			}
		}
		catch (SocketException e)
		{
			Console.WriteLine("SocketException: {0}", e);
		}
		finally
		{
			// Stop listening for new clients
			server.Stop();
		}
		Console.WriteLine("\nServer shutdown...");
		Console.ReadLine();
	}
}
