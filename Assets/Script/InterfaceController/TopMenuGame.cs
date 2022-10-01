using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Main
{
    public class TopMenuGame : MonoBehaviour
    {
        [SerializeField] private TMP_Text tema;
        [SerializeField] private TMP_Text glue;

        private string temaTitle = "релю:";        

        [SerializeField] private UnityEvent openGameMenu;
        [SerializeField] private UnityEvent<Cross, Packs> restartEvent;

        private Cross cross;
        private Packs pack;

        public void SetData(Cross cross, Packs pack)
        {
            this.cross = cross;
            this.pack = pack;

            glue.text = cross.glue;
            tema.text = temaTitle;
        }

        public void BackBtn()
        {
            openGameMenu.Invoke();
        }

        public void BackInPackList()
        {
            GameEvents.clearGame.Invoke();
            UIController.loadPackEvent.Invoke(pack.id);
        }

        public void RestartGame()
        {
            GameEvents.clearGame.Invoke();
            restartEvent.Invoke(cross, pack);
        }
    }
}
