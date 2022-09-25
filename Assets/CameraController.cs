using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main
{
    public class CameraController : MonoBehaviour
    {

        //private Vector2 initialDistance;
        //private Vector2 initialScale;

        private float distStart;
        private float distDelta;
        private float sizeCamera;

        private float minSize = 7f;
        private float maxSize = 25;

        private void Awake()
        {
            GameEvents.LoadConfigDoneEvent.AddListener(OnInit);
        }

        private void Update()
        {
            if (Input.touchCount == 1)
            {

            }


            if (Input.touchCount == 2)
            {
                GlobalStatic.canMoveBlock = false;
                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);

                // if one of the touches Ended or Canceled do nothing
                if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled
                   || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
                {
                    GlobalStatic.canMoveBlock = true;
                }

                if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    Vector2 oneToch = new Vector2(touchZero.position.x, touchZero.position.y);
                    Vector2 twoToch = new Vector2(touchOne.position.x, touchOne.position.y);

                    distStart = (oneToch - twoToch).magnitude;
                    sizeCamera = Camera.main.orthographicSize;
                }

                    // It is enough to check whether one of them began since we
                    // already excluded the Ended and Canceled phase in the line before
                if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
                { 
                    // track the initial values
                    Vector2 oneToch = new Vector2(touchZero.position.x, touchZero.position.y);
                    Vector2 twoToch = new Vector2(touchOne.position.x, touchOne.position.y);

                    float size = Camera.main.orthographicSize;

                    /*Debug.Log("sizeCamera: " + sizeCamera);
                    Debug.Log("distDelta: " + distDelta);*/

                    distDelta = ((oneToch - twoToch).magnitude - distStart)/40;

                    float delta = sizeCamera - distDelta;

                    float cameraSize = delta;

                    if (delta > maxSize)
                    {
                        cameraSize = maxSize;
                    }

                    if (delta < minSize)
                    {
                        cameraSize = minSize;
                    }

                    Camera.main.orthographicSize = cameraSize;

                    /*  if (size < minSize)
                      {
                          distDelta = maxSize + size;
                      }

                      if (size > maxSize)
                      {
                          distDelta = maxSize - size;
                      }
  */

                    //Camera.main.orthographicSize = sizeCamera - distDelta;

                }
            }
         }


        private void OnInit()
        {
            transform.position = new Vector3( GlobalStatic.xPole / 2, GlobalStatic.yPole / 2, -15 );
            Camera.main.orthographicSize = GlobalStatic.xPole/2 + 5;
        }

  
    }
}
