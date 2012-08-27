@echo off
set /a count = 0
:boucle  
set /a count = count + 1  
if %count%==1000 goto finboucle  
goto boucle  
:finboucle
FORFILES /p "%CD%\tmpUpdate" /C "cmd /c copy @path %CD%\@file"
FORFILES /p "%CD%\tmpUpdate" /C "cmd /c del @path"

@echo on
@echo OK

