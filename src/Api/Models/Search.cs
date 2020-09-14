using System.Collections.Generic;

namespace Api.Models
{
    public class Search
    {
        public int Total { get; set; }
        public List<Joke> Result { get; set; } = new List<Joke>();
    }
}