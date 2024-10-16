#!/bin/bash
#
# Copyright (c) 2021, Oracle and/or its affiliates.
# Licensed under the Universal Permissive License v 1.0 as shown at https://oss.oracle.com/licenses/upl.
#

SCRIPT_DIR=$(cd $(dirname $0); pwd)

# Add both variants of the microsoft ODBC tools path to the path
export PATH="${PATH:+${PATH}:}/opt/mssql-tools/bin"
export PATH="${PATH:+${PATH}:}/opt/mssql-tools18/bin"

sqlcmd -C -S localhost -U sa -P ${SA_PASSWORD} -Q "SELECT 1" || exit 1
