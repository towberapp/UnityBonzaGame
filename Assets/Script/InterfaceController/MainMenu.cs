using System;
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


        private void Awake()
        {
            UIController.loadPackEvent.AddListener(OnLoadPack);
        }

        private void OnLoadPack(string arg0)
        {
            gameObject.SetActive(false);
        }


        private void Start()
        {
            JsonFileControl jsonControl = new();
            List<Packs> listPack = jsonControl.GetListPack();

            foreach (Packs pack in listPack)
            {
                GameObject obj = Instantiate(menuBlockPrefab, Vector2.zero, Quaternion.identity, contentFolder.transform);
                MainMenuBlockController menuObj = obj.GetComponent<MainMenuBlockController>();

                menuObj.title.text = pack.name;
                menuObj.count.text = pack.countDone + "/" + pack.count;
                menuObj.icon.sprite = Resources.Load<Sprite>("Icons/" + pack.icon);
                menuObj.idPack = pack.id;
            }
        }


        private void OnDestroy()
        {
            UIController.loadPackEvent.RemoveListener(OnLoadPack);
        }
    }


}
