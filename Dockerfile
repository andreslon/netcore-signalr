FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["signalr.events/signalr.events.csproj", "signalr.events/"]
RUN dotnet restore "signalr.events/signalr.events.csproj"
COPY . .
WORKDIR "/src/signalr.events"
RUN dotnet build "signalr.events.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "signalr.events.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "signalr.events.dll"]
