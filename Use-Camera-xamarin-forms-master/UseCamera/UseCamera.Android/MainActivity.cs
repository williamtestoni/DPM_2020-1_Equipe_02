using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Plugin.Media;

namespace UseCamera.Droid
{
    [Activity(Label = "UseCamera", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        readonly string[] permissions= { Manifest.Permission.Camera ,Manifest.Permission.WriteExternalStorage};
       const  int cameraPermissionNumber = 341;

        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            await CrossMedia.Current.Initialize();

            global::Xamarin.Forms.Forms.Init(this, bundle);

            if ((int)Build.VERSION.SdkInt < 23)
                LoadApplication(new App());
            else {
                take_permissions();
            }
        }
        public void take_permissions() {
            if (CheckSelfPermission(permissions[0]) == (int)Permission.Granted)
            {
                LoadApplication(new App());

            }
            else {
                RequestPermissions(permissions, cameraPermissionNumber);
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch(requestCode)
            {
                case cameraPermissionNumber:
                    {
                        if (grantResults[0] == Permission.Granted) { 
                        Toast.MakeText(this, "camera permission granted", ToastLength.Long).Show();
                        LoadApplication(new App());
                        }
                        if (grantResults[1] == Permission.Granted) {
                            Toast.MakeText(this, "storage permission granted", ToastLength.Long).Show();

                        }
                        break;
                    }

            }

        }
    }
}

