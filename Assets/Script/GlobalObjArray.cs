using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{

    public class GlobalObjArray : MonoBehaviour
    {        

        private LetterData[,] arrayLetter = new LetterData [10,20];

        private void Awake()
        {
            GameEvents.ChangePosObjectEvent.AddListener(OnChangePos);
            GameEvents.SetDataToArraytEvent.AddListener(SetDataToArray);
        }

        private void SetDataToArray(LetterData letterData)
        {            
            arrayLetter[letterData.letter.xPos, letterData.letter.yPos] = letterData;
        }


        private void OnChangePos(Vector2Int oldPos, Vector2Int newPos)
        {
            arrayLetter[newPos.x, newPos.y] = arrayLetter[oldPos.x, oldPos.y];
            arrayLetter[oldPos.x, oldPos.y] = null;

            GenerateString(arrayLetter[newPos.x, newPos.y], newPos);
        }


        private void GenerateString(LetterData arg0, Vector2Int newPos)
        {
            GetGenerateArray(arg0, newPos, Vector2Int.left, Vector2Int.right, true);
            GetGenerateArray(arg0, newPos, Vector2Int.up, Vector2Int.down, false);
        }


        private void GetGenerateArray(LetterData arg0, Vector2Int pos, Vector2Int from, Vector2Int to, bool horizontal)
        {
            List<LetterData> fromList = ShowString(pos, from);
            List<LetterData> toList = ShowString(pos, to);

            List<LetterData> line = new();
            line.AddRange(fromList);
            line.Add(arg0);
            line.AddRange(toList);

            if (line.Count < 2) return;    

            string objWord = GetStringFromList(line);
            string findWord = "";
            int checkConstain = -1;
            bool blink = false;

            foreach (var item in GlobalWordArray.wordsGlobal)
            {
                if (!item.isConnect)
                {
                    int check = objWord.LastIndexOf(item.word);
                    if (objWord.LastIndexOf(item.word) >= 0)
                    {
                        checkConstain = check;
                        findWord = item.word;

                        if (item.horizontal == horizontal)
                        {
                            item.isConnect = true;
                        }
                        else
                        {
                            blink = true;
                        }
                    }
                }
            }

            if (blink)
            {
                BlinkController(line, checkConstain, findWord);
            }

            if (checkConstain < 0 || blink) return;
            MergeController(line, checkConstain, findWord, horizontal, arg0);
        }


        private void BlinkController(List<LetterData> line, int indexCointains, string word)
        {
            for (int i = indexCointains; i < (word.Length + indexCointains); i++)
            {
                BlockGlobalPublic loc = line[i].obj.GetComponent<BlockGlobalPublic>();
                loc.BlinkLetter();
            }
        }


        private void MergeController(List<LetterData> line, int indexCointains, string word, bool type, LetterData arg)
        {
            int group = arg.letter.groupId;

            for (int i = indexCointains; i < (word.Length + indexCointains); i++)
            {                
                if (line[i].letter.groupId != group)
                {
                    BlockGlobalPublic loc = line[i].obj.GetComponent<BlockGlobalPublic>();

                    // set Border
                    loc.SetBorder(type);

                    Debug.Log("CHANGT GROUP! FROM: " + line[i].letter.groupId + ", to: " + group);
                    //GameEvents.ChangeIdGroupEvent.Invoke(line[i].letter.groupId, group);
                }
            }
        }


        private string GetStringFromList(List<LetterData> list)
        {
            string words = "";
            foreach (var item in list)
            {
                words += item.letter.stringLetter;
            }
            return words;
        }


        private List<LetterData> ShowString(Vector2Int pos, Vector2Int vector)
        {
            List<LetterData> list = new();
            Vector2Int currentpos = pos + vector;
            while (CheckArray(currentpos))
            {
                list.Add(arrayLetter[currentpos.x, currentpos.y]);
                currentpos += vector;
            }

            if (vector == Vector2Int.left || vector == Vector2Int.up)
                list.Reverse();

            return list;
        }


        private bool CheckArray(Vector2Int pos)
        {
            if (pos.x < 0 || pos.y < 0) return false;

            if (arrayLetter[pos.x, pos.y] == null) return false;
            return true;
        }

    }
}
