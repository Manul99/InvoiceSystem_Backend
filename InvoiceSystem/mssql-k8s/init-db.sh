#!/bin/bash
# Wait for SQL Server to start up before running script
echo "Waiting for SQL Server to start..."
sleep 20

echo "Running init script..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -i /init/invoiceAppDB.sql
