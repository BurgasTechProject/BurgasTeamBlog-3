using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Article
    {
        private ICollection<Reserve> Reservations;
        public Article()
        {
        }
        public Article(string authorId, string title, string content, int categoryId)
        {
            this.AuthorId = authorId;
            this.Title = title;
            this.Content = content;
            this.CategoryId = categoryId;
            this.Reservations = new HashSet<Reserve>();
            this.Date = DateTime.Now;   
        }

        [Key]
        public int Id { get; set; }

        [Required]
        //[MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        public string Content { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public bool IsUserAuthor(string username)
        {
            return this.Author.UserName.Equals(username);
        }

        public DateTime Date { get; set; }

        
        public virtual ICollection<Reserve> Reserv
        {
            get { return this.Reservations; }
            set { this.Reservations = value; }
        }
    }
}