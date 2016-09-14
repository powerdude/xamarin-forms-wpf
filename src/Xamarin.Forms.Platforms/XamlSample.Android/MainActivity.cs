﻿using Android.App;

namespace XamlSample.Android
{
    using PortableForms;
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    /// <summary>
    /// The main activity.
    /// </summary>
    [Activity(Label = "XamlSample.Android", Icon = "@drawable/icon", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : FormsApplicationActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            this.LoadApplication(new App());
        }
    }
}

