using devRantDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devRantTests
{
    class Program
    {
        static devRant dr = new devRant();

        public static async void getAllRants()
        {
            var res = await dr.GetRantsAsync(devRant.SortType.recent);
            res.ForEach(r => Console.WriteLine(r.text));
        }

        public static async void getSingleRant(int id)
        {
            var res = await dr.GetRantAsync(id);
            res.rant_comments.ForEach(
                c => Console.WriteLine(c.user_username + ": " + c.body +"("+c.id+")")
            );
        }

        public static async Task<int> getUserId(string username)
        {
            var res = await dr.GetUserIdAsync(username);
            Console.WriteLine(res);
            return res;
        }

        public static async void getUserProfile(int id)
        {
            var user = await dr.GetProfileAsync(id);
            var dump = ObjectDumper.Dump(user);
            Console.WriteLine(dump);
        }

        public static async void getSearchResults(string q)
        {
            var results = await dr.SearchAsync(q);
            results.ForEach(r => Console.WriteLine(r.text));
        }

        public static async void getRandomRant()
        {
            var dump = ObjectDumper.Dump(await dr.GetRandomRantAsync());
            Console.WriteLine(dump);
        }

        static void Main(string[] args)
        {
            getAllRants();
            getSearchResults("px06");
            getSingleRant(448369);
            getUserProfile(getUserId("px06").Result);

            Console.ReadLine();
        }
    }
}
