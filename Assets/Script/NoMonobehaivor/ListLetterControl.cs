using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class ListLetterControl
    {
        private readonly List<LetterData> letterData = GlobalStatic.listLetterData;

        public void AddLetterToList(LetterData data)
        {
            letterData.Add(data);
        }
    }
}
