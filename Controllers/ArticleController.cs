using Chronoshub.DAL;
using Chronoshub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chronoshub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private SQLiteDB m_db;
        public ArticleController(SQLiteDB db) { 
        
            m_db = db;
        }


        [Route("{id}")]
        [HttpGet]
        public async Task<Article?> GetArticleById(int id)
        {
            //Find the article
            var article = await m_db.Articles.SingleAsync(x => x.Id == id);

            //Load only authors for this article
            await m_db.Entry(article).Collection(x => x.Authors)
                .LoadAsync();


            //Find journal information for this article.
            await m_db.Entry(article).Reference(x => x.Journal)
                .LoadAsync();

            return article;
        }



    }
}
