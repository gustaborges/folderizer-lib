using System.IO;

namespace FolderizerLib
{
    /// <summary>
    /// <para>This is an abstract class.</para>
    /// <para>Take a look at: <see cref="FolderizerLib.Audio.FolderizerAudio"/></para>
    /// </summary>
    public abstract class Folderizer
    {
        #region Fields

        public static readonly int SearchDepthLimit = 5;
        private string _mountingPath;
        private string _basePath;
        private uint _maxSearchDepth = 0;

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Folderizer()
        {

        }

        /// <summary>
        ///Initializes a new instance with BasePath property defined. By default, <see cref="MountingPath"/> property is set to be the same as BasePath.
        /// </summary>
        /// <param name="basePath"></param>
        public Folderizer(string basePath)
        {
            BasePath = basePath;
            MountingPath = BasePath;
        }

        public Folderizer(string basePath, string mountingPath)
        {
            BasePath = basePath;
            MountingPath = mountingPath;
        }

        public Folderizer(string basePath, string mountingPath, uint searchDepth)
        {
            BasePath = basePath;
            MountingPath = mountingPath;
            MaxSearchDepth = searchDepth;
        }
        public Folderizer(string basePath, string mountingPath, OperationMethod operationMethod)
        {
            BasePath = basePath;
            MountingPath = mountingPath;
            OperationMethod = operationMethod;
        }

        public Folderizer(string basePath, string mountingPath, uint searchDepth, OperationMethod operationMethod)
        {
            BasePath = basePath;
            MountingPath = mountingPath;
            MaxSearchDepth = searchDepth;
            OperationMethod = operationMethod;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Represents the root directory in which the files to be organized are located.
        /// </summary>
        public string BasePath
        {
            get => _basePath ?? throw new BasePathNotDefinedException("The BasePath property has not been set.");
            set => _basePath = Directory.Exists(value) ? value : throw new DirectoryNotFoundException("The given path leads to an inexistent directory.");
        }

        /// <summary>
        /// <para>Represents the directory in which the organized files along with the desired folder structure will be put.</para>
        /// <para>When not set, mounting path will be the same as base path.</para>
        /// </summary>
        public string MountingPath
        {
            get => _mountingPath is null ? BasePath : _mountingPath;
            set => _mountingPath = value;
        }

        /// <summary>
        /// This property stores either <see cref="OperationMethod.Copy"/> or <see cref="OperationMethod.Move"/>, which defines how the files will be handled.
        /// </summary>
        public OperationMethod OperationMethod { get; set; } = OperationMethod.Copy;

        /// <summary>
        /// This property defines the depth of search in subdirectories. Maximum defined at <see cref="Folderizer.SearchDepthLimit"/>
        /// </summary>
        public uint MaxSearchDepth
        {
            get => _maxSearchDepth;
            set
            {
                if (value > SearchDepthLimit)
                {
                    throw new SearchDepthExceedsAcceptableLimitException($"The search depth exceeds the acceptable threshold of {SearchDepthLimit} subdirectories.");
                }
                _maxSearchDepth = value;
            }
        }

        #endregion
        
        #region Abstract Methods

        /// <summary>
        /// Organizes the directory generating the desired directory structure.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Generates a JSON object mapped with the mounted folder structure.
        /// </summary>
        public abstract void GenerateTreeView();

        #endregion
    }
}

