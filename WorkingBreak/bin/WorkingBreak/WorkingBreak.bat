@ECHO OFF
CLS
ECHO ����� ...

SET EXEDIR=%CD%

REM *********	 ����� ����	*********************************************************************************************
REM ����� ���� ��Ш� �� �������� ����������
SET Program=%EXEDIR:~-5%

REM ��� ��������� �������
REM SET ARM=00000 

REM ********* 	������� ����������	*************************************************************************************
SET WORKDIR=WorkingBreak%ARM%
IF NOT EXIST D:\%WORKDIR%\*.* MD D:\%WORKDIR%
D:
CD \%WORKDIR%



REM ********* 	����� ����	*********************************************************************************************
REM ��� ������� ���� �����
del /q: d:\workingBreak\
copy %EXEDIR%\WorkingBreak.exe /y
copy %EXEDIR%\WorkingBreak.exe.config /y

start WorkingBreak.exe

:END