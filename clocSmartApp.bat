@echo off
%CD%\cloc-1.55.exe %CD% --exclude-dir=obj,prod,InnoSetup,manuels,Doc,FichiersDeTest --exclude-ext=saf,slt,sas,sac,resx,xml 
REM --by-file
pause