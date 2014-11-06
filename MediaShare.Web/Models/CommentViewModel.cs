using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MediaShare.Models;
using System.Linq.Expressions;

namespace MediaShare.Web.Models
{
    public class CommentViewModel
    {
    
        public static Expression<Func<Comment, CommentViewModel>> FromComment
        {
            get
            {
                return c => new CommentViewModel
                {
                    Content = c.Content,
                    Id = c.Id,
                    DateCreated = c.DateCreated,
                    Author = c.Author.UserName,
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [Range(10, 300)]
        public string Content { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Author { get; set; }
    }
}