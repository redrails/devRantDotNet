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
        static void Main(string[] args)
        {
            var x = new devRant();
            //x.GetRants().ForEach(r => Console.WriteLine(r.text));

            x.GetRant(448338).rant_comments.ForEach(
                c => Console.WriteLine(c.user_username +": "+ c.body)
            );
            Console.ReadLine();
        }
    }
}
