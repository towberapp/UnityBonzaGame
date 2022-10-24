using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class LoadSaveManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField glue;
        [SerializeField] private TMP_InputField nameFile;
        [SerializeField] private TMP_Dropdown langDrop;
        [SerializeField] private TMP_Text infoBox;
        [SerializeField] private GameObject saveMenu;
        [SerializeField] private LoadCrossUi loadCrossUi;
        [SerializeField] private GameObject saveCurrent;

        GenerateObjectSave generateObjectSave = new();        

        private void Awake()
        {
            saveMenu.SetActive(false);
            saveCurrent.SetActive(false);
        }

        public void OpenSaveMenu()
        {
            saveMenu.SetActive(true);           

            glue.text = "";
            nameFile.text = "";
            infoBox.text = "";

            if (loadCrossUi.jsonTextFile != null)
            {
                saveCurrent.SetActive(true);

                glue.text = loadCrossUi.glue;
                nameFile.text = loadCrossUi.jsonTextFile.name;

                string lang = loadCrossUi.lang;    
                int count = 0;
                foreach (var item in langDrop.options)
                {
                    if (item.text == lang)
                    {
                        langDrop.value = count;
                    }
                    count++;
                }                
            }

        }

        public void closeMenu()
        {
            saveMenu.SetActive(false);
        }

        public void SaveJson(bool overwrite)
        {
            if (glue.text == "" || nameFile.text == "") 
            {
                infoBox.text = "Empty Input";
                return;
            };

            if (glue.text.Length <= 3 || nameFile.text.Length <= 5)
            {
                infoBox.text = "Short Input";
                return;
            }
            
            string lang = langDrop.options[langDrop.value].text;
            bool check = CheckCrossIdEnable(nameFile.text, lang);


            if (!overwrite && check)
            {
                infoBox.text = "File is EXIST!";
                return;                
            }

            if (overwrite && !check)
            {
                infoBox.text = "File NOT FOUND!!!";
                return;
            }

            if (CountWord() < 2)
            {
                infoBox.text = "Must be moore word!";
                return;
            }
            
            bool save = generateObjectSave.GetGlobalObject(glue.text, nameFile.text, lang, overwrite);

            if (!save)
            {
                infoBox.text = "Don't Save";
                return;
            }

            saveMenu.SetActive(false);            
        }

        private int CountWord()
        {
            int count = 0;
            foreach (var item in GeneratorGroupControl.wordBasicsArray)
            {
                if (item.isActive) count++;
            }
            return count;
        }

        private bool CheckCrossIdEnable(string crossId, string lang)
        {
            string crossPath = "Json/" + lang + "/Cross/" + crossId;
            TextAsset jsonTextFile = Resources.Load<TextAsset>(crossPath);

            if (jsonTextFile == null)
                return false;
            else
                return true;
        }

    }
}
