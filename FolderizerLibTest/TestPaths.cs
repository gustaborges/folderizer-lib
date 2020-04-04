using System;
using System.IO;

namespace FolderizerLibTest
{
    /// <summary>
    /// This struct provides valid and invalid directory paths for the execution of unit tests.
    /// </summary>
    public readonly struct TestPaths
    {

        /*  WARNING! 
         *  Before executing any test, make sure to define below the proper location of the test directory.
         *  Make sure that there's no existent directory that is of your use in the path defined in root path,
         *  because it will be used to manipulate folders and execute delete / creation operations.
         *  
         *  Currently, the folder {FolderizerLibTest} will be created under {ProgramFiles} directory.
         *  
         *  Feel free to change the whole root directory path or choose a more proper location by setting other
         *  Environment.SpecialFolder enum value.
         */
        private static string __baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        private static string _testFolderPath = Path.Combine(__baseDirectory, "FolderizerLibTest"); // FolderizerLibTest folder will be created in MyMusic.

        /* There's no need to modify the following paths */
        private static string _validBasePath   = Path.Combine(_testFolderPath, "Folderizer-Valid-Base-Path");
        private static string _validmountingPath = Path.Combine(_testFolderPath, "Folderizer-Valid-Mounting-Path");
        private static string _inexistentDirectoryPath = Path.Combine(_testFolderPath, "Invalid-Base-Path");

        /// <summary>
        /// <para>Provides the path of the root test folder, where files can be put to test the library. These files will be copied to the BasePath folder at runtime.</para>
        /// <para>See: <see cref="ValidBasePath"/></para>
        /// <para>By default, RootTestFolderPath points to: <c>MyMusic/FolderizerLibTest</c></para>
        /// </summary>
        public static string RootTestFolderPath
        {
            get
            {
                Directory.CreateDirectory(_testFolderPath);
                return _testFolderPath;
            }
        }
        /// <summary>
        /// <para>Provides the path of an existing directory, set in this property's private field.</para>
        /// <para>In runtime, the getter creates the directory pointed in it's respective private field, 
        /// if it doesn't exist</para>
        /// <para>By default: <c>MyMusic/FolderizerLibTest/Folderizer-Valid-Base-Path</c></para>
        /// </summary>
        public static string ValidBasePath
        {
            get
            {
                Directory.CreateDirectory(_validBasePath);
                return _validBasePath;
            }
        }

        /// <summary>
        /// <para>Provides the path of an existing directory, set in this property's private field.</para>
        /// <para>In runtime, the getter creates the directory pointed in it's respective private field, 
        /// if it doesn't exist.</para>
        /// <para>By default: </para>
        /// </summary>
        public static string ValidMountingPath
        {
            get
            {
                Directory.CreateDirectory(_validmountingPath);
                return _validmountingPath;
            }
        }

        /// <summary>
        /// <para>Provides the path of an inexistent directory.</para>
        /// </summary>
        public static string NotCreatedDirectory
        {
            get
            {
                if (Directory.Exists(_inexistentDirectoryPath))
                {
                    Directory.Delete(_inexistentDirectoryPath);
                }
                return _inexistentDirectoryPath;
            }
        }
            


    }
}