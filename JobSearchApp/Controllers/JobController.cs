// Controllers/JobController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobSearchApp.Controllers;

public class JobController : Controller
{
    private readonly JobService _jobService;

    public JobController(JobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string description, string location, bool full_time = false, int page = 1)
    {
        var jobs = await _jobService.GetJobsAsync(description, location, full_time);
        ViewData["Description"] = description;
        ViewData["Location"] = location;
        ViewData["FullTime"] = full_time;
        ViewData["Page"] = page;
        return View(jobs);
    }
}