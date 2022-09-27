using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Main
{
    public class CrossListMenu : MonoBehaviour
    {
        public GameObject check;
        public TMP_Text title;
        public TMP_Text num;

        private Packs pack;
        private Cross cross = new();

        public void ClickOnBtn()
        {            
            UIController.loadCrossEvent.Invoke(cross, pack);
        }

        public void SetCross(Cross cross, int count, Packs pack)
        {
            title.text = cross.glue;
            num.text = count.ToString();
            check.SetActive(cross.status);

            this.pack = pack;
            this.cross = cross;
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }

    }
}
