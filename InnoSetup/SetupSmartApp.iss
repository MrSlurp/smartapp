#include "scripts\products.iss"

#include "scripts\products\winversion.iss"
#include "scripts\products\fileversion.iss"

#include "scripts\products\msi20.iss"
#include "scripts\products\msi31.iss"

#include "scripts\products\dotnetfx20.iss"
#include "scripts\products\dotnetfx20sp1.iss"
#include "scripts\products\dotnetfx20sp2.iss"

[CustomMessages]
win2000sp3_title=Windows 2000 Service Pack 3
winxpsp2_title=Windows XP Service Pack 2


[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{2FE97D82-A4DD-4235-A710-9C1A17CAE705}
AppName=Smart Application V3
AppVerName=Smart Application 3.1.0.0
AppPublisher=Pascal Bigot
AppCopyright=Copyright (C) 2007-2012 Pascal Bigot   
AppPublisherURL=http://www.smartappsoftware.net
AppSupportURL=http://www.smartappsoftware.net
AppUpdatesURL=http://www.smartappsoftware.net
AppVersion=3.1.0.0
DefaultDirName={pf}\M3Tool\Smart Application V3
DefaultGroupName=Smart Application V3
AllowNoIcons=yes
OutputBaseFilename=Setup_SmartApp_V3
Compression=lzma
SolidCompression=yes
WizardImageFile=images\SetupLeft.bmp
WizardSmallImageFile=images\SmartApp.bmp
SetupIconFile=images\SmartAppSln.ico 
ChangesAssociations=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"; LicenseFile:"LicenseEN.txt"
Name: "french"; MessagesFile: "compiler:Languages\French.isl" ; LicenseFile:"LicenseFR.txt"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Dirs]
Name: "{app}" ; Permissions : everyone-modify


[Files]
; exe principal de l'application
Source: "..\Prod\Release\SmartApp.exe"; DestDir: "{app}"; Flags: ignoreversion ; Permissions : users-modify
; exe updater l'application
Source: "..\Prod\Release\SmartAppUpdater.exe"; DestDir: "{app}"; Flags: ignoreversion ; Permissions : users-modify
; batch d'update final
Source: "..\Prod\Release\postUpdateCopy.bat"; DestDir: "{app}"; Flags: ignoreversion ; Permissions : users-modify
; plugins + zegraph
Source: "..\Prod\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion ; Permissions : users-modify
; fichier de documentation de zedgraph
Source: "..\Prod\Release\*.xml"; DestDir: "{app}"; Permissions : everyone-modify
; librairie d'image pour les fond de plan
Source: "..\Prod\Release\ImgLib\*"; DestDir: "{app}\ImgLib"; Flags: recursesubdirs createallsubdirs ; Permissions : users-modify
; images ressources de l'application
Source: "..\Prod\Release\Res\*"; DestDir: "{app}\Res"; Flags: recursesubdirs createallsubdirs ; Permissions : users-modify
; fichier de langues
Source: "..\Prod\Release\Lang\*.po"; DestDir: "{app}\Lang"; Permissions : everyone-modify
; fichier icone principal
Source: "..\Prod\Release\SmartApp.ico"; DestDir: "{app}"; Permissions : users-modify 
; fichier icone secondaire
Source: "..\Prod\Release\SmartAppSln.ico"; DestDir: "{app}"; Permissions : users-modify
; fichier de configuration de l'application
Source: "ressources\EN.SmartApp.exe.config"; DestDir: "{app}"; DestName:"SmartApp.exe.config"; Languages: english; Flags: onlyifdoesntexist uninsneveruninstall ; Permissions : users-modify
Source: "ressources\FR.SmartApp.exe.config"; DestDir: "{app}"; DestName:"SmartApp.exe.config"; Languages: french; Flags: onlyifdoesntexist uninsneveruninstall ; Permissions : users-modify
; fichier de fournitures
Source: "fournitures\*.saf"; DestDir: "{app}\exemples"; Flags: recursesubdirs createallsubdirs ; Permissions : users-modify
Source: "fournitures\*.slt"; DestDir: "{app}\exemples"; Flags: recursesubdirs createallsubdirs ; Permissions : users-modify
Source: "fournitures\*.pm3"; DestDir: "{app}\exemples"; Flags: recursesubdirs createallsubdirs ; Permissions : users-modify
Source: "fournitures\*.bmp"; DestDir: "{app}\exemples"; Flags: onlyifdoesntexist uninsneveruninstall ; Permissions : users-modify
Source: "fournitures\*.ini"; DestDir: "{app}"; Flags: onlyifdoesntexist uninsneveruninstall ; Permissions : users-modify

; NOTE: Don't use "Flags: ignoreversion" on any shared system files


[Registry]
Root: HKCR; Subkey: ".saf"; ValueType: string; ValueName: ""; ValueData: "smartapp"; Flags: uninsdeletevalue
Root: HKCR; Subkey: ".saf\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\SmartApp.ico"
Root: HKCR; Subkey: ".slt"; ValueType: string; ValueName: ""; ValueData: "smartappslt"; Flags: uninsdeletevalue
Root: HKCR; Subkey: ".slt\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\SmartAppSln.ico"
Root: HKCR; Subkey: "smartapp"; ValueType: string; ValueName: ""; ValueData: "Smart App files"; Flags: uninsdeletekey
Root: HKCR; Subkey: "smartapp\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\SmartApp.ico"
Root: HKCR; Subkey: "smartapp\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\SmartApp.exe"" ""%1"""
Root: HKCR; Subkey: "smartappslt"; ValueType: string; ValueName: ""; ValueData: "Smart App solution file"; Flags: uninsdeletekey
Root: HKCR; Subkey: "smartappslt\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\SmartAppSln.ico"
Root: HKCR; Subkey: "smartappslt\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\SmartApp.exe"" ""%1"""

[Icons]
Name: "{group}\Smart Config V3"; Filename: "{app}\SmartApp.exe"; WorkingDir: "{app}"
Name: "{group}\Smart Command V3"; Filename: "{app}\SmartApp.exe"; WorkingDir: "{app}"; Parameters: "-cmd"
Name: "{group}\{cm:UninstallProgram,Smart Application V3}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\Smart Config V3"; Filename: "{app}\SmartApp.exe"; WorkingDir: "{app}"; Tasks: desktopicon
Name: "{commondesktop}\Smart Command V3"; Filename: "{app}\SmartApp.exe"; WorkingDir: "{app}"; Tasks: desktopicon ;Parameters: "-cmd" 

[Run]
Filename: "{app}\SmartApp.exe"; Description: "{cm:LaunchProgram,Smart Application}"; Flags: nowait postinstall skipifsilent


[Code]
function InitializeSetup(): Boolean;
begin
	//init windows version
	initwinversion();
	
	//check if dotnetfx20 can be installed on this OS
	if not minwinspversion(5, 0, 3) then begin
		MsgBox(FmtMessage(CustomMessage('depinstall_missing'), [CustomMessage('win2000sp3_title')]), mbError, MB_OK);
		exit;
	end;
	if not minwinspversion(5, 1, 2) then begin
		MsgBox(FmtMessage(CustomMessage('depinstall_missing'), [CustomMessage('winxpsp2_title')]), mbError, MB_OK);
		exit;
	end;
	
	//if (not iis()) then exit;
	
	msi20('2.0');
	msi31('3.0');
	//ie6('5.0.2919');
	
	//dotnetfx11();
	//dotnetfx11lp();
	//dotnetfx11sp1();
	//kb886903(); //better use windows update
	//kb928366(); //better use windows update
	
	//install .netfx 2.0 sp2 if possible; if not sp1 if possible; if not .netfx 2.0
	//if minwinversion(5, 0) then begin
		//dotnetfx20();
		dotnetfx20sp2();
		//dotnetfx20sp2lp();
	//end else begin
		//if minwinversion(5, 0) and minwinspversion(5, 0, 4) then begin
			//kb835732();
			//dotnetfx20sp1();
			//dotnetfx20sp1lp();
		//end else begin
			//dotnetfx20();
			//dotnetfx20lp();
		//end;
	//end;
	
	//dotnetfx35();
	//dotnetfx35lp();
	//dotnetfx35sp1();
	//dotnetfx35sp1lp();
	
	//mdac28('2.7');
	//jet4sp8('4.0.8015');
	//sql2005express();
	
	Result := true;
end;


