# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY /Song.API/bin/Debug/net6.0/publish App/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "Song.API.dll"]