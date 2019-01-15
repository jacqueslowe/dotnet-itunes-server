using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class AppleResponse
    {
        public int resultCount { get; set; }
        public List<AppleEntity> results { get; set; }
    }
}
