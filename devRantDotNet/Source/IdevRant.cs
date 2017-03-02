using devRantDotNet.Source.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static devRantDotNet.devRant;

namespace devRantDotNet.Source
{
    interface IdevRant
    {

        Task<List<Rant>> GetRantsAsync(SortType type, int limit, int skip);

        Task<Rant> GetRantAsync(int id);

        Task<int> GetUserIdAsync(string username);

        Task<User> GetProfileAsync(long id);

        Task<List<Rant>> SearchAsync(string term);

        Task<Rant> GetRandomRantAsync();

    }
}