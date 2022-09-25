using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main
{
    public class GlobalArrayControl
    {
        private readonly LetterData[,] arrayLetter = GlobalStatic.arrayLetter;        
        private readonly ListLetterControl listLetterControl = new();

        // 1 step
        public void MoveGroupToPos(int groupId, Vector2Int vector)
        {
            List<LetterData> listGroup = listLetterControl.GetListByIdGroup(groupId);
            bool checkEmpty = CheckEmptyPosition(listGroup, vector);

            if (vector == Vector2Int.zero)
            {
                MoveBackGroup(listGroup);
                return;
            }

            if (checkEmpty)
            {
                MoveForwardGroup(listGroup, vector);
            }
            else
            {
                int x = GetIntSdvig(vector.x);
                int y = GetIntSdvig(vector.y);
                Vector2Int newVector = new (x, y);
                MoveGroupToPos(groupId, newVector);
            }
        }

        private int GetIntSdvig(int i)
        {
            if (i == 0) return 0;
            if (i > 0) return i - 1;
            if (i < 0) return i + 1;

            return i;
        }

        public void SetConnector()
        {
            List<LetterData> data = GlobalStatic.listLetterData;

            foreach (var item in data)
            {
                bool rightCheck = FindSosed(item.letter, Vector2Int.right);
                bool downCheck = FindSosed(item.letter, Vector2Int.down);

                if (rightCheck) SetBorder(item.obj, true);
                if (downCheck) SetBorder(item.obj, false);
            }
        }

        public void SetConnectorByIdGroup(int groupId, bool type)
        {
            List<LetterData> listGroup = listLetterControl.GetListByIdGroup(groupId);
            foreach (var item in listGroup)
            {
                if (item != listGroup.Last())
                SetBorder(item.obj, type);
            }
        }


        public void RemoveBorder(int groupId)
        {
            List<LetterData> listGroup = listLetterControl.GetListByIdGroup(groupId);
            foreach (var item in listGroup)
            {
                if (item != listGroup.Last())
                {
                    BlockGlobalPublic loc = item.obj.GetComponent<BlockGlobalPublic>();
                    loc.RemoveBorder();
                }                    
            }
        }


        private void SetBorder(GameObject block, bool type)
        {
            BlockGlobalPublic loc = block.GetComponent<BlockGlobalPublic>();
            loc.SetBorder(type);
        }



        // PRIVATE
        private bool FindSosed(LetterArray item, Vector2Int vector)
        {
            Vector2Int posItem = new(item.xPos, item.yPos);
            Vector2Int poleCheck = posItem + vector;

            if (!CheckRange(poleCheck)) return false;
            if (!CheckArray(poleCheck)) return false;

            LetterData neighbor = arrayLetter[poleCheck.x, poleCheck.y];
            if (item.groupId != neighbor.letter.groupId) return false;

            return true;
        }


        private bool CheckEmptyPosition(List<LetterData> listGroup, Vector2Int vector)
        {
            bool check = true;
            // clear checked array before checked
            foreach (var item in listGroup)
            {
                arrayLetter[item.letter.xPos, item.letter.yPos] = null;
            }

            foreach (var item in listGroup)
            {
                Vector2Int itemPos = new(item.letter.xPos, item.letter.yPos);
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


        public void ShowArray()
        {
            for (int i = 0; i < arrayLetter.GetLength(1); i++)
            {
                for (int j = 0; j < arrayLetter.GetLength(0); j++)
                {
                    if (arrayLetter[j, i] != null)
                        GlobalStatic.DebugObject(arrayLetter[j,i]);
                }
            }
        }

        private void MoveBackGroup(List<LetterData> listGroup)
        {
            foreach (var item in listGroup)
            {
                Vector2Int pos = new(item.letter.xPos, item.letter.yPos);
                arrayLetter[pos.x, pos.y] = item;

                item.obj.GetComponent<BlockGlobalPublic>().SetPosition(pos);
            }

        }

        // 2 step
        private void MoveForwardGroup(List<LetterData> listGroup, Vector2Int vector)
        {
            // set to array again 
            foreach (var item in listGroup)
            {
                Vector2Int itemPos = new(item.letter.xPos, item.letter.yPos);
                Vector2Int newpolePos = itemPos + vector;

                arrayLetter[newpolePos.x, newpolePos.y] = item;

                // change object pos
                item.letter.xPos = newpolePos.x;
                item.letter.yPos = newpolePos.y;

                //change Phisict
                item.obj.GetComponent<BlockGlobalPublic>().SetPosition(newpolePos);
            }

            // check to sosedy;
            foreach (var item in listGroup)
            {
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
            //if (item.letter.groupId == neighbor.letter.groupId) return false;

            return true;
        }





        //PRIVATE

        private void GetGenerateArrayNew(LetterData data, bool horizontal)
        {
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

            int checkConstain = -1;

            bool blink = false;
            foreach (var item in GlobalStatic.wordsGlobal)
            {
                if (!item.isConnect)
                {
                    int check = objWord.LastIndexOf(item.word);
                    
                    if (check >= 0)
                    {
                        Vector2Int posOneLetter = new Vector2Int(item.xStart, item.yStart);
                        // word is found!
                        bool checkOrigin = CheckActualWord(line, check, item.word, posOneLetter);

                        Debug.Log("CHECK ORIGIN: " + checkOrigin);

                        checkConstain = check;
                        findWord = item.word;

                        if (item.horizontal == horizontal && checkOrigin)
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

            MergeController(line, checkConstain, findWord, horizontal, data);

            bool checkWord = CheckWordComplete();     
            
            if (checkWord)
            {
                GameEvents.winGameEvent.Invoke();
            }
        }


        private bool CheckWordComplete()
        {
            bool check = true;
            foreach (var item in GlobalStatic.wordsGlobal)
            {
                Debug.Log("CHECK: " + item.word + ", isConnect: " + item.isConnect);
                if (!item.isConnect) check = false;
            }
            return check;
        }


        // add all group in word
        private bool CheckActualWord(List<LetterData> line, int check, string word, Vector2Int posOneLetter)
        {
            List<int> groupList = new List<int>();
            List<LetterData> listAllGroup = new();

            //Debug.Log("POS ONE: " + posOneLetter);

            Vector2Int delta = Vector2Int.zero;

            for (int i = check; i < word.Length+check; i++)
            {
                if (i == check)
                {
                    Vector2Int letterOne = new Vector2Int(line[i].letter.xPos, line[i].letter.yPos);
                    delta = letterOne - posOneLetter;
                }
                // find delta beetween first element and posOneLetter
                // then check all listAllGroup and check original array

                //GlobalStatic.DebugObject(line[i]);
                //GlobalStatic.DebugObject(line[i]);

                int currentGroup = line[i].letter.groupId;
                bool checkGroupList = groupList.Contains(currentGroup);
                
                if (!checkGroupList)
                {
                    groupList.Add(currentGroup);
                    List<LetterData> listGroupById = listLetterControl.GetListByIdGroup(currentGroup);
                    listAllGroup.AddRange(listGroupById);
                }                
            }

            //Debug.Log("COUNT NEW LIST: " + listAllGroup.Count);
            //Debug.Log("DELTA: " + delta);

            // all letter for check
            foreach (var item in listAllGroup)
            {
                Vector2Int posLetter = new Vector2Int(item.letter.xPos, item.letter.yPos);
                Vector2Int newDeltaPos = posLetter - delta;

                //check newDeltapos equal originArray
                if (GlobalStatic.originalArrayLetter[newDeltaPos.x, newDeltaPos.y] == null) return false;

                LetterArray origLetter = GlobalStatic.originalArrayLetter[newDeltaPos.x, newDeltaPos.y];

                // NEEDS CHECK ON NULL!!!
                if (origLetter.stringLetter != item.letter.stringLetter)
                {
                    return false;
                }


                /*Debug.Log("LETTER: " + item.letter.stringLetter);
                Debug.Log("posLetter: " + posLetter);
                Debug.Log("delta: " + delta);
                Debug.Log("NEW DELTAPOS: " + newDeltaPos);
                Debug.Log("-------");*/
            }

            return true;

            //GlobalStatic.originalArrayLetter;
            // 

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
                BlockGlobalPublic loc = line[i].obj.GetComponent<BlockGlobalPublic>();
                loc.FlashLetter();

                if (i != (word.Length + indexCointains) - 1)
                {
                    SetBorder(line[i].obj, type);
                }

                if (line[i].letter.groupId != group)
                    listLetterControl.SetGroupId(line[i].letter.groupId, group);                
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
            if (arrayLetter[pos.x, pos.y] == null ) return false;
            return true;
        }

    }
}
