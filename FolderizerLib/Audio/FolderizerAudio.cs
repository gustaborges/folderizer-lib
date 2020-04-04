using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FolderizerLib.Audio
{
    public partial class FolderizerAudio : Folderizer, IAudioOrganizer
    {
        #region Constructors

        public FolderizerAudio() : base()
        {
        }

        public FolderizerAudio(string basePath) : base(basePath)
        {
        }

        public FolderizerAudio(string basePath, string mountingPath) : base(basePath, mountingPath)
        {
        }

        public FolderizerAudio(string basePath, string mountingPath, uint searchDepth) : base(basePath, mountingPath, searchDepth)
        {
        }

        public FolderizerAudio(string basePath, string mountingPath, OperationMethod operationMethod) : base(basePath, mountingPath, operationMethod)
        {
        }

        public FolderizerAudio(string basePath, string mountingPath, uint searchDepth, OperationMethod operationMethod) : base(basePath, mountingPath, searchDepth, operationMethod)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// <para>Sets the desired organization structure which will be used to organize the audio files of the given directory.</para>
        /// <para>The exception <see cref="InvalidTagSequenceException" /> will be thrown if:</para>
        /// <list type="bullet">
        /// <item><description>When used, <see cref="AudioTag.Album"/> is not placed at the last position of the organization sequence;</description></item>
        /// <item><description>There are duplicates <see cref="AudioTag"/>.</description></item>
        /// </list>
        /// <example>
        /// Correct usage example:
        /// <code>
        /// AudioFolderizer.SetOrganizationSequence(AudioTags.Year, AudioTags.Artist, AudioTags.Album);
        /// </code>
        /// </example>
        /// 
        /// </summary>
        public void SetOrganizationSequence(params AudioTag[] tags)
        {
            TagsSequence = tags.ToList();
        }

        /// <inheritdoc/>
        public override void Execute()
        {
            var files = Directory.EnumerateFiles(BasePath);
            StringBuilder finalFilePath = new StringBuilder();

            foreach (string file in files)
            {
                if (NotAudioFile(file))
                    continue;

                // Clears StringBuilder
                finalFilePath.Clear();
                
                // Generates the new file location based on the tags informed.
                finalFilePath.Append(GenerateMountedPath(file, MountingPath));
                
                // Creates the new file location.
                Directory.CreateDirectory(finalFilePath.ToString());
                
                // Appends the name of the file to the new path
                finalFilePath.Append($"\\{Path.GetFileName(file)}");

                // Effects move / copy operation
                if (OperationMethod == OperationMethod.Move)
                    File.Move(sourceFileName: file, destFileName: finalFilePath.ToString());
                else
                    File.Copy(sourceFileName: file, destFileName: finalFilePath.ToString());
            }
        }



        /// <inheritdoc/>
        public override void GenerateTreeView()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    /// <summary>
    /// This class provides means to dynamically organize audio files from a directory into a given folder structure.
    /// </summary>
    public partial class FolderizerAudio
    {
        private List<AudioTag> _tagsSequence;

        #region Properties

        /// <summary>
        /// Provides access to the organization sequence defined in <see cref="SetOrganizationSequence"/>
        /// </summary>
        public List<AudioTag> TagsSequence
        {
            get
            {
                return _tagsSequence;
            }
            private set
            {
                _tagsSequence = AudioTagSequenceValidator.ValidateSequence(value);
            }
        }

        #endregion


        #region Methods

        /// <summary>
        /// Generates the final location for the given <paramref name="filePath"/> according to the sequence set in <see cref="TagsSequence"/>.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="mountingPath"></param>
        private string GenerateMountedPath(string filePath, string mountingPath)
        {
            string mountedPath = mountingPath;

            foreach (AudioTag tag in TagsSequence)
            {
                mountedPath += $"\\{ GetTagValue(tag, filePath) }";
            }
            return mountedPath;
        }

        /// <summary>
        /// Gets the value of the file's given tag value.
        /// </summary>
        /// <param name="currentFilePath"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string GetTagValue(AudioTag tag, string currentFilePath)
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
        ///Evaluates if the file is an audio file whose format is supported by <see cref="FolderizerAudio"/>.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool NotAudioFile(string filePath)
        {
            return !SupportedAudioFormats.Include(format: Path.GetExtension(filePath));
        }

        #endregion

        private static class SupportedAudioFormats
        {
            private static readonly string supportedFormats = ".aa|.aax|.aac|.aiff|.ape|.dsf|.flac|.m4a|.m4b|.m4p|.mp3|.mpc|.mpp|.ogg|.oga|.wav|.wma|.wv|.webm";

            /// <summary>
            /// Verifies if the given format is supported as audio file.
            /// </summary>
            public static bool Include(string format)
            {
                return supportedFormats.Contains(format);
            }
        }
    }
}
