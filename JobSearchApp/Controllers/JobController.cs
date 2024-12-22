// Controllers/JobController.cs
using Microsoft.AspNetCore.Mvc;
using JobSearchApp.Services;

public class JobController : Controller
{
    private readonly JobService _jobService;

    public JobController(JobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string description, string location, int page = 1)
    {
        var jobs = await _jobService.GetJobsAsync(description, location);
        ViewData["Description"] = description;
        ViewData["Location"] = location;
        ViewData["Page"] = page;
        return View(jobs);
    }
}