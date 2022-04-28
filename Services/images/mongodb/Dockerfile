FROM mongo:latest

ENV MONGO_INITDB_DATABASE=mongo
ENV MONGO_INITDB_ROOT_USERNAME=mongo
ENV MONGO_INITDB_ROOT_PASSWORD=e2N70mWaXFWlPpDy

ENV MONGO_BACKUP /data/backup
# this 777 will be replaced by 700 at runtime (allows semi-arbitrary "--user" values)
RUN mkdir -p "$MONGO_BACKUP" && chown -R mongodb:mongodb "$MONGO_BACKUP" && chmod 777 "$MONGO_BACKUP"
VOLUME /data/backup

EXPOSE 27017