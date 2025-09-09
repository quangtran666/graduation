# Agent mode

# Coding Standards

- For single-line if statements, omit curly braces `{}`
- When implementing EF Core repositories for mutations, avoid using `async/await` and `SaveChangesAsync`. Instead, consolidate all changes at the UnitOfWork level to call `SaveChangesAsync` once or use transactions
- Using Options Pattern in ASP.NET Core when possible
- Using erroror package for result pattern implementation, never throw exceptions for flow control
- Never using magic string, number, find relative files for constants or if not exist, create a new one
- Không sử dụng fully qualified name, thay vào đó sử dụng qualified namespace

# Project Information

## Backend

- Follows **Clean Architecture** principles
- **API Layer**: Contains controllers, middleware, and web API configurations
- **Application Layer**: Houses business logic, services, commands/queries, and validators
- **Domain Layer**: Contains domain entities
- **Infrastructure Layer**: Manages external services and integrations
- **Contract Layer**: Defines DTOs and response models
- Uses `Directory.Build.props` and `Directory.Packages.props` for centralized package version and configuration management across solution projects

### Technology Stack

- **Database**: SQL Server, Redis
- **ORM**: Entity Framework Core
- **CQRS**: MediatR for separating commands/queries from controllers
- **Validation**: FluentValidation
- **Background Jobs**: Hangfire
- **Email Service**: FluentEmail
- **Error Handling**: ErrorOr for result pattern implementation

## Frontend

- Built with **React 19** and **Vite**
- **Routing**: TanStack Router
- **Forms**: React Hook Form with Zod validation
- **UI Components**: shadcn/ui
- **HTTP Client**: Axios for API calls
- **Server State Management**: React Query (TanStack Query)
- **Error Handling**: Neverthrow
- **Styling**: Tailwind CSS
- **Internationalization**: i18next
- **Authentication**: js-cookie for token storage
