#include "isxdl\isxdl.iss"

[CustomMessages]
DependenciesDir=MyProgramDependencies

english.depdownload_msg=The following applications are required before setup can continue:%n%n%1%nDownload and install now?
french.depdownload_msg=L'application suivante est requise pour poursuivre l'installation:%n%n%1%nTélécharger et installer maintenant?

english.depdownload_memo_title=Download dependencies
french.depdownload_memo_title=Téléchargement des dépendences

english.depinstall_memo_title=Install dependencies
french.depinstall_memo_title=Installer les dépendences

english.depinstall_title=Installing dependencies
french.depinstall_title=Installation des dépendences

english.depinstall_description=Please wait while Setup installs dependencies on your computer.
french.depinstall_description=Merci de patienter pendant que le setup installe les dépendances.

english.depinstall_status=Installing %1...
french.depinstall_status=Installation de %1...

english.depinstall_missing=%1 must be installed before setup can continue. Please install %1 and run Setup again.
french.depinstall_missing=%1 doit ętre installé avant de poursuivre l'installation. Merci d'installer %1 et de relancer l'installation.

english.depinstall_error=An error occured while installing the dependencies. Please restart the computer and run the setup again or install the following dependencies manually:%n
french.depinstall_error=Une erreur s'est produite pendant l'installation des dépendances. Veuillez redémarrer l'ordinateur et relancer l'installation, ou ou installez les dépendences manuellement:%n

french.isxdl_langfile=french.ini
english.isxdl_langfile=english.ini


[Files]
Source: "scripts\isxdl\french.ini"; Flags: dontcopy
Source: "scripts\isxdl\english.ini"; Flags: dontcopy

[Code]
type
	TProduct = record
		File: String;
		Title: String;
		Parameters: String;
	end;
	
var
	installMemo, downloadMemo, downloadMessage: string;
	products: array of TProduct;
	DependencyPage: TOutputProgressWizardPage;

  
procedure AddProduct(FileName, Parameters, Title, Size, URL: string);
var
	path: string;
	i: Integer;
begin
	installMemo := installMemo + '%1' + Title + #13;
	
	path := ExpandConstant('{src}{\}') + CustomMessage('DependenciesDir') + '\' + FileName;
	if not FileExists(path) then begin
		path := ExpandConstant('{tmp}{\}') + FileName;
		
		isxdl_AddFile(URL, path);
		
		downloadMemo := downloadMemo + '%1' + Title + #13;
		downloadMessage := downloadMessage + '    ' + Title + ' (' + Size + ')' + #13;
	end;
	
	i := GetArrayLength(products);
	SetArrayLength(products, i + 1);
	products[i].File := path;
	products[i].Title := Title;
	products[i].Parameters := Parameters;
end;

function InstallProducts: Boolean;
var
	ResultCode, i, productCount, finishCount: Integer;
begin
	Result := true;
	productCount := GetArrayLength(products);
		
	if productCount > 0 then begin
		DependencyPage := CreateOutputProgressPage(CustomMessage('depinstall_title'), CustomMessage('depinstall_description'));
		DependencyPage.Show;
		
		for i := 0 to productCount - 1 do begin
			DependencyPage.SetText(FmtMessage(CustomMessage('depinstall_status'), [products[i].Title]), '');
			DependencyPage.SetProgress(i, productCount);
			
			if Exec(products[i].File, products[i].Parameters, '', SW_SHOWNORMAL, ewWaitUntilTerminated, ResultCode) then begin
				//success; ResultCode contains the exit code
				if ResultCode = 0 then
					finishCount := finishCount + 1
				else begin
					Result := false;
					break;
				end;
			end else begin
				//failure; ResultCode contains the error code
				Result := false;
				break;
			end;
		end;
		
		//only leave not installed products for error message
		for i := 0 to productCount - finishCount - 1 do begin
			products[i] := products[i+finishCount];
		end;
		SetArrayLength(products, productCount - finishCount);
		
		DependencyPage.Hide;
	end;
end;

function PrepareProductToInstall: String;
var
	i: Integer;
	s: string;
begin
	if not InstallProducts() then begin
		s := CustomMessage('depinstall_error');
		
		for i := 0 to GetArrayLength(products) - 1 do begin
			s := s + #13 + '    ' + products[i].Title;
		end;
		
		Result := s;
	end;
end;

function UpdateReadyMemo(Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo, MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
var
	s: string;
begin
	if downloadMemo <> '' then
		s := s + CustomMessage('depdownload_memo_title') + ':' + NewLine + FmtMessage(downloadMemo, [Space]) + NewLine;
	if installMemo <> '' then
		s := s + CustomMessage('depinstall_memo_title') + ':' + NewLine + FmtMessage(installMemo, [Space]) + NewLine;

	s := s + MemoDirInfo + NewLine + NewLine + MemoGroupInfo
	
	if MemoTasksInfo <> '' then
		s := s + NewLine + NewLine + MemoTasksInfo;

	Result := s
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
	Result := true;

	if CurPageID = wpReady then begin

		if downloadMemo <> '' then begin
			//change isxdl language only if it is not english because isxdl default language is already english
			if ActiveLanguage() <> 'en' then begin
				ExtractTemporaryFile(CustomMessage('isxdl_langfile'));
				isxdl_SetOption('language', ExpandConstant('{tmp}{\}') + CustomMessage('isxdl_langfile'));
			end;
			//isxdl_SetOption('title', FmtMessage(SetupMessage(msgSetupWindowTitle), [CustomMessage('appname')]));
			
			if SuppressibleMsgBox(FmtMessage(CustomMessage('depdownload_msg'), [downloadMessage]), mbConfirmation, MB_YESNO, IDYES) = IDNO then
				Result := false
			else if isxdl_DownloadFiles(StrToInt(ExpandConstant('{wizardhwnd}'))) = 0 then
				Result := false;
		end;
	end;
end;

function IsX64: Boolean;
begin
	Result := Is64BitInstallMode and (ProcessorArchitecture = paX64);
end;

function IsIA64: Boolean;
begin
	Result := Is64BitInstallMode and (ProcessorArchitecture = paIA64);
end;

function GetURL(x86, x64, ia64: String): String;
begin
	if IsX64() and (x64 <> '') then
		Result := x64;
	if IsIA64() and (ia64 <> '') then
		Result := ia64;
	
	if Result = '' then
		Result := x86;
end;