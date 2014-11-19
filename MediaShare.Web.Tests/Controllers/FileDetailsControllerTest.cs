namespace MediaShare.Web.Tests.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Principal;
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using MediaShare.Data;
    using MediaShare.Models;
    using MediaShare.Web.Controllers;
    using MediaShare.Web.Infrastructure.Mapping;
    using MediaShare.Web.Models.Files;

    [TestClass]
    public class FileDetailsControllerTest
    {
        private IMediaShareData data;

        private Mock<IIdentity> identity;

        private MediaFile[] files;

        [TestInitialize]
        public void Init()
        {
            var mock = new MockRepository(MockBehavior.Default);
            this.identity = mock.Create<IIdentity>();
            
            this.identity.SetupGet(p => p.Name).Returns("asdasd");
         
            this.files = new MediaFile[]
            {
                new MediaFile()
                {
                    Id = 1,
                    Title = "Pesho",
                    Type = MediaType.Video,
                    DateCreated = new DateTime(2012,12,20),
                    Thumbnail = new byte[] { 100, 200, 30, 58, 60, 99 },
                    AuthorId = "asdasd",
                    Comments = new Comment[]
                    {
                        new Comment
                        {
                            Id = 21,
                            AuthorId = "dsasdagssad",
                            Content = "Opa",
                            DateCreated = new DateTime(2000,12,12),
                            MediaFileId = 1
                        },
                        new Comment
                        {
                            Id = 10,
                            AuthorId = "asdasd",
                            Content = "hahaha",
                            DateCreated = new DateTime(2000,12,13),
                            MediaFileId = 1,
                        }
                    },
                    Votes = new Vote[]
                    {
                        new Vote
                        {
                            Id = 1, MediaFileId = 1, Value = 4
                        },
                        new Vote
                        {
                            Id = 2, MediaFileId = 1, Value = 3
                        }
                    },
                    Content = new byte[] { 45, 43, 12, 56, 78 },
                    ViewsCount = 4,
                    Description = "Opaa"
                }
            };

            this.data = Telerik.JustMock.Mock.Create<IMediaShareData>();
            Telerik.JustMock
                   .Mock
                   .Arrange(() => this.data.Files.All())
                   .Returns(() => this.files.AsQueryable());
            Telerik.JustMock
                   .Mock
                   .Arrange(() => this.data.Comments.All())
                   .Returns(() => this.files[0].Comments.AsQueryable());
            Telerik.JustMock
                   .Mock
                   .Arrange(() => this.data.SaveChanges())
                   .Returns(() => this.files[0].ViewsCount);
            AutoMapperConfig.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetNonExistingFile_ShouldThrowException()
        {
            FileDetailsController controller = new FileDetailsController(this.data, this.identity.Object);
            // Act
            ViewResult result = controller.Details(5) as ViewResult;
        }

        [TestMethod]
        public void GetFileDetailsById_ShouldReturnFileDetails()
        {
            FileDetailsController controller = new FileDetailsController(this.data,this.identity.Object);

            // Act
            ViewResult result = controller.Details(1) as ViewResult;
            
            var res = result.ViewData.Model as MediaFileViewModel;
            Assert.AreEqual("Pesho", res.Title);
            Assert.AreEqual(MediaType.Video, res.Type);
            Assert.AreEqual(4, res.ViewsCount);
            Assert.AreEqual(2, res.Votes.Count);
        }
        
        [TestMethod]
        public void IncreaseViewsCount_ShouldIncreaseViews()
        {
            FileDetailsController controller = new FileDetailsController(this.data,this.identity.Object);
            var old = this.files[0].ViewsCount;
            controller.IncreaseViewCount(1);
                      
            Assert.AreEqual(old + 1, this.files[0].ViewsCount);
        }

        [TestMethod]
        public void GetFileContent_ShouldReturnRespectiveType()
        {
            FileDetailsController controller = new FileDetailsController(this.data,this.identity.Object);

            FileContentResult result = controller.AudioContentById(1) as FileContentResult;           
            var resContent = result.ContentType;
            var expectedContent = "audio/mp3";
            Assert.AreEqual(expectedContent, resContent);

            result = controller.VideoContentById(1) as FileContentResult;
            resContent = result.ContentType;
            expectedContent = "video/mp4";
            Assert.AreEqual(expectedContent, resContent);

            result = controller.VideoContentByIdWebM(1) as FileContentResult;
            resContent = result.ContentType;
            expectedContent = "video/webm";
            Assert.AreEqual(expectedContent, resContent);     
       
            var res = result.FileContents;
            var expected = this.files[0].Content;
            CollectionAssert.AreEquivalent(expected, res);
        }
    }
}