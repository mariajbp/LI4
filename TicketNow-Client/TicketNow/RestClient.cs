using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class RestClient<T>
{
    private string token = null;

    // This code matches the HTTP Request that we included in our api controller
    public async Task<string> checkLogin(string username, string password)
    {
       
        var client = new HttpClient();
        var byteArray = Encoding.ASCII.GetBytes(username+":"+password);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

        //to test with localhost, change the next ip(192.168.43.42) to your pc local ip
        HttpResponseMessage response = await client.GetAsync("http://192.168.43.42:5000/api/login");
        HttpContent content = response.Content;

        //Read the string.
        string result = await content.ReadAsStringAsync();
        if (result != null &&
            result.Length >= 50)
        {
            
            JObject jdynamic = JsonConvert.DeserializeObject<dynamic>(result);
            var accessToken = jdynamic.Value<string>("token");

            //get token
            token = accessToken;
            
            
        }
        return token;
    }

}