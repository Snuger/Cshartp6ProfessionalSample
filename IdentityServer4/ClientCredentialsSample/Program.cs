using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace ClientCredentialsSample
{
    class Program
    {
        static  async Task Main(string[] args)
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

           var tokenResponse= await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest(){
                Address=discover.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                //Scope = "784030086364925952"   

           });
        
            if (tokenResponse.IsError)
            {

                System.Console.WriteLine(tokenResponse.Error);
                return;
            }

            token = tokenResponse.AccessToken;
            System.Console.WriteLine(tokenResponse.Json);
      
			
			
			 var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync("http://localhost:5088/WeatherForecast");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
			
			Console.WriteLine("====================分隔线=========================");
		    response = await apiClient.GetAsync("http://localhost:5088/Weather");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }	
	
            Console.ReadLine();
        }
    }
}
