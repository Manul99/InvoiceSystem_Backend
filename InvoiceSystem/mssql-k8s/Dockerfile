FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=mydatabase@1999

USER root
COPY init-db.sh /init/init-db.sh
COPY invoiceAppDB.sql /init/invoiceAppDB.sql
RUN chmod +x /init/init-db.sh

USER mssql

CMD ["/bin/bash", "/init/init-db.sh"]
