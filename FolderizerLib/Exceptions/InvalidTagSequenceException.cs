using System;

namespace FolderizerLib
{
    [Serializable]
    public class InvalidTagSequenceException : Exception
    {
        public InvalidTagSequenceException()
        {
        }

        public InvalidTagSequenceException(string message) : base(message)
        {
        }

    }
}