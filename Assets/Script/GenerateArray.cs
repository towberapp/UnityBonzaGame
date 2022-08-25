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
            foreach (var item in letterArray)
            {
                GameObject letter = Instantiate(letterPrefab, new Vector2(0,0), Quaternion.identity, letterFolder);
                BlockGlobalPublic 
                    blockGlobal = letter.GetComponent<BlockGlobalPublic>();
                    blockGlobal.letterArray = item;
                    blockGlobal.InitBlock();

                GameEvents.SetObjToArraytEvent.Invoke(letter, item);

                //GameEvents.OnEndDrag.Invoke(item.groupId);
            }
        }
    }








    [System.Serializable]
    public class GlobalArray
    {        
        public int cointId;
        public List<LetterArray> letterArray = new();
    }


    [System.Serializable]
    public class LetterArray
    {
        public int xPos;
        public int yPos;
        public int groupId;
        public int letterId;
        public string stringLetter;
        public List<ChainData> chain = new();
    }


    [System.Serializable]
    public class ChainData
    {
        public int xDelta;
        public int yDelta;
        public int idChainLetter;
        public bool isConnected = false;
    }



}
