### Coding Conventions

- For single-line if statements, omit curly braces `{}`
- When implementing EF Core repositories for mutations, avoid using `async/await` and `SaveChangesAsync`. Instead, consolidate all changes at the UnitOfWork level to call `SaveChangesAsync` once or use transactions
- Use Options Pattern in ASP.NET Core when possible (see `AuthSettings` in `App.Application.Auth.Configurations`)
- Use ErrorOr package for result pattern implementation, never throw exceptions for flow control
- Never use magic strings/numbers - find relative constant files or create new ones
- Do not use fully qualified names, instead use qualified namespace imports or aliases
- Use `record` types for commands/queries and DTOs (see `LoginCommand` pattern)

### Project-Specific Workflows

**Multi-Frontend Architecture:**

- Two separate React applications: `web` (port 3000) and `admin` (port 3100)
- Both share identical tech stack but separate feature sets
- Root workspace uses Bun workspaces with shared lint-staged configuration

**Backend Development:**

- Use `make api-build` instead of direct dotnet commands
- Database migrations: `make db-add name=MigrationName`, `make db-update`
- Additional commands: `make api-run`, `make api-watch`, `make db-remove`, `make db-list`
- Centralized package versions in `Directory.Packages.props` - never add versions to individual `.csproj` files

**Frontend Development:**

- Routes follow file-based structure in `web/src/routes/` and `admin/src/routes/` (use parentheses for route groups like `(auth)/`)
- Features organized by domain: `{web|admin}/src/features/{domain}/{feature}/` with `components/`, `hooks/`, `schemas/` subfolders
- API calls structured: `{web|admin}/src/api/{domain}/{feature}/` with separate `endpoint.ts`, `request.ts`, `response.ts` files

### Things to Avoid

- Don't write a \*.md file summarizing the conversation
- Never run commands: `dotnet run`, `bun run dev`, `dotnet watch` etc (use make commands and VSCode tasks)
- Do not write files other than page files inside the `routes/` folder
- Avoid direct package version references in `.csproj` files (use centralized `Directory.Packages.props`)
- Don't start both `web` and `admin` on same port - web uses 3000, admin uses 3100

### Safety and Permissions

Allowed without prompt:

- read files, list files
- write files, create files

Ask first:

- package installs
- git push
- deleting files, chmod
- running full build

### Architecture & Implementation Patterns

#### Backend (Clean Architecture)

**CQRS with MediatR Pattern:**

- Commands/Queries as records: `public record LoginCommand(string Email, string Password) : IRequest<ErrorOr<LoginResult>>;`
- Handlers in separate files: `{Command|Query}Handler.cs` implementing `IRequestHandler<TRequest, TResponse>`
- Controllers are thin - only inject `IMediator` and forward requests (see `AuthController`)

**Layer Boundaries:**

- **API Layer (`App.Api`)**: Controllers, middleware, dependency injection configuration
- **Application Layer (`App.Application`)**: Business logic, CQRS handlers, validators, services
- **Domain Layer (`App.Domain`)**: Entities, enums, domain logic
- **Infrastructure Layer (`App.Infrastructure`)**: EF Core, external services, implementations
- **Contract Layer (`App.Contract`)**: DTOs, request/response models

**Dependency Injection Pattern:**

- Each layer has `DI/DependencyInjection.cs` with extension methods: `AddApplication()`, `AddInfrastructure()`, etc.
- Clean `Program.cs` with sequential layer registration

**Feature Organization:**

- Group by feature: `Auth/{Commands|Events|Services}/FeatureName/`
- Each command/query has dedicated folder with handler, validator, and related types

#### Frontend (React + TanStack)

**File-based Routing:**

- Routes in `web/src/routes/` map to URLs
- Route groups use parentheses: `(auth)/login.tsx` → `/login`
- Root route context provides `i18n` and `queryClient`

**Feature-First Organization:**

- `web/src/features/{domain}/{feature}/` structure
- Sub-folders: `components/`, `hooks/`, `schemas/` per feature
- API calls: `web/src/api/{domain}/{feature}/` with `endpoint.ts`, `request.ts`, `response.ts`

**Form Patterns:**

- React Hook Form + Zod validation
- Custom hooks for form logic: `use{Feature}Hook`
- ErrorOr-compatible error handling with neverthrow

### Development Commands

**Backend (use Makefiles, not direct dotnet commands):**

```bash
make api-build          # Build solution
make api-clean          # Clean solution
make api-restore        # Restore packages
make db-add name=Name   # Add migration
make db-update          # Apply migrations
make db-list            # List migrations
```

**Package Management:**

- All versions centralized in `api/Directory.Packages.props`
- Never add `<PackageReference Version="...">` to individual `.csproj` files
- Use `<PackageReference Include="PackageName" />` only

### Project Information

#### Key Directories

- `api/src/App.Domain/Entities` - Domain entities
- `api/src/App.Contract` - DTOs and response models
- `api/src/App.Application` - Business logic and CQRS handlers
- `api/src/App.Infrastructure` - EF Core and external services
- `web/src/features` - Feature-specific React components and logic
- `admin/src/features` - Admin-specific feature components and logic
- `web/src/api` - Frontend API client functions
- `admin/src/api` - Admin frontend API client functions
- `web/src/locales` - i18next translation files

### Good and bad examples

### When stuck

- ask a clarifying question, propose a short plan
- do not make up answers, say "I don't know" if you don't know

#### Backend

- Follows **Clean Architecture** principles
- **API Layer**: Contains controllers, middleware, and web API configurations
- **Application Layer**: Houses business logic, services, commands/queries, and validators
- **Domain Layer**: Contains domain entities
- **Infrastructure Layer**: Manages external services and integrations
- **Contract Layer**: Defines DTOs and response models
- Uses `Directory.Build.props` and `Directory.Packages.props` for centralized package version and configuration management across solution projects

##### Technology Stack

- **Database**: SQL Server, Redis
- **ORM**: Entity Framework Core
- **CQRS**: MediatR for separating commands/queries from controllers
- **Validation**: FluentValidation
- **Background Jobs**: Hangfire
- **Email Service**: FluentEmail
- **Error Handling**: ErrorOr for result pattern implementation
- **Authentication**: JWT with cookie-based token storage (access tokens stored in HTTP-only cookies)

### Frontend

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
