using UnityEngine;
using TMPro;
using System;

namespace Main
{
    public class GenerateGameObjectLetter : MonoBehaviour
    {
        public Transform letterFolder;
        public GameObject letterPrefab;
        [SerializeField] private TMP_InputField wordInput;
        [SerializeField] private TMP_Text countWordText;

        GeneratorGroupControl generatorGroupControl = new();
        
        private int countWord = 0;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                RemoveGroup();
            }                    
        }

        public void AddWord()
        {
            if (wordInput.text.Length == 0) return;

            string word = wordInput.text.ToUpper();
            generatorGroupControl.GenerateWord(word, letterFolder, letterPrefab, new Vector2Int(0, countWord));
            wordInput.text = "";
            ChangeCountWord(1);
        }

        public void RotateGroup()
        {
            generatorGroupControl.RotateGroupById();
        }

        public void RemoveGroup()
        {
           bool remove = generatorGroupControl.RemoveGroup(GeneratorGroupControl.idGroup);

            if (remove) ChangeCountWord(- 1);
        }

        public void ClearAll()
        {
            generatorGroupControl.ClearAll();
            countWord = 0;
            ChangeCountWord(0);
        }

        public void ChangeCountWord(int change)
        {
            countWord += change;
            countWordText.text = countWord.ToString();
        }

    }
}
