using UnityEngine;
using UnityEngine.EventSystems;

namespace Main
{
    public class MovingControlEvent : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Camera cam;
        private int groipId;
        private BlockGlobalPublic globalPublic;
        private Vector2Int startIntPoint;
        private readonly GlobalArrayControl globalArrayControl = new();

        private void Awake()
        {
            cam = Camera.main;
            globalPublic = GetComponent<BlockGlobalPublic>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            this.groipId = globalPublic.letterArray.groupId;
            this.startIntPoint = new Vector2Int(globalPublic.letterArray.xPos, globalPublic.letterArray.yPos);

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
            GameEvents.OnGragEnd.Invoke(groipId);

            int xPos = Mathf.RoundToInt(transform.position.x);
            int yPos = Mathf.RoundToInt(transform.position.y);

            Vector2Int newPos = new (xPos, yPos);
            Vector2Int deltaPos = newPos - startIntPoint;

            globalArrayControl.MoveGroupToPos(groipId, deltaPos);
        }

    }
}