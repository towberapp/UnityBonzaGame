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
        [SerializeField] private GameObject selectPack;
        [SerializeField] private GameObject beforeGame;
        [SerializeField] private GameObject notificationPanel;

        [Header("Script")]
        [SerializeField] private TopMenuGame topmenu;


        [Header("DevOp")]
        [SerializeField] private bool clearPref = false;        

        [Header("Events")]
        public static UnityEvent<string> loadPackEvent = new();
        public static UnityEvent<Cross, Packs> loadCrossEvent = new();             
        public static UnityEvent<string> notificationEvent = new();
        public static UnityEvent inGameMenuEvent = new();
        public static string lang = "RU";        

        [SerializeField] private UnityEvent loadGame;        
        [SerializeField] private UnityEvent<string> selectPackEventLocal;
        [SerializeField] private UnityEvent winEvent;

        JsonFileControl jsonControl = new();


        private void Awake()
        {    
            if (clearPref) PlayerPrefs.DeleteAll();                 
            loadPackEvent.AddListener(OnSelectPack);
            loadGame.Invoke();
            loadCrossEvent.AddListener(OnBeforeLoadCrossEvent);            
            notificationEvent.AddListener(OnNotification);
            GameEvents.winGameEvent.AddListener(OnWin);            
        }

        public void OnNotification(string notif)
        {
            StartCoroutine(Notif(notif));
            IEnumerator Notif(string notif)
            {
                notificationPanel.SetActive(true);
                notificationPanel.GetComponent<NotificationController>().SetText(notif);
                yield return new WaitForSeconds(2.0f);
                notificationPanel.SetActive(false);
            }  
        }

        public void OnExit()
        {
            Application.Quit();
        }

        private void OnWin()
        {
            string crossId = topmenu.cross.id;            
            winEvent.Invoke();
            jsonControl.SaveCrossStatus(crossId);
        }

        private void OnBeforeLoadCrossEvent(Cross cross, Packs pack)
        {
            beforeGame.SetActive(true);
            beforeGame.GetComponent<BeforeGameControl>().SetData(cross, pack);
            selectPack.SetActive(false);
        }


        private void OnSelectPack(string packId)
        {            
            selectPackEventLocal.Invoke(packId);         
        }



        private void OnDestroy()
        {
            loadPackEvent.RemoveListener(OnSelectPack);            
            loadCrossEvent.RemoveListener(OnBeforeLoadCrossEvent);            
            notificationEvent.RemoveAllListeners();
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
