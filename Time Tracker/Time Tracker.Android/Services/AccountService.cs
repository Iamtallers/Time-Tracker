using Firebase.Auth;
using Firebase.Firestore;
using System.Threading.Tasks;
using Time_Tracker.Droid.ServiceListeners;
using Time_Tracker.Models;
using Time_Tracker.Services.Account;
using Xamarin.Forms;

[assembly: Dependency(typeof(Time_Tracker.Droid.Services.AccountService))]
namespace Time_Tracker.Droid.Services
{
    public class AccountService : IAccountService
    {
        public AccountService()
        {
        }

        public Task<AuthenticatedUser> GetUserAsync()
        {
            var tcs = new TaskCompletionSource<AuthenticatedUser>();
            FirebaseFirestore.Instance
                .Collection("users")
                .Document(FirebaseAuth.Instance.CurrentUser.Uid)
                .Get()
                .AddOnCompleteListener(new OnCompleteListener(tcs));

            return tcs.Task;
        }

        public Task<bool> LoginAsync(string username, string password)
        {
            var tcs = new TaskCompletionSource<bool>();
            FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(username, password)
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
            tcs.SetResult(true);
        }
    }
}