using FolderizerLib;
using FolderizerLib.Audio;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using static System.Environment;

namespace FolderizerLibTest.UnitTests
{
    class AudioFolderizerTest
    {
        private FolderizerAudio audioFolderizer;

        public AudioFolderizerTest()
        {
            DirectoryInfo i = new DirectoryInfo(TestPaths.ValidBasePath);
            // Copying the test files to the directory to be used as base path, if they haven't been yet.
            foreach (string file in Directory.EnumerateFiles(TestPaths.RootTestFolderPath))
            {            
                try
                { 
                    string fileDestinationName = file.Split("\\").Last();
                    File.Copy(file, Path.Combine(TestPaths.ValidBasePath, fileDestinationName));
                }
                catch (Exception e) { return;  }
            }
        }

        [SetUp]
        public void Setup()
        {
            audioFolderizer = new FolderizerLib.Audio.FolderizerAudio();
            //
            Directory.Delete(TestPaths.ValidMountingPath);
        }

        #region SetBasePath Tests
        [Test]
        public void SetBasePath_WhenExistentDirectory_BasePathPropertyShouldBeSet()
        {
            audioFolderizer.BasePath = TestPaths.ValidBasePath;
            Assert.AreEqual(TestPaths.ValidBasePath, audioFolderizer.BasePath);
        }

        [Test]
        public void SetBasePath_WhenNotExistentDirectory_ShouldThrowDirectoryNotFoundException()
        {
            Assert.Throws<DirectoryNotFoundException>(() => audioFolderizer.BasePath = "");
        }
        #endregion

        #region SetMountingPath Tests
        [Test]
        public void SetMountingPath_WhenNotSet_ShouldBeSameAsBasePath()
        {
            audioFolderizer.MountingPath = TestPaths.ValidMountingPath;
            Assert.AreEqual(TestPaths.ValidMountingPath, audioFolderizer.MountingPath);
        }

        [Test]
        public void SetMountingPath_WhenExistentDirectory_mountingPathPropertyShouldBeSet()
        {
            audioFolderizer.MountingPath = TestPaths.ValidMountingPath;
            Assert.AreEqual(TestPaths.ValidMountingPath, audioFolderizer.MountingPath);
        }


        [Test]
        public void SetMountingPath_WhenNotExistentDirectory_mountingPathPropertyShouldBeSet()
        {
            audioFolderizer.MountingPath = TestPaths.NotCreatedDirectory;
            Assert.AreEqual(TestPaths.NotCreatedDirectory, audioFolderizer.MountingPath);
        }
        #endregion

        #region SetOperationMethod Tests
        [Test]
        public void SetOperationMethod_WhenDifferentFromDefaultMethod_OperationMethodPropertyShouldBeSet()
        {
            audioFolderizer.OperationMethod = OperationMethod.Move;
            Assert.AreEqual(OperationMethod.Move, audioFolderizer.OperationMethod);
        }

        [Test]
        public void SetOperationMethod_WhenNotSet_OperationMethodPropertyShouldBeCopy()
        {
            Assert.AreEqual(OperationMethod.Copy, audioFolderizer.OperationMethod);
        }

        #endregion

        #region SetMaxSearchDepth Tests
        [Test]
        public void SetMaxSearchDepth_WhenDepthNotSet_DepthShouldBeZero()
        {
            Assert.AreEqual(0, audioFolderizer.MaxSearchDepth);
        }

        [Test]
        public void SetMaxSearchDepth_WhenDepthBiggerThanFive_ShouldThrowSearchDepthExceedsAcceptableLimitException()
        {
            Assert.Throws<SearchDepthExceedsAcceptableLimitException>(() => audioFolderizer.MaxSearchDepth = 6);
        }

        [Test]
        public void SetMaxSearchDepth_WhenDepthLesserThanOrEqualToFive_SearchDepthPropertyShouldBeSet()
        {
            audioFolderizer.MaxSearchDepth = 5;
            Assert.AreEqual(5, audioFolderizer.MaxSearchDepth);
        }

        #endregion

        #region SetOrganizationSequence Tests
        [Test]
        public void SetOrganizationSequence_WhenProvidedASingleTag_TagSequencePropertyShouldContainTheGivenTag()
        {
            audioFolderizer.SetOrganizationSequence(AudioTag.Album);
            Assert.True(audioFolderizer.TagsSequence.Contains(AudioTag.Album));
        }

        [Test]
        public void SetOrganizationSequence_WhenProvidedAValidSequenceOfTwoTags_TagSequencePropertyShouldContainTheGivenSequence()
        {
            audioFolderizer.SetOrganizationSequence(AudioTag.Genre, AudioTag.Album);
            Assert.True(audioFolderizer.TagsSequence[0] == AudioTag.Genre);
            Assert.True(audioFolderizer.TagsSequence[1] == AudioTag.Album);
        }

        [Test]
        public void SetOrganizationSequence_WhenProvidedDuplicateTags_ShouldThrowInvalidTagSequenceException()
        {
            Assert.Throws(typeof(InvalidTagSequenceException), () => audioFolderizer.SetOrganizationSequence(AudioTag.Genre, AudioTag.Genre));
        }

        [Test]
        public void SetOrganizationSequence_WhenAlbumTagIsNotLocatedInLastPosition_ShouldThrowInvalidTagSequenceException()
        {
            Assert.Throws(typeof(InvalidTagSequenceException), () => audioFolderizer.SetOrganizationSequence(AudioTag.Album, AudioTag.Year, AudioTag.Artist));
        }

        [Test]
        public void SetOrganizationSequence_WhenAlbumTagIsPresentInLastPosition_ShouldNotThrowInvalidTagSequenceException()
        {
            audioFolderizer.SetOrganizationSequence(AudioTag.Year, AudioTag.Artist, AudioTag.Album);
        }
        #endregion

        #region Execute() Tests

        [Test]
        public void Execute_WhenValidBasePathAndCriteriaOneTag_ShouldResultProperDirectoryStructure()
        {
            audioFolderizer.BasePath = TestPaths.ValidBasePath;
            audioFolderizer.MountingPath = Environment.GetFolderPath(SpecialFolder.MyMusic);

            audioFolderizer.SetOrganizationSequence(AudioTag.Artist, AudioTag.Album);
            audioFolderizer.Execute();

        }

        #endregion
    }
}
