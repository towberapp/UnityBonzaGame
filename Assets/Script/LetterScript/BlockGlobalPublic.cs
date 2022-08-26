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
        [SerializeField] private GameObject textObject;

        [SerializeField] private GameObject fon;
        [SerializeField] private GameObject connector;


        public void InitBlock()
        {
            onInit.Invoke();
            OnInitBlock();
        }

        public void MoveOn()
        {

        }

        public void MoveOff()
        {
            
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

        public void SetPosition(Vector2Int pos)
        {
            transform.position = (Vector2)pos;
        }

        public void BlinkLetter()
        {
            StartCoroutine(ChangeColorText());
        }

        private void OnInitBlock()
        {
            this.textComponent.text = this.letterArray.stringLetter;
            transform.position = new Vector2(this.letterArray.xPos, this.letterArray.yPos);
        }

        private IEnumerator ChangeColorText()
        {
            textComponent.color = Color.blue;
            yield return new WaitForSeconds(0.3f);
            textComponent.color = Color.white;
        }

    }
}
