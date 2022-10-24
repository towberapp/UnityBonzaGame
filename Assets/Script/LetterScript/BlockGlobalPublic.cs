using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Main
{
    public class BlockGlobalPublic : MonoBehaviour
    {
        public LetterArray letterArray;                
        public UnityEvent onInit = new();

        [SerializeField] private TMP_Text textComponent;
        [SerializeField] private GameObject textObject;
        [SerializeField] private GameObject backTeny;

        [SerializeField] private GameObject fon;
        [SerializeField] private GameObject connector;

        [SerializeField] private GameObject FlashFx;
        [SerializeField] private GameObject FlashFxError;
        [SerializeField] private GameObject winFx;

        private void Awake()
        {
            GameEvents.winGameEvent.AddListener(OnWin);
            GeneratorEvents.SelectGroupEvent.AddListener(OnSelectWord);
        }

        private void OnSelectWord(int groupId)
        {
            if (letterArray.groupId == groupId)
            {
                textComponent.color = Color.green;
            } else
            {
                textComponent.color = Color.white;
            }
        }

        private void OnWin()
        {
            Invoke(nameof(FxShow), Random.Range(0.2f, 3f));      
        }


        private void FxShow()
        {
            Instantiate(winFx, transform.position, Quaternion.Euler(-90, 0, 0), transform);
            textComponent.color = Color.green;
        }

        public void InitBlock()
        {
            onInit.Invoke();
            OnInitBlock();
        }

        public void MoveOn()
        {
            backTeny.SetActive(true);
        }

        public void MoveOff()
        {
            backTeny.SetActive(false);
        }

        public void SetBorder(bool type)
        {
            //type true - horizontal
            int zRotation = type ? 0 : 90;
            float xDvig = type ? 0.5f : 0;
            float yDvig = type ? 0f : -0.5f;

            Instantiate(
                connector, new Vector2(transform.position.x + xDvig, transform.position.y + yDvig),
                Quaternion.Euler(new Vector3(0, 0, zRotation)), transform);
        }

        public void RemoveBorder()
        {
            Transform[] childs = GetComponentsInChildren<Transform>();
            foreach (Transform child in childs)
            {
                if (child.CompareTag("Border"))
                    Destroy(child.gameObject);
            }
        }

        public void SetPosition(Vector2Int pos)
        {
            transform.position = (Vector2)pos;
        }

        public void FlashLetter()
        {
            Vector3 fxVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);      
            Instantiate(FlashFx, fxVector, Quaternion.Euler(-90, 0, 0), transform);

            //textComponent.color = new Color(0.6f,0.83f,0.88f, 1f);
            textComponent.color = new Color(1f, 0.75f, 0.32f, 1f);
        }

        public void BlinkLetter()
        {
            Instantiate(FlashFxError, transform.position, Quaternion.identity, transform);
            StartCoroutine(ChangeColorText());
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }


        // PRIVATE



        private void OnInitBlock()
        {
            this.textComponent.text = this.letterArray.stringLetter;
            transform.position = new Vector2(this.letterArray.xPos, this.letterArray.yPos);
        }

        private IEnumerator ChangeColorText()
        {
            textComponent.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            textComponent.color = Color.white;
        }

    }
}
