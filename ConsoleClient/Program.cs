using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Program pr = new Program();
			pr.run();
		}


		public void run()
		{
			//TcpListener(System.Net.IPAddress localaddr, int port)
			TCPClient.Main0(null);
		}
	}
}
