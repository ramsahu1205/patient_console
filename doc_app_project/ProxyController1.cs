using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace doc_app_project
{
    [Route("proxy/{**url}")]
    [ApiController]
    public class ProxyController1 : ControllerBase
    {
        [HttpGet]
        public async Task<dynamic> Get(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
              new MediaTypeWithQualityHeaderValue("application/json"));
            string backendUrl = EnvData.data[EnvCont.server_url] + "/" + url;
            Console.WriteLine("proxy path==>" + backendUrl);
            var response = await client.GetAsync(backendUrl);
            response.Content.Headers.Remove("Content-Type");
            response.Content.Headers.Add("Content-Type", "application/json");
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }


        [HttpPost]
        public async Task<dynamic> Post( string url, [FromBody] object bodyData)
        {
            Console.WriteLine("object data==>" + bodyData.ToString());
            StringContent data = new StringContent(bodyData.ToString(), Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            string backendUrl = EnvData.data[EnvCont.server_url] + "/" + url;
            Console.WriteLine("proxy path==>" + backendUrl);
            var response = await client.PostAsync(backendUrl,data);
            response.Content.Headers.Remove("Content-Type");
            response.Content.Headers.Add("Content-Type", "application/json");
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }

    }
}
