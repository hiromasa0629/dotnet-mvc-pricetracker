FROM mcr.microsoft.com/dotnet/sdk
WORKDIR /app

COPY console.csproj .
RUN dotnet restore console.csproj

COPY . .
RUN dotnet publish -c Release -o out
RUN chmod 777 entrypoint.sh

RUN apt-get update && apt-get -y install cron

RUN touch /cron.log
RUN chmod 777 /cron.log
RUN chmod 777 ./script.sh


ENTRYPOINT [ "./entrypoint.sh" ]