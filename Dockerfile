#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KoerberExercise/KoerberExercise.csproj", "KoerberExercise/"]
COPY ["KoerberExercise.Logic/KoerberExercise.Logic.csproj", "KoerberExercise.Logic/"]
COPY ["KoerberExercise.Data/KoerberExercise.Data.csproj", "KoerberExercise.Data/"]
RUN dotnet restore "KoerberExercise/KoerberExercise.csproj"
COPY . .
WORKDIR "/src/KoerberExercise"
RUN dotnet build "KoerberExercise.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KoerberExercise.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KoerberExercise.dll"]