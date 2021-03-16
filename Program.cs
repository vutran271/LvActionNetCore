using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace LvActionNetCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

            BotDavid botDavid = new BotDavid();
            var page = await botDavid.ConnectRoulette("zq207", "v123",1,155);
            
            
            // change source code
            
            // if success call console event to store data
            
        }
    }
}
