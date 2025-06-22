#!/bin/bash
# Wait for SQL Server to start
echo "Waiting for SQL Server to start..."
sleep 20

# Run the SQL script
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourPassword123 -i /init/invoiceAppDB.sql
