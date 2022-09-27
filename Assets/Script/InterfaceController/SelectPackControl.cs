using System;
using TMPro;
using UnityEngine;
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


        public void OnClickBack()
        {
            UIController.loadMainMenu.Invoke();
            gameObject.SetActive(false);
        }

        public void LoadCrossList(string packId)
        {
            string loadJson = PlayerPrefs.GetString("pack_" + packId);                       
            Packs loadPacks = JsonUtility.FromJson<Packs>(loadJson);

            icon.sprite = Resources.Load<Sprite>("Icons/" + loadPacks.icon);
            title.text = loadPacks.name;

            int count = 1;
            foreach (Cross cross in loadPacks.cross)
            {
                GameObject obj = Instantiate(crossBlockPrefab, Vector2.zero, Quaternion.identity, contentFolder.transform);
                CrossListMenu menuObj = obj.GetComponent<CrossListMenu>();
                menuObj.SetCross(cross, count, loadPacks);
                count++;
            }
        }

    }
}
