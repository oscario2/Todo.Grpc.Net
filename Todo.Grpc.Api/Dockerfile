FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Todo.Grpc.Api/Todo.Grpc.Api.csproj", "Todo.Grpc.Api/"]
RUN dotnet restore "Todo.Grpc.Api/Todo.Grpc.Api.csproj"
COPY . .
WORKDIR "/src/Todo.Grpc.Api"
RUN dotnet build "Todo.Grpc.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Todo.Grpc.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.Grpc.Api.dll"]
