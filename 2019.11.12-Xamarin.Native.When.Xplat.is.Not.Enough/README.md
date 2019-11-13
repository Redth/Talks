Native Xamarin: When Xplat is not enough
========================================

 - Video: https://www.youtube.com/watch?v=NyqxScrnJKw
 - SpeakerDeck: https://speakerdeck.com/redth
 - Other Links
   - Gitter Xamarin Components Chat: https://gitter.im/xamarin/XamarinComponents


iOS Binding Notes:
==================

## MSColorPicker Bindings with Sharpie


Here's a summary of what we did to get the MSColorPicker `ApiDefinition.cs` and `StructsAndEnums.cs` generated, as well as building the static library for MSColorPicker with all of the architectures in it (armv7, arm64 x86_64, x86).

### Setup CocoaPods

You'll need to install [CocoaPods](https://cocoapods.org).

Next you need to create a file named `Podfile` with the contents:

```
platform :ios, '8.0'
install! 'cocoapods', :integrate_targets => false
target 'Xamarin' do
  pod 'MSColorPicker', '1.0.1'
end
```

Next, you'll need to run `pod install` in the directory with the Podfile.

### Building the static binary

 1. Navigate you terminal into the `Pods` folder. (`cd Pods`)
 2. Run `xcodebuild -sdk iphoneos` to build the armv7 / arm64 binary
 3. Run `xcodebuild -sdk iphonesimulator` to build the x86 / x86_64 binary
 4. Navigate back up a folder level (`cd ..`)
 5. Run `lipo -create -o MSColorPicker.a build/Release-iphoneos/MSColorPicker/libMSColorPicker.a build/Release-iphonesimulator/MSColorPicker/libMSColorPicker.a`
 
The result should be a file named `MSColorPicker.a`.  If you run `lipo -info MSColorPicker.a` it should list all of the architectures in it.

### Generating the Binding definitions

For this step you will need to install [Objective Sharpie](https://docs.microsoft.com/en-us/xamarin/cross-platform/macios/binding/objective-sharpie/get-started).

Run: `sharpie bind -sdk iphoneos Pods/Pods.xcodeproj -c -IPods/MSColorPicker`

### Add everything to a binding project

Create an iOS Binding Library project.

Next you're ready to add the `MSColorPicker.a` file to your binding project's _Native Frameworks_ project section.

Be sure to set the following properties on the file once you've added it to your project:
 - Static: `yes`
 - Force Load: `yes`
 - Frameworks: `UIKit`
 - SmartLink: `yes`
 - LinkerFlags: `-ObjC`

Finally, copy the contents of the `ApiDefinition.cs` and `StructsAndEnums.cs` files which sharpie generated earlier into the `ApiDefinition.cs` and `Structs.cs` files in your binding project, overwriting the existing contents.


Android Binding Notes
=====================

For Android, we needed to download the .aar file from Maven.  

We started looking at the latest version here: https://search.maven.org/artifact/com.jaredrummler/colorpicker/1.1.0/aar

Looking in the pom xml file, we could see it had dependencies on some AndroidX bits that we wanted to avoid, so we eventually found https://search.maven.org/artifact/com.jaredrummler/colorpicker/1.0.2/aar instead which uses Android Support and downloaded the .aar.

We added it to our project and made sure its build action was set to `LibraryProjectZip`.



