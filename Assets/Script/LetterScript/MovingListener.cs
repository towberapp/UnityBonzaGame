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

        private void Awake()
        {
            GameEvents.OnPonterDownEvent.AddListener(OnPointer);
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
            this.startPoint = new Vector2(delta.x - transform.position.x, delta.y - transform.position.y);

            globalPublic.MoveOn();
        }

        private void OnDragEvent(Vector2 pos, int id)
        {
            if (globalPublic.letterArray.groupId != id) return;
            transform.position = new Vector3(pos.x - this.startPoint.x, pos.y - this.startPoint.y, -5);
        }


    }
}
