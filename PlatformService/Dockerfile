FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /app
COPY *.csproj ./
RUN dotnet dev-certs https -ep /app/Certificates/certificate.pfx -p test --trust
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=build-env /app/Certificates/certificate.pfx /app/Certificates/certificate.pfx
ENTRYPOINT ["dotnet", "PlatformService.dll"]