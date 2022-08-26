using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Main
{
    public class GameEvents : MonoBehaviour
    {
        public static UnityEvent<int> OnBeginDrag = new();
        public static UnityEvent<Vector2, int> OnPonterDownEvent = new();
        public static UnityEvent<Vector2, int> OnGragEvent = new();
        public static UnityEvent<int> OnGragEnd = new();

        // allStatic Varible is load
        public static UnityEvent LoadConfigDoneEvent = new();

        private void OnDestroy()
        {
            OnBeginDrag.RemoveAllListeners();
            OnPonterDownEvent.RemoveAllListeners();
            OnGragEvent.RemoveAllListeners();
            OnGragEnd.RemoveAllListeners();
            LoadConfigDoneEvent.RemoveAllListeners();


        }
    }
}
