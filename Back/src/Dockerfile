FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

COPY ["./Finance.API/Finance.API.csproj", "src/Finance.API/"]
COPY ["./Finance.Application/Finance.Application.csproj", "src/Finance.Application/"]
COPY ["./Finance.Persistence/Finance.Persistence.csproj", "src/Finance.Persistence/"]
COPY ["./Finance.Domain/Finance.Domain.csproj", "src/Finance.Domain/"]
COPY ["./Finance.Test/Finance.Test.csproj", "src/Finance.Test/"]

RUN dotnet restore "src/Finance.API/Finance.API.csproj" --disable-parallel

COPY . .

WORKDIR "/src/Finance.API/"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

ENV ASPNETCORE_URLS=http://+:5000

FROM base AS runtime
WORKDIR /app

COPY --from=publish /app/publish .
RUN ls -l
RUN mkdir /app/Resources
RUN chmod 755 /app/Resources

ENV ASPNETCORE_URLS=https://+:5001;http://+:5000

ENTRYPOINT [ "dotnet", "Finance.API.dll" ]