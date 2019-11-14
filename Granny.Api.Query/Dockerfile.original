FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Granny.Api.Query/Granny.Api.Query.csproj", "Granny.Api.Query/"]
RUN dotnet restore "Granny.Api.Query/Granny.Api.Query.csproj"
COPY . .
WORKDIR "/src/Granny.Api.Query"
RUN dotnet build "Granny.Api.Query.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Granny.Api.Query.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Granny.Api.Query.dll"]