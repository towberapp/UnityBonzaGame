using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class CrossListMenu : MonoBehaviour
    {
        public GameObject check;
        public TMP_Text title;
        public TMP_Text num;

        private Packs pack;
        private Cross cross = new();

        private bool enableCross = false;

        private void CheckFile(string crossId)
        {
            JsonFileControl jsonControl = new();
            enableCross = jsonControl.CheckCrossIdEnable(crossId);

            if (enableCross)
                SetEnable();
            else
                SetDisable();
        }

        public void ClickOnBtn()
        {
            if (enableCross)
            {
                UIController.loadCrossEvent.Invoke(cross, pack);
            }
            else 
            {
                UIController.notificationEvent.Invoke("Ошибка загрузки кроссворда");
            }                
        }


        public void SetCross(Cross cross, int count, Packs pack)
        {
            CheckFile(cross.id);

            title.text = cross.glue;
            num.text = count.ToString();
            check.SetActive(cross.status);

            this.pack = pack;
            this.cross = cross;
        }

        private void SetEnable()
        {
            Image image = GetComponent<Image>();
            Color tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
        }

        private void SetDisable()
        {
            Image image = GetComponent<Image>();
            Color tempColor = image.color;
            tempColor.a = 0.5f;
            image.color = tempColor;
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }

    }
}
