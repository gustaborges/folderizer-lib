using System;
using System.Runtime.Serialization;

namespace FolderizerLib
{
    [Serializable]
    internal class BasePathNotDefinedException : Exception
    {
        public BasePathNotDefinedException()
        {
        }

        public BasePathNotDefinedException(string message) : base(message)
        {
        }

        public BasePathNotDefinedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BasePathNotDefinedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}