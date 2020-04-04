
namespace FolderizerLib.Audio
{
    /// <summary>
    /// Provides an interface for <see cref="Folderizer"/> implementations that handle audio files, such as
    /// <see cref="FolderizerAudio"/>.
    /// </summary>
    public interface IAudioOrganizer
    {
        /// <summary>
        /// Sets the desired directory structure by passing the tags to be considered during organization.
        /// </summary>
        /// <param name="tags"></param>
        public void SetOrganizationSequence(params AudioTag[] tags);


    }
}
