using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace INchangCal_Android
{
    [Activity(Label = "INCAL", MainLauncher = true)]
    public class loginPage : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //화면 띄우기
            SetContentView(Resource.Layout.LoginPage);
            //변수 선언

            Button Login_Button = FindViewById<Button>(Resource.Id.LoginPage_LoginButton);
            EditText Id_EditText = FindViewById<EditText>(Resource.Id.LoginPage_IdText);
            EditText Ps_EditText = FindViewById<EditText>(Resource.Id.LoginPage_PsText);
            TextView error_TextView = FindViewById<TextView>(Resource.Id.LoginPage_ErrorText);

            Login_Button.Click += (sender, e) =>
            {
                string id = Id_EditText.Text;
                string ps = Ps_EditText.Text;
                if (id == "hello" && ps == "world")
                {
                var intent = new Intent(this, typeof(MainPage));
                StartActivity(intent);
                }
            };
        }

        public override void OnBackPressed()
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);

            builder.SetPositiveButton("확인", (senderAlert, args) => {
                Finish();
            });

            builder.SetNegativeButton("취소", (senderAlert, args) => {
                return;
            });

            Android.App.AlertDialog alterDialog = builder.Create();
            alterDialog.SetTitle("알림");
            alterDialog.SetMessage("프로그램을 종료 하시겠습니까?");
            alterDialog.Show();
        }

    }
}

