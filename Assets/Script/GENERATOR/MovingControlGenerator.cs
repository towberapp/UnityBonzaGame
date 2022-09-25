using UnityEngine;
using UnityEngine.EventSystems;

namespace Main
{
    public class MovingControlGenerator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Camera cam;
        private int groipId;
        private BlockGlobalPublic globalPublic;
        private Vector2Int startIntPoint;
        private readonly GlobalArrayControl globalArrayControl = new();
        private readonly GeneratorGroupControl generatorGroupControl = new();

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
            GeneratorEvents.SelectGroupEvent.Invoke(groipId);
            GeneratorGroupControl.idGroup = groipId;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
           GameEvents.OnPonterUpEvent.Invoke(groipId);
            //Debug.Log("POINT UP: " + groipId);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
           GameEvents.OnBeginDrag.Invoke(groipId);
        }

        public void OnDrag(PointerEventData eventData)
        {           
            Vector2 pos = cam.ScreenToWorldPoint(eventData.position);
            
            if (GlobalStatic.canMoveBlock)
            GameEvents.OnGragEvent.Invoke(pos, groipId);          
        }
         
        public void OnEndDrag(PointerEventData eventData)
        {
            GameEvents.OnGragEnd.Invoke(groipId);

            //Debug.Log("END DRAG: " + groipId);

            int xPos = Mathf.RoundToInt(transform.position.x);
            int yPos = Mathf.RoundToInt(transform.position.y);

            Vector2Int newPos = new (xPos, yPos);
            Vector2Int deltaPos = newPos - startIntPoint;

            generatorGroupControl.SetPosition(groipId, deltaPos);

            //globalArrayControl.MoveGroupToPos(groipId, deltaPos);
        }
 
    }
}