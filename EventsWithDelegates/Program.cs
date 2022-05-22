using System;

namespace EventsWithDelegates
{
    //creating delegate what expects to string params and returns null
    //this delegate can hold references to another methods
    public delegate void Handler(string alpha, int beta);
    internal class Program
    {
        //creating event and assigning delegate to it. so methods that subscribe to event 
        //have to be of same structure as Delegate, it will hold references to methods.
        //Event cannot exist without delegate. its just wrapper for delegate
        public static event Handler Stuck;
        static void Main(string[] args)
        {
            //adding that method to event. Invoking event. that will run all methods that are registered to it
            Stuck += ShowPrice;
            //Stuck.Invoke("Text", 100);


            var video = new Video() { Title = "Video 1" };
            var videoEncoder = new VideoEncoder();//publisher
            var mailService = new MailService();//subscriber
            var messageService = new MessageService();//subscriber

            //subscribing to VIdeoEncoded event method from mailSerice called OnVideoEncoded
            //methods must have same structure as delegate in VideoEncoder class
            videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
            videoEncoder.VideoEncoded += messageService.OnVideoEncoded;
            videoEncoder.Encode(video);

        }
        static void ShowPrice(string alpha, int beta)
        {
            Console.WriteLine($"alpha: {alpha}, beta: {beta}");
        }
    }
}
