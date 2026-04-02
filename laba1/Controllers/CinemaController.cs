using Microsoft.AspNetCore.Mvc;

namespace laba1.Controllers
{
    [Route("cinema")]
    public class CinemaController : Controller
    {
        // /cinema
        [Route("")]
        public IActionResult NowPlaying()
        {
            ViewBag.Title = "Сейчас в прокате";

            ViewBag.Movies = new[]
            {
                new { Id = 1, Name = "Дюна 2" },
                new { Id = 2, Name = "Аватар 3" },
                new { Id = 3, Name = "Оппенгеймер" }
            };

            return View();
        }

        // /cinema/movie/1
        [Route("movie/{movieId}")]
        public IActionResult Movie(int movieId)
        {
            ViewBag.MovieId = movieId;
            ViewBag.MovieName = $"Фильм #{movieId}";

            ViewData["Rating"] = (movieId % 5) + 1;
            ViewData["Duration"] = 120 + movieId;

            return View();
        }

        // /cinema/sessions/2026-02-20
        [Route("sessions/{date}")]
        public IActionResult Sessions(DateTime date)
        {
            ViewBag.Date = date.ToShortDateString();

            ViewBag.Sessions = new[]
            {
                "12:00",
                "15:30",
                "18:00",
                "21:00"
            };

            return View();
        }

        // Кастомный маршрут
        [Route("today")]
        public IActionResult Today()
        {
            string today = DateTime.Today.ToString("yyyy-MM-dd");
            return Redirect($"/cinema/sessions/{today}");
        }

    }
}
