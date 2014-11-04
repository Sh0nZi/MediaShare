﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }
        
        public int Value { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int FileId { get; set; }

    }
}