using System;
using System.Collections.Generic;
using System.Text;

namespace FileExtractor
{
    public interface IExtractor
    {
        public string ReadFileHTMLtoString(string filePath);
    }
}
