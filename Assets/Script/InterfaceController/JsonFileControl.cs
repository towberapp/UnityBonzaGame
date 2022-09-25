using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class JsonFileControl 
    {
        string menuPath = "Json/RU/menu";

        public MainMenuObject GetMenu()
        {
            TextAsset jsonTextFile = Resources.Load<TextAsset>(menuPath);
            return JsonUtility.FromJson<MainMenuObject>(jsonTextFile.text);
        }



        public List<Packs> GetListPack()
        {
            MainMenuObject menuObject = GetMenu();

            List<Packs> listPack = new();

            foreach (PacksMenu pack in menuObject.packs)
            {
                string packName = "pack_" + pack.id;

                if (!PlayerPrefs.HasKey(packName))
                {
                    //check file
                    string packsPath = "Json/RU/Packs/" + pack.id;
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

            return listPack;
        }

    }
}
