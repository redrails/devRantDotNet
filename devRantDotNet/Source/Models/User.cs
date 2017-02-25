using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devRantDotNet.Source.Models
{
    public class User
    {

        public string username { get; set; }
        public long score { get; set; }
        public string about { get; set; }
        public string location { get; set; }
        public long created_time { get; set; }
        public string skills { get; set; }
        public string github { get; set; }
        public string website { get; set; }
        public List<Rant> rants { get; set; } = new List<Rant>();
        public List<Rant> upvoted { get; set; } = new List<Rant>();
        public List<Comment> comments { get; set; } = new List<Comment>();
        public List<Rant> favorites { get; set; } = new List<Rant>();
        public int counts_rants { get; set; }
        public int counts_upvoted { get; set; }
        public int counts_comments { get; set; }
        public int counts_favorites { get; set; }
        public int counts_collabs { get; set; }
        public string avatar { get; set; }
    }
}
