xcodebuild -sdk iphoneos -arch arm64
xcodebuild -sdk iphonesimulator -arch x86_64

lipo -create -o build/Release-iphoneos/MapboxWrapper.framework/MapboxWrapper \
    build/Release-iphonesimulator/MapboxWrapper.framework/MapboxWrapper

lipo -info build/Release-iphoneos/MapboxWrapper.framework/MapboxWrapper