FROM postgres:latest

LABEL title="DigiPay Dev Postgresql"
LABEL description="Latest postgresql wrapper image for development use."
LABEL "com.mydigipay.image.author"="Ehsan Maleki Zoeram"
LABEL "com.mydigipay.image.vendor"="DigiPay Fintech Co."

ENV POSTGRES_DB=postgres
ENV POSTGRES_USER=postgres
ENV POSTGRES_PASSWORD=ZwA59hEvVnOLks0i

ENV PGBACKUP /var/lib/postgresql/backup
# this 777 will be replaced by 700 at runtime (allows semi-arbitrary "--user" values)
RUN mkdir -p "$PGBACKUP" && chown -R postgres:postgres "$PGBACKUP" && chmod 777 "$PGBACKUP"
VOLUME /var/lib/postgresql/backup

EXPOSE 5432
