using FolderizerLib.Audio;
using FolderizerLib;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections;
using System.IO;
using System.Linq;
using System;

namespace FolderizerLibTest.UnitTests
{
    class AudioFolderizer_Organize_Test
    {
        private FolderizerAudio folderizerAudio;

        [SetUp]
        public void Setup()
        {
            folderizerAudio = new FolderizerLib.Audio.FolderizerAudio();
            
            DeleteBasePath();
            PopulateBasePath();
            
            DeleteMountingPath();
            
            folderizerAudio.BasePath = TestPaths.ValidBasePath;
        }


        #region Tests of Organize() when MountingPath equals to BasePath

        [Test]
        public void Organize_WhenBasePathEqualToMountingPath_BasePathIsValid_OneTagCriteria_ShouldResultProperDirectoryStructureInBasePath()
        {
            folderizerAudio.SetDesiredDirectoryStructure(AudioTag.Artist);
            folderizerAudio.Organize();

            AssertDirectoryStructureArtist();
        }

        [Test]
        public void Organize_WhenBasePathEqualToMountingPath_BasePathIsValid_TwoTagsCriteria_ShouldResultProperDirectoryStructureInBasePath()
        {
            folderizerAudio.SetDesiredDirectoryStructure(AudioTag.Artist, AudioTag.Album);
            folderizerAudio.Organize();

            AssertDirectoryStructureArtistAlbum();
        }

        [Test]
        public void Organize_WhenBasePathBecomesInvalidBeforeExecution_ShouldThrowDirectoryNotFoundException()
        {
            folderizerAudio.SetDesiredDirectoryStructure(AudioTag.Artist, AudioTag.Album);
            DeleteBasePath();
            Assert.Throws<DirectoryNotFoundException>(()=> { folderizerAudio.Organize(); });
        }

        #endregion

        #region Tests of Organize() when MountingPath differs from BasePath
        [Test]
        public void Organize_WhenMountingPathNotEqualBasePath_OneTagCriteria_ShouldResultProperDirectoryStructureInMountingPath()
        {
            folderizerAudio.SetDesiredDirectoryStructure(AudioTag.Artist);
            folderizerAudio.Organize();

            AssertDirectoryStructureArtist();
        }

        private void AssertDirectoryStructureArtist()
        {
            var distinctArtists = TestAudioFiles.Files.Select((f) => f.AlbumArtist).Distinct();
            foreach (var artist in distinctArtists)
                Assert.True(Directory.Exists($"{folderizerAudio.MountingPath}\\{artist}"));
        }

        [Test]
        public void Organize_WhenMountingPathNotEqualBasePath_TwoTagsCriteria_ShouldResultProperDirectoryStructureInMountingPath()
        {
            folderizerAudio.MountingPath = TestPaths.ValidMountingPath;
            folderizerAudio.SetDesiredDirectoryStructure(AudioTag.Artist, AudioTag.Album);
            folderizerAudio.Organize();

            AssertDirectoryStructureArtistAlbum();
        }


        [Test]
        public void Organize_WhenSetToMoveFiles_ShouldResultProperDirectoryStructureInBasePath()
        {
            folderizerAudio.OperationMethod = OperationMethod.Move;
            folderizerAudio.MountingPath = TestPaths.ValidMountingPath;
            folderizerAudio.SetDesiredDirectoryStructure(AudioTag.Year, AudioTag.Album);
            folderizerAudio.Organize();

            AssertDirectoryStructureYearAlbum();
            AssertFilesHaveBeenMovedFromBasePath();
        }

        [Test]
        public void Organize_WhenExceptionOccursWhileOperatingOverFiles_ShouldReturnExecutionResultWithFailureLogged()
        {
            var fileWithForcedError = TestAudioFiles.Files[0];
            var fileWithForcedErrorPath = $"{folderizerAudio.BasePath}\\{fileWithForcedError.Name}{fileWithForcedError.Format}";

            using (var stream = File.OpenRead(fileWithForcedErrorPath))
            {
                folderizerAudio.MountingPath = TestPaths.ValidMountingPath;
                folderizerAudio.OperationMethod = OperationMethod.Move;
                folderizerAudio.SetDesiredDirectoryStructure(AudioTag.Year, AudioTag.Album);
                
                ExecutionResult executionResult = folderizerAudio.Organize();
                
                Assert.True(executionResult.Errors.Length > 0);
                Assert.True(executionResult.Errors[0].FilePath.Equals(fileWithForcedErrorPath));
            }
        }

        #endregion

        private void PopulateBasePath()
        {
            IEnumerable rootTestFolderFiles = Directory.EnumerateFiles(TestPaths.RootTestFolderPath);

            foreach (string file in rootTestFolderFiles)
            {
                try
                {
                    string fileDestinationName = file.Split("\\").Last();
                    File.Copy(file, Path.Combine(TestPaths.ValidBasePath, fileDestinationName));
                }
                catch { return; }
            }
        }

        private void DeleteBasePath()
        {
            if (Directory.Exists(TestPaths.ValidBasePath))
                Directory.Delete(TestPaths.ValidBasePath, true);
        }

        private void DeleteMountingPath()
        {
            if (Directory.Exists(TestPaths.ValidMountingPath))
                Directory.Delete(TestPaths.ValidMountingPath, true);
        }


        private void AssertFilesHaveBeenMovedFromBasePath()
        {
            foreach (var file in TestAudioFiles.Files)
            {
                Assert.False(File.Exists($"{folderizerAudio.BasePath}\\{file.Name}{file.Format}"));
            }
        }

        private void AssertDirectoryStructureArtistAlbum()
        {
            var distinctArtists = TestAudioFiles.Files.Select((f) => f.AlbumArtist).Distinct();

            foreach (var artist in distinctArtists)
            {
                var albumsOfTheArtist = TestAudioFiles.Files
                   .Where((f) => f.AlbumArtist.Equals(artist))
                   .Select((f) => f.Album)
                   .Distinct();

                foreach (var album in albumsOfTheArtist)
                    Assert.True(Directory.Exists($"{folderizerAudio.MountingPath}\\{artist}\\{album}"));
            }
        }

        private void AssertDirectoryStructureYearAlbum()
        {
            var distinctYears = TestAudioFiles.Files.Select((f) => f.Year).Distinct();

            foreach (var year in distinctYears)
            {
                var albumsOfTheYear = TestAudioFiles.Files
                   .Where((f) => f.AlbumArtist.Equals(year))
                   .Select((f) => f.Album)
                   .Distinct();

                foreach (var album in albumsOfTheYear)
                {
                    Assert.True(Directory.Exists($"{folderizerAudio.MountingPath}\\{year}\\{album}"));
                }
            }
        }

    }
}
