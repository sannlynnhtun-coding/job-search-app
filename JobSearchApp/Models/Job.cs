namespace JobSearchApp.Models
{
    public class Job
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Company { get; set; }
        public string CompanyUrl { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HowToApply { get; set; }
        public string CompanyLogo { get; set; }
    }
}
