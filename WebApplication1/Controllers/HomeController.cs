using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
public IActionResult Index()
{
    return View();
}


   [HttpPost]
    public IActionResult Index(string inputArray)
    {
        if (string.IsNullOrWhiteSpace(inputArray))
        {
            ModelState.AddModelError("", "Масив не може бути порожнім.");
            return View();
        }

        try
        {
            // Перетворення введених даних у масив чисел
            var numbers = inputArray.Split(',')
                                    .Select(x => int.Parse(x.Trim()))
                                    .ToArray();

            if (numbers.Length < 2)
            {
                ModelState.AddModelError("", "Масив повинен містити принаймні 2 елементи.");
                return View();
            }

            // Заміна чисел
            var result = new int[numbers.Length];
            result[0] = numbers[0];
            result[^1] = numbers[^1];

            ViewBag.Result = string.Join(", ", result);
        }
        catch (FormatException)
        {
            ModelState.AddModelError("", "Введіть коректний масив чисел через кому.");
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
