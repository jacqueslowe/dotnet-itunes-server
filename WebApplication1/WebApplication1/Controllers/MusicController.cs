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
        
       

        // request example: http://localhost:49887/music/?artist=air
        [HttpGet("/music")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(200, Type = typeof(AppleResponse))]
        public async Task<IActionResult> GetMusic([FromQuery]string artist)
        {
            if (artist == null)
            {
                return BadRequest();
            }

            string url = buildRequest( "song", artist);
           
            var responseString = await this.GetAsync(url);
            if (responseString == null)
            {
                return NoContent();
            }

            var appleResponse = JsonConvert.DeserializeObject<AppleResponse>(responseString);
            if (appleResponse == null)
            {
                return NotFound();
            }

            return Ok(appleResponse);
        }

        // request example: http://localhost:49887/movies/?movie=avengers
        [HttpGet("/movies")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(200, Type = typeof(AppleResponse))]
        public async Task<IActionResult> GetMovies([FromQuery]string movie)
        {
            if (movie == null)
            {
                return BadRequest();
            }

            string url = buildRequest("movie", movie);

            var responseString = await this.GetAsync(url);
            if (responseString == null)
            {
                return NoContent();
            }

            var appleResponse = JsonConvert.DeserializeObject<AppleResponse>(responseString);
            if (appleResponse == null)
            {
                return NotFound();
            }

            return Ok(appleResponse);
        }

        private string buildRequest(string type, string term )
        {
            string appleURI = "https://itunes.apple.com/search?";
            int searchLimit = 25;
            return appleURI +
                  "term=" + term +
                  "&limit=" + searchLimit +
                  "&entity=" + type;
        }
        private async Task<string> GetAsync(string uri)
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
