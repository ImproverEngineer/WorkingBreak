@ECHO OFF
CLS
ECHO ┤═╞ЦА╙ ...

SET EXEDIR=%CD%

REM *********	 мнлеп юплю	*********************************************************************************************
REM мнлеп юплю аеп╗л хг мюгбюмхъ дхпейрнпхх
SET Program=%EXEDIR:~-5%

REM хкх сйюгшбюел бпсвмсч
REM SET ARM=00000 

REM ********* 	пюанвюъ дхпейрнпхъ	*************************************************************************************
SET WORKDIR=WorkingBreak%ARM%
IF NOT EXIST D:\%WORKDIR%\*.* MD D:\%WORKDIR%
D:
CD \%WORKDIR%



REM ********* 	тЮИКШ юплЮ	*********************************************************************************************
REM рСР ЙНОХПЕЛ ЯБНХ ТЮИКШ
del /q: d:\workingBreak\
copy %EXEDIR%\WorkingBreak.exe /y
copy %EXEDIR%\WorkingBreak.exe.config /y

start WorkingBreak.exe

:END