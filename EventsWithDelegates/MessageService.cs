using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWithDelegates
{
    /// <summary>
    /// Class responsible for sending text message when video is encoded
    /// </summary>
    public class MessageService
    {
        public void OnVideoEncoded(object soruce, VideoEventArgs e)
        {
            Console.WriteLine($"MessageService: Sending a text message... {e.Video.Title}");
        }
    }
}
