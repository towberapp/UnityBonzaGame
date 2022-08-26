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


        public List<LetterData> GetListByIdGroup (int idgroup)
        {
            List<LetterData> list = new ();
            foreach (var item in letterData)
            {
                if (item.letter.groupId == idgroup)
                {
                    list.Add(item);
                }
            }
            return list;
        }


        public void SetGroupId(int from, int to)
        {
            foreach (var item in letterData)
            {
                if (item.letter.groupId == from)
                {
                    item.letter.groupId = to;
                }
            }
        }
    }
}
