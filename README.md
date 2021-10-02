# Todo.Grpc.Net

## Introduction
A contract first boilerplate supporting `gRPC (HTTP/2)` and gRPC proxied `JSON (HTTP/1)`.

## Features
* JWT authentication using `FireBase` supporting roles and policies
    * `UserManager` nor `IdentityUser` is not provided in this repo 
* Swagger with `Bearer` and `gRPC` support
* Automatically includes any `.proto` added to the `Protos` directory
    * Any additional folder needs to be added to `AdditionalImportDirs` in .csproj
* ASP is set to `Http1AndHttp2` and `HttpService` to `AcceptAnyServerCertificate` to allow `Fiddler` debugging
* Tests using [AAA](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices#arranging-your-tests) convention in `XUnit`

## GRPC server
To use the GRPC server and require protobuf to be sent
```csharp
 // requires at least <Protobuf ... GrpcServices="Server" /> in .csproj
var server = new Server()
{
    Services =
    {
        EchoRpcService.BindService(new GreetingService())
    },
    Ports =
    {
        // https://auth0.com/blog/securing-grpc-microservices-dotnet-core/
        new ServerPort("localhost", 50052, ServerCredentials.Insecure)
    }
};
server.Start();
```

## GRPC client
```csharp
var channel = new Channel("127.0.0.1:50052", ChannelCredentials.Insecure);

// requires at least <Protobuf ... GrpcServices="Client" /> in .csproj
var client = new EchoRpcService.EchoRpcServiceClient(channel);
var echo = client.AddItem(new EchoRequest() { Message = "echo"});
```

## JetBrains
Include ["Protos"](https://github.com/jvolkman/intellij-protobuf-editor#path-settings) for auto-completion of imports