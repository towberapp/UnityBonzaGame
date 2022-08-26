using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class GenerateArray : MonoBehaviour
    {
        [SerializeField] private Transform letterFolder;
        [SerializeField] private GameObject letterPrefab;
                        

        private void Start()
        {
            TextAsset jsonTextFile = Resources.Load<TextAsset>("Json/ArrayVer3");
            GlobalArray gArray = JsonUtility.FromJson<GlobalArray>(jsonTextFile.text);

            LoadConfig();

            GenerateBlock(gArray.letterArray);    
        }

        private void LoadConfig()
        {
            GlobalStatic.xPole = 8;
            GlobalStatic.yPole = 15;

            GlobalStatic.arrayLetter = new LetterData[GlobalStatic.xPole, GlobalStatic.yPole];

            GameEvents.LoadConfigDoneEvent.Invoke();
        }


        private void GenerateBlock(List<LetterArray> letterArray)
        {
            ListLetterControl listLetterControl = new ();
            GlobalArrayControl globalArrayControl = new ();

            foreach (var item in letterArray)
            {
                GameObject obj = Instantiate(letterPrefab, new Vector2(0,0), Quaternion.identity, letterFolder);
                BlockGlobalPublic 
                    blockGlobal = obj.GetComponent<BlockGlobalPublic>();
                    blockGlobal.letterArray = item;
                    
                blockGlobal.InitBlock();

                LetterData letterData = new()
                {
                    letter = item,
                    obj = obj
                };

                listLetterControl.AddLetterToList(letterData);
                globalArrayControl.SetDataToArray(letterData);                

                //GameEvents.OnEndDrag.Invoke(item.groupId);
            }


        }
    }




}
