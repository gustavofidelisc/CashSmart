FROM mcr.microsoft.com/mssql/server:2022-latest

USER root
WORKDIR /usr/src/app

COPY init-db.sh /usr/src/app/init-db.sh
COPY entrypoint.sh /usr/src/app/entrypoint.sh
COPY StoredProcedures.sql /usr/src/app/StoredProcedures.sql

RUN sed -i 's/\r$//' /usr/src/app/init-db.sh /usr/src/app/entrypoint.sh && \
    chmod +x /usr/src/app/entrypoint.sh /usr/src/app/init-db.sh

USER mssql

ENTRYPOINT ["/usr/src/app/entrypoint.sh"]

