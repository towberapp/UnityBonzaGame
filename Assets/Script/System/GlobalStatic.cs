using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public static class GlobalStatic
    {
        public static List<LetterData> listLetterData = new();
        public static LetterData[,] arrayLetter;
        public static LetterArray[,] originalArrayLetter;
        public static List<WordBasicSave> wordsGlobal = new();


        public static Config config = new();
        public static int xPole;
        public static int yPole;
        public static bool canMoveBlock = true;

        public static void DebugObject(object obj)
        {
            var debugObj = JsonUtility.ToJson(obj);
            Debug.Log(debugObj);
        }
    }


    // save object
    [System.Serializable]
    public class GlobalArrayNew
    {
        public string glue;
        public int length;
        public List<WordBasicSave> wordBasicSaves = new();
    }

    // save object
    [System.Serializable]
    public class WordBasicSave
    {
        public string word;
        public bool horizontal;
        public bool isConnect = false;
        public int xStart;
        public int yStart;
    }


    [System.Serializable]
    public class Config
    {
        public int width;
        public int height;
        public string glue;
    }


    [System.Serializable]
    public class WordBasicGenerate
    {
        public string word;
        public bool horizontal;
        public int xStart;
        public int yStart;
        public bool isActive = true;
        public int groupId;
    }


    [System.Serializable]
    public class LetterData
    {
        public LetterArray letter;
        public GameObject obj;
    }


    [System.Serializable]
    public class LetterArray
    {
        public int xPos;
        public int yPos;
        public int groupId; 
        public string stringLetter;
    }
}
