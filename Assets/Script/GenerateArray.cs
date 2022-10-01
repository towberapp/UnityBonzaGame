using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Main
{
    public class GenerateArray : MonoBehaviour
    {        
        [SerializeField] private Transform letterFolder;
        [SerializeField] private GameObject letterPrefab;

        private void Awake()
        {
            GameEvents.clearGame.AddListener(onClearLetter);
        }

        private void onClearLetter()
        {
            foreach (Transform child in letterFolder)
            {
                Destroy(child.gameObject);
            }

            GlobalStatic.listLetterData = new();
            GlobalStatic.arrayLetter = null;
            GlobalStatic.originalArrayLetter = null;
            GlobalStatic.wordsGlobal = new();
    }

        public void StartGame(string crossId, int diff)
        {
            //Debug.Log("START: " + crossId + ", diff: " + diff);

            string crossPath = "Json/" + UIController.lang + "/Cross/" + crossId;
            LoadGame(crossPath, diff);
        }

        private void LoadGame(string loadFileName, int diff)
        {
            JsonController jsonController = new JsonController();            
            List<LetterArray> letterArray = jsonController.Init(loadFileName, diff);
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
