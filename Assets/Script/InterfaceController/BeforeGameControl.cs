using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class BeforeGameControl : MonoBehaviour
    {
        private string packId;
        private Cross cross = new();

        [Header("Screen Element")]
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text title;

        public void OnClickBack()
        {
            UIController.loadPackEvent.Invoke(packId);
            gameObject.SetActive(false);
        }

        public void SetData(Cross cross, Packs pack)
        {
            this.packId = pack.id;
            this.cross = cross;

            icon.sprite = Resources.Load<Sprite>("Icons/" + pack.icon);
            title.text = pack.name;

            // check file and load cross

        }
    }
}
