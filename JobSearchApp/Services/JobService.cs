using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobSearchApp.Services;

public class JobService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JobService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Job>> GetJobsAsync(string description, string location, bool fullTime)
    {
        var request = _httpContextAccessor.HttpContext.Request;
        var domainUrl = request.Scheme + "://" + request.Host.Value;
        var response = await _httpClient.GetStringAsync($"{domainUrl}/positions.json");
        var lst = JsonConvert.DeserializeObject<List<Job>>(response);

        if (!string.IsNullOrEmpty(description))
        {
            lst = lst.Where(x => x.Title.ToLower().Contains(description.ToLower())).ToList();
        }

        if (!string.IsNullOrEmpty(location))
        {
            lst = lst.Where(x => x.Location.ToLower().Contains(location.ToLower())).ToList();
        }

        if (fullTime)
        {
            lst = lst.Where(x => 
                x.Type.ToLower().Contains("full-time") ||
                x.Type.ToLower().Contains("full time")).ToList();
        }

        return lst;
    }
}
