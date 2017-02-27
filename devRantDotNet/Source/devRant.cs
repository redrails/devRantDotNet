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
    /// <summary>
    /// A C# Wrapper for the devRant API
    /// </summary>
    public class devRant
    {

        /// <summary>
        /// Rant sort type
        /// </summary>
        public enum SortType
        {
            /// <summary>
            /// Sorts using algo on devRant
            /// </summary>
            algo,

            /// <summary>
            /// Sorts by top rants
            /// </summary>
            top,

            /// <summary>
            /// Sorts by recent rants
            /// </summary>
            recent
        }

        /// <summary>
        /// Uses <see cref="HttpWebRequest"/> and <see cref="HttpWebResponse"/> to create requests to the API.
        /// Returns the JSON result.
        /// </summary>
        /// <param name="url">The url of the endpoint</param>
        /// <returns>The JSON Result</returns>
        private async Task<string> MakeRequestAsync(string url)
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
            var response = (HttpWebResponse)await request.GetResponseAsync();

            using(var sr = new StreamReader(response.GetResponseStream()))
            {
                t = sr.ReadToEnd();
            }

            return t;
        }

        /// <summary>
        /// Converting the JSON received into a Rant object which is specified in <see cref="Rant"/>
        /// </summary>
        /// <param name="r">The JSON string received by the API containing the Rant</param>
        /// <returns>A Rant object with the properties received</returns>
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

        /// <summary>
        /// Convert a comment on a rant to a Comment object as specified in <see cref="Comment"/>
        /// </summary>
        /// <param name="c">The JSON string received by the API containing the comment</param>
        /// <returns>A Comment object with the received properties</returns>
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

        /// <summary>
        /// Returns all the rants from the feed at: https://www.devrant.io/feed
        /// </summary>
        /// <param name="type"> Type of sort e.g. Top, Algo or Recent</param>
        /// <returns>A List of Rants which are iterable</returns>
        public async Task<List<Rant>> GetRantsAsync(SortType type)
        {
            var req = await MakeRequestAsync(Values.AllRants+"?sort="+type+"&app=3");
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

        /// <summary>
        /// Gets a single rant given the Id of it
        /// </summary>
        /// <param name="id">The Id of the rant that is being queried</param>
        /// <returns>The Rant that is requested as a Rant object</returns>
        public async Task<Rant> GetRantAsync(int id)
        {
            try
            {
                var req = await MakeRequestAsync(Values.SingleRant + id + Values.AppId);
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

        /// <summary>
        /// Gets the user Id given a username
        /// </summary>
        /// <param name="username">username of the person that the Id is wanted for</param>
        /// <returns>The Id of the user with the username specified</returns>
        public async Task<int> GetUserIdAsync(string username)
        {
            try
            {
                var req = await MakeRequestAsync(Values.UsernameById + "?username=" + username + "&app=3");
                dynamic results = JsonConvert.DeserializeObject<dynamic>(req);

                if (results.success != "true")
                {
                    throw new Exception("Something went wrong!");
                }

                return results.user_id;

            }
            catch { return 0; }
        }

        /// <summary>
        /// Gets a user profile with all the rants and other info associated with it
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>The User object with all the user-info and rants</returns>
        public async Task<User> GetProfileAsync(long id)
        {
            try
            {
                var req = await MakeRequestAsync(Values.User + id + Values.AppId);
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

        /// <summary>
        /// Allows to search the devRant website
        /// </summary>
        /// <param name="term">The term to search with</param>
        /// <returns>A List of rants matching the search terms</returns>
        public async Task<List<Rant>> SearchAsync(string term)
        {
            try
            {
                var req = await MakeRequestAsync(Values.Search + "?term=" + term + "&app=3");
                dynamic results = JsonConvert.DeserializeObject<dynamic>(req);

                if (results.success != "true")
                {
                    throw new Exception("Something went wrong!");
                }

                List<Rant> search_results = new List<Rant>();

                for (var i = 0; i < results.results.Count; i++)
                {
                    search_results.Add(JSONToRantObject(results.results[i]));
                }

                return search_results;

            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// SURPRISE!!!
        /// </summary>
        /// <returns>A random rant as a Rant object</returns>
        public async Task<Rant> GetRandomRantAsync()
        {
            try
            {
                var req = await MakeRequestAsync(Values.Random + Values.AppId);
                dynamic results = JsonConvert.DeserializeObject<dynamic>(req);

                return results.success == "true" ? JSONToRantObject(results.rant) : null;
            }
            catch
            {
                return null;
            }
        }
    }
}