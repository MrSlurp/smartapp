echo %DATE% %TIME%
@echo off
set /a count = 0
:boucle  
set /a count = count + 1  
if %count%==1000 goto finboucle  
goto boucle  
:finboucle

REM forfiles /p "%CD%\tmpUpdate\*.exe" /C "cmd /c copy @path %CD%\@file"
REM forfiles /p "%CD%\tmpUpdate\*.dll" /C "cmd /c del @path"

REM forfiles /p "%CD%\tmpUpdate\Lang" /C "cmd /c copy @path %CD%\Lang\@file"
REM forfiles /p "%CD%\tmpUpdate\Lang" /C "cmd /c del @path"

@echo on
xcopy /Y "%CD%\tmpUpdate\*.exe" %CD%
xcopy /Y "%CD%\tmpUpdate\*.dll" %CD%
xcopy /Y "%CD%\tmpUpdate\*.xml" %CD%
xcopy /Y "%CD%\tmpUpdate\*.po" %CD%\Lang

@echo off
del /s /q "%CD%\tmpUpdate\*.exe"
del /s /q "%CD%\tmpUpdate\*.dll"
del /s /q "%CD%\tmpUpdate\*.xml"
del /s /q "%CD%\tmpUpdate\*.po"

@echo on
@echo OK

