using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class ListLetterControl
    {
        private List<LetterData> letterData = GlobalStatic.listLetterData;

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

        public void DeleteListByIdGroup(int idGroup)
        {
            List<LetterData> newData = new();

            foreach (var item in GlobalStatic.listLetterData)
            {
                if (item.letter.groupId != idGroup)
                    newData.Add(item);
            }

            GlobalStatic.listLetterData = newData;
        }
    }
}
