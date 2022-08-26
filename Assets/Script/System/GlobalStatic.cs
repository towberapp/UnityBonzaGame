using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public static class GlobalStatic
    {
        public static List<LetterData> listLetterData = new();
        public static LetterData[,] arrayLetter;

        public static int xPole;
        public static int yPole;
    }


    public class PosClass
    {
        public int x;
        public int y;
    }


    [System.Serializable]
    public class LetterData
    {
        public LetterArray letter;
        public GameObject obj;
    }


    [System.Serializable]
    public class GlobalArray
    {
        public int cointId;
        public List<LetterArray> letterArray = new();
    }


    [System.Serializable]
    public class LetterArray
    {
        public int xPos;
        public int yPos;
        public int groupId;
        public int letterId;
        public string stringLetter;
    }
}
