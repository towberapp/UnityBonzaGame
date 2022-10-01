using TMPro;
using UnityEngine;

namespace Main
{
    public class NotificationController : MonoBehaviour
    {
        [SerializeField] private TMP_Text notif;

        public void SetText(string text)
        {
            notif.text = text;  
        }
    }
}
