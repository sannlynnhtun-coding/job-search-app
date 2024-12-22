# 🚀 Job Search Application with ASP.NET Core MVC

This repository contains a Job Search application built with ASP.NET Core MVC. The application allows users to search for job listings based on job descriptions, locations, and full-time status. The project includes a robust unit testing suite using xUnit to ensure functionality and reliability.

## 🌟 Business Domain

The primary goal of this application is to provide a platform where users can search for job opportunities based on specific criteria. The application leverages the GitHub Jobs API to fetch job listings and presents them to the users in a user-friendly interface.

### ✨ Key Features:
- **Job Search**: Users can search for jobs by description, location, and filter by full-time positions.
- **Job Listings**: Display job listings with details such as title, company, location, type, and application instructions.
- **Pagination**: Implement pagination to navigate through multiple pages of job listings.
- **Clear Search**: Users can clear the search criteria and reload the page to display all jobs.
- **Unit Testing**: Ensure the functionality with comprehensive unit tests using xUnit and Moq.

## 🏗 Project Structure

### Models

The `Job` model represents the job data fetched from the GitHub Jobs API.

### Services

The `JobService` class is responsible for fetching job data from the GitHub Jobs API and filtering the results based on user criteria.

### Controllers

The `JobController` class handles HTTP requests and interacts with the `JobService` to retrieve and display job listings.

### Views

The `Index.cshtml` view displays the job listings and includes a search form for filtering jobs.

### Unit Testing

Unit tests are essential for ensuring the functionality of the `JobService`. We use xUnit and Moq to create comprehensive tests.

### Test Explorer Output

To ensure the test messages are visible in the test explorer, we use the `output.WriteLine` method to log the messages, providing detailed information about the test results.