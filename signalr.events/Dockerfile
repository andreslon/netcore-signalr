FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["signalr.events.csproj", "./"]
RUN dotnet restore "./signalr.events.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "signalr.events.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "signalr.events.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "signalr.events.dll"]
