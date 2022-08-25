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
        public static UnityEvent<int> OnEndDrag = new();
        public static UnityEvent<Vector2, int> OnPonterDownEvent = new();
        public static UnityEvent<Vector2, int> OnGragEvent = new();


        public static UnityEvent<LetterArray, int> OnChechLetter = new();
        public static UnityEvent<int, int> ChangeIdGroupEvent = new();

        public static UnityEvent<GameObject, LetterArray> SetObjToArraytEvent = new();
        public static UnityEvent<GameObject, Vector2Int, Vector2Int> ChangePosObjectEvent = new();


        // ARCHIVE
        public static UnityEvent<GameObject, GameObject> CollapsBlock = new();

        /// <summary>
        /// Connect from to. ::: <int>slovo</int>, <int>fromBukva</int>, <int>toBukva</int>
        /// </summary>
        public static UnityEvent<int, int, int> ConnectedBukvaEvent = new();

        /// <summary>
        /// slovo, bukva, ObjectToConnect
        /// </summary>
        public static UnityEvent<int, int, GameObject> SendObjectToConnectEvent = new();


        private void OnDestroy()
        {
            OnBeginDrag.RemoveAllListeners();
            OnEndDrag.RemoveAllListeners();
        }
    }
}
