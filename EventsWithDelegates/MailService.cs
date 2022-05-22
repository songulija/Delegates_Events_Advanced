using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWithDelegates
{
    /// <summary>
    /// Class responsible for sending email when video is encoded
    /// </summary>
    public class MailService
    {
        public void OnVideoEncoded(object source, VideoEventArgs e)
        {
            Console.WriteLine($"MailService: Sending and email... {e.Video.Title}");
        }
    }
}
