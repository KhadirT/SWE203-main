using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.CafeteriaName = "Sakarya University Cafeteria";
        ViewBag.CurrentDate = DateTime.Now.ToString("dd.MM.yyyy");

        var collectedOrders = Repository.GetCollected();
        ViewBag.CollectedCount = collectedOrders.Count;

        return View(collectedOrders);
    }
    public IActionResult AllOrders()
    {
        var orders = Repository.GetAll();
        ViewBag.TotalOrders = orders.Count;
        return View(orders);
    }

    [HttpGet]
    public IActionResult Details(string? orderId)
    {
        if (string.IsNullOrWhiteSpace(orderId))
        {
            return BadRequest("An OrderID is required.");
        }

        var order = Repository.FindByOrderId(orderId);
        if (order is null)
        {
            return NotFound($"No meal order was found for OrderID '{orderId}'.");
        }

        return View(order);
    }

    [HttpGet]
    public IActionResult Collect()
    {
        return View(new CollectMealViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Collect(CollectMealViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var order = Repository.FindByOrderId(model.OrderID);
        if (order is null)
        {
            ModelState.AddModelError(nameof(model.OrderID),
                $"No meal order was found for OrderID '{model.OrderID}'.");
            return View(model);
        }

        if (!Repository.MarkAsCollected(order))
        {
            ModelState.AddModelError(nameof(model.OrderID),
                "This meal has already been collected.");
            return View(model);
        }

        model.Message = $"Order {order.OrderID} was collected successfully at {order.CollectedAt:dd.MM.yyyy HH:mm}.";
        return View(model);
    }
}
