#!/bin/bash

# Generate a .ipa
xbuild /p:Configuration=Release /p:Platform=iPhone /p:BuildIpa=true /p:IpaPackageName=LoginSample.iOS.ipa LoginSample.iOS/LoginSample.iOS.csproj	

# Send it off to Test Cloud
mono packages/Xamarin.UITest.0.7.2/tools/test-cloud.exe submit LoginSample.iOS/bin/iPhone/Release/LoginSample.iOS.ipa YOUR-TEST-CLOUD-KEY --devices YOUR-DEVICES-HASH --series "master" --locale "en_US" --user your@email.com --assembly-dir LoginSample.iOS.UITests/bin/Release/
