using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Main
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Folders")]
        [SerializeField] private GameObject contentFolder;

        [Header("Objects")]
        [SerializeField] private GameObject menuBlockPrefab;
        
        void Start()
        {
            string menuPath = "Json/RU/menu";            
            TextAsset jsonTextFile = Resources.Load<TextAsset>(menuPath);
            MainMenuObject menuObject = JsonUtility.FromJson<MainMenuObject>(jsonTextFile.text);

            /*
            if (!PlayerPrefs.HasKey("menu"))
            {
                TextAsset jsonTextFile = Resources.Load<TextAsset>(menuPath);
                menuObject = JsonUtility.FromJson<MainMenuObject>(jsonTextFile.text);
                string menuString = JsonUtility.ToJson(menuObject);
                PlayerPrefs.SetString("menu", menuString);
            }*/

            //string loadMenu = PlayerPrefs.GetString("menu");
            //menuObject = JsonUtility.FromJson<MainMenuObject>(loadMenu);



            List<Packs> listPack = new();

            // check playerPref
            foreach (PacksMenu pack in menuObject.packs)
            {
                string packName = "pack_" + pack.id;
                
                if (PlayerPrefs.HasKey(packName))
                {
                    //check file
                    string packsPath = "Json/RU/Packs/"+pack.id;
                    TextAsset jsonPacks = Resources.Load<TextAsset>(packsPath);

                    if (jsonPacks != null)
                    {
                        Packs packs = JsonUtility.FromJson<Packs>(jsonPacks.text);
                        string json = JsonUtility.ToJson(packs);
                        PlayerPrefs.SetString(packName, json);
                    }                    
                } 
                else 
                {
                    // check ver and replace
                }
                
                string loadJson = PlayerPrefs.GetString(packName);
                Packs loadPacks = JsonUtility.FromJson<Packs>(loadJson);
                listPack.Add(loadPacks);
            }

            GenerateMenu(listPack);

        }


        private void GenerateMenu(List<Packs> listPack)
        {
            foreach (Packs pack in listPack)
            {
                GameObject obj = Instantiate(menuBlockPrefab, Vector2.zero, Quaternion.identity, contentFolder.transform);
                MainMenuBlockController menuObj = obj.GetComponent<MainMenuBlockController>();

                //string txt = Encoding.UTF7.GetString(pack.name.bytes);

                menuObj.title.text = pack.name;
                menuObj.count.text = pack.countDone + "/" + pack.count;
                menuObj.icon.sprite = Resources.Load<Sprite>("Icons/"+ pack.icon);
            }
        }



        private void OnDestroy()
        {
            
        }
    }


}
