sc create POEService binpath="%~dp0\POEService.exe"
sc config POEService start=auto
sc start POEService