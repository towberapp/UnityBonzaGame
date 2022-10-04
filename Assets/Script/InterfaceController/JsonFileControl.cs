using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Main
{
    [Serializable]
    public class CrossStatus
    {
       public Dictionary<string, bool> data = new();
    }

    public class PackStatus
    {
        public Dictionary<string, bool> data = new();
    }

    public class JsonFileControl 
    {
        private string menuPath = "Json/" + UIController.lang + "/menu";        
        private Dictionary<string, bool> statusCross = new();        

        string name = "crossData";


        public void LoadCrossData()
        {            
            if (PlayerPrefs.HasKey(name))
            {
                string crossData = PlayerPrefs.GetString(name);
                CrossStatus dicNew = JsonConvert.DeserializeObject<CrossStatus>(crossData);
                statusCross = dicNew.data;                
            }
        }

        public int GetPackDoneByIdPack(string id)
        {
            LoadCrossData();
            Packs pack = GetPack(id);

            int count = 0;
            foreach (var item in pack.cross)
            {
                if (GetCrossStatus(item.id)) count++;                
            }
            return count;
        }


        public bool GetCrossStatus(string crossId)
        {
            return statusCross.ContainsKey(crossId);
        }


        public void DeleteAllCrossStatus()
        {
            PlayerPrefs.DeleteKey(name);
        }


        public void SaveCrossStatus(string crossId)
        {
            LoadCrossData();
            if (GetCrossStatus(crossId)) return;

            statusCross[crossId] = true;
            CrossStatus 
                cs = new();
                cs.data = statusCross;

            string json = JsonConvert.SerializeObject(cs, Formatting.Indented);
            PlayerPrefs.SetString(name, json);
        }


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

        public void SetCrossIsDone()
        {

        }

        public Packs GetPack(string packId)
        {
            string packsPath = "Json/" + UIController.lang + "/Packs/" + packId;
            TextAsset jsonPacks = Resources.Load<TextAsset>(packsPath);
            return JsonUtility.FromJson<Packs>(jsonPacks.text);
        }


        public List<Packs> GetListPack()
        {
            MainMenuObject menuObject = GetMenu();
            List<Packs> listPack = new();

            foreach (PacksMenu pack in menuObject.packs)
            {
                Packs packs = GetPack(pack.id);                
                if (packs != null)
                {                    
                    listPack.Add(packs);
                }
            }
            return listPack;
        }

 

    }
}
