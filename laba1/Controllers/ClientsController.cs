
using laba1.Models;
using laba1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace laba1.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientRepository _repository;

        public ClientsController(IClientRepository repository)
        {
            _repository = repository;
        }

        // GET: /Clients
        public IActionResult Index()
        {
            var clients = _repository.GetAll();
            return View(clients);
        }

        // GET: /Clients/Details/5
        public IActionResult Details(int id)
        {
            var client = _repository.GetById(id);
            if (client == null)
                return NotFound();

            return View(client);
        }

        // GET: /Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                client.RegistrationDate = DateTime.Now;
                _repository.Add(client);

                TempData["SuccessMessage"] = "Клиент добавлен!";
                return RedirectToAction(nameof(Index));
            }

            return View(client);
        }

        // GET: /Clients/Edit/5
        public IActionResult Edit(int id)
        {
            var client = _repository.GetById(id);
            if (client == null)
                return NotFound();

            return View(client);
        }

        // POST: /Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Client client)
        {
            if (id != client.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(client);
                    TempData["SuccessMessage"] = "Клиент обновлен!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(client);
        }

        // GET: /Clients/Delete/5
        public IActionResult Delete(int id)
        {
            var client = _repository.GetById(id);
            if (client == null)
                return NotFound();

            return View(client);
        }

        // POST: /Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            TempData["SuccessMessage"] = "Клиент удален!";
            return RedirectToAction(nameof(Index));
        }
        // По дате
        public IActionResult ByDate(DateTime from, DateTime to)
        {
            var clients = _repository.GetClientsByDateRange(from, to);
            return View(clients);
        }

        // Старые клиенты
        public IActionResult Oldest(int count = 5)
        {
            var clients = _repository.GetOldestClients(count);
            return View(clients);
        }

        // Поиск
        public IActionResult Search(string term)
        {
            var clients = _repository.SearchClients(term);
            return View(clients);
        }

        // Группировка
        public IActionResult GroupedByActivity()
        {
            var clients = _repository.GetClientsGroupedByActivity();
            return View(clients);
        }

        // Пагинация
        public IActionResult Paginated(int page = 1, int pageSize = 5)
        {
            var clients = _repository.GetClientsWithPagination(page, pageSize);
            ViewBag.TotalPages = _repository.GetTotalPages(pageSize);
            ViewBag.CurrentPage = page;
            return View(clients);
        }
    }
}