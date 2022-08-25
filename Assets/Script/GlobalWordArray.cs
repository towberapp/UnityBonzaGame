using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class GlobalWordArray : MonoBehaviour
    {
        static public List<WordBasic> wordsGlobal = new ();

        private void Awake()
        {
            WordBasic wordBasic = new();
                wordBasic.word = "�����";
                wordBasic.horizontal = true;

            wordsGlobal.Add(wordBasic);

            WordBasic wordBasic1 = new();
                wordBasic1.word = "����";
                wordBasic1.horizontal = false;

            wordsGlobal.Add(wordBasic1);
        }
    }

    [System.Serializable]
    public class WordBasic
    {
        public string word;
        public bool horizontal;
        public bool isConnect = false;
    }
}
