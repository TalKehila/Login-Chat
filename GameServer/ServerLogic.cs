using FirebaseAdmin.Messaging;

namespace GameServer
{
	public class ServerLogic
	{
		public static void LoadGame(bool load)
		{
			try
			{	

				if (load)
				{
                    Console.WriteLine("game Loaded Succssfuly");
                }
			}
			catch(Exception ex)
			{
                Console.WriteLine($"the game Not Loaded {ex.Message}");
            }
		}

		public static void Answer(bool UserApprove)
		{
			try
			{
				if (UserApprove)
				{				
                    Console.WriteLine("game load succssfully");
				}
				else
				{
                    Console.WriteLine("Sorry the game was denined");
                }
			}
			catch(Exception ex)
			{
                Console.WriteLine($"Failed to process request: {ex.Message} ");
            }			
		}
	}
}

// send game reqest 
// game deneced // cancel or aprove 
//gamestart 
// player move
