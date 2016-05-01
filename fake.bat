@echo off

set fake_args=%*

cd "._fake"
if not exist paket.lock (
  call "paket.bat" "install"
) else (
  call "paket.bat" "restore"
)
cd ".."

"._fake\packages\FAKE\tools\FAKE.exe" "build.fsx" %fake_args%
