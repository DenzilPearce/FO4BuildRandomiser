@echo off

setlocal
set "builddir=%~dP0GeneratedSolution"
set "sourcedir=%~dP0wpfVersion"

if exist %builddir% rmdir %builddir% /S /Q

IF %ERRORLEVEL% NEQ 0 (
  goto error
)

mkdir %builddir%

call cmake -S %sourcedir% -G "Visual Studio 17 2022" -B %builddir% -A Win32

IF %ERRORLEVEL% NEQ 0 (
  goto error
)

goto end

:error
color 4F
EXIT /B 1

:end
endlocal