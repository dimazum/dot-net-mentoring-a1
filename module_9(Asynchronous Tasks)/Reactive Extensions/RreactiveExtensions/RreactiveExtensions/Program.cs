using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reactive.Linq;
using System.Text;

namespace RreactiveExtensions
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>() { 1, 2, 34, 554, 645, 64 };

            var obsList = list.ToObservable<int>();

              var obsListDisposable = obsList.Subscribe(i => Console.WriteLine("Next element: {0}", i),
                e => Console.WriteLine("Error: {0}", e.Message),
                () =>
                {
                    Console.WriteLine("Range observation complete");
                });
              obsListDisposable.Dispose();


            var urls = new string[] { "http://rsdn.ru", "http://gotdotnet.ru", "http://blogs.msdn.com" };

            var observableWebResponses = from url in Observable.ToObservable(urls)
                                         let request = WebRequest.Create(url)
                                         from response in request.GetResponseAsync()
                                         select new { Url = url, response.ContentLength };
         

            var aggregatedResults = observableWebResponses
                .TimeInterval()
                .Aggregate(
                    new StringBuilder(),
                    (sb, r) =>
                    {
            sb.AppendFormat("{0}: {1}, interval {2}ms",
                            r.Value.Url, r.Value.ContentLength,
                            r.Interval).AppendLine();
            return sb;
                    });

            try
            {
                var requestsResult = aggregatedResults.First().ToString();
                Console.WriteLine("Aggregated web request results:{0}{1}", Environment.NewLine, requestsResult);
            }
            catch (WebException e)
            {
                Console.WriteLine("Error requesting web page: {0}", e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine("Error writing results to file: {0}", e.Message);
            }
            Console.ReadLine();
        }
    }
}
