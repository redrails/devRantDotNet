[![Build status](https://ci.appveyor.com/api/projects/status/yd6s0ji4rm67mwhr?svg=true)](https://ci.appveyor.com/project/redrails/devrantdotnet) [![NuGet](https://buildstats.info/nuget/redrails.devRantDotNet)](https://www.nuget.org/packages/redrails.devRantDotNet/)



Unofficial C# Wrapper for the [devRant](https://www.devrant.io/) public API.

![](https://www.devrant.io/static/devrant/img/landing/landing-avatars2.png)

# devRantDotNet

This is a library written in C#.NET for access to the devRant API. It aims to provide serialization on the components received so you can easily make use of the library to build applications and other code.

# Prerequisites

- .NET Framework 4+

# Installation

The easiest way to install devRantDotNet is using nuget, run the following command in the [Package Manager Console](https://docs.nuget.org/docs/start-here/using-the-package-manager-console)
```
    PM> Install-Package redrails.devRantDotNet
```

If you want to run from source then you can clone this repository and add it as a reference in your project.

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
}
```

Most methods are available in this library with some ongoing work on improving it. All methods are Async.

# Usage and Documentation

Please see the wiki: https://github.com/redrails/devRantDotNet/wiki/Documentation

# Contributing

If you would like to contribute then please submit pull-requests.

# Licensing 

Feel free to use the library as it is open-source but please do give credit when using it. 
