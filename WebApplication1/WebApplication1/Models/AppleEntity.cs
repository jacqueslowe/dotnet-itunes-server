using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AppleEntity
    {
        public string kind { get; set; }
        //artists based attributes
        public string artistId { get; set; }
        public string artistName { get; set; }
        public string artistViewUrl { get; set; }
        // track based attributes
        public string trackName { get; set; }
        public string trackId { get; set; }
        public string trackNumber { get; set; }
        public string previewUrl { get; set; }
        
        //from
        public string collectionName { get; set; }
        public string country { get; set; }
       
       
    }
}
