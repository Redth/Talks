#!/bin/bash
MONOXBUILD=/Library/Frameworks/Mono.framework/Commands/xbuild

$MONOXBUILD /p:Configuration=Release /t:SignAndroidPackage ../WatchFaceDemo/WatchFaceDemo.csproj

cp ../WatchFaceDemo/bin/Release/com.demo.watchface-Signed.apk Resources/raw/wearable_app.apk
