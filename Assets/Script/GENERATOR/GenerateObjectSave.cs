using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

namespace Main
{    
    public class GenerateObjectSave
    {

        public bool GetGlobalObject(string glue, string name, string lang, bool overwrite)
        {
            Vector2Int minpos = GetMinPos();
            List<WordBasicSave> wordBasic = MoveWordToZero(minpos);

            GlobalArrayNew global = new()
            {
                glue = glue,
                length = wordBasic.Count,
                lang = lang,
                wordBasicSaves = wordBasic
            };

            string strJson = JsonUtility.ToJson(global);
            SaveJson(strJson, name, lang, overwrite);

            return true;
        }


        private void SaveJson(string data, string name, string lang, bool overwrite)
        {            
            string path = "Assets/Resources/Json/" + lang + "/Cross/" + name + ".json";
       
            FileMode fm = FileMode.Create;

            using (FileStream fs = new FileStream(path, fm))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(data);
                    Debug.Log("SAVE OK: " + name + ".json");
                }
            } 
        }




        private List<WordBasicSave> MoveWordToZero(Vector2Int vector)
        {
            List<WordBasicSave> wordBasic = new();

            foreach (var item in GeneratorGroupControl.wordBasicsArray)
            {    
                if (item.isActive)
                {
                    WordBasicSave wb = new()
                    {
                        xStart = item.xStart - vector.x,
                        yStart = item.yStart - vector.y,
                        word = item.word,
                        horizontal = item.horizontal,
                    };
                    wordBasic.Add(wb);
                }                    
            }

            return wordBasic;
        }


        private Vector2Int GetMinPos()
        {
            Vector2Int minPos = new();

            foreach (var item in GlobalStatic.listLetterData)
            {
                if (GlobalStatic.listLetterData.First() == item)
                {
                    minPos.x = item.letter.xPos;
                    minPos.y = item.letter.yPos;
                }
                
                if (item.letter.xPos < minPos.x)
                {
                    minPos.x = item.letter.xPos;
                }

                if (item.letter.yPos < minPos.y)
                {
                    minPos.y = item.letter.yPos;
                }
            }
            return minPos;
        }


        private string GenerateName(string data)
        {
            var hash = new Hash128();
            hash.Append(data);
            return hash.ToString();
        }


    }
}
