using Microsoft.AspNetCore.Mvc;

namespace HonestOverhead;

public class DefaultController : Controller
{
    [HttpGet("[action]")]
    public IActionResult About()
    {
        return View();
    }

    [HttpGet("[action]")]
    public IActionResult Error()
    {
        return View();
    }

    [HttpGet("")]
    public IActionResult Main()
    {
        return View(new ContactModel());
    }

    [HttpPost("")]
    public IActionResult Main(
        ContactModel contactModel)
    {
        if (!ModelState.IsValid)
        {
            Response.StatusCode =
                StatusCodes.Status400BadRequest;
            return View(contactModel);
        }

        contactModel.IsComplete = true;
        return View(contactModel);
    }
}