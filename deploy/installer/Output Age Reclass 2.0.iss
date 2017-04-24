#define PackageName      "Age Reclass Output"
#define PackageNameLong  "Age Reclassification Output"
#define Version          "2.0"
#define ReleaseType      "official"
#define CoreVersion      "6.0"
#define CoreReleaseAbbr  ""

#define ExtDir "C:\Program Files\LANDIS-II\v6\bin\extensions"
#define AppDir "C:\Program Files\LANDIS-II\v6\"
#define LandisPlugInDir "C:\Program Files\LANDIS-II\plug-ins"

#include "package (Setup section) v6.0.iss"


[Files]
; This .dll IS the extension (ie, the extension's assembly)
; NB: Do not put an additional version number in the file name of this .dll
; (The name of this .dll is defined in the extension's \src\*.csproj file)
Source: ..\..\src\bin\debug\Landis.Extension.Output.AgeReclass.dll; DestDir: {#ExtDir}; Flags: replacesameversion


; Requisite auxiliary libraries
; NB. These libraries are used by other extensions and thus are never uninstalled.
; (There are no auxiliary libraries for Output Age Reclassification)


; User Guides are no longer shipped with installer
; Source: docs\LANDIS-II Age Reclass Output v2.0 User Guide.pdf; DestDir: {#AppDir}\docs


; Complete example for testing the extension
Source: ..\examples\*.txt; DestDir: {#AppDir}\examples\Output Age Reclass
Source: ..\examples\*.gis; DestDir: {#AppDir}\examples\Output Age Reclass
Source: ..\examples\*.bat; DestDir: {#AppDir}\examples\Output Age Reclass


; LANDIS-II identifies the extension with the info in this .txt file
; NB. New releases must modify the name of this file and the info in it
#define InfoTxt "Output Age Reclass 2.0.txt"
Source: {#InfoTxt}; DestDir: {#LandisPlugInDir}

[Run]
;; Run plug-in admin tool to add entries for each plug-in
#define PlugInAdminTool  CoreBinDir + "\Landis.PlugIns.Admin.exe"



Filename: {#PlugInAdminTool}; Parameters: "remove ""Output Age Reclass"" "; WorkingDir: {#LandisPlugInDir}
Filename: {#PlugInAdminTool}; Parameters: "add ""{#InfoTxt}"" "; WorkingDir: {#LandisPlugInDir}



[Code]
{ Check for other prerequisites during the setup initialization }
#include "package (Code section) v3.iss"


//-----------------------------------------------------------------------------

function InitializeSetup_FirstPhase(): Boolean;
begin
  Result := True
end;
