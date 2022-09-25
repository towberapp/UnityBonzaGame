using System.Collections;
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


        public void GenerateWord(string word, Transform letterFolder, GameObject letterPrefab)
        {
            int groupId = wordBasicsArray.Count;

            Vector2Int pos = new(GlobalStatic.xPole / 2, GlobalStatic.yPole);

            WordBasicGenerate wordBasic = new()
            {
                word = word,
                horizontal = true,                
                xStart = pos.x,
                yStart = pos.y,
                groupId = groupId,
            };
            wordBasicsArray.Add(wordBasic);


            for (int i = 0; i < word.Length; i++)
            {

                LetterArray letterArray = new()
                {
                    xPos = pos.x + i,
                    yPos = pos.y,
                    stringLetter = word.Substring(i, 1),
                    groupId = groupId
                };

                GameObject obj = Object.Instantiate(letterPrefab, new Vector2(0, 0), Quaternion.identity, letterFolder);
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
        }


        public void RemoveGroup()
        {
            wordBasicsArray[GeneratorGroupControl.idGroup].isActive = false;
            List<LetterData> listGroup = listLetterControl.GetListByIdGroup(GeneratorGroupControl.idGroup);

            foreach (var item in listGroup)
            {
                item.obj.GetComponent<BlockGlobalPublic>().DestroyObject();
            }
            
            listLetterControl.DeleteListByIdGroup(GeneratorGroupControl.idGroup);
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
            Debug.Log("rotate: " + GeneratorGroupControl.idGroup);

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
