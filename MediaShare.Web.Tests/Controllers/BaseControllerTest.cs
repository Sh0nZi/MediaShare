namespace MediaShare.Web.Tests.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Telerik.JustMock;

    using MediaShare.Data;
    using MediaShare.Models;
    using MediaShare.Web.Controllers;

    [TestClass]
    public class BaseControllerTest
    {
        private IMediaShareData data;

        private MediaFile[] files;

        [TestInitialize]
        public void Init()
        {
            this.files = new MediaFile[]
            {
                new MediaFile()
                {
                    Id = 1,
                    Title = "Pesho",
                    Type = MediaType.Video,
                    DateCreated = new DateTime(2012,12,20),
                    Thumbnail = ""
                }
            };

            this.data = Mock.Create<IMediaShareData>();
            Mock.Arrange(() => this.data.Files.All())
                .Returns(() => this.files.AsQueryable());
        }

        [TestMethod]
        public void GetFileThumbnail_ShouldReturnJpegFile()
        {
            BaseController controller = new HomeController(this.data);

            // Act
            FileContentResult result = controller.ByIdThumbnail(1) as FileContentResult;

            // Assert
            var res = result.FileContents;
            var expected = this.files[0].Thumbnail;
            var resContent = result.ContentType;
            var expectedContent = "image/jpeg";

            CollectionAssert.AreEquivalent(expected, res);
            Assert.AreEqual(expectedContent, resContent);
        }
    }
}