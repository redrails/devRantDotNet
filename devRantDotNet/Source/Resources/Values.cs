﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devRantDotNet
{
    internal static class Values
    {
        internal const string API = "https://devrant.com/api";
        internal const string AppId = "app=3";
        internal const string UsernameById = API + "/get-user-id";
        internal const string AllRants = API + "/devrant/rants";
        internal const string SingleRant = API + "/devrant/rants/";
        internal const string User = API + "/users/";
        internal const string Search = API + "/devrant/search";
        internal const string Random = API + "/devrant/rants/surprise";
    }
}
