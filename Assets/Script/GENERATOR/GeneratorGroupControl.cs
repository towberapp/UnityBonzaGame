using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class GeneratorGroupControl
    {
        //STATIC!
        public static int idGroup = -1;
        public static List<WordBasicGenerate> wordBasicsArray = new List<WordBasicGenerate>();

        //PRIVATE
        private ListLetterControl listLetterControl = new();        
        private GlobalArrayControl globalArrayControl = new();


        public int GenerateWord(string word, Transform letterFolder, GameObject letterPrefab, Vector2Int coord)
        {
            int groupId = wordBasicsArray.Count;

            WordBasicGenerate wordBasic = new()
            {
                word = word,
                horizontal = true,                
                xStart = coord.x,
                yStart = coord.y,
                groupId = groupId,
            };

            wordBasicsArray.Add(wordBasic);

            for (int i = 0; i < word.Length; i++)
            {

                LetterArray letterArray = new()
                {
                    xPos = coord.x + i,
                    yPos = coord.y,
                    stringLetter = word.Substring(i, 1),
                    groupId = groupId
                };

                GameObject obj = Object.Instantiate(letterPrefab, Vector2.zero, Quaternion.identity, letterFolder);
                
                BlockGlobalPublic
                    blockGlobal = obj.GetComponent<BlockGlobalPublic>();
                    blockGlobal.letterArray = letterArray;
                    blockGlobal.InitBlock();

                LetterData letterData = new()
                {
                    letter = letterArray,
                    obj = obj
                };

                GlobalStatic.listLetterData.Add(letterData);
            }
            
            globalArrayControl.SetConnectorByIdGroup(groupId, true);

            return groupId;
        }

        public void ClearAll()
        {
            foreach (LetterData item in GlobalStatic.listLetterData)
            {
                item.obj.GetComponent<BlockGlobalPublic>().DestroyObject();
            }
            GlobalStatic.listLetterData.Clear();
            wordBasicsArray.Clear();
        }


        public bool RemoveGroup(int idGroup)
        {
            if (!wordBasicsArray[idGroup].isActive) return false;

            wordBasicsArray[idGroup].isActive = false;
            List<LetterData> listGroup = listLetterControl.GetListByIdGroup(idGroup);                      

            foreach (var item in listGroup)
            {
                item.obj.GetComponent<BlockGlobalPublic>().DestroyObject();
            }
            
            listLetterControl.DeleteListByIdGroup(idGroup);

            return true;
        }


        public void SetPosition(int groupId, Vector2Int vector)
        {            
            List<LetterData> listGroup = listLetterControl.GetListByIdGroup(groupId);

            wordBasicsArray[groupId].xStart += vector.x;
            wordBasicsArray[groupId].yStart += vector.y;

            foreach (var item in listGroup)
            {
                Vector2Int itemPos = new(item.letter.xPos, item.letter.yPos);
                Vector2Int newpolePos = itemPos + vector;

                // change object pos
                item.letter.xPos = newpolePos.x;
                item.letter.yPos = newpolePos.y;

                //change Phisict
                item.obj.GetComponent<BlockGlobalPublic>().SetPosition(newpolePos);
            }
        }


        public void RotateGroupById()
        {
            if (GeneratorGroupControl.idGroup < 0) return;
            List<LetterData> listGroup = listLetterControl.GetListByIdGroup(GeneratorGroupControl.idGroup);

            bool isHorizontal = IsHorizontal(listGroup);

            // remove border
            globalArrayControl.RemoveBorder(GeneratorGroupControl.idGroup);
            // set Border
            globalArrayControl.SetConnectorByIdGroup(GeneratorGroupControl.idGroup, !isHorizontal);
           

            wordBasicsArray[GeneratorGroupControl.idGroup].horizontal = !isHorizontal;

            int newXpos = listGroup[0].letter.xPos;
            int newYpos = listGroup[0].letter.yPos;

            for (int i = 1; i < listGroup.Count; i++)
            {
                if (isHorizontal)
                {
                    listGroup[i].letter.xPos = newXpos;
                    listGroup[i].letter.yPos = newYpos - i;
                } else
                {
                    listGroup[i].letter.xPos = newXpos + i;
                    listGroup[i].letter.yPos = newYpos;
                }

                Vector2Int pos = new(listGroup[i].letter.xPos, listGroup[i].letter.yPos);
                listGroup[i].obj.GetComponent<BlockGlobalPublic>().SetPosition(pos);
            }
          
        }
        

        private bool IsHorizontal(List<LetterData> listGroup)
        {
            if (listGroup[0].letter.xPos != listGroup[1].letter.xPos) return true;
            return false;
        }

    }
}
