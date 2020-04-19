using FolderizerLib;
using FolderizerLib.Audio;
using NUnit.Framework;
using System.IO;

namespace FolderizerLibTest.UnitTests
{
    class AudioFolderizer_Configurations_Test
    {
        private FolderizerAudio audioFolderizer;

        [SetUp]
        public void Setup()
        {
            audioFolderizer = new FolderizerLib.Audio.FolderizerAudio();
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

        #region SetDesiredDirectoryStructure Tests

        [Test]
        public void SetOrganizationSequence_WhenProvidedASingleTag_TagSequencePropertyShouldContainTheGivenTag()
        {
            audioFolderizer.SetDesiredDirectoryStructure(AudioTag.Album);
            Assert.True(audioFolderizer.TagsSequence.Contains(AudioTag.Album));
        }

        [Test]
        public void SetOrganizationSequence_WhenProvidedAValidSequenceOfTwoTags_TagSequencePropertyShouldContainTheGivenSequence()
        {
            audioFolderizer.SetDesiredDirectoryStructure(AudioTag.Genre, AudioTag.Album);
            Assert.True(audioFolderizer.TagsSequence[0] == AudioTag.Genre);
            Assert.True(audioFolderizer.TagsSequence[1] == AudioTag.Album);
        }

        [Test]
        public void SetOrganizationSequence_WhenProvidedDuplicateTags_ShouldThrowInvalidTagSequenceException()
        {
            Assert.Throws(typeof(InvalidTagSequenceException), () => audioFolderizer.SetDesiredDirectoryStructure(AudioTag.Genre, AudioTag.Genre));
        }

        [Test]
        public void SetOrganizationSequence_WhenAlbumTagIsNotLocatedInLastPosition_ShouldThrowInvalidTagSequenceException()
        {
            Assert.Throws(typeof(InvalidTagSequenceException), () => audioFolderizer.SetDesiredDirectoryStructure(AudioTag.Album, AudioTag.Year, AudioTag.Artist));
        }

        [Test]
        public void SetOrganizationSequence_WhenAlbumTagIsPresentInLastPosition_ShouldNotThrowInvalidTagSequenceException()
        {
            audioFolderizer.SetDesiredDirectoryStructure(AudioTag.Year, AudioTag.Artist, AudioTag.Album);
        }
        #endregion

        
    }
}
