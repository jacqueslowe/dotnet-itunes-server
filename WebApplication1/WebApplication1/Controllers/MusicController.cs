using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MusicController : Controller
    {
        private string appleURI = "https://itunes.apple.com/search?";
        private int searchLimit = 25;
        private string entityType = "song";

        // route example: http://localhost:49887/music/?artist=air
        [HttpGet("/music")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(AppleResponse))]
        public async Task<IActionResult> Get([FromQuery]string artist)
        {
            string url = appleURI +
                    "term=" + artist +
                    "&limit=" + searchLimit +
                    "&entity=" + entityType;
            if (artist == null)
            {
                return BadRequest();
            }

            var jsonString = await this.GetAsync(url);
            var appleResponse = JsonConvert.DeserializeObject<AppleResponse>(jsonString);
            if (appleResponse == null)
            {
                return NotFound();
            }

            return Ok(appleResponse);
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
