namespace JobSearchApp.Services;

public class JobService
{
    private readonly HttpClient _httpClient; public JobService(HttpClient httpClient) { _httpClient = httpClient; } public async Task<List<Job>> GetJobsAsync(string description, string location) { var response = await _httpClient.GetStringAsync($"https://jobs.github.com/positions.json?description={description}&location={location}"); return JsonConvert.DeserializeObject<List<Job>>(response); }
}