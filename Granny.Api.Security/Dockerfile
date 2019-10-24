FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Granny.Api.Security/Granny.Api.Security.csproj", "Granny.Api.Security/"]
RUN dotnet restore "Granny.Api.Security/Granny.Api.Security.csproj"
COPY . .
WORKDIR "/src/Granny.Api.Security"
RUN dotnet build "Granny.Api.Security.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Granny.Api.Security.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Granny.Api.Security.dll"]