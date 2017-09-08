using Android.App;
using Android.Widget;
using Android.OS;
using Com.Umeng.Message;
using Android.Content;
using Com.Umeng.Message.Entity;
using System;
using Com.Umeng.Message.Inapp;

namespace PushSample
{
    [Activity(Label = "PushSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        MyReceiver recever;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            recever = new MyReceiver();
            var filter = new IntentFilter();
            filter.AddAction(MainApplication.UPDATE_STATUS_ACTION);
            RegisterReceiver(recever, filter);

            PushAgent pushAgent = PushAgent.GetInstance(this);
            pushAgent.OnAppStart();
            var id = pushAgent.RegistrationId;

            if (bundle == null)
                InAppMessageManager.GetInstance(this).ShowCardMessage(this, "main", new MyUmengAppMsgCloseCallback());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterReceiver(recever);
        }

        class MyUmengAppMsgCloseCallback : Java.Lang.Object, IUmengInAppMsgCloseCallback
        {
            public void OnColse()
            {
                //UmLog.i(TAG, "card message close");
            }
        }

        class MyReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {
            }
        }
    }
}

