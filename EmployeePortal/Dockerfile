FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_14.x  | bash \     
	&& apt-get install nodejs -yq
	
WORKDIR /src

# 1st dot for context 2nd dot for working directory
COPY . . 

RUN dotnet restore ./EmployeePortal.sln

RUN dotnet publish ./EmployeePortal/EmployeePortal.csproj -o /app -c Release

FROM base 

WORKDIR /app 

COPY --from=build /app .

EXPOSE 80 

ENTRYPOINT dotnet EmployeePortal.dll