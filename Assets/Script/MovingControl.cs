using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Main
{
    public class MovingControl : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {

        Vector2 startPoint;

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector2 delta = Camera.main.ScreenToWorldPoint(eventData.position);            
            startPoint = new Vector2(delta.x - transform.position.x, delta.y - transform.position.y);
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            SetBorderCollider(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = new Vector2(pos.x - startPoint.x, pos.y - startPoint.y);            
        }
         
        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = new Vector2(
                            Mathf.RoundToInt(transform.position.x),
                            Mathf.RoundToInt(transform.position.y)
                            );

            SetBorderCollider(true);

        }

        private void SetBorderCollider(bool status)
        {
            Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
            foreach (var item in colliders)
            {
                if (item.CompareTag("Border"))
                {
                    item.enabled = status;
                }
            }
        }

    }
}