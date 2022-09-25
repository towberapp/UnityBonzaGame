using TMPro;
using UnityEngine;

namespace Main
{
    public class LoadSaveManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField glue;
        [SerializeField] private TMP_InputField nameFile;
        [SerializeField] private TMP_Text infoBox;
        [SerializeField] private GameObject saveMenu;        

        GenerateObjectSave generateObjectSave = new();

        private void Awake()
        {
            saveMenu.SetActive(false);
        }

        public void OpenSaveMenu()
        {
            saveMenu.SetActive(true);
            glue.text = "";
            nameFile.text = "";
            infoBox.text = "";
        }

        public void closeMenu()
        {
            saveMenu.SetActive(false);
        }

        public void SaveJson()
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

            bool save = generateObjectSave.GetGlobalObject(glue.text, nameFile.text);
            if (!save)
            {
                infoBox.text = "Don't Save";
                return;
            }

            saveMenu.SetActive(false);            
        }

         

    }
}
