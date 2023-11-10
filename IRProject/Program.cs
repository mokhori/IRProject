using Lucene.Net;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System.Data;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace IRProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Corpus documentStore = new Corpus();
            //read documents and fill document Store
            documentStore.ReadDocsFromDirectory("../../../Resources");

            //index all documents
            InvertedIndex index = new InvertedIndex(documentStore.GetAll());

            Console.Write("Search(Enter 'end' to exit): ");
            var input = Console.ReadLine();

            while (input.ToLower() != "end")
            {
                //--------------processing query with and,or,not------------------

                //split by or
                var querySplitByOr = Regex.Split(input, " or ");

                PostingList orPostingList = new PostingList();

                foreach (var item in querySplitByOr)
                {
                    //split by and
                    var tokens = Regex.Split(item, " and ");

                    PostingList andPostingList = new PostingList(documentStore);

                    foreach (var token in tokens)
                    {
                        var newToken = token.Trim();

                        //calculate token with not
                        if (newToken.Contains("not "))
                        {
                            newToken = newToken.Replace("not ", "");
                            andPostingList = andPostingList & documentStore.Not(index.Get(newToken));
                        }
                        else
                        {
                            andPostingList = andPostingList & index.Get(newToken);
                        }

                    }

                    orPostingList = orPostingList | andPostingList;

                }

                if (orPostingList.docIds.Count > 0)

                    Console.WriteLine(documentStore.GetBookNames(orPostingList));
                else
                    Console.WriteLine("Not Found!");


                Console.Write("Search(Enter 'end' to exit): ");
                input = Console.ReadLine();

            }
        }
    }
}