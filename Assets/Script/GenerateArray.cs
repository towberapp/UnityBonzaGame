using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Main
{
    public class GenerateArray : MonoBehaviour
    {
        [SerializeField] private string loadFileName;
        [SerializeField] private Transform letterFolder;
        [SerializeField] private GameObject letterPrefab;       

        private void Start()
        {
            JsonController jsonController = new JsonController();
            
            List<LetterArray> letterArray = jsonController.Init(loadFileName);

            GameEvents.LoadConfigDoneEvent.Invoke();            

            foreach (var item in letterArray)
            {

                GameObject obj = Instantiate(letterPrefab, new Vector2(0, 0), Quaternion.identity, letterFolder);
                BlockGlobalPublic 
                    blockGlobal = obj.GetComponent<BlockGlobalPublic>();
                    blockGlobal.letterArray = item;
                    blockGlobal.InitBlock();

                LetterData letterData = new()
                {
                    letter = item,
                    obj = obj
                };

                GlobalStatic.arrayLetter[letterData.letter.xPos, letterData.letter.yPos] = letterData;
                GlobalStatic.listLetterData.Add(letterData);
            }

            // connect 
            GlobalArrayControl globalArrayControl = new();
                globalArrayControl.SetConnector();

        }


    }
}
