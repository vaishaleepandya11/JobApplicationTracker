# Job Application Tracker

Serverless backend to track job applications. It reminds you to follow up on stale applications by email and stores resumes in Blob Storage. Built with Azure Functions and Clean Architecture.

## Tech Stack
- C#, .NET 8, Azure Functions (Isolated Worker)
- EF Core + SQL Server (LocalDB for dev)
- Azure Queue Storage, Azure Blob Storage (Azurite locally)
- SMTP (Gmail) for email notifications

## Architecture
Clean Architecture, 4 layers:
- **Domain**: entities, repository interfaces
- **Application**: services, DTOs, interfaces (`IEmailSender`, `IBlobStorageService`)
- **Infrastructure**: EF Core repository, SMTP sender, Blob storage service
- **Presentation**: Azure Functions (HTTP, Timer, Queue triggers)

Dependency direction: Presentation → Infrastructure/Application → Domain

## Features
| Function | Trigger | What it does |
|---|---|---|
| AddApplication | HTTP POST | Create a job application |
| GetApplications | HTTP GET | List all applications |
| UpdateApplicationStatus | HTTP PUT | Update status |
| ReminderCheckerJob | Timer | Finds "Applied" apps older than 7 days, sends them to a queue |
| ReminderNotificationSender | Queue | Sends follow-up reminder email |
| UploadResume | HTTP POST (`/api/resumes`) | Uploads resume to Blob Storage and links it to an application |

## Run Locally
1. Install .NET 8 SDK, Azure Functions Core Tools, Azurite
2. Copy `local.settings.sample.json` to `local.settings.json` and fill in your values
3. Run `Update-Database` (EF Core migrations)
4. Start Azurite, then run the Presentation project (F5)

## Roadmap
- Weekly summary report (Timer)
- Azure deployment
- Unit tests (xUnit), Docker, GitHub Actions CI/CD