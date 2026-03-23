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
  COUNTRY ||--o{ VISAAPPLICATION : has
  APPLICATIONSTATUS ||--o{ VISAAPPLICATION : uses
  VISATYPE ||--o{ VISAAPPLICATION : uses

  COUNTRY {
    int Id PK
    varchar Code
    varchar Name
  }

  APPLICATIONSTATUS {
    int Id PK
    varchar Status
  }

  VISATYPE {
    int Id PK
    varchar Type
  }

  VISAAPPLICATION {
    int Id PK
    varchar ApplicantName
    date DateOfBirth
    varchar PassportNumber
    int Nationality FK
    date ApplicationDate
    int Status FK
    int VisaType FK
  }
