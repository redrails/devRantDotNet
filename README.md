Unofficial C# Wrapper for the [devRant](https://www.devrant.io/) public API.

![](https://www.devrant.io/static/devrant/img/landing/landing-avatars2.png)

# Prerequisites

- .NET Framework 4+

# Getting started

```
using devRantDotNet;

namespace MyProject
{
	class MyProgram
	{
		public static void Main(string[] args)
		{
			using(var dr = new devRant())
			{
				Console.WriteLine(dr.GetUserIdAsync("px06").Result);
				Console.WriteLine(dr.GetRantsAsync().Result);
				...
			}
		}
	}

```

Most methods are available in this library with some ongoing work on improving it. All methods are Async.

# devRantDotNet

This is a library written in C#.NET for access to the devRant API. It aims to provide serialization on the components received so you can easily make use of the library to build applications and other code.

# Usage and Documentation

Please see the wiki: https://github.com/redrails/devRantDotNet/wiki/Documentation

# Contributing

If you would like to contribute then please submit pull-requests.

# Licensing 

Feel free to use the library as it is open-source but please do give credit when using it. 