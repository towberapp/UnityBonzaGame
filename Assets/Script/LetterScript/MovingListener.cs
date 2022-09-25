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
        private SpriteRenderer rend;
        private float deltaSdvig = 0.10f;
        private bool isMove = false;

        private void Awake()
        {
            GameEvents.OnPonterDownEvent.AddListener(OnPointer);
            GameEvents.OnPonterUpEvent.AddListener(OnPointerUp);
            GameEvents.OnGragEvent.AddListener(OnDragEvent);
            GameEvents.OnGragEnd.AddListener(OnDragEnd);            

            globalPublic = GetComponent<BlockGlobalPublic>();

        }

        private void OnDragEnd(int group)
        {
            if (globalPublic.letterArray.groupId != group) return;            
            globalPublic.MoveOff();
        }

        private void OnPointer(Vector2 delta, int id)
        {
            if (globalPublic.letterArray.groupId != id) return;

            this.startPoint = new Vector2(delta.x - transform.position.x, delta.y - transform.position.y - deltaSdvig*2);
            transform.position = new Vector2(transform.position.x + deltaSdvig, transform.position.y + deltaSdvig);
            globalPublic.MoveOn();
        }

        private void OnPointerUp(int id)
        {           
            if (globalPublic.letterArray.groupId != id) return;

            if (!isMove)
            {
                transform.position = new Vector2(transform.position.x - deltaSdvig, transform.position.y - deltaSdvig);
                globalPublic.MoveOff();
            }

            if (isMove) isMove = false;
        }

        private void OnDragEvent(Vector2 pos, int id)
        {                        
            if (globalPublic.letterArray.groupId != id) return;

            isMove = true;
            transform.position = new Vector3(pos.x - this.startPoint.x + deltaSdvig, pos.y - this.startPoint.y - deltaSdvig, -5);
        }


    }
}
