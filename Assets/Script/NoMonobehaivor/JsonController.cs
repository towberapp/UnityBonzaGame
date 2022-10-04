using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main
{
    public class JsonController
    {
        private GlobalArrayNew globalArray;
        private int block = 1;
        private int totalGroup = 0;
        private int polePerimetr = 3;
        private Config config = new();

        public List<LetterArray> Init(string name, int diff)
        {
            block = 4 - diff;

            // load json
            TextAsset jsonTextFile = Resources.Load<TextAsset>(name);
            globalArray = JsonUtility.FromJson<GlobalArrayNew>(jsonTextFile.text);
            
            // word array
            GlobalStatic.wordsGlobal = globalArray.wordBasicSaves;

            // glue
            config.glue = globalArray.glue;

            // maxSize
            Vector2Int size = GetPoleSize(globalArray.wordBasicSaves);
            config.width = size.x;
            config.height = size.y;

            // creat list array
            List<LetterArray> letterArray = GenerateListArray(globalArray.wordBasicSaves);

            // create array
            LetterArray[,] letter2DArray = Create2DArray(letterArray);

            //set group            
            SetGroupLetter(letter2DArray);

            // creat dic of group
            Dictionary<int, Vector2Int> dicGroup = CreatDicGroup();

            //shaffle group
            ShaffleGroup(letterArray, dicGroup);

            return letterArray;
        }


        private LetterArray[,] Create2DArray(List<LetterArray> letterArray)
        {
            int gWidth = config.width;
            int gHeight = config.height;

            int widthReal = gWidth % block == 0 ? gWidth : (block - gWidth % block + gWidth);
            int heightReal = gHeight % block == 0 ? gHeight : (block - gHeight % block + gHeight);

            LetterArray[,] letter2DArray = new LetterArray[widthReal, heightReal];
            foreach (LetterArray item in letterArray)
            {
                letter2DArray[item.xPos, item.yPos] = item;
            }
            return letter2DArray;
        }


        private List<LetterArray> GenerateListArray(List<WordBasicSave> wordBasicSaves)
        {
            List<LetterArray> list = new();
            GlobalStatic.originalArrayLetter = new LetterArray[config.width, config.height];

            int groupId = 0;

            foreach (WordBasicSave wordBasic in wordBasicSaves)
            {
                int xVector = 0;
                int yVector = 0;

                foreach (var word in wordBasic.word)
                {
                    int xPos = wordBasic.xStart + xVector;
                    int yPos = wordBasic.yStart + yVector;


                    if (GlobalStatic.originalArrayLetter[xPos, yPos] == null) 
                    {
                        LetterArray lst = new()
                        {
                            xPos = xPos,
                            yPos = yPos,
                            groupId = groupId,
                            stringLetter = word.ToString()
                        };
                        list.Add(lst);
                        GlobalStatic.originalArrayLetter[xPos, yPos] = lst;
                    }


                    if (wordBasic.horizontal)
                    {
                        xVector++;
                    }
                    else
                    {
                        yVector--;
                    }

                };
                groupId++;
            }

            return list;
        }
    


        private Vector2Int GetPoleSize(List<WordBasicSave> allWords)
        {
            Vector2Int maxPos = new();

            foreach (WordBasicSave item in allWords)
            {
                int lng = item.word.Length;

                Vector2Int delta = new();

                if (item.horizontal)
                {
                    delta.x = lng + item.xStart;
                    delta.y = item.yStart;
                } else
                {
                    delta.x = item.xStart;
                    delta.y = item.yStart;
                }

                if (allWords.First() == item)
                {
                    maxPos.x = delta.x;
                    maxPos.y = delta.y;
                }

                if (delta.x > maxPos.x)
                {
                    maxPos.x = delta.x;
                }

                if (delta.y > maxPos.y)
                {
                    maxPos.y = delta.y;
                }
            }

            maxPos += Vector2Int.one;
            return maxPos;
        }


        private void ShaffleGroup(List<LetterArray> letterArray, Dictionary<int, Vector2Int> dicGroup)
        {   
            System.Random rnd = new ();
            for (int i = 0; i < dicGroup.Count; i++)
            {
                int j = rnd.Next(dicGroup.Count);
                Vector2Int temp = dicGroup[j];
                dicGroup[j] = dicGroup[i];
                dicGroup[i] = temp;
            }

            foreach (LetterArray item in letterArray)
            {
                item.xPos = dicGroup[item.groupId].x + item.xPos;
                item.yPos = dicGroup[item.groupId].y + item.yPos;
            }
        }


        private Dictionary<int, Vector2Int> CreatDicGroup()
        {
            int yPole = Convert.ToInt32(Math.Ceiling(Math.Sqrt(totalGroup)));
            float xPoleTemp = (float) totalGroup / (float) yPole;
            int xPole = Convert.ToInt32(Math.Ceiling(xPoleTemp));

            int xArray = (xPole-1) + (xPole * block);
            int yArray = (yPole-1) + (yPole * block);

            int xGlobal = xArray + polePerimetr * 2;
            int yGlobal = yArray + polePerimetr * 2;

            GlobalStatic.xPole = xGlobal;
            GlobalStatic.yPole = yGlobal;
            GlobalStatic.arrayLetter = new LetterData[xGlobal, yGlobal];

            Dictionary<int, Vector2Int> dicGroup = new();

            // make group coord
            int groipCount = 0;

            for (int y = polePerimetr; y < yArray + polePerimetr; y+= block+1)
            {
                for (int x = polePerimetr; x < xArray + polePerimetr; x+= block+1)
                {
                    dicGroup.Add(groipCount, new Vector2Int(x, y));
                    groipCount++;
                }
            }

            return dicGroup;
        }


        private void SetGroupLetter(LetterArray[,] letter2DArray)
        {
            int blockX = letter2DArray.GetLength(0) / block;
            int blockY = letter2DArray.GetLength(1) / block;

            int countLetter = 0;
            int groupId = 0;

            for (int y1 = 0; y1 < blockY; y1++)
            {           
                for (int x1 = 0; x1 < blockX; x1++)
                {
                    for (int y = 0; y < block; y++)
                    {
                        for (int x = 0; x < block; x++)
                        {
                            int realX = x + x1 * block;
                            int realY = y + y1 * block;

                            if (letter2DArray[realX, realY] != null)
                            {
                                LetterArray letter = letter2DArray[realX, realY];
                                countLetter++;
                                letter.groupId = groupId;
                                letter.xPos = x;
                                letter.yPos = y;
                            }
                        }
                    }

                    if (countLetter > 0)
                    {
                        groupId++;
                        countLetter = 0;
                    }

                }
            }

            totalGroup = groupId;
        }


        
    }
}
