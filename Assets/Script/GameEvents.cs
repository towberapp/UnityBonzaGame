using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Main
{
    public static class GameEvents
    {
        public static UnityEvent<GameObject, GameObject> CollapsBlock = new();
    }
}
