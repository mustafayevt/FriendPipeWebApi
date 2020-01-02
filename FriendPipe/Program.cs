using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FriendPipe
{
    public class Program
    {
        public static void Main(string[] args)
        {
  //          var host = new WebHostBuilder()

  //.UseKestrel()

  //.UseUrls("http://157.230.123.5:80")

  //.UseContentRoot(Directory.GetCurrentDirectory())

  //.UseStartup<Startup>()

  //.Build();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseKestrel()
                .UseStartup<Startup>();
    }
}
