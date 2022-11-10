using UnityEngine;
using Firebase.Extensions;
using Firebase.Analytics;
using UnityEngine.Events;

namespace Main
{
    public class FirebaseInit : MonoBehaviour
    {
        public UnityEvent OnInitialized = new UnityEvent();        

        private void Awake()
        {
             Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {

                 var dependencyStatus = task.Result;
                 if (dependencyStatus == Firebase.DependencyStatus.Available)
                    {
                        OnInitialized.Invoke();
                    }
                 else
                    {
                        UnityEngine.Debug.LogError(System.String.Format(
                            "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    }
             });
        }


    }
}
