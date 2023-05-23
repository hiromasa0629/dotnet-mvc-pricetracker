FROM mcr.microsoft.com/dotnet/sdk
WORKDIR /app

COPY assessment.csproj .
RUN dotnet restore assessment.csproj
RUN dotnet tool install --global dotnet-ef

COPY . .
RUN dotnet publish -c Release -o out

RUN chmod 777 entrypoint.sh

ENTRYPOINT [ "./entrypoint.sh" ]
