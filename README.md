#This is
If you need to modify wave files , you can choose it.
This tool's functions are 
- normalize
- noise reduction

#Compile & install

##Download sox from below url
(https://sourceforge.net/projects/sox/)

##Copy calibration.cs to {SOX_PATH}

##Find C# compiler
cd c:\windows
dir /b /s | findstr csc.exe

c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe
c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe.config
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe.config 
                       : 
                       : 

##Compile
cd {SOX_PATH}
c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  -target:library -out:calibration.exe calibration.cs

# How to use

# Basic
{SOX_PATH}\calibration.exe {Your Wave File}

#Noise reduction
please put noise reduction wave file {SOX_PATH}.

Reduction wave file names are 
- noise1.wav
- noise2.wav
- noise3.wav 
     : 
     :
- noise9.wav

#How to create noise reduction wave file
Please create only noise wave after calibrate using Wave File Editor.
My recomended Wave File Editor is Audacity. 
(https://www.audacityteam.org/)

