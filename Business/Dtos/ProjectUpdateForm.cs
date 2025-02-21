using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Business.Dtos;

public class ProjectUpdateForm : ObservableObject
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int ManagerId { get; set; }
    public int StatusId { get; set; }



    /* Genererat av chat GPT 4o för att kunna ränka ut totalpris för ett projekt 
   * _hours skapas och får ett värde från användaren, _totalPrice räknas ut genom att multiplicera _hours med priset på vald produkt. */
    private int _hours; 
    public int Hours
    {
        get => _hours; 
        set
        {
            SetProperty(ref _hours, value);
            CalculateTotalPrice();
        }
    }

    private decimal _totalPrice;
    public decimal TotalPrice
    {
        get => _totalPrice;
        private set => SetProperty(ref _totalPrice, value);
    }

    private ProductModel _selectedProduct;
    public ProductModel SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            SetProperty(ref _selectedProduct, value);
            CalculateTotalPrice();
        }
    }

    private void CalculateTotalPrice()
    {
        if (SelectedProduct != null)
        {
            TotalPrice = Hours * SelectedProduct.Price;
        }
    }
}
