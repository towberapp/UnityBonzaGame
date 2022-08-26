using UnityEngine;
using UnityEngine.EventSystems;

namespace Main
{
    public class MovingControlEvent : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Camera cam;
        private int groipId;
        private BlockGlobalPublic globalPublic;

        private void Awake()
        {
            cam = Camera.main;
            globalPublic = GetComponent<BlockGlobalPublic>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            this.groipId = globalPublic.letterArray.groupId;
            Vector2 delta = cam.ScreenToWorldPoint(eventData.position);            
            GameEvents.OnPonterDownEvent.Invoke(delta, groipId);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            GameEvents.OnBeginDrag.Invoke(groipId);
        }

        public void OnDrag(PointerEventData eventData)
        {           
            Vector2 pos = cam.ScreenToWorldPoint(eventData.position);
            GameEvents.OnGragEvent.Invoke(pos, groipId);          
        }
         
        public void OnEndDrag(PointerEventData eventData)
        {
            // before drop groip, check busy place
            //

            GameEvents.OnEndDrag.Invoke(groipId); 
        }

    }
}