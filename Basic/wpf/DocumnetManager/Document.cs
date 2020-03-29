using System;
using System.Collections.Generic;
using System.Text;

namespace DocumnetManager
{

    public interface IDocumnet {
        string Title { get; set; }
        string Content { get; set; }

    }


    public class Document : IDocumnet
    {
        public Document()
        {
        }

        public Document(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; set; }
        public string Content { get; set; }
    }
}
