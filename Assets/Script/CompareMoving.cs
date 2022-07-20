using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class CompareMoving: MonoBehaviour
    {
        private void Awake()
        {
            GameEvents.CollapsBlock.AddListener(OnCollapse);
        }

        public void OnCollapse(GameObject main, GameObject arg)
        {
            Debug.Log("OnCollapse");
            
            SquareBlockController[] block = arg.GetComponentsInChildren<SquareBlockController>();

            foreach (var item in block)
            {
                item.gameObject.transform.SetParent(main.transform);
            }

            Destroy(arg.gameObject);


        }
    }
}
