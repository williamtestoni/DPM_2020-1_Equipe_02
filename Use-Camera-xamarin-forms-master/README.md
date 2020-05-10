# Use-Camera-xamarin-forms
Use Camera to take images and take videos and can use gallery to pick up to 



to not make errors 

1- in AndroidManifest.xml ::::  put between 
application tag

that code 


	<provider android:name="android.support.v4.content.FileProvider"
              android:authorities="com.companyname.UseCamera.fileprovider" 
      android:exported="false" android:grantUriPermissions="true">
			<meta-data android:name="android.support.FILE_PROVIDER_PATHS"
                 android:resource="@xml/filepaths"></meta-data>
		</provider>
    
    
    
 2-   create file in android project in Resources call xml and create in it file call filepaths
    and put in this file 
    
     <paths xmlns:android="http://schemas.android.com/apk/res/android">
     <external-path name="my_images" path="Android/data/com.companyname.UseCamera/files/Pictures" />
     <external-path name="my_movies" path="Android/data/com.companyname.UseCamera/files/Movies" />  
  
  
  3- don't forget run time permissions it you target mobile with api mor than 23
  
  4- add permisions in mainfest 
  
  
        <uses-permission android:name="android.permission.CAMERA" />
	<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
	<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
  
  
  
  5- in assemply file 
  
  
     [assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
    [assembly: UsesPermission(Android.Manifest.Permission.ReadExternalStorage)]
    [assembly: UsesPermission(Android.Manifest.Permission.Camera)]
    [assembly: UsesFeature("android.hardware.camera", Required = false)]
    [assembly: UsesFeature("android.hardware.camera.autofocus", Required = false)]
