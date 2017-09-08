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
using Com.Umeng.Message;
using Com.Umeng.Message.Entity;

namespace PushSample
{
    [Application(Label = "MyApplication")]
    public class MainApplication : Application
    {
        public static string UPDATE_STATUS_ACTION = "apps.example.com.test.action.UPDATE_STATUS";
        public MainApplication(IntPtr handle, JniHandleOwnership ownerShip)
            : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            PushAgent pushAgent = PushAgent.GetInstance(this);
            pushAgent.SetDebugMode(false);
            pushAgent.MessageHandler = new MyMsgHandler();
            pushAgent.Register(new MyRegister(this));
        }


        class MyMsgHandler : UmengMessageHandler
        {
            public override Notification GetNotification(Context p0, UMessage p1)
            {
                return base.GetNotification(p0, p1);
            }

            public override void DealWithCustomMessage(Context p0, UMessage p1)
            {
                base.DealWithCustomMessage(p0, p1);
            }
        }

        class MyRegister : Java.Lang.Object, IUmengRegisterCallback
        {
            Application app;
            public MyRegister(Application app)
            {
                this.app = app;
            }

            public void OnFailure(string p0, string p1)
            {
                //a.RunOnUiThread(() => Toast.MakeText(a, $"×¢²áÊ§°Ü {p0} {p1}", ToastLength.Long).Show());

                app.SendBroadcast(new Intent(UPDATE_STATUS_ACTION));
            }

            public void OnSuccess(string p0)
            {
                //a.RunOnUiThread(() => Toast.MakeText(a, $"×¢²á³É¹¦ token={p0}", ToastLength.Long).Show());
                app.SendBroadcast(new Intent(UPDATE_STATUS_ACTION));
            }
        }
    }
}