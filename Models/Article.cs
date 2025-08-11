using System.ComponentModel.DataAnnotations.Schema;

namespace Chronoshub.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Content { get; set; }


        //List of authors potentially on the article.
        public List<Author> Authors { get; } = [];


        //[ForeignKey]
        public int JournalId { get; set; }
        public Journal Journal { get; set; }

        public Article() { }
    }
}
