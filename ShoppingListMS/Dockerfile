#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ShoppingListMS/ShoppingListMS.Api.csproj ShoppingListMS/
COPY ShoppingList/ShoppingList.csproj ShoppingList/
RUN dotnet restore "ShoppingListMS/ShoppingListMS.Api.csproj"
COPY . .
WORKDIR "/src/ShoppingListMS"
RUN dotnet build "ShoppingListMS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShoppingListMS.Api.csproj" -c Release -o /app/publish

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingListMS.Api.dll"]
