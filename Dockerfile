FROM mongo:4.4

COPY mongo-init/init-mongo.js /docker-entrypoint-initdb.d/

