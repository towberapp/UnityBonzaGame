using UnityEngine;
using TMPro;
using System;

namespace Main
{
    public class GenerateGameObjectLetter : MonoBehaviour
    {
        [SerializeField] private Transform letterFolder;
        [SerializeField] private GameObject letterPrefab;

        [SerializeField] private TMP_InputField wordInput;

        GeneratorGroupControl generatorGroupControl = new();

        public void AddWord()
        {
            if (wordInput.text.Length == 0) return;

            string word = wordInput.text.ToUpper();
            generatorGroupControl.GenerateWord(word, letterFolder, letterPrefab);
            wordInput.text = "";
        }

        public void RotateGroup()
        {
            generatorGroupControl.RotateGroupById();
        }

        public void RemoveGroup()
        {
            generatorGroupControl.RemoveGroup();
        }

    }
}
