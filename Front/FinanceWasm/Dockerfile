FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY FinanceWasm.csproj .
RUN dotnet restore FinanceWasm.csproj
COPY . .
RUN dotnet build FinanceWasm.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish FinanceWasm.csproj -c Release -o /app/publish
ENV ASPNETCORE_URLS=https://+:5002;http://+:5003

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 5003