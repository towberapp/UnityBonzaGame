using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main
{
    public class BorderControl : MovingControl
    {        
        public int chain;
        public int slovo;
        public int bukva;
        public bool isConnected = false;

        //[SerializeField] private SquareBlockController squareBlockController;

        private Collider2D _collision;
 

        private void Awake()
        {
            _collision = gameObject.GetComponent<Collider2D>();

            //slovo = squareBlockController.slovo;
            //bukva = squareBlockController.bukva;

            //GameEvents.OnBeginDrag.AddListener(OnBeginDragEvent);
            //GameEvents.OnEndDrag.AddListener(OnEndDragEvent);
        }

        private void OnEndDragEvent()
        {
            _collision.isTrigger = true;
        }

        private void OnBeginDragEvent()
        {
            _collision.isTrigger = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {            
            //Debug.LogWarning("Collision: " + collision.tag);
            CheckChain(collision);
        }

        private void CheckChain(Collider2D collision)
        {
            if (chain == 0) return;

            if (collision.gameObject.GetComponent<BorderControl>() != null)
            {
                BorderControl borderCollision = collision.gameObject.GetComponent<BorderControl>();

                if (borderCollision.chain == chain && borderCollision.slovo == slovo && !isConnected)
                {
                    isConnected = true;
                    borderCollision.isConnected = true;                    
                    
                    _collision.enabled = false;
                    collision.enabled = false;

                    print("Is Connectd");
                    SetConnectedBorder(collision);

                    GameEvents.ConnectedBukvaEvent.Invoke(slovo, bukva, borderCollision.bukva);
                }
            }
        }

        

        private void SetConnectedBorder(Collider2D collision)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            collision.gameObject.GetComponent<Renderer>().enabled = true;
        }




        //public bool touch = true;



        /*     

              private void OnTriggerEnter2D(Collider2D collision)
              {
                  DisableCollider(collision);

                  if (collision.CompareTag("Border") && touch && collision.isTrigger)
                  {               
                      collision.gameObject.GetComponent<BorderControl>().touch = false;
                      touch = false;

                      gameObject.GetComponent<Renderer>().enabled = true;
                      collision.gameObject.GetComponent<Renderer>().enabled = true;

                      GameObject moveBlockMain = transform.parent.parent.parent.gameObject;
                      GameObject moveBlock = collision.transform.parent.parent.parent.gameObject;

                      if (moveBlockMain != moveBlock)
                          GameEvents.CollapsBlock.Invoke(moveBlockMain, moveBlock);
                  }
              } 

              private void DisableCollider(Collider2D collision)
              {
                  if (collision.isTrigger && _collision.isTrigger)
                  {
                      gameObject.GetComponent<Collider2D>().isTrigger = false;
                  }
              }*/

    }
}
