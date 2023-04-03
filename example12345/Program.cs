using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpCompletionOptionDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

            using var host = builder.Build();
            await host.StartAsync();

            for (int i = 0; i < 100; i++)
            {
                using var client = new HttpClient();
                var response = await client.GetAsync("http://localhost:5000/api/values", HttpCompletionOption.ResponseHeadersRead);
                string status = response.Headers.GetValues("isOk").First();
                Console.WriteLine($"Status client:{status}");
       
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Good,status client cod {(int)response.StatusCode}");

                }
                else
                {
                    Console.WriteLine($"Bad,status client cod {(int)response.StatusCode}");
                }
            }


            //Console.WriteLine($"Total bytes read: {totalRead}");

            await host.StopAsync();
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static int count = 200;
        // GET api/values
        [HttpGet]
        public ActionResult<Message> Get()
        {
            if (count > 399)
                count = 0;
            HttpContext.Response.StatusCode = count++;
            HttpContext.Response.Headers.Add("isOk", count.ToString());
            Console.WriteLine($"Server send:{count}");
            return new Message("4343");
            //throw new Exception();
        }
    }
    public record Message(string messageException);
}
