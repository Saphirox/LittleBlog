using System;
using System.Collections.Generic;

namespace LittleBlog.BLL.Infrastructure
{
    public class Result
    {
        public Result()
        {}

        public Result(bool isSuccedeed)
        {
            IsSuccedeed = isSuccedeed;

            if (IsSuccedeed == false)
            {
                throw new ArgumentException(nameof(IsSuccedeed) + " must be declared");
            }
        }

        public Result(bool isSuccedeed, HashSet<string> errors)
        {
            IsSuccedeed = isSuccedeed;
            Errors = errors;
        }
        
        public bool IsSuccedeed { get; set; }
        public HashSet<string> Errors { get; set; }
    }
}