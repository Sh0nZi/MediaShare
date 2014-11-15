namespace MediaShare.Web.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using AutoMapper.QueryableExtensions;
    using Telerik.JustMock;

    using MediaShare.Data;
    using MediaShare.Models;
    using MediaShare.Web.Controllers;
    using MediaShare.Web.Infrastructure.Mapping;
    using MediaShare.Web.Models.Files;

    [TestClass]
    public class HomeControllerTest
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
                    }
                },
                new MediaFile()
                {
                    Id = 2,
                    Title = "Gosho",
                    Type = MediaType.Video,
                    DateCreated = new DateTime(2012,12,20),
                    Votes = new Vote[]
                    {
                        new Vote
                        {
                            Id = 3, MediaFileId = 2, Value = 2
                        },
                        new Vote
                        {
                            Id = 4, MediaFileId = 2, Value = 3
                        }
                    }
                },
                new MediaFile()
                {
                    Id = 3,
                    Title = "4alga",
                    Type = MediaType.Audio,
                    DateCreated = new DateTime(2012,12,20),
                    Votes = new Vote[]
                    {
                        new Vote
                        {
                            Id = 1, MediaFileId = 3, Value = 4
                        },
                        new Vote
                        {
                            Id = 2, MediaFileId = 3, Value = 3
                        }
                    }
                },
                new MediaFile()
                {
                    Id = 4,
                    Title = "Ivan",
                    Type = MediaType.Audio,
                    DateCreated = new DateTime(2012,12,20),
                    Votes = new Vote[]
                    {
                        new Vote
                        {
                            Id = 3, MediaFileId = 4, Value = 2
                        },
                        new Vote
                        {
                            Id = 4, MediaFileId = 4, Value = 3
                        }
                    }
                }
            };

            this.data = Mock.Create<IMediaShareData>();
            Mock.Arrange(() => this.data.Files.All())
                .Returns(() => this.files.AsQueryable());
       
            AutoMapperConfig.Execute();
        }

        [TestMethod]
        public void GetTopVideoFiles_ShouldReturnCollectionOfTopVideoFiles()
        {
            HomeController controller = new HomeController(this.data);

            // Act
            PartialViewResult result = controller.GetTopVideo() as PartialViewResult;

            // Assert
            var res = result.ViewData.Model as List<BasicMediaFileViewModel>;
            var expected = this.files.AsQueryable()
                               .Project()
                               .To<BasicMediaFileViewModel>()
                               .Where(f => f.Type == MediaType.Video)
                               .OrderByDescending(f => (double)f.Votes.Sum(v => v.Value) / f.Votes.Count)
                               .ThenBy(f => f.Votes.Count)
                               .Take(6)
                               .ToList();
            CollectionAssert.AreEquivalent(expected, res);
        }
        
        [TestMethod]
        public void GetTopAudioFiles_ShouldReturnCollectionOfTopAudioFiles()
        {
            HomeController controller = new HomeController(this.data);

            // Act
            PartialViewResult result = controller.GetTopAudio() as PartialViewResult;

            // Assert
            var res = result.ViewData.Model as List<BasicMediaFileViewModel>;
            var expected = this.files.AsQueryable()
                               .Project()
                               .To<BasicMediaFileViewModel>()
                               .Where(f => f.Type == MediaType.Audio)
                               .OrderByDescending(f => (double)f.Votes.Sum(v => v.Value) / f.Votes.Count)
                               .ThenBy(f => f.Votes.Count)
                               .Take(6)
                               .ToList();
            CollectionAssert.AreEquivalent(expected, res);
        }
    }
}