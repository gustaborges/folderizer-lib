namespace FolderizerLib.Audio
{
    /// <summary>
    /// Abstract class implemented by classes that organize audio files.
    /// </summary>
    public abstract class AudioOrganizer : Organizer
    {
        public AudioOrganizer()
        {
        }
        /// <inheritdoc/>
        public AudioOrganizer(string basePath) : base(basePath)
        {
        }
        /// <inheritdoc/>
        public AudioOrganizer(string basePath, string mountingPath) : base(basePath, mountingPath)
        {
        }
        /// <inheritdoc/>
        public AudioOrganizer(string basePath, string mountingPath, uint searchDepth) : base(basePath, mountingPath, searchDepth)
        {
        }
        /// <inheritdoc/>
        public AudioOrganizer(string basePath, string mountingPath, OperationMethod operationMethod) : base(basePath, mountingPath, operationMethod)
        {
        }
        /// <inheritdoc/>
        public AudioOrganizer(string basePath, string mountingPath, uint searchDepth, OperationMethod operationMethod) : base(basePath, mountingPath, searchDepth, operationMethod)
        {
        }

        public abstract void SetDesiredDirectoryStructure(params AudioTag[] tags);
    }
}
