using devRantDotNet.Source.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace devRantDotNet
{

    public enum SortType
    {
        algo,
        top,
        recent
    }

    public class devRant
    {
        private string MakeRequest(string url)
        {
            var request = WebRequest.Create(url+Values.AppId);
            request.ContentType = "application/json; charset=utf-8";
            string t;
            var response = (HttpWebResponse)request.GetResponse();

            using(var sr = new StreamReader(response.GetResponseStream()))
            {
                t = sr.ReadToEnd();
            }

            return t;
        }

        private Rant JSONToRantObject(dynamic r)
        {
            var rant = new Rant
            {
                id = r.id,
                text = r.text,
                num_upvotes = r.num_upvotes,
                num_downvotes = r.num_downvotes,
                score = r.score,
                created_time = r.created_time,
                num_comments = r.num_comments,
                tags = r.tags.ToObject<List<string>>(),
                vote_state = r.vote_state,
                edited = r.edited,
                user_id = r.user_id,
                user_username = r.user_username,
                user_score = r.user_score,
                user_avatar_url = r.user_avatar.i
            };
            try
            {
                rant.attachedImageUrl = r.attached_image.url;
            }
            catch { }
            return rant;

        }

        public List<Rant> GetRants()
        {
            var req = MakeRequest(Values.AllRants);
            dynamic results = JsonConvert.DeserializeObject<dynamic>(req);

            if (results.success != "true")
            {
                throw new Exception("Something went wrong");
            }

            List<Rant> rants = new List<Rant>();

            for(int i = 0; i<results.rants.Count; i++)
            {
                var r = results.rants[i];
                Rant rant = JSONToRantObject(r);

                rants.Add(rant);
            }

            return rants;
        }

        public Rant GetRant(int id)
        {
            try
            {
                var req = MakeRequest(Values.SingleRant + id + Values.AppId);
                dynamic results = JsonConvert.DeserializeObject<dynamic>(req);
                var r = results.rant;

                Rant rant = JSONToRantObject(r);

                for (var i = 0; i < results.comments.Count; i++)
                {
                    var current = results.comments[i];
                    rant.rant_comments.Add(new Comment
                    {
                       id = current.id,
                       rant_id = current.rant_id,
                       body = current.body,
                       num_upvotes = current.num_upvotes,
                       num_downvotes = current.num_downvotes,
                       score = current.score,
                       created_time = current.created_time,
                       vote_state = current.vote_state,
                       user_id = current.user_id,
                       user_username = current.user_username,
                       user_score = current.user_score,
                       user_avatar = current.user_avatar.i
                    });
                }
                return rant;
            } catch (Exception e)
            {
                return null;
            }
        }

    }
}