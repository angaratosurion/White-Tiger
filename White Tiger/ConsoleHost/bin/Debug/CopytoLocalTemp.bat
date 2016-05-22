mkdir "%temp%\White_Tiger\ConsoleHost"
dir
copy *.* "%temp%\White_Tiger\ConsoleHost\"
cd ..
cd ..
cd ..
cd "White Tiger Managment\bin\Debug"
xcopy ".\WhiteTiger_Settings" "%temp%\White_Tiger\ConsoleHost\WhiteTiger_Settings\" /E /Y
"%temp%\White_Tiger\ConsoleHost\ConsoleHost.exe"
