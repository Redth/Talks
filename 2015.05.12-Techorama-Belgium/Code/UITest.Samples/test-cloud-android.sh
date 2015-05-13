#!/bin/bash

# Build the solution
xbuild /p:Configuration=Release

# Generate a .apk
xbuild /t:SignAndroidPackage /p:Configuration=Release LoginSample.Android/LoginSample.Android.csproj
	
# Send it off to Test Cloud
mono packages/Xamarin.UITest.0.7.2/tools/test-cloud.exe submit LoginSample.Android/bin/Release/LoginSample.LoginSample-Signed.apk YOUR-TEST-CLOUD-KEY --devices YOUR-DEVICES-HASH --series "master" --locale "en_US" --user your@email.com --assembly-dir LoginSample.Android.UITests/bin/Release/
