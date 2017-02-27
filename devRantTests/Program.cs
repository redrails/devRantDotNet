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

        public static void getAllRants()
        {
            dr.GetRants(devRant.SortType.recent).ForEach(r => Console.WriteLine(r.text));
        }

        public static void getSingleRant(int id)
        {
            dr.GetRant(id).rant_comments.ForEach(
                c => Console.WriteLine(c.user_username + ": " + c.body +"("+c.id+")")
            );
        }

        public static void getUserId(string username)
        {
            Console.WriteLine(dr.GetUserId(username));
        }

        public static void getUserProfile(int id)
        {
            var user = dr.GetProfile(id);
            var dump = ObjectDumper.Dump(user);
            Console.WriteLine(dump);
        }

        public static void getSearchResults(string q)
        {
            var results = dr.Search(q);
            results.ForEach(r => Console.WriteLine(r.text));
        }

        public static void getRandomRant()
        {
            var dump = ObjectDumper.Dump(dr.GetRandomRant());
            Console.WriteLine(dump);
        }

        static void Main(string[] args)
        {
            //getAllRants();
            //getUserProfile(dr.GetUserId("px06"));
            //getSearchResults("px06");
            //getSingleRant(448369);

            getRandomRant();
            Console.WriteLine("-----------------------------------");
            getUserProfile(dr.GetUserId("px06"));

            Console.ReadLine();
        }
    }
}
