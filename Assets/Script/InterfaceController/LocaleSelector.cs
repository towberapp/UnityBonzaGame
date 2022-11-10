using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Main
{
    public class LocaleSelector : MonoBehaviour
    {
        private bool active = false;


        [SerializeField] private Locale startLocale;

        private void Awake()
        {
            StartCoroutine(SetLocale(startLocale));
        }

        public void ChangeLocale(Locale LocalId)
        {
            if (LocalizationSettings.SelectedLocale == LocalId) return;            
            if (active) return;
            StartCoroutine(SetLocale(LocalId)); 
        }

        IEnumerator SetLocale(Locale _localId)
        {
            active = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = _localId;
            active = false;
        }
    }
}
