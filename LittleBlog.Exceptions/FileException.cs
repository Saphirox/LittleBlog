using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBlog.Exceptions
{
    public class FileException : Exception
    {
        public FileException(string message) : base(message)
        {
        }

        public static FileException UnpropriateFileExtenstion(string name)
        {
            return new FileException($"Unpropriate file extenstion {name}");
        }

        public static FileException FileExists(string path)
        {
            return new FileException($"File is exits in path {path}");
        }

        public static FileException NotFileExists(string path)
        {
            return new FileException($"File is not exits in path {path}");
        }

        public static FileException FileNameNotExists(string name)
        {
            return new FileException($"File name { name } is not exits");

        }
    }
}