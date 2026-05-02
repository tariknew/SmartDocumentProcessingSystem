using System;

namespace Model.Utilities
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message) { }
    }
}
