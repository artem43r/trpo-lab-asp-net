using Microsoft.AspNetCore.Mvc;
using laba1.Models;
using laba1.Repositories;

namespace laba1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        // ===== CRUD =====

        public IActionResult Index()
        {
            var products = _repository.GetAll();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedDate = DateTime.Now;
                _repository.Add(product);

                TempData["SuccessMessage"] = "Товар добавлен!";
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _repository.Update(product);
                TempData["SuccessMessage"] = "Товар обновлен!";
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            TempData["SuccessMessage"] = "Товар удален!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Category(string category)
        {
            var products = _repository.GetByCategory(category);
            ViewBag.Category = category;
            return View(products);
        }

        public IActionResult InStock()
        {
            var products = _repository.GetInStock();
            return View("Index", products);
        }

        // ===== LINQ =====

        public IActionResult ByPrice(decimal min, decimal max)
        {
            var products = _repository.GetProductsByPriceRange(min, max);
            ViewBag.MinPrice = min;
            ViewBag.MaxPrice = max;
            ViewBag.Title = $"Товары от {min:C} до {max:C}";
            return View(products);
        }

        public IActionResult TopExpensive(int count = 5)
        {
            var products = _repository.GetTopExpensiveProducts(count);
            ViewBag.Title = $"Топ {count} дорогих товаров";
            ViewBag.Count = count;
            return View(products);
        }

        public IActionResult Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return RedirectToAction(nameof(Index));

            var products = _repository.SearchProducts(term);

            ViewBag.SearchTerm = term;
            ViewBag.Title = $"Поиск: {term}";
            ViewBag.Count = products.Count();

            return View(products);
        }

        public IActionResult Statistics()
        {
            var products = _repository.GetAll();

            var stats = new ProductsStatisticsViewModel
            {
                TotalCount = _repository.GetTotalCount(),
                AveragePrice = _repository.GetAveragePrice(),
                InStockCount = _repository.GetInStock().Count(),
                PriceRange = _repository.GetPriceRange(),

                Categories = products
                    .GroupBy(p => p.Category)
                    .Select(g => new CategoryStatViewModel
                    {
                        Category = g.Key ?? "Без категории",
                        Count = g.Count(),
                        AveragePrice = g.Average(p => p.Price),
                        MinPrice = g.Min(p => p.Price),
                        MaxPrice = g.Max(p => p.Price)
                    })
                    .OrderBy(c => c.Category)
                    .ToList()
            };

            return View(stats);
        }

        public IActionResult GroupedByCategory()
        {
            var products = _repository.GetAll();
            return View(products);
        }

        public IActionResult Paginated(int page = 1, int pageSize = 5)
        {
            var products = _repository.GetProductsWithPagination(page, pageSize);
            var totalPages = _repository.GetTotalPages(pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.HasPreviousPage = page > 1;
            ViewBag.HasNextPage = page < totalPages;

            return View(products);
        }
    }
}