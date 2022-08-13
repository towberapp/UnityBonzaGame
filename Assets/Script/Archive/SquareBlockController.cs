using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Main
{
    public class SquareBlockController : MonoBehaviour
    {
        public int slovo;
        public int bukva;

        private void Awake()
        {
            GameEvents.ConnectedBukvaEvent.AddListener(OnBukvaConnect);
            GameEvents.SendObjectToConnectEvent.AddListener(OnSendObjectToConnect);
        }

        private void OnBukvaConnect(int slovoConnect, int fromBukvaConnect, int toBukvaConnect)
        {            

            if (slovoConnect == slovo && fromBukvaConnect == bukva)         
            {
                Debug.LogFormat("{0}, {1}, {2}", slovoConnect, fromBukvaConnect, toBukvaConnect);
                GameEvents.SendObjectToConnectEvent.Invoke(slovo, toBukvaConnect, gameObject);                
            }
        }

        private void OnSendObjectToConnect(int slovoConnect, int fromBukvaConnect, GameObject connectObect)
        {            
            if (slovoConnect == slovo && fromBukvaConnect == bukva)
            {
                Debug.LogFormat("OnSendObjectToConnect: {0}, {1}, {2}", slovoConnect, fromBukvaConnect, connectObect);
                GameEvents.CollapsBlock.Invoke(gameObject.transform.parent.gameObject, connectObect.transform.parent.gameObject);
            }
        }


    }
}
