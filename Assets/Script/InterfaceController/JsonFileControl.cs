using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class JsonFileControl 
    {
        string menuPath = "Json/" + UIController.lang + "/menu";

        public MainMenuObject GetMenu()
        {
            TextAsset jsonTextFile = Resources.Load<TextAsset>(menuPath);
            return JsonUtility.FromJson<MainMenuObject>(jsonTextFile.text);
        }

        public bool CheckCrossIdEnable(string crossId)
        {
            string crossPath = "Json/" + UIController.lang + "/Cross/" + crossId;
            TextAsset jsonTextFile = Resources.Load<TextAsset>(crossPath);

            if (jsonTextFile == null) 
                return false;
            else 
                return true;
        }


        public List<Packs> GetListPack()
        {
            MainMenuObject menuObject = GetMenu();

            List<Packs> listPack = new();

            foreach (PacksMenu pack in menuObject.packs)
            {
                string packName = "pack_" + pack.id;

                // check version

                if (!PlayerPrefs.HasKey(packName))
                {
                    //check file
                    string packsPath = "Json/"+ UIController.lang + "/Packs/" + pack.id;
                    TextAsset jsonPacks = Resources.Load<TextAsset>(packsPath);

                    if (jsonPacks != null)
                    {
                        Packs packs = JsonUtility.FromJson<Packs>(jsonPacks.text);
                        //UpdateCrossData(packs);
                        // check personal file
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


       /* private void UpdateCrossData(Packs packs)
        {
            foreach (Cross cross in packs.cross)
            {
                //checkfile
                string crossPath = "Json/" + UIController.lang + "/Cross/" + cross.id;
                TextAsset jsonCross = Resources.Load<TextAsset>(crossPath);
                //GlobalArrayNew dataCross = JsonUtility.FromJson<GlobalArrayNew>(jsonCross.text);

                if (dataCross != null)
                {
                    cross.name = dataCross
                }
            }
        }*/

    }
}
