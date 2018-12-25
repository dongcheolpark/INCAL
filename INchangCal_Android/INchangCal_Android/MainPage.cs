using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace INchangCal_Android
{
    [Activity(Label = "INCAL")]
    class MainPage : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            Button Login_Button = FindViewById<Button>(Resource.Id.LoginPage_LoginButton);
            EditText Id_EditText = FindViewById<EditText>(Resource.Id.LoginPage_IdText);
            EditText Ps_EditText = FindViewById<EditText>(Resource.Id.LoginPage_PsText);
            TextView error_TextView = FindViewById<TextView>(Resource.Id.LoginPage_ErrorText);

        }
    }
}