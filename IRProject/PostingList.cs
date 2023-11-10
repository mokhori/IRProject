using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IRProject
{
    public class PostingList
    {
        public List<int> docIds = new List<int>();

        public PostingList()
        {
            
        }


        public PostingList(Corpus corpus)
        {
            foreach (var item in corpus.GetAll())
            {
                docIds.Add(item.docId);
            }
            Sort();
        }


        public PostingList(params int[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                docIds.Add(ids[i]);
            }
        }


        public void Add(int id)
        {
            docIds.Add(id);
        }

        public void Sort() { docIds.Sort(); }

        public int Size() { return docIds.Count; }

        public static PostingList operator &(PostingList first, PostingList second)
        {
            PostingList result = new PostingList();
            int i = 0, j = 0;

            while (i < first.Size() & j < second.Size())
            {
                int a = first.docIds[i];
                int b = second.docIds[j];

                if (a == b)
                {
                    result.Add(a);
                    i++;
                    j++;
                }
                else if (a < b)
                    i++;
                else
                    j++;
            }

            return result;
        }

        public static PostingList operator |(PostingList first, PostingList second)
        {
            PostingList result = first;


            for (int i = 0; i < second.Size(); i++)
            {
                if (!result.docIds.Contains(second.docIds[i]))
                    result.Add(second.docIds[i]);
            }

            result.Sort();


            return result;
        }

        public override string ToString()
        {
            return string.Join(";", docIds); ;
        }


    }
}
