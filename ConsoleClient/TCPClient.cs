using System;
using System.Net.Sockets;
using System.Text;

class TCPClient
{
	public static void Main0(string[] args)
	{
		// Set the server IP address and port number
		string serverIP = "127.0.0.1";
		int port = 8888;

		try
		{
			// Create a TcpClient
			TcpClient client = new TcpClient(serverIP, port);

			// Get a client stream for reading and writing
			NetworkStream stream = client.GetStream();

			// Buffer for reading data
			byte[] bytes = new byte[1024];

			while (true)
			{
				// Enter the string to be transmitted to the server
				Console.Write("Enter text to send to server: ");
				string data = Console.ReadLine();

				// Translate the text string to bytes
				byte[] msg = Encoding.ASCII.GetBytes(data);

				// Send the message to the connected server
				stream.Write(msg, 0, msg.Length);
				Console.WriteLine("Sent: {0}", data);

				// Receive the response from the server
				int bytesRead = stream.Read(bytes, 0, bytes.Length);
				string responseData = Encoding.ASCII.GetString(bytes, 0, bytesRead);
				Console.WriteLine("Received: {0}", responseData);
			}
			
			// Close everything
			stream.Close();
			client.Close();
		}
		catch (ArgumentNullException e)
		{
			Console.WriteLine("ArgumentNullException: {0}", e);
		}
		catch (SocketException e)
		{
			Console.WriteLine("SocketException: {0}", e);
		}

		Console.WriteLine("\nPress Enter to exit...");
		Console.ReadLine();
	}
}
