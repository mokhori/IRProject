using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IRProject
{
    public class InvertedIndex
    {
        public Dictionary<string, PostingList> table = new Dictionary<string, PostingList>();


        public InvertedIndex()
        {

        }

        public InvertedIndex(List<Document> docs)
        {
            foreach (var item in docs)
            {
                Add(item);

            }
        }

        public void Add(Document doc)
        {
            var tokens = Regex.Split(doc.body, "\\s+");

            HashSet<string> distinctTokens = new HashSet<string>();

            foreach (var token in tokens)
            {
                distinctTokens.Add(token);
            }

            foreach (var token in distinctTokens)
            {
                if (!table.ContainsKey(token))
                {
                    table[token] = new PostingList(doc.docId);
                }
                else
                {
                    table[token].Add(doc.docId);
                }

            }

            foreach (var item in table.Values)
            {
                item.Sort();
            }

        }

        public PostingList Get(string token)
        {
            if (table.ContainsKey(token))
                return table[token];

            return new PostingList();
        }

    }
}
