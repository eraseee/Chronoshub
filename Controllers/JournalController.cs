using Chronoshub.DAL;
using Chronoshub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chronoshub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {

        private SQLiteDB m_db;
        public JournalController(SQLiteDB db)
        {

            m_db = db;
        }


        [Route("{id}")]
        [HttpGet]
        public async Task<List<Article>?> GetArticlesByJournalId(int id)
        {
            var journal = await m_db.Journals.SingleAsync(x => x.Id == id);

            await m_db.Entry(journal).Collection(x => x.Articles).LoadAsync();


            //Potentially need a for/foreach loop here, to make sure all the data for the articles have been loaded.
            //Make this into a function, as it is copy pasted multiple places.
            foreach (var article in journal.Articles)
            {
                //Load only authors for this article
                await m_db.Entry(article).Collection(x => x.Authors)
                    .LoadAsync();


                //Find journal information for this article.
                await m_db.Entry(article).Reference(x => x.Journal)
                    .LoadAsync();
            }

            return journal.Articles;
        }
    }
}
