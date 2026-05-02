using System.Collections.Generic;
namespace laba1.Models
{
    /// <summary>
    /// ViewModel для страницы статистики товаров.
    /// Содержит все данные, необходимые для отображения статистики.
    /// </summary>
    public class ProductsStatisticsViewModel
    {
        /// <summary>
        /// Общее количество товаров
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Средняя цена всех товаров
        /// </summary>
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// Количество товаров в наличии
        /// </summary>
        public int InStockCount { get; set; }
        /// <summary>
        /// Диапазон цен (минимальная и максимальная)
        /// </summary>
        public (decimal MinPrice, decimal MaxPrice) PriceRange { get; set; }
        /// <summary>
        /// Список категорий со статистикой по каждой
        /// </summary>
        public IEnumerable<CategoryStatViewModel> Categories { get; set; } = new List<CategoryStatViewModel>();
    }
    /// <summary>
    /// ViewModel для статистики по категории
    /// </summary>
    public class CategoryStatViewModel
    {
        /// <summary>
        /// Название категории
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Количество товаров в категории
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Средняя цена в категории
        /// </summary>
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// Минимальная цена в категории
        /// </summary>
        public decimal MinPrice { get; set; }
        /// <summary>
        /// Максимальная цена в категории
        /// </summary>
        public decimal MaxPrice { get; set; }
    }
}