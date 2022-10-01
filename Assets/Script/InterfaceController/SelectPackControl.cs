using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Main
{
    public class SelectPackControl : MonoBehaviour
    {
        [Header("Folders")]
        [SerializeField] private GameObject contentFolder;

        [Header("Objects")]
        [SerializeField] private GameObject crossBlockPrefab;

        [Header("Screen Element")]
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text title;

        [SerializeField] private UnityEvent onBackSelectPack;

        public void OnClickBack()
        {
            onBackSelectPack.Invoke();           
        }

        public void LoadCrossList(string packId)
        {
            JsonFileControl jsonControl = new();
            Packs loadPacks = jsonControl.GetPack(packId);

            jsonControl.LoadCrossData();


            icon.sprite = Resources.Load<Sprite>("Icons/" + loadPacks.icon);
            title.text = loadPacks.name;

            int count = 1;
            foreach (Cross cross in loadPacks.cross)
            {
                cross.status = jsonControl.GetCrossStatus(cross.id);

                GameObject obj = Instantiate(crossBlockPrefab, Vector2.zero, Quaternion.identity, contentFolder.transform);
                CrossListMenu menuObj = obj.GetComponent<CrossListMenu>();
                menuObj.SetCross(cross, count, loadPacks);
                count++;
            }
        }

    }
}
