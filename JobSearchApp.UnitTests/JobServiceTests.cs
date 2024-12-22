using JobSearchApp.Models;
using JobSearchApp.Services;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Http;
using Xunit.Abstractions;

namespace JobSearchApp.UnitTests;

public class JobServiceTests
{
    private readonly ITestOutputHelper _output;

    public JobServiceTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task GetJobsAsync_ReturnsFilteredJobs()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var jobList = new List<Job>
        {
            new Job { Id = "1", Title = "Software Engineer", Location = "New York", Type = "Full Time" },
            new Job { Id = "2", Title = "Data Scientist", Location = "San Francisco", Type = "Part Time" }
        };
        var responseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(jobList))
        };

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        var mockHttpContext = new Mock<HttpContext>();
        var mockRequest = new Mock<HttpRequest>();
        mockRequest.Setup(r => r.Scheme).Returns("http");
        mockRequest.Setup(r => r.Host).Returns(new HostString("localhost"));
        mockHttpContext.Setup(c => c.Request).Returns(mockRequest.Object);
        mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(mockHttpContext.Object);

        var jobService = new JobService(httpClient, mockHttpContextAccessor.Object);

        // Act
        var jobs = await jobService.GetJobsAsync("Software Engineer", "New York", true);

        // Assert
        Assert.Single(jobs);
        Assert.Equal("1", jobs[0].Id);

        _output.WriteLine("Number of jobs returned: {0}", jobs.Count);
        _output.WriteLine("Returned job ID: {0}", jobs[0].Id);
    }

    [Fact]
    public async Task GetJobsAsync_EmptyDescriptionAndLocation_ReturnsAllJobs()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var jobList = new List<Job>
        {
            new Job { Id = "1", Title = "Software Engineer", Location = "New York", Type = "Full Time" },
            new Job { Id = "2", Title = "Data Scientist", Location = "San Francisco", Type = "Part Time" }
        };
        var responseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(jobList))
        };

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        var mockHttpContext = new Mock<HttpContext>();
        var mockRequest = new Mock<HttpRequest>();
        mockRequest.Setup(r => r.Scheme).Returns("http");
        mockRequest.Setup(r => r.Host).Returns(new HostString("localhost"));
        mockHttpContext.Setup(c => c.Request).Returns(mockRequest.Object);
        mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(mockHttpContext.Object);

        var jobService = new JobService(httpClient, mockHttpContextAccessor.Object);

        // Act
        var jobs = await jobService.GetJobsAsync("", "", false);

        // Assert
        Assert.Equal(2, jobs.Count);
    }
}
