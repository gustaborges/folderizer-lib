using System;
using System.Collections.Generic;
using System.Text;

namespace FolderizerLib
{
    public class ExecutionResult
    {
        private List<Error> _errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionResult"/> class.
        /// </summary>
        public ExecutionResult()
        {
            _errors = new List<Error>();
        }
        /// <summary>
        /// Returns a collection of logged errors occurred while organizing the directory.
        /// <para>Each <see cref="Error"/> encapsulates information like <see cref="Error.FileName"/>,  <see cref="Error.FilePath"/>, <see cref="Error.ExceptionMessage"/></para>
        /// </summary>
        public Error[] Errors
        {
            get
            {
                return _errors.ToArray();
            }
        }

        /// <summary>
        /// Adds an <see cref="Error"/> to a collection of errors stored in <see cref="ExecutionResult"/> instances, which can be accessed through <see cref="ExecutionResult.Errors"/> property.
        /// </summary>
        /// <param name="error"></param>
        public void AppendError(Error error)
        {
            _errors.Add(error);
        }
    }
}
