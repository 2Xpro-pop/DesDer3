#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DesDer3.Pl/DesDer3.Pl.csproj", "DesDer3.Pl/"]
RUN dotnet restore "DesDer3.Pl/DesDer3.Pl.csproj"
COPY . .
WORKDIR "/src/DesDer3.Pl"
RUN dotnet build "DesDer3.Pl.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DesDer3.Pl.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesDer3.Pl.dll"]