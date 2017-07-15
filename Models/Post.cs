using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MC_Forum.Models
{
    public class Post
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Post title is required.")]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "Post body is required.")]
        public string PostBody { get; set; }

        [Key]
        [ForeignKey("ID")]
        public int UserID { get; set; }
        public virtual UserAccount user { get; set; }
    }
}