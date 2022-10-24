using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Main
{
    public class LolacizationGameObjectControl : MonoBehaviour
    {
        [SerializeField] private Locale[] localeDisable;
        [SerializeField] private GameObject disableObject;


        private void Awake()
        {
            LocalizationSettings.SelectedLocaleChanged += (newLocale) => {
                Debug.Log("locale changed: " + newLocale);
                CheckLocale();
            };

            CheckLocale();
        }


        public void CheckLocale()
        {
            bool needsActive = true;
            
            foreach (Locale item in localeDisable)
            {
                if (item == LocalizationSettings.SelectedLocale)
                {
                    disableObject.SetActive(false);
                    needsActive = false;
                }
            }

            if (!disableObject.activeSelf && needsActive)
            {
                disableObject.SetActive(true);
            }
        }
        
    }
}