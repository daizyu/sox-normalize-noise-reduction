# this is
If you need to modify wave files , you can choose it.
This tool's functions are 
- normalize
- noise reduction

# how to use

calibration {WAVE_FILE_MAME}

# compile & install

## Download sox from below url
(https://sourceforge.net/projects/sox/)

## Copy calibration.cs to {SOX_PATH}

## find C# compiler
cd c:\windows
dir /b /s | findstr csc.exe

c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe
c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe.config
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe.config
                       :
                       :

## compile
cd {SOX_PATH}
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  -target:library -out:calibration.exe calibration.cs

# How to use

# basic
{SOX_PATH}\calibration.exe {Your Wave File}

#noise reduction
please put noise reduction wave file {SOX_PATH}.

Reduction wave file names are 
- noise1.wav
- noise2.wav
- noise3.wav
     :
     :
- noise9.wav

# how to create noise reduction wave file
Please create only noise wave after calibrate using Wave File Editor.
My recomended Wave File Editor is Audacity. 
(https://www.audacityteam.org/)

