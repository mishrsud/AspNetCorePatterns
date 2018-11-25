# Reference https://github.com/dotnet/dotnet-docker/blob/master/samples/aspnetapp/Dockerfile
FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app
 
COPY . ./dotnetapp/
WORKDIR /app/dotnetapp
RUN dotnet restore *.sln
 
WORKDIR /app/
COPY . ./
WORKDIR /app/dotnetapp
RUN dotnet publish -c Release -o out
 
#FROM build AS testrunner
#WORKDIR /app/tests
#COPY --from=build /app/dotnetapp/src/Daemon.Core.Test/*.* ./
#RUN dotnet test --logger:trx
 
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/dotnetapp/Smi.Api/out ./
ENTRYPOINT ["dotnet", "Smi.Api.dll"]