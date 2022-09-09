# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY /Song.API/bin/Debug/net6.0/publish App/
WORKDIR /App

LABEL io.k8s.display-name="Song.API" \
      io.k8s.description="Song.API backend server" \
      io.openshift.expose-service="8080-http"

EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

ENTRYPOINT ["dotnet", "Song.API.dll"]