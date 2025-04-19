using UniversalCarShop.Customers;

namespace UniversalCarShop.Customers;

/// <summary>
/// Интерфейс для репозитория покупателей
/// </summary>
public interface ICustomerRepository
{
    /// <summary>
    /// Получает всех покупателей
    /// </summary>
    IEnumerable<Customer> GetAll();

    /// <summary>
    /// Получает покупателя по имени
    /// </summary>
    Customer? GetByName(string name);

    /// <summary>
    /// Добавляет покупателя
    /// </summary>
    void Add(Customer customer);
}