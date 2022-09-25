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

        public static UnityEvent<string> loadPackEvent = new();

        //[SerializeField] private TMP_Text clue;

        private void Awake()
        {
            GameEvents.LoadConfigDoneEvent.AddListener(OnLoad);
            loadPackEvent.AddListener(OnSelectPack);
        }

        private void OnSelectPack(string packId)
        {
            selectPack.SetActive(true);
            Debug.Log("SELECT PACK: " + packId);
        }

        private void Start()
        {
            mainMenu.SetActive(true);
        }


        private void OnLoad()
        {
            //clue.text = GlobalStatic.config.glue;
        }

        private void OnDestroy()
        {
            loadPackEvent.RemoveListener(OnSelectPack);
        }
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
        public bool status = false;
        public int ver;
        public string name;
    }
}
