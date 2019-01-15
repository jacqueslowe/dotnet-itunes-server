using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MusicController
    {
        private string appleURI = "https://itunes.apple.com/search?";
        private int searchLimit = 25;
        private string entityType = "song";

        // route example: http://localhost:49887/music/?artist=air
        [Microsoft.AspNetCore.Mvc.Route("{artist}")]
        public async Task<AppleResponse> Index([FromQuery] string artist)
        {
            string url = appleURI +
                    "term=" + artist + 
                    "&limit=" + searchLimit +
                    "&entity=" + entityType;
            if(artist != null )
            {
                var jsonString = await this.GetAsync(url);
                var response = JsonConvert.DeserializeObject<AppleResponse>(jsonString);

                return response;
            }
            return new AppleResponse();
        }

        public async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (System.IO.Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
