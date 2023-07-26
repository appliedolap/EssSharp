## EssSharp
#### A (partly) auto-generated C# REST client for Essbase.

The EssSharp C# REST client is an open source, .NET Standard 2.0 library created by [Applied OLAP](https://www.appliedolap.com) for working with an Essbase server using its REST API. This is intended to be used with Essbase 21+ but may also work, to an extent, with Essbase 19. This library cannot be used with older Essbase/EPM versions such as EPM 11.1.2.4.

This library is a very functional but does not implement everything available in the Essbase REST API. Contributions and pull requests are welcome. We will be filling in methods as they are needed.

## Why Use This Library?

The Essbase Java API has been the gold standard for connectivity and interaction with Essbase over the years. With the introduction of the Essbase REST API in Essbase 19 and higher, it becomes possible to consume Essbase services directly from .NET code. Additionally, this library provides access to some functionality that is _only_ available via the REST API, is less sensitive to version changes (sometimes the Essbase Java JARs would receive binary incompatible changes), and it may be easier to access your Essbase server using its normal HTTPS port instead of the APS port.

## Including EssSharp in a C# Project

You can easily pull this library into an existing .NET project using the latest NuGet packages: [EssSharp](https://www.nuget.org/packages/EssSharp), [EssSharp.Abstractions](https://www.nuget.org/packages/EssSharp.Abstractions). The abstractions library provides shared classes and interfaces implemented by EssSharp.

###### example.csproj:
```xml
  <ItemGroup>
    <PackageReference Include="EssSharp" Version="*.*" />
    <PackageReference Include="EssSharp.Abstractions" Version="*.*" />
  </ItemGroup>
```

## Using EssSharp

The library can be consumed as simply as doing the following:

###### example.cs
```csharp
// Create a new server factory..
EssServerFactory factory = new EssServerFactory();

// Create and return a connected server.
// Note: Replace "example" with your Essbase server hostname.
IEssServer server = await factory.CreateEssServerAsync(server: "http://example:9000/essbase", username: "admin", password: "welcome1");

// Get the list of applications from the server.
List<IEssApplication> applications = await server.GetApplicationsAsync();

// Print the name of each application to the console.
foreach (IEssApplication application in applications)
{
    Console.WriteLine("App: " + application.Name);
}
```

The above code prints out the list of applications on the server. If you want to get an Essbase 21 server up and running using Docker, you might want to look at the [Essbase Docker](https://github.com/appliedolap/docker-essbase) project (also by Applied OLAP).

## Technical Details

The Essbase REST API Swagger/OpenAPI definition document is used as the basis of the auto-generated classes in this library, although a series of changes are applied to it in order to 'fix' things that the OpenAPI generator would otherwise struggle with. These adjustments can be found in the [process.sh](https://github.com/appliedolap/EssSharp/blob/develop/process.sh) script. Generally these changes are to fix return types that would otherwise not deserialize properly. Once the document has been processed/fixed, the Open API code generator is run. The code is generated directly into the [/src/EssSharp/Api](https://github.com/appliedolap/EssSharp/tree/develop/src/EssSharp/Api), [/src/EssSharp/Client](https://github.com/appliedolap/EssSharp/tree/develop/src/EssSharp/Client), and [/src/EssSharp/Model](https://github.com/appliedolap/EssSharp/tree/develop/src/EssSharp/Model) folders.

## License

This library is licensed under the Apache License 2.0.