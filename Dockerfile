# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY /SongAPI/bin/Debug/net6.0/publish App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "Album.Api.dll"]