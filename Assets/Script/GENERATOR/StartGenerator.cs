using UnityEngine;

namespace Main
{
    public class StartGenerator : MonoBehaviour
    {        

        private void Awake()
        {
            GlobalStatic.xPole = 15;
            GlobalStatic.yPole = 20;
        }

        void Start()
        {
            GameEvents.LoadConfigDoneEvent.Invoke();           
        }

    }
}
