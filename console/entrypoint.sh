#!/bin/bash

# Pass env variable to cron env
printf "MYSQL_SERVER=$MYSQL_SERVER\nMYSQL_USER=$MYSQL_USER\nMYSQL_PASSWORD=$MYSQL_PASSWORD\n\n*/5 * * * * /app/script.sh >> /cron.log 2>&1\n\n" > /etc/cron.d/schedule
chmod 0644 /etc/cron.d/schedule

crontab /etc/cron.d/schedule

cron && tail -f /cron.log