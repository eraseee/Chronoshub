using Chronoshub.DAL;
using Chronoshub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chronoshub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private SQLiteDB m_db;
        public AuthorController(SQLiteDB db)
        {

            m_db = db;
        }


        [Route("{id}")]
        [HttpGet]
        public async Task<List<Article>?> GetArticlesByAuthorId(int id)
        {
            //Find the author from the id
            var author = await m_db.Authors.SingleAsync(x => x.Id == id);

            //Load the articles by this author
            await m_db.Entry(author).Collection(x => x.Articles).LoadAsync();

            //Potentially need a for/foreach loop here, to make sure all the data for the articles have been loaded.
            //Make this into a function, as it is copy pasted multiple places.
            foreach (var article in author.Articles)
            {
                //Load only authors for this article
                await m_db.Entry(article).Collection(x => x.Authors)
                    .LoadAsync();


                //Find journal information for this article.
                await m_db.Entry(article).Reference(x => x.Journal)
                    .LoadAsync();
            }


            //Return articles
            return author.Articles;
        }
    }
}
