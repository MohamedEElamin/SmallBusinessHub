echo off

rem batchfile to run a script create a db
rem 10/12/2019

rem sqlcmd -S localhost -E -i SmallBussinessHubDB.sql
rem sqlcmd -S localhost\mssqlserver -E -i SmallBussinessHubDB.sql
sqlcmd -S MOHAMEDHP\SQLEXPRESS01 -E -i  SmallBussinessHubDB.sql


ECHO  .
ECHO if no error messages apper DB was created
PAUse