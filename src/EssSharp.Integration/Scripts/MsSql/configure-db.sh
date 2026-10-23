#!/bin/bash
#
# Copyright (c) 2021, Oracle and/or its affiliates.
# Licensed under the Universal Permissive License v 1.0 as shown at https://oss.oracle.com/licenses/upl.
#

SCRIPT_DIR=$(cd $(dirname $0); pwd)

# Add both variants of the microsoft ODBC tools path to the path
#export PATH="${PATH:+${PATH}:}/opt/mssql-tools/bin"
export PATH="${PATH:+${PATH}:}/opt/mssql-tools18/bin"

# Sleep for 5 seconds to give the database a chance to start up
sleep 5

# Wait up to 10 minutes for SQL Server to answer, with every database online: SUM(state) is 0 then.
# https://docs.microsoft.com/en-us/sql/relational-databases/system-catalog-views/sys-databases-transact-sql?view=sql-server-2017
# Only a 0 counts; while SQL Server is starting, sqlcmd prints its connection error instead, on a
# first boot of a busy machine for longer than a minute.

DBSTATUS=
i=0

while [ $i -lt 120 ]; do
   i=$(expr $i + 1)
   DBSTATUS=$(sqlcmd -C -h -1 -t 5 -b -U sa -P $SA_PASSWORD -Q "SET NOCOUNT ON; Select SUM(state) from sys.databases" 2>/dev/null | tr -d '[:space:]')
   [ "$DBSTATUS" = "0" ] && break
   echo "********** Waiting for the database to start"
   sleep 5
done

if [ "$DBSTATUS" != "0" ]; then
   echo "********** SQL Server took more than 10 minutes to start up or one or more databases are not in an ONLINE state"
   exit 1
fi

# create the CertDB database
sqlcmd -C -S localhost -U sa -P ${SA_PASSWORD} -d master -i ${SCRIPT_DIR}/setup.sql &

# create the samplebasic database
#sqlcmd -C -S localhost -U sa -P ${SA_PASSWORD} -d master -i ${SCRIPT_DIR}/create.samplebasic.database.sqlserver.sql &
