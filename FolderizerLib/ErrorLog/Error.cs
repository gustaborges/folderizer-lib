using System.IO;

namespace FolderizerLib
{
    /// <summary>
    /// An instance of this class encapsulates information about errors occurred during the execution of the files organization.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Error"/>. 
        /// </summary>
        public Error()
        {

        }

        /// <summary>
        /// Represents the name of file file, without the path.
        /// </summary>
        public string FileName { get => Path.GetFileName(FilePath); }

        /// <summary>
        /// Represents the complete file path.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Contains a string with the message of the thrown exception.
        /// </summary>
        public string ExceptionMessage { get; set; }
    }
}