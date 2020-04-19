using System.Collections.Generic;
using System.Linq;

namespace FolderizerLib.Audio
{
    /// <summary>
    /// Means of validating a sequence of audio tags representing a directory structure.
    /// </summary>
    public class AudioTagSequenceValidator : IAudioTagSequenceValidator
    {
        /// <inheritdoc/>
        public void Validate(List<AudioTag> tagSequence)
        {
            if (SequenceContainsDuplicateTags(tagSequence))
                throw new InvalidTagSequenceException("The sequence must not contain duplicate tags.");

            if (SequenceContainsAlbumTagNotInLastPosition(tagSequence))
                throw new InvalidTagSequenceException("The tag \"Album\" is not allowed if not in last position");
        }

        /// <summary>
        /// Method used in <see cref="Validate(List{AudioTag})"/>
        /// </summary>
        /// <param name="tagSequence"></param>
        /// <returns></returns>
        private static bool SequenceContainsAlbumTagNotInLastPosition(List<AudioTag> tagSequence)
        {
            return (tagSequence.Contains(AudioTag.Album)) && (tagSequence.IndexOf(AudioTag.Album) != tagSequence.Count - 1);
        }

        /// <summary>
        /// Method used in <see cref="Validate(List{AudioTag})"/>
        /// </summary>
        /// <param name="tagSequence"></param>
        /// <returns></returns>
        private static bool SequenceContainsDuplicateTags(List<AudioTag> tagSequence)
        {
            return tagSequence.Count != tagSequence.Distinct().Count();
        }
    }
}
