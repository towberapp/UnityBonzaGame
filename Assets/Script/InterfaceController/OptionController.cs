using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class OptionController : MonoBehaviour
    {
        
        public void onTelegram()
        {
            Application.OpenURL("tg://resolve?domain=bonzaygame");
        }
    }
}
