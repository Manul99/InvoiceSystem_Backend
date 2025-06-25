FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y
#ENV SA_PASSWORD

USER root

# Install basic dependencies first
RUN apt-get update && apt-get install -y --no-install-recommends \
    curl \
    gnupg2 \
    apt-transport-https \
    software-properties-common \
    ca-certificates \
    wget && \
    rm -rf /var/lib/apt/lists/*

# Add Microsoft GPG key
RUN mkdir -p /usr/share/keyrings && \
    curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > /usr/share/keyrings/microsoft.gpg

# Add Microsoft MSSQL repo
RUN echo "deb [arch=amd64 signed-by=/usr/share/keyrings/microsoft.gpg] https://packages.microsoft.com/repos/mssql-release/debian/10/prod buster main" > /etc/apt/sources.list.d/mssql-release.list

# Update & install mssql-tools and unixodbc-dev
RUN apt-get update && ACCEPT_EULA=Y apt-get install -y --no-install-recommends mssql-tools unixodbc-dev

# Add mssql-tools to PATH
RUN echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc

# Clean up
RUN apt-get clean && rm -rf /var/lib/apt/lists/*

ENV PATH="${PATH}:/opt/mssql-tools/bin"

COPY init-db.sh /init/init-db.sh
COPY invoiceAppDB.sql /init/invoiceAppDB.sql
RUN chmod +x /init/init-db.sh

USER mssql

CMD ["/bin/bash", "/init/init-db.sh"]
