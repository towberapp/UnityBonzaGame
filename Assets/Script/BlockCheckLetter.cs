using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class BlockCheckLetter : MonoBehaviour
    {
        [SerializeField] private GameObject connector;
        
        private BlockGlobalPublic globalPublic;

        private void Awake()
        {
            GameEvents.OnChechLetter.AddListener(OnCheck);
            globalPublic = GetComponent<BlockGlobalPublic>();
        }

        private void OnCheck(LetterArray leterCheck, int idGroup)
        {           
            LetterArray ownLetter = globalPublic.letterArray;            
            if (ownLetter.groupId == idGroup) return;

            // bad place :)
            /*if (ownLetter.xPos == leterCheck.xPos && ownLetter.yPos == leterCheck.yPos)
            {
               
            }*/

            foreach (var item in leterCheck.chain)
            {               
                if (leterCheck.xPos + item.xDelta == ownLetter.xPos && 
                    leterCheck.yPos + item.yDelta == ownLetter.yPos &&
                    item.idChainLetter == ownLetter.letterId && !item.isConnected)
                {
                    // connect all in group;                    
                    item.isConnected = true;
                    GameEvents.ChangeIdGroupEvent.Invoke(globalPublic.letterArray.groupId, leterCheck.groupId);

                    print("DELTA: " + item.xDelta + "," + item.yDelta);

                    int zRotation = item.yDelta != 0 ? 90 : 0;

                    Instantiate(
                        connector, 
                        new Vector2(transform.position.x - (item.xDelta / 2.0f), transform.position.y - (item.yDelta / 2.0f)),
                        Quaternion.Euler(new Vector3(0, 0, zRotation)), 
                        transform
                        );

                }                               
            }
        }

   
    }
}
