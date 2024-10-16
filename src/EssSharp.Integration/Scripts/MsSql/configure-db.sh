#!/bin/bash
#
# Copyright (c) 2021, Oracle and/or its affiliates.
# Licensed under the Universal Permissive License v 1.0 as shown at https://oss.oracle.com/licenses/upl.
#

SCRIPT_DIR=$(cd $(dirname $0); pwd)

# Add both variants of the microsoft ODBC tools path to the path
export PATH="${PATH:+${PATH}:}/opt/mssql-tools/bin"
export PATH="${PATH:+${PATH}:}/opt/mssql-tools18/bin"

# Sleep for 5 seconds to give the database a chance to start up
sleep 5

# Wait 60 seconds for SQL Server to start up by ensuring that 
# calling SQLCMD does not return an error code, which will ensure that sqlcmd is accessible
# and that system and user databases return "0" which means all databases are in an "online" state
# https://docs.microsoft.com/en-us/sql/relational-databases/system-catalog-views/sys-databases-transact-sql?view=sql-server-2017 

DBSTATUS=1
ERRCODE=1
i=0

while [ $DBSTATUS -ne 0 -a $i -lt 60 ]; do
   echo "********** Waiting for the database to start"
   i=$(expr $i + 1)
   DBSTATUS=$(sqlcmd -C -h -1 -t 1 -U sa -P $SA_PASSWORD -Q "SET NOCOUNT ON; Select SUM(state) from sys.databases")
   sleep 1
   DBSTATUS=${DBSTATUS:-1}
done

if [ $DBSTATUS -ne 0 ]; then 
   echo "********** SQL Server took more than 60 seconds to start up or one or more databases are not in an ONLINE state"
   exit 1
fi

# create the CertDB database
sqlcmd -C -S localhost -U sa -P ${SA_PASSWORD} -d master -i ${SCRIPT_DIR}/setup.sql &

# create the samplebasic database
#sqlcmd -C -S localhost -U sa -P ${SA_PASSWORD} -d master -i ${SCRIPT_DIR}/create.samplebasic.database.sqlserver.sql &
