# Minecraft Server Forum - built with ASP.NET Core 2.0 Preview 2
This project is currently viewable at [alexmancheno.me](http://alexmancheno.me/).

Here's the description of Asp.Net Core from [Microsoft](https://docs.microsoft.com/en-us/aspnet/core/):

"ASP.NET Core is a new open-source and cross-platform framework for building modern cloud-based Internet-connected applications, such as web apps, IoT apps and mobile backends. It was architected to provide an optimized development framework for apps that are deployed to the cloud or run on-premises. It consists of modular components with minimal overhead, so you retain flexibility while constructing your solutions. You can develop and run ASP.NET Core apps on Windows, Mac and Linux. ASP.NET Core apps can run on .NET Core or on the .NET Framework.

ASP.NET Core provides the following improvements compared to ASP.NET:
* A unified story for building web UI and web APIs.
* Integration of modern client-side frameworks and development workflows.
* A cloud-ready environment-based configuration system.
* Built-in dependency injection.
* A light-weight and modular HTTP request pipeline.
* Ability to host on IIS or self-host in your own process.
* Built on .NET Core, which supports true side-by-side app versioning.
* Ships entirely as NuGet packages.
* New tooling that simplifies modern web development.
* Build and run cross-platform ASP.NET Core apps on Windows, Mac, and Linux.
* Open-source and community-focused."

A recent internship had been using C#/ASP.NET (not the cross-platform version) to write back-end services and I ended up really liking C#, so I decided to build this forum with ASP.NET Core to give this new direction a try. I had at first been building this forum with ASP.NET 4.6 (hosted on a Windows vm with IIS), and so I decided to migrate it over to ASP.NET Core (hosted on a Ubuntu 16.04 vm with Nginx reverse-proxy).

 So far I've noticed a drastic speed increase in loading up webpages with ASP.NET Core (could be results from Ubuntu/Nginx), and I'm excited to continue seeing ASP.NET Core continue to progress (since it's relatively new). I encourage others to give ASP.NET Core a try!