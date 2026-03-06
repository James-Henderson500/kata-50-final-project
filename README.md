# kata-50-final-project
Final project to build a complete web application using C#, .NET, HTML, CSS, TypeScript, and React.

Frontend Features:
- Display a list of visa applications with filtering options.
- View detailed information about a specific visa application.
- Form to submit a new visa application.
- Option to update the status of an application.

Unit Testing:
- Implement unit tests for backend API endpoints.
- Ensure at least 80% code coverage.
- Use NUnit as a testing framework for the API and React Testing Library with Jest for the frontend.

```mermaid
erDiagram

    Country {
        string Code PK
        string Name
    }

    ApplicationStatus {
        string Status PK
    }

    VisaType {
        string Type PK
    }

    VisaApplication {
        int Id PK
        string ApplicantName
        datetime DateOfBirth
        string PassportNumber
        string Nationality FK
        datetime ApplicationDate
        string Status FK
        string VisaType FK
    }

    %% Relationships
    Country ||--o{ VisaApplication : "Nationality"
    ApplicationStatus ||--o{ VisaApplication : "Status"
    VisaType ||--o{ VisaApplication : "VisaType"
