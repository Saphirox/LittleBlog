using System;

namespace LittleBlog.Exceptions
{
    public class FileException : Exception
    {
        public FileException(string message) : base(message)
        {
        }

        public static FileException UnpropriateFileExtension(string name)
        {
            return new FileException($"Unpropriate file extension {name}");
        }

        public static FileException FileExists(string path)
        {
            return new FileException($"File is exist in path {path}");
        }

        public static FileException NotFileExists(string path)
        {
            return new FileException($"File is not exist in path {path}");
        }

        public static FileException FileNameNotExists(string name)
        {
            return new FileException($"File name { name } is not exist");

        }
    }
}