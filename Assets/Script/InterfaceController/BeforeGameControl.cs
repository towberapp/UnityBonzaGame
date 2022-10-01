using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Main
{
    public class BeforeGameControl : MonoBehaviour
    {
        private string packId;
        private Packs pack;
        private Cross cross = new();
        private int levelDiff = 0;

        [Header("Difficul")]
        [SerializeField] private Image[] btnImage;
        [SerializeField] private Sprite[] spriteBrn;

        [Header("Screen Element")]
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text title;

        [Header("Glue")]
        [SerializeField] private TMP_Text glue;

        [Header("GenerateArray")]
        [SerializeField] private GenerateArray generateArray;

        public UnityEvent<Cross, Packs> startGameEvent;        


        private void Start()
        {
            ChangeLevel(1);
        }

        public void OnClickBack()
        {
            UIController.loadPackEvent.Invoke(packId);
            gameObject.SetActive(false);
        }

        public void SetData(Cross cross, Packs pack)
        {
            this.packId = pack.id;
            this.cross = cross;
            this.pack = pack;

            icon.sprite = Resources.Load<Sprite>("Icons/" + pack.icon);
            title.text = pack.name;

            glue.text = cross.glue;            
        }

        public void ChangeLevel(int level)
        {
            foreach (var item in btnImage)
            {
                item.sprite = spriteBrn[0];
            }
            btnImage[level].sprite = spriteBrn[1];
            levelDiff = level;
        }

        public void BeginGame()
        {
            generateArray.StartGame(cross.id, levelDiff);            
            startGameEvent.Invoke(cross, pack);            
        }

    }
}
