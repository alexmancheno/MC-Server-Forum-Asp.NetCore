using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data;

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

        public int PostTopic { get; set; } 

        public DateTime DatePosted { get; set; }

        [Key]
        [ForeignKey("ID")]
        public int UserID { get; set; }
        public virtual UserAccount user { get; set; }

        // public Post(DataRow row)
        // {
        //     ID = (int)row[0];
        //     PostTitle = (string)row[1];
        //     PostBody = (string)row[2];
        //     PostTopic = (int)row[3];
        //     // Missing DatePosted field!!!
        //     UserID = (int)row[4];
        // }
    }
}