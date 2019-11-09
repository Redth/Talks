#addin "Cake.FileHelpers"
#addin "Cake.XCode"
#addin "Cake.Xamarin.Build"

var POD_NAME = "MSColorPicker";
var POD_VERSION = "1.0.1";
var POD_IOS_VERSION = "8.0";

FileWriteText("./Podfile", $@"
platform :ios, '{POD_IOS_VERSION}'
install! 'cocoapods', :integrate_targets => false
target 'Xamarin' do
  pod '{POD_NAME}', '{POD_VERSION}'
end");

CocoaPodInstall("./");

var sdks = new [] { "iphoneos", "iphonesimulator" };
foreach (var sdk in sdks)
  XCodeBuild(new XCodeBuildSettings { Sdk = sdk, Project = "./Pods/Pods.xcodeproj"});

RunLipoCreate("./", $"./{POD_NAME}.a",
  $"./build/Release-iphoneos/{POD_NAME}/lib{POD_NAME}.a",
  $"./build/Release-iphonesimulator/{POD_NAME}/lib{POD_NAME}.a");

StartProcess("sharpie", $"bind -sdk iphoneos Pods/Pods.xcodeproj -c -IPods/{POD_NAME}");
