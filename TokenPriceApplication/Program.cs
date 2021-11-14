using Microsoft.Extensions.DependencyInjection;
using Model.Models;
using Newtonsoft.Json;
using Service.Repository;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Timers;
using System.Threading.Tasks;

namespace TokenPriceApplication
{

    class TokenPrice
    {
        public string USD { get; set; }
    }

    class Program
    {
        private static HttpClient client = new HttpClient();
        private static Timer aTimer;

        static void Main()
        {
            client.BaseAddress = new Uri("https://min-api.cryptocompare.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //Dependency injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITokenRepository, TokenRepository>()
                .AddSingleton<ITokenService, TokenService>()
                .BuildServiceProvider();

            var tokenService = serviceProvider.GetService<ITokenService>();

            aTimer = new Timer();
            aTimer.Interval = 5 * 60 * 1000;
            aTimer.Elapsed += (sender, e) => PullPrice(sender, e, tokenService);
            aTimer.Enabled = true;
            Console.WriteLine("Press the Enter key to exit the program at any time... ");
            //First run
            PullPrice(null, null, tokenService);
            Console.ReadLine();
        }

        static async Task<decimal> GetTokenPriceAsync(string fSyms, string tSyms = "USD")
        {
            var priceJson = "";
            HttpResponseMessage response = await client.GetAsync($"data/price?fsym={fSyms}&tsyms={tSyms}");
            if (response.IsSuccessStatusCode)
            {
                priceJson = await response.Content.ReadAsStringAsync();
            }

            TokenPrice tokenPrice = JsonConvert.DeserializeObject<TokenPrice>(priceJson);

            var value = tokenPrice.GetType().GetProperty(tSyms).GetValue(tokenPrice, null);

            decimal price;
            var validPrice = decimal.TryParse((string)value, out price);
            return validPrice ? price : 0;
        }

        static void PullPrice(object source, ElapsedEventArgs e, ITokenService tokenService)
        {
            try
            {
                Console.WriteLine("===== Start Pull Price =====");

                IEnumerable<Token> allToken = tokenService.GetAll();

                allToken.ToList().ForEach(async token =>
                {
                    var price = await GetTokenPriceAsync(token.Symbol);
                    token.Price = price;
                    tokenService.Update(token);
                    Console.WriteLine($"Token [{token.Symbol}] price: {price}");
                });
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }
}

