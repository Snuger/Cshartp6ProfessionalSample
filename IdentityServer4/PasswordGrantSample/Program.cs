using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace PasswordGrantSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //获取discover
            string token = string.Empty;
            var client = new HttpClient();
            var discover = await client.GetDiscoveryDocumentAsync("http://localhost:5000/identity");
            if (discover.IsError)
            {

                Console.WriteLine(discover.Error);
                return;
            }


            //请求token
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = discover.TokenEndpoint,
                ClientId = "tone",
                ClientSecret = "RELt6nw3JGC",
                //Scope = "784419794395860992", 可填，可不填，测试OK

                UserName = "10001",
                Password = "12345678"

            });

            if (tokenResponse.IsError)
            {

                System.Console.WriteLine(tokenResponse.Error);
                return;
            }

            token = tokenResponse.AccessToken;
            System.Console.WriteLine(tokenResponse.Json);

           // var apiClient = new HttpClient();
            //apiClient.SetToken(string.Empty, token);

            Console.ReadLine();
        }
    }
}
