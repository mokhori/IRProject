using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace IRProject
{
    public class Corpus
    {
        private Dictionary<int,Document> docs = new Dictionary<int,Document>();

        public void Add(Document doc) {
            docs.Add(doc.docId, doc);
        }

        public List<Document> GetAll()
        {
            return docs.Values.ToList();
        }

        public Document Get(int docId)
        {
            return docs[docId];
        }


        public PostingList Not(PostingList postingList)
        {
            var result = new PostingList();


            foreach (var doc in docs)
            {
                if (!postingList.docIds.Contains(doc.Key))
                {
                    result.Add(doc.Key);
                }
            }
            return result;
        }



        public void ReadDocsFromDirectory(string path) {

            string[] filePaths = Directory.GetFiles(path, "*.txt");

            foreach (string filePath in filePaths)
            {
                
                string fileContent = File.ReadAllText(filePath);

                string name = Path.GetFileNameWithoutExtension(filePath);

                this.Add(new Document(name, fileContent));

            }

        }


        public string GetBookNames(PostingList postingList)
        {
            string result="";

            foreach (var id in postingList.docIds)
            {
                result += Get(id).name+"\n";
                
            }

            return result;

        }
        


    }
}
