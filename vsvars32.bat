@SET VS_DRIVE=C

@IF EXIST "%VS_DRIVE%:\Program Files (x86)\Microsoft Visual Studio 10.0\VC" GOTO SETUP_VC10_X64
@IF EXIST "%VS_DRIVE%:\Program Files\Microsoft Visual Studio 10.0\VC" GOTO SETUP_VC10_X86
@IF EXIST "%VS_DRIVE%:\Program Files (x86)\Microsoft Visual Studio 9.0\VC" GOTO SETUP_VC9_X64
@IF EXIST "%VS_DRIVE%:\Program Files\Microsoft Visual Studio 9.0\VC" GOTO SETUP_VC9_X86

@echo "No Visual Studio setup found"
exit

:SETUP_VC9_X86
@echo "SETUP_VC9_X86"
@SET VSINSTALLDIR=%VS_DRIVE%:\Program Files\Microsoft Visual Studio 9.0
@SET VCINSTALLDIR=%VS_DRIVE%:\Program Files\Microsoft Visual Studio 9.0\VC
@GOTO CONTINUE

:SETUP_VC9_X64
@echo "SETUP_VC9_X64"
@SET VSINSTALLDIR=%VS_DRIVE%:\Program Files (x86)\Microsoft Visual Studio 9.0
@SET VCINSTALLDIR=%VS_DRIVE%:\Program Files (x86)\Microsoft Visual Studio 9.0\VC
@GOTO CONTINUE

:SETUP_VC10_X86
@echo "SETUP_VC10_X86"
@SET VSINSTALLDIR=%VS_DRIVE%:\Program Files\Microsoft Visual Studio 10.0
@SET VCINSTALLDIR=%VS_DRIVE%:\Program Files\Microsoft Visual Studio 10.0\VC
@GOTO CONTINUE

:SETUP_VC10_X64
@echo "SETUP_VC10_X64"
@SET VSINSTALLDIR=%VS_DRIVE%:\Program Files (x86)\Microsoft Visual Studio 10.0
@SET VCINSTALLDIR=%VS_DRIVE%:\Program Files (x86)\Microsoft Visual Studio 10.0\VC
@GOTO CONTINUE

:CONTINUE

@ECHO VSINSTALLDIR=%VSINSTALLDIR%
@ECHO VCINSTALLDIR=%VCINSTALLDIR%
@SET FrameworkDir=C:\Windows\Microsoft.NET\Framework
@SET FrameworkVersion=v2.0.50727
@SET Framework35Version=v3.5
@if "%VSINSTALLDIR%"=="" goto error_no_VSINSTALLDIR
@if "%VCINSTALLDIR%"=="" goto error_no_VCINSTALLDIR


@call :GetWindowsSdkDir

@if not "%WindowsSdkDir%" == "" (
	set "PATH=%WindowsSdkDir%bin;%PATH%"
	set "INCLUDE=%WindowsSdkDir%include;%INCLUDE%"
	set "LIB=%WindowsSdkDir%lib;%LIB%"
)


@rem
@rem Root of Visual Studio IDE installed files.
@rem
@set DevEnvDir=%VSINSTALLDIR%\Common7\IDE

@set PATH=%VSINSTALLDIR%\Common7\IDE;%VSINSTALLDIR%\VC\BIN;%VSINSTALLDIR%\Common7\Tools;C:\Windows\Microsoft.NET\Framework\v3.5;C:\Windows\Microsoft.NET\Framework\v2.0.50727;%VSINSTALLDIR%\VC\VCPackages;%PATH%
@set INCLUDE=%VCINSTALLDIR%\INCLUDE;%INCLUDE%
@set LIB=%VCINSTALLDIR%\LIB;%LIB%
@set LIBPATH=C:\Windows\Microsoft.NET\Framework\v3.5;C:\Windows\Microsoft.NET\Framework\v2.0.50727;%VSINSTALLDIR%\VC\LIB;%LIBPATH%

@goto end

:GetWindowsSdkDir
@call :GetWindowsSdkDirHelper HKLM > nul 2>&1
@if errorlevel 1 call :GetWindowsSdkDirHelper HKCU > nul 2>&1
@if errorlevel 1 set WindowsSdkDir=%VCINSTALLDIR%\PlatformSDK\
@exit /B 0

:GetWindowsSdkDirHelper
@for /F "tokens=1,2*" %%i in ('reg query "%1\SOFTWARE\Microsoft\Microsoft SDKs\Windows" /v "CurrentInstallFolder"') DO (
	if "%%i"=="CurrentInstallFolder" (
		SET "WindowsSdkDir=%%k"
	)
)
@if "%WindowsSdkDir%"=="" exit /B 1
@exit /B 0

:error_no_VSINSTALLDIR
@echo ERROR: VSINSTALLDIR variable is not set. 
@goto end

:error_no_VCINSTALLDIR
@echo ERROR: VCINSTALLDIR variable is not set. 
@goto end

:end
