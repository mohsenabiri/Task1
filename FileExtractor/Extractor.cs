using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace FileExtractor
{
    public class Extractor :IExtractor
    {
        public string ReadFileHTMLtoString(string filePath)
        {
            FileInfo fileInfo;
            FileStream fileStream=null;
            StreamReader streamReader = null;
            try
            {
                fileInfo = new FileInfo(filePath);
                fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read);
                streamReader = new StreamReader(fileStream);
                return streamReader.ReadToEnd();
            }
            catch (Exception exeption)
            {
                return exeption.Message;
            }
            finally
            {                
                streamReader?.Close();
                fileStream?.Close();
            }

        }
    }
}
