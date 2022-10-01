using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace Main
{
    public class MainMenuBlockController : MonoBehaviour
    {
        public Image icon;
        public TMP_Text title;
        public TMP_Text count;

        public string idPack;

        public void ClickOnBtn()
        {
            UIController.loadPackEvent.Invoke(idPack);   
        } 

    }
    
}
