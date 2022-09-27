using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Main
{
    public class UIController : MonoBehaviour
    {
        [Header("UI Panels")]
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject selectPack;
        [SerializeField] private GameObject beforeGame;

        [Header("DevOp")]
        [SerializeField] private bool clearPref = false;

        public static UnityEvent<string> loadPackEvent = new();
        public static UnityEvent<Cross, Packs> loadCrossEvent = new();
        public static UnityEvent loadMainMenu = new();

        public static string lang = "RU";

        private void Awake()
        {
            if (clearPref) PlayerPrefs.DeleteAll();

            //GameEvents.LoadConfigDoneEvent.AddListener(OnLoad);
            loadPackEvent.AddListener(OnSelectPack);
            loadMainMenu.AddListener(OnLoadMainMenu);
            loadCrossEvent.AddListener(OnBeforeLoadCrossEvent);
            
            selectPack.SetActive(false);
            beforeGame.SetActive(false);
        }
        
        private void OnBeforeLoadCrossEvent(Cross cross, Packs pack)
        {
            beforeGame.SetActive(true);
            beforeGame.GetComponent<BeforeGameControl>().SetData(cross, pack);
            selectPack.SetActive(false);
        }

        private void OnLoadMainMenu()
        {
            mainMenu.SetActive(true);
        }

        private void OnSelectPack(string packId)
        {
            selectPack.SetActive(true);
            selectPack.GetComponent<SelectPackControl>().LoadCrossList(packId);            
        }

        private void Start()
        {
            OnLoadMainMenu();
        }


        private void OnDestroy()
        {
            loadPackEvent.RemoveListener(OnSelectPack);
        }
    }


    [Serializable]
    public class SelectCross
    {
        public string idPack;
        public string idCross;
    }


    [Serializable]
    public class MainMenuObject
    {
        public List<PacksMenu> packs;
    }

    [Serializable]
    public class PacksMenu
    {
        public string id;
        public int sort;
    }

    [Serializable]
    public class Packs
    {
        public string id;        
        public int sort;
        public int ver;
        public string name;
        public string icon;
        public int count;
        public int countDone = 0;
        public List<Cross> cross = new();
    }

    [Serializable]
    public class Cross
    {
        public string id;        
        public int sort;        
        public bool status = false; // complete
        public int ver;
        public string glue;
        public int length;
    }
}
