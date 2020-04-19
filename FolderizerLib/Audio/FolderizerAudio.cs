using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FolderizerLib.Audio
{
    /// <summary>
    /// This class provides means to dynamically organize a directory's audio files according to a desired folder structure.
    /// </summary>
    public partial class FolderizerAudio : AudioOrganizer
    {
        /// <inheritdoc/>
        public FolderizerAudio() { }
        /// <inheritdoc/>
        public FolderizerAudio(string basePath) : base(basePath) { }
        /// <inheritdoc/>
        public FolderizerAudio(string basePath, string mountingPath) : base(basePath, mountingPath) { }
        /// <inheritdoc/>
        public FolderizerAudio(string basePath, string mountingPath, uint searchDepth) : base(basePath, mountingPath, searchDepth) { }
        /// <inheritdoc/>
        public FolderizerAudio(string basePath, string mountingPath, OperationMethod operationMethod) : base(basePath, mountingPath, operationMethod) { }
        /// <inheritdoc/>
        public FolderizerAudio(string basePath, string mountingPath, uint searchDepth, OperationMethod operationMethod) : base(basePath, mountingPath, searchDepth, operationMethod) { }


        #region Methods

        /// <summary>
        /// <para>Sets the desired organization structure which will be used to organize the audio files of the given directory.</para>
        /// <para>The exception <see cref="InvalidTagSequenceException" /> will be thrown if:</para>
        /// <list type="bullet">
        /// <item><description>If <see cref="AudioTag.Album"/> is present, but not in the last position of the organization sequence;</description></item>
        /// <item><description>If there are duplicates <see cref="AudioTag"/> values.</description></item>
        /// </list>
        /// <example>
        /// Usage example:
        /// <code>
        /// SetDesiredDirectoryStructure(AudioTag.Artist, AudioTag.Year, AudioTag.Album)
        /// </code>
        /// </example>
        /// </summary>
        public override void SetDesiredDirectoryStructure(params AudioTag[] tags)
        {
            TagsSequence = tags.ToList();
        }

        /// <inheritdoc/>
        public override ExecutionResult Organize()
        {
            var executionResult = new ExecutionResult();
            var files = Directory.EnumerateFiles(BasePath);
            StringBuilder finalFilePath = new StringBuilder();

            foreach (string file in files)
            {
                if (NotAudioFile(file))
                    continue;

                finalFilePath.Clear();

                // Generates the new file location based on the desired folder structure of the directory
                finalFilePath.Append(GenerateFinalLocation(filePath: file, MountingPath));

                // Creates the folder structure
                Directory.CreateDirectory(finalFilePath.ToString());

                // Appends the name of the file to the new path
                finalFilePath.Append($"\\{Path.GetFileName(file)}");

                // Effects move / copy operation
                try
                {
                    if (OperationMethod == OperationMethod.Move)
                        File.Move(sourceFileName: file, destFileName: finalFilePath.ToString());
                    else
                        File.Copy(sourceFileName: file, destFileName: finalFilePath.ToString());
                }
                catch(Exception ex)
                {
                    executionResult.LogException(new Error(file, ex.Message));
                }
            }
            return executionResult;
        }

        /// <inheritdoc/>
        public override string GenerateTreeView()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public partial class FolderizerAudio
    {
        private List<AudioTag> _tagsSequence;
        private static readonly string _supportedAudioFormats = ".aa|.aax|.aac|.aiff|.ape|.dsf|.flac|.m4a|.m4b|.m4p|.mp3|.mpc|.mpp|.ogg|.oga|.wav|.wma|.wv|.webm";


        #region Properties

        /// <summary>
        /// A sequence of tags whose order represents the desired directory structure. The value can be set by <see cref="SetDesiredDirectoryStructure"/>
        /// </summary>
        public List<AudioTag> TagsSequence
        {
            get => _tagsSequence;

            private set
            {
                try
                {
                    new AudioTagSequenceValidator().Validate(value);
                    _tagsSequence = value;
                }
                catch(InvalidTagSequenceException exception)
                {
                    throw exception;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generates the final location for the given <paramref name="filePath"/> according to the sequence set in <see cref="TagsSequence"/>.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="mountingPath"></param>
        private string GenerateFinalLocation(string filePath, string mountingPath)
        {
            string mountedPath = mountingPath;

            foreach (AudioTag tag in TagsSequence)
            {
                mountedPath += $"\\{ GetTagValueFromFile(tag, filePath) }";
            }
            return mountedPath;
        }

        /// <summary>
        /// Returns the file's correspondent meta value.
        /// </summary>
        /// <param name="currentFilePath"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetTagValueFromFile(AudioTag tag, string currentFilePath)
        {
            var file = TagLib.File.Create(currentFilePath);
            switch (tag)
            {
                case AudioTag.Album: return file.Tag.Album;
                case AudioTag.Artist: return file.Tag.FirstAlbumArtist;
                case AudioTag.Year: return file.Tag.Year.ToString();
                case AudioTag.Genre: return file.Tag.FirstGenre;
                default: return String.Empty;
            }
        }

        /// <summary>
        ///Evaluates if the path leads to an audio file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool NotAudioFile(string filePath)
        {
            return !_supportedAudioFormats.Contains(Path.GetExtension(filePath));
        }

        #endregion
    }
}
