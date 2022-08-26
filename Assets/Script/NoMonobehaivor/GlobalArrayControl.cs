using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class GlobalArrayControl
    {

        private readonly LetterData[,] arrayLetter = GlobalStatic.arrayLetter;        
        private readonly ListLetterControl listLetterControl = new();

        public void SetDataToArray(LetterData letterData)
        {            
            arrayLetter[letterData.letter.xPos, letterData.letter.yPos] = letterData;
        }

        public void MoveGroupToPos(int groupId, Vector2Int vector)
        {            
            List<LetterData> listGroup = listLetterControl.GetListByIdGroup(groupId);
            bool checkEmpty = CheckEmptyPosition(listGroup, vector);

            if (checkEmpty)
            {
                MoveForwardGroup(listGroup, vector);
            }
            else
            {
                MoveBackGroup(listGroup);
            }
        }

        private void MoveForwardGroup(List<LetterData> listGroup, Vector2Int vector)
        {
            foreach (var item in listGroup)
            {
                Vector2Int itemPos = new(item.letter.xPos, item.letter.yPos);
                Vector2Int newpolePos = itemPos + vector;

                // change array
                arrayLetter[newpolePos.x, newpolePos.y] = arrayLetter[itemPos.x, itemPos.y];
                arrayLetter[itemPos.x, itemPos.y] = null;

                // change object pos
                item.letter.xPos = newpolePos.x; 
                item.letter.yPos = newpolePos.y;

                //change Phisict
                item.obj.GetComponent<BlockGlobalPublic>().SetPosition(newpolePos);

                //check sosedey left or right
                bool leftCheck = CheckNeighbor(item, Vector2Int.left);
                bool rightCheck = CheckNeighbor(item, Vector2Int.right);
                bool upCheck = CheckNeighbor(item, Vector2Int.up);
                bool downCheck = CheckNeighbor(item, Vector2Int.down);

                if (leftCheck || rightCheck)
                    GetGenerateArrayNew(item, true);

                if (upCheck || downCheck)
                    GetGenerateArrayNew(item, false);
            }

        }

        private bool CheckNeighbor(LetterData item, Vector2Int vector)
        {
            Vector2Int posItem = new(item.letter.xPos, item.letter.yPos);
            Vector2Int poleCheck = posItem + vector;

            // arrray range
            if (!CheckRange(poleCheck)) return false;

            // check sosed
            if (!CheckArray(poleCheck)) return false;

            LetterData neighbor = arrayLetter[poleCheck.x, poleCheck.y];
            if (item.letter.groupId == neighbor.letter.groupId) return false;

            return true;
        }


        private bool CheckEmptyPosition(List<LetterData> listGroup, Vector2Int vector)
        {
            bool check = true;
            foreach (var item in listGroup)
            {
                Vector2Int itemPos = new (item.letter.xPos, item.letter.yPos);
                Vector2Int newpolePos = itemPos + vector;

                if (!CheckEmptyByPos(newpolePos)) check = false;
            }
            return check;
        }

        private bool CheckEmptyByPos(Vector2Int pos)
        {
            if (!CheckRange(pos)) return false;
            if (CheckArray(pos)) return false;
            return true;
        }


        //PRIVATE

        private void GetGenerateArrayNew(LetterData data, bool horizontal)
        {
            //Debug.Log("GetGenerateArrayNew: " + horizontal);

            Vector2Int pos = new(data.letter.xPos, data.letter.yPos);
            Vector2Int from = horizontal ? Vector2Int.left : Vector2Int.up;
            Vector2Int to = horizontal ? Vector2Int.right : Vector2Int.down;

            List<LetterData> fromList = ShowString(pos, from);
            List<LetterData> toList = ShowString(pos, to);
            List<LetterData> line = new();
            line.AddRange(fromList);
            line.Add(data);
            line.AddRange(toList);

            if (line.Count < 3) return;

            string objWord = GetStringFromList(line);
            string findWord = "";

            //Debug.Log("objWord: " + objWord);

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

            Debug.Log("MERGE!!!!");
            MergeController(line, checkConstain, findWord, horizontal, data);
        }



        private void MoveBackGroup(List<LetterData> listGroup)
        {
            foreach (var item in listGroup)
            {
                Vector2Int pos = new(item.letter.xPos, item.letter.yPos);
                item.obj.GetComponent<BlockGlobalPublic>().SetPosition(pos);
            }
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
                if (i != (word.Length + indexCointains) - 1)
                {
                    BlockGlobalPublic loc = line[i].obj.GetComponent<BlockGlobalPublic>();
                    loc.SetBorder(type);
                }

                if (line[i].letter.groupId != group)
                    listLetterControl.SetGroupId(line[i].letter.groupId, group);                
            }
        }


        private void SetGroup(int from, int to)
        {

        }

        private string GetStringFromList(List<LetterData> list)
        {
            string words = "";
            foreach (var item in list)
            {
                words += item.letter.stringLetter;
            }

            Debug.Log("WORD: " + words);
            return words;
        }


        private List<LetterData> ShowString(Vector2Int pos, Vector2Int vector)
        {
            List<LetterData> list = new();
            Vector2Int currentpos = pos + vector;
            while (CheckRange(currentpos) && CheckArray(currentpos))
            {
                list.Add(arrayLetter[currentpos.x, currentpos.y]);
                currentpos += vector;
            }

            if (vector == Vector2Int.left || vector == Vector2Int.up)
                list.Reverse();

            return list;
        }


        private bool CheckRange(Vector2Int pos)
        {
            if (pos.x < 0 || pos.y < 0 || pos.x >= GlobalStatic.xPole || pos.y >= GlobalStatic.yPole) return false;
            return true;
        }

        private bool CheckArray(Vector2Int pos)
        {
            // if false - pusto;

            if (arrayLetter[pos.x, pos.y] == null) return false;
            return true;
        }

    }
}
