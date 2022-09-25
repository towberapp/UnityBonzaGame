using UnityEngine;

namespace Main
{
    public class StartGenerator : MonoBehaviour
    {        

        private void Awake()
        {
            GlobalStatic.xPole = 12;
            GlobalStatic.yPole = 15;
        }

        void Start()
        {
            GameEvents.LoadConfigDoneEvent.Invoke();           
        }

    }
}
