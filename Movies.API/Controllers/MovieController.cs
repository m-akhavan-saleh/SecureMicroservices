using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.Model;

namespace Movies.API.Controllers
{
    [Route(Constant.Movies_Api_Route_Name)]
    [ApiController]
    //[Authorize(Constant.Client_Id_Policy)]
    [Authorize]
    public class MovieController : ControllerBase
    {
        /// <summary>
        /// تعریف یک آبجک از نوع کانتکس
        /// جهت ارتباط با بانک اطلاعاتی
        /// </summary>
        private readonly MoviesContext _context;

        /// <summary>
        /// تعریف سازنده پیش فرض
        /// </summary>
        /// <param name="context"></param>
        public MovieController(MoviesContext context)
        {
            _context = context;

            MoviesContextSeed.Seed(_context);
        }

        /// <summary>
        /// متد دریافت اطلاعات فیلم ها
        /// </summary>
        /// <returns>لیست فیلم</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        /// <summary>
        /// متد دریافت اطلاعات فیلم
        /// </summary>
        /// <param name="id">شناسه فیلم</param>
        /// <returns>اطلاعات فیلم</returns>
        [HttpGet(Constant.Movies_Api_Route_Id)]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        /// <summary>
        /// متد ذخیره سازی اطلاعات فیلم
        /// </summary>
        /// <param name="movie">اطلاعات فیلم</param>
        /// <returns>نتیجه</returns>
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);

            await _context.SaveChangesAsync();

            return CreatedAtAction(Constant.Movies_Api_Action_Get_Movie, new { id = movie.Id }, movie);
        }

        /// <summary>
        /// متد بروز رسانی اطلاعات فیلم
        /// </summary>
        /// <param name="id">شناسه فیلم</param>
        /// <param name="movie">اطلاعات فیلم</param>
        /// <returns>نتیجه</returns>
        [HttpPut(Constant.Movies_Api_Route_Id)]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// متد مربوط به حذف اطلاعات فیلم
        /// </summary>
        /// <param name="id">شناسه فیلم</param>
        /// <returns>نتیجه</returns>
        [HttpDelete(Constant.Movies_Api_Route_Id)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// متد بررسی وجود اطلاعات فیلم
        /// </summary>
        /// <param name="id">شناسه فیلم</param>
        /// <returns>نتیجه</returns>
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}