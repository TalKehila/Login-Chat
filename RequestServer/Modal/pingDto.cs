using System.Net.NetworkInformation;

namespace RequestServer.Modal
{
    public class pingDto
    {
        public static bool PingAddress(string adress , int timeout)
        {
            try
            {
                using (Ping  ping = new Ping())
                {
                    PingReply reply = ping.Send(adress, timeout);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch (PingException)
            {
            return false;
            }
        }
    }
}