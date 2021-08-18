using Firebase.Auth;
using Foundation;
using System;
using System.Threading.Tasks;
using Time_Tracker.Models;
using Time_Tracker.Services.Account;
using Xamarin.Forms;

[assembly: Dependency(typeof(Time_Tracker.iOS.Services.AccountService))]
namespace Time_Tracker.iOS.Services
{
    public class AccountService : IAccountService
    {
        public Task<AuthenticatedUser> GetUserAsync()
        {
            var tcs = new TaskCompletionSource<AuthenticatedUser>();
            Firebase.CloudFirestore.Firestore.SharedInstance
                .GetCollection("users")
                .GetDocument(Auth.DefaultInstance.CurrentUser.Uid)
                .GetDocument((snapshot, error) =>
                {
                    if (error != null)
                    {
                        // somthing went wrong
                        tcs.TrySetResult(default(AuthenticatedUser));
                        return;
                    }
                    tcs.TrySetResult(new AuthenticatedUser
                    {
                        Id = snapshot.Id,
                        FirstName = snapshot.GetValue(new NSString("FirstName")).ToString(),
                        LastName = snapshot.GetValue(new NSString("LastName")).ToString()
                    });

                });

                return tcs.Task;
        }

        public Task<bool> LoginAsync(string username, string password)
        {
            var tcs = new TaskCompletionSource<bool>();
            Auth.DefaultInstance.SignInWithPasswordAsync(username, password)
                .ContinueWith((task) => OnAuthCompleted(task, tcs));
            return tcs.Task;
        }

        private void OnAuthCompleted(Task task, TaskCompletionSource<bool> tcs)
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                //somthing went wrong
                tcs.SetResult(false);
                return;
            }
            // user is logged in
            tcs.SetResult(true);
        }
    }
}