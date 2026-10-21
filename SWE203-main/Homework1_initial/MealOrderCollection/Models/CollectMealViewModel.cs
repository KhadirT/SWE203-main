using System.ComponentModel.DataAnnotations;

public class CollectMealViewModel
{
    [Required(ErrorMessage = "Please enter an OrderID.")]
    public string OrderID { get; set; } = string.Empty;

    public string? Message { get; set; }
}