using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devRantDotNet.Source.Models
{
    public class Comment
    {
        public long id { get; set; }
        public long rant_id { get; set; }
        public string body { get; set; }
        public int num_upvotes { get; set; }
        public int num_downvotes { get; set; }
        public int score { get; set; }
        public long created_time { get; set; }
        public int vote_state { get; set; }
        public long user_id { get; set; }
        public string user_username { get; set; }
        public long user_score { get; set; }
        public string user_avatar { get; set; }

    }
}
