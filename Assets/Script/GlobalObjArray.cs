using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class GlobalObjArray : MonoBehaviour
    {        

        private GameObject[,] arrayObj = new GameObject[10, 20];
        //private

        private void Awake()
        {
            GameEvents.ChangePosObjectEvent.AddListener(OnChangePos);
            GameEvents.SetObjToArraytEvent.AddListener(SetObjToArray);
        }

        private void SetObjToArray(GameObject arg0, LetterArray array)
        {
            arrayObj[array.xPos, array.yPos] = arg0;
        }


        private void OnChangePos(GameObject arg0, Vector2Int oldPos, Vector2Int newPos)
        {
            arrayObj[oldPos.x, oldPos.y] = null;
            arrayObj[newPos.x, newPos.y] = arg0;

            GenerateString(arg0, newPos);
        }


        private void GenerateString(GameObject arg0, Vector2Int newPos)
        {
            GetGenerateArray(arg0, newPos, Vector2Int.left, Vector2Int.right, true);
            GetGenerateArray(arg0, newPos, Vector2Int.up, Vector2Int.down, false);
        }


        private void GetGenerateArray(GameObject arg0, Vector2Int pos, Vector2Int from, Vector2Int to, bool horizontal) 
        {
            List<GameObject> fromList = ShowString(pos, from);
            List<GameObject> toList = ShowString(pos, to);

            List<GameObject> line = new();
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
                        } else
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

        private void BlinkController(List<GameObject> line, int indexCointains, string word)
        {
            for (int i = indexCointains; i < (word.Length + indexCointains); i++)
            {
                BlockGlobalPublic loc = line[i].GetComponent<BlockGlobalPublic>();
                loc.BlinkLetter();
            }
        }

        private void MergeController(List<GameObject> line, int indexCointains, string word, bool type, GameObject arg)
        {
            BlockGlobalPublic gl = arg.GetComponent<BlockGlobalPublic>();
            int group = gl.letterArray.groupId;

            for (int i = indexCointains; i < (word.Length + indexCointains); i++)
            {
                BlockGlobalPublic loc = line[i].GetComponent<BlockGlobalPublic>();

                if (loc.letterArray.groupId != group)
                {
                    // set Border
                    loc.SetBorder(type);

                    GameEvents.ChangeIdGroupEvent.Invoke(loc.letterArray.groupId, group);
                }
            }
        }



        private string GetStringFromList(List<GameObject> list)
        {
            string words = "";
            foreach (var item in list)
            {
                BlockGlobalPublic data = item.GetComponent<BlockGlobalPublic>();
                LetterArray letterData = data.letterArray;

                words += letterData.stringLetter;
            }
            return words;
        }


        private List<GameObject> ShowString(Vector2Int pos, Vector2Int vector)
        {
            List<GameObject> list = new ();
            Vector2Int currentpos = pos+vector;
            while (CheckArray(currentpos))
            {
                list.Add(arrayObj[currentpos.x, currentpos.y]);                                               
                currentpos += vector;
            }

            if (vector == Vector2Int.left || vector == Vector2Int.up)
                list.Reverse();

            return list;
        }


        private LetterArray GetLetter(Vector2Int pos)
        {
            BlockGlobalPublic data = arrayObj[pos.x, pos.y].GetComponent<BlockGlobalPublic>();
            return data.letterArray;
        }


        private bool CheckArray(Vector2Int pos)
        {
            if (pos.x < 0 || pos.y < 0) return false;

            if (arrayObj[pos.x, pos.y] == null) return false;
            return true;
        }


        private void ShowArray()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (arrayObj[j, i] != null)
                    {                        
                        LetterArray la = GetLetter(new Vector2Int(j,i));
                        Debug.Log("LETTER: " + la.stringLetter + " coord: " + la.xPos + "," + la.yPos);                        
                    }                    
                }
            }
        }


    }
}
