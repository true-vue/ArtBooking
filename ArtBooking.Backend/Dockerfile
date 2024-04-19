FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5001

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["ArtBooking.Backend/ArtBooking.Backend.csproj", "ArtBooking.Backend/"]
RUN dotnet restore "ArtBooking.Backend/ArtBooking.Backend.csproj"
COPY . .
WORKDIR "/src/ArtBooking.Backend"
RUN dotnet build "ArtBooking.Backend.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "ArtBooking.Backend.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArtBooking.Backend.dll"]
