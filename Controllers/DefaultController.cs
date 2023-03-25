﻿using Microsoft.AspNetCore.Mvc;

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
    public IActionResult Main(
        bool isComplete = false)
    {
        return View(
            new ContactModel
            {
                IsComplete = isComplete,
            });
    }

    [HttpGet("[action]")]
    public IActionResult Reviews()
    {
        return View();
    }
}