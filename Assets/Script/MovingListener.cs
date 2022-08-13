using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class MovingListener : MonoBehaviour
    {
 
        private Vector2 startPoint;
        private BlockGlobalPublic globalPublic;

        private void Awake()
        {
            GameEvents.OnPonterDownEvent.AddListener(OnPointer);
            GameEvents.OnGragEvent.AddListener(OnDragEvent);
            GameEvents.OnEndDrag.AddListener(OnEndDrag);
            globalPublic = GetComponent<BlockGlobalPublic>();
        }

        private void OnPointer(Vector2 delta, int id)
        {
            if (globalPublic.letterArray.groupId != id) return;
            this.startPoint = new Vector2(delta.x - transform.position.x, delta.y - transform.position.y);
        }

        private void OnDragEvent(Vector2 pos, int id)
        {
            if (globalPublic.letterArray.groupId != id) return;
            transform.position = new Vector2(pos.x - this.startPoint.x, pos.y - this.startPoint.y);
        }

        private void OnEndDrag(int id)
        {           
            if (globalPublic.letterArray.groupId != id) return;
            
            int xPos = Mathf.RoundToInt(transform.position.x);
            int yPos = Mathf.RoundToInt(transform.position.y);  

            transform.position = new Vector2(xPos,yPos);

            globalPublic.letterArray.xPos = xPos;
            globalPublic.letterArray.yPos = yPos;

            GameEvents.OnChechLetter.Invoke(globalPublic.letterArray, id);
        }



    }
}
