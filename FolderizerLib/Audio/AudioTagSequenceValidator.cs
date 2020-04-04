using System.Collections.Generic;
using System.Linq;

namespace FolderizerLib.Audio
{
    static class AudioTagSequenceValidator
    {
        /// <summary>
        /// This method receives a list populated with <see cref="AudioTag"/> and evaluates whether it is or not a valid sequence.
        /// </summary>
        /// <param name="tagSequence"></param>
        /// <returns></returns>
        public static List<AudioTag> ValidateSequence(List<AudioTag> tagSequence)
        {
            if (SequenceContainsDuplicateTags(tagSequence))
                throw new InvalidTagSequenceException("The sequence must not contain duplicate tags.");

            if (SequenceContainsAlbumTagNotInLastPosition(tagSequence))
                throw new InvalidTagSequenceException("The tag \"Album\" is not allowed if not in last position");

            return tagSequence;
        }

        /// <summary>
        /// Method used in <see cref="ValidateSequence(List{AudioTag})"/>
        /// </summary>
        /// <param name="tagSequence"></param>
        /// <returns></returns>
        private static bool SequenceContainsAlbumTagNotInLastPosition(List<AudioTag> tagSequence)
        {
            return (tagSequence.Contains(AudioTag.Album)) && (tagSequence.IndexOf(AudioTag.Album) != tagSequence.Count - 1);
        }

        /// <summary>
        /// Method used in <see cref="ValidateSequence(List{AudioTag})"/>
        /// </summary>
        /// <param name="tagSequence"></param>
        /// <returns></returns>
        private static bool SequenceContainsDuplicateTags(List<AudioTag> tagSequence)
        {
            return tagSequence.Count != tagSequence.Distinct().Count();
        }


    }
}
