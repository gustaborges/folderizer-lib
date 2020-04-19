using System;

namespace FolderizerLib
{
    /// <summary>
    /// Thrown when an invalid tag sequence representing the desired directory structured is provided.
    /// </summary>
    [Serializable]
    public class InvalidTagSequenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTagSequenceException"/> class.
        /// </summary>
        public InvalidTagSequenceException()
        {
        }
        /// <summary>
        /// <para>Initializes a new instance of the <see cref="InvalidTagSequenceException"/> class with a specified error message.</para>
        /// <para></para>
        /// </summary>
        public InvalidTagSequenceException(string message) : base(message)
        {
        }

    }
}