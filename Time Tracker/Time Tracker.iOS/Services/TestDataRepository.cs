using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Time_Tracker.iOS.Services;
using Time_Tracker.Models;
using UIKit;
using Xamarin.Forms;

[assembly:Dependency(typeof(TestDataRepository))]
namespace Time_Tracker.iOS.Services
{
    public class TestDataRepository : BaseRepository<TestData>
    {
        public override string DocumentPath =>
            "users/"
            +Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid
            +"/test";
    }
}