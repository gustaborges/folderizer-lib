using System.Collections.Generic;

namespace FolderizerLib.Audio
{
    /// <summary>
    /// Defines an interface for classes that validates sequences of <see cref="AudioTag"/>.
    /// </summary>
    public interface IAudioTagSequenceValidator
    {
        /// <summary>
        /// Determines the validity of a given sequence of <see cref="AudioTag"/>
        /// </summary>
        /// <param name="tagSequence"></param>
        /// <exception cref="InvalidTagSequenceException">Thrown when the evaluated sequence is invalid.</exception>
        /// <returns>The method returns void.</returns>
        public abstract void Validate(List<AudioTag> tagSequence);
    }
}