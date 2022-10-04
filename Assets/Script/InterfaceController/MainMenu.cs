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


        private void Start()
        {
            JsonFileControl jsonControl = new();
            List<Packs> listPack = jsonControl.GetListPack();

            foreach (Packs pack in listPack)
            {
                GameObject obj = Instantiate(menuBlockPrefab, Vector2.zero, Quaternion.identity, contentFolder.transform);
                MainMenuBlockController menuObj = obj.GetComponent<MainMenuBlockController>();

                int packDone = jsonControl.GetPackDoneByIdPack(pack.id);

                menuObj.title.text = pack.name;
                menuObj.count.text = packDone + "/" + pack.count;
                menuObj.icon.sprite = Resources.Load<Sprite>("Icons/" + pack.icon);
                menuObj.idPack = pack.id;
            }
        }

    }

}
