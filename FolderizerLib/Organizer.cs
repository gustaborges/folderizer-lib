using System.IO;

namespace FolderizerLib
{
    /// <summary>
    /// Abstract class implemented by classes that organize files.
    /// </summary>
    public abstract class Organizer
    {
        #region Fields

        private string _mountingPath;
        private string _basePath;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Organizer()
        {

        }

        /// <summary>
        ///Initializes a new instance with <see cref="BasePath"/> property defined. By default, <see cref="MountingPath"/> property is set to be the same as <see cref="BasePath"/>.
        /// </summary>
        /// <param name="basePath"></param>
        public Organizer(string basePath)
        {
            BasePath = basePath;
            MountingPath = BasePath;
        }

        public Organizer(string basePath, string mountingPath)
        {
            BasePath = basePath;
            MountingPath = mountingPath;
        }

        public Organizer(string basePath, string mountingPath, uint searchDepth)
        {
            BasePath = basePath;
            MountingPath = mountingPath;
            MaxSearchDepth = searchDepth;
        }

        public Organizer(string basePath, string mountingPath, OperationMethod operationMethod)
        {
            BasePath = basePath;
            MountingPath = mountingPath;
            OperationMethod = operationMethod;
        }

        public Organizer(string basePath, string mountingPath, uint searchDepth, OperationMethod operationMethod)
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
        /// <para>Represents the directory in which desired folder structure along with the organized files will be located.</para>
        /// <para>When not set, mounting path equals to base path.</para>
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
        /// This property defines the depth of search in subdirectories. The default is zero.
        /// </summary>
        public uint MaxSearchDepth { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Results in the creation of the specified folder structure and organization of the files located in the base path.
        /// </summary>
        public abstract ExecutionResult Organize();

        /// <summary>
        /// Returns a Json string reflecting a preview of the resulting directory structure.
        /// </summary>
        public abstract string GenerateTreeView();
        #endregion
    }
}

