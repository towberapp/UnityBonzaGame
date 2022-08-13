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

        public void InitBlock()
        {
            onInit.Invoke();
            OnInitBlock();
        } 

        private void OnInitBlock()
        {

            this.textComponent.text = this.letterArray.stringLetter;
            transform.position = new Vector2(this.letterArray.xPos, this.letterArray.yPos);
        }
    }
}
