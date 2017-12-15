using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devRantDotNet.Source.Models
{
    /// <summary>
    /// This is the rant model for the current DevRant rants
    /// </summary>
    public class Rant
    {

        public long id { get; set; }
        public string text { get; set; }
        public int score { get; set; }
        public int rt { get; set; }
        public int rc { get; set; }
        public long created_time { get; set; }
        public string attachedImageUrl { get; set; }
        public int num_comments { get; set; }
        public IList<string> tags { get; set; } = new List<string>();
        public int vote_state { get; set; }
        public bool edited { get; set; }
        public long user_id { get; set; }
        public string user_username { get; set; }
        public long user_score { get; set; }
        public string user_avatar_url { get; set; }
        public List<Comment> rant_comments { get; set; } = new List<Comment>();
    }
}
