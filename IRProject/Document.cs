using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRProject
{
    public class Document
    {
        private static int idCounter = 0;

        public int docId { get; set; }
        public string name { get; set; }
        public string body { get; set; }

        public Document(string name,string body)
        {
            this.docId = idCounter++;
            this.name = name;
            this.body = body;
        }


        public override string ToString()
        {
            return docId+"-"+name;
        }

    }
}
