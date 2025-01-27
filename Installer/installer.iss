; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "TrayDir"
#define MyAppVersion "3.3.1"
#define MyAppPublisher "samver"
#define MyAppURL "https://samver.ca/TrayDir"
#define MyAppExeName "TrayDir.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{98A92E06-269C-4525-B897-BE09DDA4BBC3}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={commonpf}\{#MyAppPublisher}\{#MyAppName}
DefaultGroupName=samver/TrayDir
DisableProgramGroupPage=yes
LicenseFile=license.txt
;InfoBeforeFile=before-info.txt
;InfoAfterFile=after-info.txt
; Remove the following line to run in administrative install mode (install for all users.)
; PrivilegesRequired=lowest
; PrivilegesRequiredOverridesAllowed=dialog
OutputDir=C:\Programming\samverApps\TrayDir\Installer\bin
OutputBaseFilename="TrayDir_{#MyAppVersion}_Install"
SetupIconFile=C:\Programming\samverApps\TrayDir\TrayDir\Resources\file-exe.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern
UsePreviousAppDir=no
DirExistsWarning=no
CloseApplications=force

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Programming\samverApps\TrayDir\TrayDir\bin\Release\TrayDir.exe"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Dirs]
Name: {app};Permissions: users-full;