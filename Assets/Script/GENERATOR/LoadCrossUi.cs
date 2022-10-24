using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class LoadCrossUi : MonoBehaviour
    {
        public TextAsset jsonTextFile;
        public string glue;
        public string lang;

        [SerializeField] private GenerateGameObjectLetter generateGameObjectLetter;

        private GeneratorGroupControl generatorGroupControl = new();

        private bool load = false;
        
        public void LoadCross()
        {
            if (jsonTextFile == null) return;
            if (load) return;

            load = true;

            GlobalArrayNew globalArray = JsonUtility.FromJson<GlobalArrayNew>(jsonTextFile.text);
            List<WordBasicSave> wordGlobal = globalArray.wordBasicSaves;
            glue = globalArray.glue;
            lang = globalArray.lang;

            foreach (WordBasicSave item in wordGlobal)
            {
                int id = generatorGroupControl.GenerateWord(
                    item.word,
                    generateGameObjectLetter.letterFolder,
                    generateGameObjectLetter.letterPrefab,
                    new Vector2Int(item.xStart, item.yStart)
                    );

                if (!item.horizontal)
                {
                    GeneratorGroupControl.idGroup = id;
                    generatorGroupControl.RotateGroupById();
                }

                generateGameObjectLetter.ChangeCountWord(1);
            }

            StartCoroutine(UnlockLoad());
        }

        IEnumerator UnlockLoad()
        {
            yield return new WaitForSeconds(2.0f);
            load = false;
        }

    }
}
