using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main
{
    public class MoveGamePole : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        [SerializeField] private GameObject camObject;

        private Vector3 dragOrigin;


        private void Awake()
        {
            GameEvents.LoadConfigDoneEvent.AddListener(OnLoadConfig);
        }

        private void OnLoadConfig()
        {
            transform.position = new Vector3(GlobalStatic.xPole / 2, GlobalStatic.yPole / 2, 1);
            transform.localScale = new Vector3(GlobalStatic.xPole + 5, GlobalStatic.yPole + 5, 0);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 delta = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 campos = camObject.transform.position;

            campos.x = Mathf.Clamp(campos.x + delta.x, 0, GlobalStatic.xPole);
            campos.y = Mathf.Clamp(campos.y + delta.y, 0, GlobalStatic.yPole);

            camObject.transform.position = campos;
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }


    }
}
