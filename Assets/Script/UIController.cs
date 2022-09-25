using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Main
{
    public class UIController : MonoBehaviour
    {
        [Header("UI Panels")]
        [SerializeField] private GameObject mainMenu;


        [SerializeField] private TMP_Text clue;

        private void Awake()
        {
            GameEvents.LoadConfigDoneEvent.AddListener(OnLoad);
        }

        private void Start()
        {
            mainMenu.SetActive(true);
        }


        private void OnLoad()
        {
            clue.text = GlobalStatic.config.glue;
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
        public bool status;
    }
}
