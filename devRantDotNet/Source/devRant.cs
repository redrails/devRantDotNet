using devRantDotNet.Source.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace devRantDotNet
{
    public class devRant
    {
        public enum SortType
        {
            algo,
            top,
            recent
        }

        private string MakeRequest(string url)
        {

            // Set a default policy level for the "http:" and "https" schemes.
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Default);
            HttpWebRequest.DefaultCachePolicy = policy;

            //Create request
            var request = WebRequest.Create(url + Values.AppId);

            // Define a cache policy for this request only. 
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.CachePolicy = noCachePolicy;

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

        private Comment JSONToCommentObject(dynamic c)
        {
            Comment comment = new Comment()
            {
                id = c.id,
                rant_id = c.rant_id,
                body = c.body,
                num_upvotes = c.num_upvotes,
                num_downvotes = c.num_downvotes,
                score = c.score,
                created_time = c.created_time,
                vote_state = c.vote_state,
                user_id = c.user_id,
                user_username = c.user_username,
                user_score = c.user_score,
                user_avatar = c.user_avatar.i
            };

            return comment;
        }

        public List<Rant> GetRants(SortType type)
        {
            var req = MakeRequest(Values.AllRants+"?sort="+type+"&app=3");
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
                    rant.rant_comments.Add(JSONToCommentObject(current));
                }
                return rant;
            } catch (Exception e)
            {
                return null;
            }
        }

        public int GetUserId(string username)
        {
            try
            {
                var req = MakeRequest(Values.UsernameById + "?username=" + username + "&app=3");
                dynamic results = JsonConvert.DeserializeObject<dynamic>(req);

                if (results.success != "true")
                {
                    throw new Exception("Something went wrong!");
                }

                return results.user_id;

            }
            catch { return 0; }
        }

        public User GetUser(long id)
        {
            try
            {
                var req = MakeRequest(Values.User + id + Values.AppId);
                dynamic results = JsonConvert.DeserializeObject<dynamic>(req);
                var profile = results.profile;
                if(results.success != "true")
                {
                    throw new Exception("Something went wrong!");
                }

                User user = new User();
                user.username = profile.username;
                user.score = profile.score;
                user.about = profile.about;
                user.location = profile.location;
                user.created_time = profile.created_time;
                user.skills = profile.skills;
                user.github = profile.github;
                user.website = profile.website;

                for (var i = 0; i < profile.content.content.rants.Count; i++){
                    user.rants.Add(JSONToRantObject(profile.content.content.rants[i]));
                }

                for (var i = 0; i < profile.content.content.upvoted.Count; i++)
                {
                    user.upvoted.Add(JSONToRantObject(profile.content.content.upvoted[i]));
                }

                for(var i = 0; i < profile.content.content.comments.Count; i++)
                {
                    user.comments.Add(JSONToCommentObject(profile.content.content.comments[i]));
                }

                for(var i = 0; i < profile.content.content.favorites.Count; i++)
                {
                    user.favorites.Add(JSONToRantObject(profile.content.content.favorites[i]));
                }

                user.counts_rants = profile.content.counts.rants;
                user.counts_upvoted = profile.content.counts.upvoted;
                user.counts_comments = profile.content.counts.comments;
                user.counts_favorites = profile.content.counts.favorites;
                user.counts_collabs = profile.content.counts.collabs;

                user.avatar = profile.avatar.i;

                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}