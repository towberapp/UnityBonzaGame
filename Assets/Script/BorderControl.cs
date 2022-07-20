using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main
{
    public class BorderControl : MovingControl
    {

        [SerializeField] 

        public bool touch = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Border") && touch)
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

    }
}
