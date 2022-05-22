using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventsWithDelegates
{
    public class VideoEventArgs : EventArgs
    {
        public Video Video { get; set; }
    }
    /// <summary>
    /// VideoEncoder class knows nothing about MailService or TextService
    /// We can add more methods that will subscribe to event. 
    /// </summary>
    public class VideoEncoder
    {
        //delegate which determines method structure in subscriber. that will subscribe to event
        //it expects VideoEventArgs that should have Video object
        public delegate void VideoEncodedEventHandler(object source, VideoEventArgs args);

        //Define an Event based on that delegate. passing delegate. 
        public event VideoEncodedEventHandler VideoEncoded;
        //EventHandler, EventHandler<TEventArgs> Or can use System delegate EventHandler.
        //passing Event Args in brackets
        public event EventHandler<VideoEventArgs> VideoEncoded1;


        //Publish/Raise event
        public void Encode(Video video)
        {
            //simulating encoding. setting thread to sleep for three seconds
            Console.WriteLine("Encoding video...");
            Thread.Sleep(3000);

            //Notifies subscribers that are interested in that event
            OnVideoEncoded(video);
        }
        //Event publisher methods should be pretected, virtual and void. Name should start with word "On"
        protected virtual void OnVideoEncoded(Video video)
        {
            //checking if there are subscribers to this event. notifying subsribers
            if(VideoEncoded != null)
            {
                //invoking event. that will run all methods subscribed to it. passing source(this class), passing additional data
                VideoEncoded.Invoke(this, new VideoEventArgs() { Video = video});
            }
        }
    }
}
