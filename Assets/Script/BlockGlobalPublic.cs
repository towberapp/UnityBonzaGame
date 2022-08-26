using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

namespace Main
{
    public class BlockGlobalPublic : MonoBehaviour
    {
        public LetterArray letterArray;                
        public UnityEvent onInit = new();

        [SerializeField] private TMP_Text textComponent;
        [SerializeField] private GameObject connector;

        public void InitBlock()
        {
            onInit.Invoke();
            OnInitBlock();
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

        public void BlinkLetter()
        {
            StartCoroutine(ChangeColorText());
        }

        private IEnumerator ChangeColorText()
        {
            textComponent.color = Color.blue;
            yield return new WaitForSeconds(0.3f);
            textComponent.color = Color.white;
        }

        private void Awake()
        {
            GameEvents.ChangeIdGroupEvent.AddListener(OnGroupIdChange);
        }

        private void OnGroupIdChange(int fromID, int toId)
        {
            if (fromID == letterArray.groupId)
            {
                letterArray.groupId = toId;
            }
        }

        private void OnInitBlock()
        {
            this.textComponent.text = this.letterArray.stringLetter;
            transform.position = new Vector2(this.letterArray.xPos, this.letterArray.yPos);
        }
    }
}
