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
            GenerateBlock(gArray.letterArray);    
        }

        private void GenerateBlock(List<LetterArray> letterArray)
        {
            ListLetterControl listLetterControl = new ();

            foreach (var item in letterArray)
            {
                GameObject obj = Instantiate(letterPrefab, new Vector2(0,0), Quaternion.identity, letterFolder);
                BlockGlobalPublic 
                    blockGlobal = obj.GetComponent<BlockGlobalPublic>();
                    blockGlobal.letterArray = item;
                    
                blockGlobal.InitBlock();
                
                LetterData letterData = new();
                    letterData.letter = item;
                    letterData.obj = obj;


                listLetterControl.AddLetterToList(letterData);
                GameEvents.SetDataToArraytEvent.Invoke(letterData);

                //GameEvents.OnEndDrag.Invoke(item.groupId);
            }


        }
    }




}
