# Authentication Server Design Document

## Overview

The Authentication Server is a comprehensive, secure authentication and authorization system designed to provide enterprise-grade identity management capabilities for multiple applications. The system implements JWT-based authentication, application-specific role-based access control, multi-factor authentication, and comprehensive security monitoring using fully customized components without third-party OAuth dependencies. The server acts as a centralized identity provider that can manage users, roles, and permissions across multiple client applications with fine-grained access control.

The architecture follows Clean Architecture principles with clear separation of concerns and dependency inversion:
- **Entities Layer**: Core business entities and domain logic
- **Use Cases Layer**: Application-specific business rules and orchestration
- **Interface Adapters Layer**: Controllers, presenters, and gateways
- **Frameworks & Drivers Layer**: External frameworks, databases, and web servers
- **Cross-Cutting Concerns**: Security, logging, validation, and caching

## Architecture

### System Overview

The Authentication Server follows a microservices-inspired layered architecture designed for scalability, security, and multi-tenancy. The system serves as a centralized identity provider for multiple client applications.

### High-Level System Architecture

```mermaid
graph TB
    subgraph "Client Applications"
        WebApp1[Web App 1]
        WebApp2[Web App 2]
        MobileApp[Mobile App]
        API1[API Service 1]
        API2[API Service 2]
    end
    
    subgraph "Load Balancer / API Gateway"
        LB[Load Balancer]
        Gateway[API Gateway]
        RateLimit[Rate Limiter]
    end
    
    subgraph "Authentication Server Cluster"
        subgraph "Instance 1"
            AuthServer1[Auth Server]
        end
        subgraph "Instance 2"
            AuthServer2[Auth Server]
        end
        subgraph "Instance N"
            AuthServerN[Auth Server]
        end
    end
    
    subgraph "Data Layer"
        PrimaryDB[(Primary Database)]
        ReadReplica[(Read Replica)]
        Redis[(Redis Cache)]
    end
    
    subgraph "External Services"
        EmailSvc[Email Service]
        Monitoring[Monitoring & Logging]
        Backup[Backup Service]
    end
    
    WebApp1 --> LB
    WebApp2 --> LB
    MobileApp --> LB
    API1 --> LB
    API2 --> LB
    
    LB --> Gateway
    Gateway --> RateLimit
    RateLimit --> AuthServer1
    RateLimit --> AuthServer2
    RateLimit --> AuthServerN
    
    AuthServer1 --> PrimaryDB
    AuthServer1 --> ReadReplica
    AuthServer1 --> Redis
    AuthServer1 --> EmailSvc
    
    AuthServer2 --> PrimaryDB
    AuthServer2 --> ReadReplica
    AuthServer2 --> Redis
    
    AuthServerN --> PrimaryDB
    AuthServerN --> ReadReplica
    AuthServerN --> Redis
    
    PrimaryDB --> ReadReplica
    PrimaryDB --> Backup
    AuthServer1 --> Monitoring
    AuthServer2 --> Monitoring
    AuthServerN --> Monitoring
```

### Clean Architecture Visualization

#### 1. Clean Architecture Layers Overview

```mermaid
graph TB
    subgraph "Clean Architecture Layers"
        subgraph "Layer 4: Frameworks & Drivers"
            F1[Web Server<br/>Express.js]
            F2[Database<br/>PostgreSQL]
            F3[Cache<br/>Redis]
            F4[Email Service<br/>SMTP]
            F5[File System<br/>Storage]
        end
        
        subgraph "Layer 3: Interface Adapters"
            I1[Controllers<br/>HTTP Handlers]
            I2[Presenters<br/>Response Formatters]
            I3[Gateways<br/>Data Access]
            I4[Repositories<br/>Persistence]
        end
        
        subgraph "Layer 2: Use Cases"
            U1[Authentication<br/>Business Rules]
            U2[Authorization<br/>Business Rules]
            U3[User Management<br/>Business Rules]
            U4[Application Management<br/>Business Rules]
        end
        
        subgraph "Layer 1: Entities"
            E1[Domain Entities<br/>Core Business Logic]
            E2[Value Objects<br/>Data Validation]
            E3[Domain Services<br/>Complex Business Rules]
        end
    end
    
    %% Dependency Direction (Inward)
    F1 -.-> I1
    F2 -.-> I3
    F3 -.-> I3
    F4 -.-> I4
    F5 -.-> I4
    
    I1 -.-> U1
    I2 -.-> U2
    I3 -.-> U3
    I4 -.-> U4
    
    U1 -.-> E1
    U2 -.-> E2
    U3 -.-> E3
    U4 -.-> E1
    
    style E1 fill:#ff9999
    style E2 fill:#ff9999
    style E3 fill:#ff9999
    style U1 fill:#99ccff
    style U2 fill:#99ccff
    style U3 fill:#99ccff
    style U4 fill:#99ccff
    style I1 fill:#99ff99
    style I2 fill:#99ff99
    style I3 fill:#99ff99
    style I4 fill:#99ff99
    style F1 fill:#ffcc99
    style F2 fill:#ffcc99
    style F3 fill:#ffcc99
    style F4 fill:#ffcc99
    style F5 fill:#ffcc99
```

#### 2. Component Interaction Flow

```mermaid
graph LR
    subgraph "External World"
        Client[Client Application]
        Database[(Database)]
        Cache[(Cache)]
        Email[Email Service]
    end
    
    subgraph "Auth Server - Clean Architecture"
        subgraph "Frameworks Layer"
            WebServer[Web Server]
            DBDriver[DB Driver]
            CacheDriver[Cache Driver]
            EmailDriver[Email Driver]
        end
        
        subgraph "Adapters Layer"
            Controller[Controller]
            Presenter[Presenter]
            Repository[Repository]
            Gateway[Gateway]
        end
        
        subgraph "Use Cases Layer"
            UseCase[Use Case]
        end
        
        subgraph "Entities Layer"
            Entity[Entity]
            ValueObject[Value Object]
            DomainService[Domain Service]
        end
    end
    
    Client --> WebServer
    WebServer --> Controller
    Controller --> UseCase
    UseCase --> Entity
    UseCase --> DomainService
    UseCase --> Repository
    UseCase --> Gateway
    Repository --> DBDriver
    Gateway --> EmailDriver
    Gateway --> CacheDriver
    DBDriver --> Database
    EmailDriver --> Email
    CacheDriver --> Cache
    UseCase --> Presenter
    Presenter --> Controller
    Controller --> WebServer
    WebServer --> Client
    
    style Entity fill:#ff9999
    style ValueObject fill:#ff9999
    style DomainService fill:#ff9999
    style UseCase fill:#99ccff
    style Controller fill:#99ff99
    style Presenter fill:#99ff99
    style Repository fill:#99ff99
    style Gateway fill:#99ff99
    style WebServer fill:#ffcc99
    style DBDriver fill:#ffcc99
    style CacheDriver fill:#ffcc99
    style EmailDriver fill:#ffcc99
```

#### 3. Authentication Flow Visualization

```mermaid
sequenceDiagram
    participant Client
    participant WebServer as Web Server<br/>(Framework)
    participant Controller as Auth Controller<br/>(Adapter)
    participant UseCase as Register UseCase<br/>(Use Case)
    participant Entity as User Entity<br/>(Entity)
    participant Repository as User Repository<br/>(Adapter)
    participant Database as Database<br/>(Framework)
    participant EmailGateway as Email Gateway<br/>(Adapter)
    participant EmailService as Email Service<br/>(Framework)
    
    Client->>WebServer: POST /auth/register
    WebServer->>Controller: HTTP Request
    Controller->>UseCase: RegisterUserRequest
    UseCase->>Entity: Create User
    Entity-->>UseCase: User Created
    UseCase->>Repository: Save User
    Repository->>Database: INSERT User
    Database-->>Repository: Success
    Repository-->>UseCase: User Saved
    UseCase->>EmailGateway: Send Verification
    EmailGateway->>EmailService: Send Email
    EmailService-->>EmailGateway: Email Sent
    EmailGateway-->>UseCase: Success
    UseCase-->>Controller: RegisterUserResponse
    Controller-->>WebServer: HTTP Response
    WebServer-->>Client: 201 Created
    
    rect rgb(255, 153, 153)
        note over Entity: Entities Layer<br/>Pure Business Logic
    end
    
    rect rgb(153, 204, 255)
        note over UseCase: Use Cases Layer<br/>Application Rules
    end
    
    rect rgb(153, 255, 153)
        note over Controller, EmailGateway: Interface Adapters<br/>Convert & Present
    end
    
    rect rgb(255, 204, 153)
        note over WebServer, EmailService: Frameworks & Drivers<br/>External Tools
    end
```

#### 4. Permission System Architecture

```mermaid
graph TB
    subgraph "Permission Architecture Layers"
        subgraph "Presentation Layer"
            API[REST API Endpoints]
            GraphQL[GraphQL Interface]
            CLI[Command Line Interface]
        end
        
        subgraph "Application Layer"
            PermissionUseCase[Permission Use Cases]
            RoleUseCase[Role Use Cases]
            AuthorizationUseCase[Authorization Use Cases]
        end
        
        subgraph "Domain Layer"
            PermissionEntity[Permission Entity]
            RoleEntity[Role Entity]
            UserEntity[User Entity]
            ApplicationEntity[Application Entity]
            PermissionService[Permission Domain Service]
        end
        
        subgraph "Infrastructure Layer"
            PermissionRepo[Permission Repository]
            RoleRepo[Role Repository]
            CacheService[Permission Cache]
            AuditService[Audit Service]
        end
        
        subgraph "Data Layer"
            PermissionDB[(Permission Tables)]
            RoleDB[(Role Tables)]
            UserRoleDB[(User-Role Relations)]
            CacheDB[(Redis Cache)]
        end
    end
    
    API --> PermissionUseCase
    GraphQL --> RoleUseCase
    CLI --> AuthorizationUseCase
    
    PermissionUseCase --> PermissionEntity
    RoleUseCase --> RoleEntity
    AuthorizationUseCase --> PermissionService
    
    PermissionEntity --> PermissionService
    RoleEntity --> PermissionService
    UserEntity --> PermissionService
    ApplicationEntity --> PermissionService
    
    PermissionService --> PermissionRepo
    PermissionService --> RoleRepo
    PermissionService --> CacheService
    PermissionService --> AuditService
    
    PermissionRepo --> PermissionDB
    RoleRepo --> RoleDB
    RoleRepo --> UserRoleDB
    CacheService --> CacheDB
    
    style PermissionEntity fill:#ff9999
    style RoleEntity fill:#ff9999
    style UserEntity fill:#ff9999
    style ApplicationEntity fill:#ff9999
    style PermissionService fill:#ff9999
    style PermissionUseCase fill:#99ccff
    style RoleUseCase fill:#99ccff
    style AuthorizationUseCase fill:#99ccff
    style API fill:#99ff99
    style GraphQL fill:#99ff99
    style CLI fill:#99ff99
    style PermissionRepo fill:#99ff99
    style RoleRepo fill:#99ff99
    style CacheService fill:#99ff99
    style AuditService fill:#99ff99
```

#### 5. Multi-Application Context Visualization

```mermaid
graph TB
    subgraph "Multi-Application Auth Server"
        subgraph "Shared Identity Layer"
            GlobalUser[Global User Identity]
            GlobalRoles[System-Wide Roles]
            GlobalPermissions[Global Permissions]
        end
        
        subgraph "Application A Context"
            AppA[Application A]
            AppAUsers[App A Users]
            AppARoles[App A Roles]
            AppAPerms[App A Permissions]
        end
        
        subgraph "Application B Context"
            AppB[Application B]
            AppBUsers[App B Users]
            AppBRoles[App B Roles]
            AppBPerms[App B Permissions]
        end
        
        subgraph "Application C Context"
            AppC[Application C]
            AppCUsers[App C Users]
            AppCRoles[App C Roles]
            AppCPerms[App C Permissions]
        end
        
        subgraph "Cross-Application Services"
            IdentityService[Identity Service]
            PermissionResolver[Permission Resolver]
            TokenService[Token Service]
            AuditService[Audit Service]
        end
    end
    
    GlobalUser --> AppAUsers
    GlobalUser --> AppBUsers
    GlobalUser --> AppCUsers
    
    GlobalRoles --> AppARoles
    GlobalRoles --> AppBRoles
    GlobalRoles --> AppCRoles
    
    AppA --> AppAUsers
    AppA --> AppARoles
    AppA --> AppAPerms
    
    AppB --> AppBUsers
    AppB --> AppBRoles
    AppB --> AppBPerms
    
    AppC --> AppCUsers
    AppC --> AppCRoles
    AppC --> AppCPerms
    
    IdentityService --> GlobalUser
    PermissionResolver --> GlobalPermissions
    PermissionResolver --> AppAPerms
    PermissionResolver --> AppBPerms
    PermissionResolver --> AppCPerms
    
    TokenService --> IdentityService
    TokenService --> PermissionResolver
    
    AuditService --> IdentityService
    AuditService --> PermissionResolver
    
    style GlobalUser fill:#ff6666
    style GlobalRoles fill:#ff6666
    style GlobalPermissions fill:#ff6666
    style AppA fill:#66ccff
    style AppB fill:#66ccff
    style AppC fill:#66ccff
    style IdentityService fill:#66ff66
    style PermissionResolver fill:#66ff66
    style TokenService fill:#66ff66
    style AuditService fill:#66ff66
```

#### 6. Data Flow Through Clean Architecture

```mermaid
flowchart TD
    Start([HTTP Request]) --> WebFramework[Web Framework<br/>Express.js]
    WebFramework --> Middleware[Middleware Pipeline<br/>Auth, Validation, Rate Limiting]
    Middleware --> Controller[Controller<br/>Parse Request]
    
    Controller --> UseCaseInput[Use Case Input<br/>Request DTO]
    UseCaseInput --> UseCase[Use Case<br/>Business Logic]
    
    UseCase --> EntityValidation{Entity Validation}
    EntityValidation -->|Valid| DomainLogic[Domain Logic<br/>Entity Methods]
    EntityValidation -->|Invalid| DomainError[Domain Error]
    
    DomainLogic --> RepositoryCall[Repository Interface<br/>Data Access]
    RepositoryCall --> DatabaseOperation[Database Operation<br/>SQL/NoSQL]
    
    DatabaseOperation --> RepositoryResult[Repository Result]
    RepositoryResult --> UseCaseResult[Use Case Result<br/>Response DTO]
    
    UseCaseResult --> Presenter[Presenter<br/>Format Response]
    Presenter --> HTTPResponse[HTTP Response<br/>JSON/XML]
    
    DomainError --> ErrorPresenter[Error Presenter<br/>Format Error]
    ErrorPresenter --> ErrorResponse[Error Response<br/>HTTP Error]
    
    HTTPResponse --> End([Client Response])
    ErrorResponse --> End
    
    style WebFramework fill:#ffcc99
    style Middleware fill:#ffcc99
    style Controller fill:#99ff99
    style Presenter fill:#99ff99
    style ErrorPresenter fill:#99ff99
    style UseCase fill:#99ccff
    style DomainLogic fill:#ff9999
    style EntityValidation fill:#ff9999
    style DomainError fill:#ff9999
```

#### 7. Dependency Inversion Principle Visualization

```mermaid
graph TB
    subgraph "High-Level Modules (Stable)"
        UseCase[Use Case<br/>Business Logic]
        Entity[Entity<br/>Domain Logic]
    end
    
    subgraph "Abstractions (Interfaces)"
        IRepository[Repository Interface]
        IEmailService[Email Service Interface]
        ITokenService[Token Service Interface]
    end
    
    subgraph "Low-Level Modules (Volatile)"
        PostgreSQLRepo[PostgreSQL Repository]
        SMTPEmail[SMTP Email Service]
        JWTToken[JWT Token Service]
        MongoRepo[MongoDB Repository]
        SendGridEmail[SendGrid Email Service]
        Auth0Token[Auth0 Token Service]
    end
    
    UseCase --> IRepository
    UseCase --> IEmailService
    UseCase --> ITokenService
    
    Entity --> IRepository
    
    IRepository <|.. PostgreSQLRepo
    IRepository <|.. MongoRepo
    
    IEmailService <|.. SMTPEmail
    IEmailService <|.. SendGridEmail
    
    ITokenService <|.. JWTToken
    ITokenService <|.. Auth0Token
    
    style UseCase fill:#99ccff
    style Entity fill:#ff9999
    style IRepository fill:#ffff99
    style IEmailService fill:#ffff99
    style ITokenService fill:#ffff99
    style PostgreSQLRepo fill:#ffcc99
    style SMTPEmail fill:#ffcc99
    style JWTToken fill:#ffcc99
    style MongoRepo fill:#ffcc99
    style SendGridEmail fill:#ffcc99
    style Auth0Token fill:#ffcc99
```

### Clean Architecture Layers Detail

#### 1. Entities Layer (Core Domain)

**Core Business Entities:**
```typescript
// Pure business logic, no external dependencies
class User {
  private constructor(
    private readonly id: UserId,
    private readonly email: Email,
    private password: Password,
    private readonly profile: UserProfile
  ) {}
  
  static create(email: Email, password: Password): User {
    // Domain validation logic
    return new User(UserId.generate(), email, password, UserProfile.empty());
  }
  
  changePassword(currentPassword: string, newPassword: Password): void {
    if (!this.password.verify(currentPassword)) {
      throw new InvalidPasswordError();
    }
    this.password = newPassword;
  }
  
  hasPermission(permission: Permission, context: PermissionContext): boolean {
    // Core permission logic
    return this.permissions.includes(permission) || 
           this.roles.some(role => role.hasPermission(permission, context));
  }
}

class Application {
  private constructor(
    private readonly id: ApplicationId,
    private readonly name: string,
    private readonly permissions: Permission[],
    private readonly roles: Role[]
  ) {}
  
  static create(name: string, owner: User): Application {
    return new Application(
      ApplicationId.generate(),
      name,
      Permission.defaultPermissions(),
      Role.defaultRoles()
    );
  }
  
  addPermission(permission: Permission): void {
    if (this.permissions.some(p => p.equals(permission))) {
      throw new DuplicatePermissionError();
    }
    this.permissions.push(permission);
  }
}
```

**Value Objects:**
```typescript
class Email {
  private constructor(private readonly value: string) {}
  
  static create(email: string): Email {
    if (!this.isValid(email)) {
      throw new InvalidEmailError(email);
    }
    return new Email(email);
  }
  
  private static isValid(email: string): boolean {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
  }
  
  toString(): string {
    return this.value;
  }
}

class Permission {
  private constructor(
    private readonly resource: string,
    private readonly action: string,
    private readonly conditions: PermissionCondition[]
  ) {}
  
  static create(resource: string, action: string): Permission {
    return new Permission(resource, action, []);
  }
  
  matches(resource: string, action: string, context: PermissionContext): boolean {
    return this.resource === resource && 
           this.action === action &&
           this.conditions.every(condition => condition.evaluate(context));
  }
}
```

#### 2. Use Cases Layer (Application Business Rules)

**Use Case Interfaces:**
```typescript
interface RegisterUserUseCase {
  execute(request: RegisterUserRequest): Promise<RegisterUserResponse>;
}

interface AuthenticateUserUseCase {
  execute(request: AuthenticateUserRequest): Promise<AuthenticateUserResponse>;
}

interface CheckPermissionUseCase {
  execute(request: CheckPermissionRequest): Promise<CheckPermissionResponse>;
}
```

**Use Case Implementations:**
```typescript
class RegisterUserUseCaseImpl implements RegisterUserUseCase {
  constructor(
    private readonly userRepository: UserRepository,
    private readonly emailService: EmailService,
    private readonly passwordService: PasswordService,
    private readonly auditService: AuditService
  ) {}
  
  async execute(request: RegisterUserRequest): Promise<RegisterUserResponse> {
    // 1. Validate input
    const email = Email.create(request.email);
    const password = await this.passwordService.hash(request.password);
    
    // 2. Check if user exists
    const existingUser = await this.userRepository.findByEmail(email);
    if (existingUser) {
      throw new UserAlreadyExistsError(email);
    }
    
    // 3. Create user entity
    const user = User.create(email, password);
    
    // 4. Save user
    await this.userRepository.save(user);
    
    // 5. Send verification email
    await this.emailService.sendVerificationEmail(user);
    
    // 6. Audit log
    await this.auditService.log(new UserRegisteredEvent(user));
    
    return RegisterUserResponse.success(user.getId());
  }
}

class CheckPermissionUseCaseImpl implements CheckPermissionUseCase {
  constructor(
    private readonly userRepository: UserRepository,
    private readonly permissionService: PermissionService,
    private readonly auditService: AuditService
  ) {}
  
  async execute(request: CheckPermissionRequest): Promise<CheckPermissionResponse> {
    // 1. Get user
    const user = await this.userRepository.findById(request.userId);
    if (!user) {
      throw new UserNotFoundError(request.userId);
    }
    
    // 2. Create permission context
    const context = PermissionContext.create(
      request.applicationId,
      request.resource,
      request.ipAddress,
      request.timestamp
    );
    
    // 3. Check permission using domain logic
    const permission = Permission.create(request.resource, request.action);
    const hasPermission = await this.permissionService.checkPermission(
      user, 
      permission, 
      context
    );
    
    // 4. Audit log
    await this.auditService.log(new PermissionCheckedEvent(
      user.getId(),
      permission,
      hasPermission,
      context
    ));
    
    return CheckPermissionResponse.create(hasPermission);
  }
}
```

#### 3. Interface Adapters Layer

**Controllers:**
```typescript
class AuthController {
  constructor(
    private readonly registerUserUseCase: RegisterUserUseCase,
    private readonly authenticateUserUseCase: AuthenticateUserUseCase,
    private readonly authPresenter: AuthPresenter
  ) {}
  
  async register(req: Request, res: Response): Promise<void> {
    try {
      const request = RegisterUserRequest.fromHttp(req);
      const response = await this.registerUserUseCase.execute(request);
      const httpResponse = this.authPresenter.presentRegistration(response);
      res.status(201).json(httpResponse);
    } catch (error) {
      const errorResponse = this.authPresenter.presentError(error);
      res.status(errorResponse.status).json(errorResponse);
    }
  }
}
```

**Gateways (Repository Implementations):**
```typescript
class PostgreSQLUserRepository implements UserRepository {
  constructor(private readonly db: DatabaseConnection) {}
  
  async save(user: User): Promise<void> {
    const userData = UserMapper.toDatabase(user);
    await this.db.query(
      'INSERT INTO users (id, email, password_hash, ...) VALUES ($1, $2, $3, ...)',
      [userData.id, userData.email, userData.passwordHash, ...]
    );
  }
  
  async findByEmail(email: Email): Promise<User | null> {
    const result = await this.db.query(
      'SELECT * FROM users WHERE email = $1',
      [email.toString()]
    );
    
    if (result.rows.length === 0) {
      return null;
    }
    
    return UserMapper.fromDatabase(result.rows[0]);
  }
}
```

#### 4. Frameworks & Drivers Layer

**Web Framework Setup:**
```typescript
// Express.js setup with dependency injection
const container = new Container();

// Register dependencies
container.bind<UserRepository>('UserRepository').to(PostgreSQLUserRepository);
container.bind<EmailService>('EmailService').to(SMTPEmailService);
container.bind<RegisterUserUseCase>('RegisterUserUseCase').to(RegisterUserUseCaseImpl);

// Setup routes
const authController = container.get<AuthController>('AuthController');
app.post('/auth/register', authController.register.bind(authController));
```

### Dependency Inversion Examples

**Repository Interfaces (defined in Use Cases layer):**
```typescript
interface UserRepository {
  save(user: User): Promise<void>;
  findById(id: UserId): Promise<User | null>;
  findByEmail(email: Email): Promise<User | null>;
}

interface EmailService {
  sendVerificationEmail(user: User): Promise<void>;
  sendPasswordResetEmail(user: User, token: string): Promise<void>;
}
```

**Implementations (in Interface Adapters layer):**
```typescript
class PostgreSQLUserRepository implements UserRepository {
  // Implementation details
}

class SMTPEmailService implements EmailService {
  // Implementation details
}
```

### Clean Architecture Benefits

#### 1. Independence
- **Framework Independence**: Business logic doesn't depend on Express.js or any web framework
- **Database Independence**: Can switch from PostgreSQL to MongoDB without changing business logic
- **UI Independence**: Same business logic can serve REST API, GraphQL, or CLI
- **External Service Independence**: Can switch email providers without affecting core logic

#### 2. Testability
- **Unit Testing**: Entities and Use Cases can be tested in isolation
- **Integration Testing**: Interface Adapters can be tested with mock implementations
- **End-to-End Testing**: Full system testing through the web interface

#### 3. Maintainability
- **Clear Boundaries**: Each layer has well-defined responsibilities
- **Dependency Inversion**: High-level modules don't depend on low-level modules
- **Single Responsibility**: Each component has one reason to change

### Dependency Injection Container

```typescript
// Container setup for Clean Architecture
class DIContainer {
  private bindings = new Map<string, any>();
  
  bind<T>(token: string): BindingBuilder<T> {
    return new BindingBuilder<T>(token, this.bindings);
  }
  
  get<T>(token: string): T {
    return this.bindings.get(token);
  }
}

// Registration
container.bind<UserRepository>('UserRepository').to(PostgreSQLUserRepository);
container.bind<EmailGateway>('EmailGateway').to(SMTPEmailService);
container.bind<RegisterUserUseCase>('RegisterUserUseCase').to(RegisterUserUseCaseImpl);
container.bind<AuthController>('AuthController').to(AuthControllerImpl);

// Usage in route setup
const authController = container.get<AuthController>('AuthController');
app.post('/auth/register', authController.register.bind(authController));
```

### Error Handling in Clean Architecture

```typescript
// Domain Errors (Entities layer)
abstract class DomainError extends Error {
  abstract readonly code: string;
}

class InvalidEmailError extends DomainError {
  readonly code = 'INVALID_EMAIL';
  constructor(email: string) {
    super(`Invalid email format: ${email}`);
  }
}

class UserAlreadyExistsError extends DomainError {
  readonly code = 'USER_ALREADY_EXISTS';
  constructor(email: Email) {
    super(`User with email ${email} already exists`);
  }
}

// Application Errors (Use Cases layer)
class ApplicationError extends Error {
  constructor(
    public readonly code: string,
    message: string,
    public readonly cause?: Error
  ) {
    super(message);
  }
}

// Infrastructure Errors (Frameworks layer)
class InfrastructureError extends Error {
  constructor(
    public readonly code: string,
    message: string,
    public readonly cause?: Error
  ) {
    super(message);
  }
}

// Error Presenter (Interface Adapters layer)
class ErrorPresenter {
  presentError(error: Error): HttpResponse {
    if (error instanceof DomainError) {
      return {
        status: 400,
        body: {
          error: {
            code: error.code,
            message: error.message
          }
        }
      };
    }
    
    if (error instanceof ApplicationError) {
      return {
        status: 422,
        body: {
          error: {
            code: error.code,
            message: error.message
          }
        }
      };
    }
    
    // Infrastructure errors should not expose internal details
    return {
      status: 500,
      body: {
        error: {
          code: 'INTERNAL_SERVER_ERROR',
          message: 'An unexpected error occurred'
        }
      }
    };
  }
}
```

### Data Flow Architecture

```mermaid
sequenceDiagram
    participant Client as Client App
    participant Gateway as API Gateway
    participant Auth as Auth Server
    participant DB as Database
    participant Cache as Redis Cache
    participant Email as Email Service
    
    Note over Client,Email: User Registration Flow
    Client->>Gateway: POST /auth/register
    Gateway->>Auth: Forward request
    Auth->>DB: Check email exists
    DB-->>Auth: Email available
    Auth->>Auth: Hash password
    Auth->>DB: Create user record
    DB-->>Auth: User created
    Auth->>Auth: Generate verification token
    Auth->>Email: Send verification email
    Email-->>Auth: Email sent
    Auth->>Cache: Store verification token
    Auth-->>Gateway: Registration success
    Gateway-->>Client: 201 Created
    
    Note over Client,Email: Authentication Flow
    Client->>Gateway: POST /auth/login
    Gateway->>Auth: Forward credentials
    Auth->>DB: Validate user credentials
    DB-->>Auth: User validated
    Auth->>Auth: Generate JWT tokens
    Auth->>Cache: Store refresh token
    Auth->>DB: Create session record
    Auth->>DB: Log audit event
    Auth-->>Gateway: Return tokens
    Gateway-->>Client: 200 OK + tokens
    
    Note over Client,Email: Permission Check Flow
    Client->>Gateway: GET /api/resource (with JWT)
    Gateway->>Auth: Validate token + check permissions
    Auth->>Cache: Check token cache
    Cache-->>Auth: Token valid
    Auth->>DB: Get user permissions for app
    DB-->>Auth: Permission list
    Auth->>Auth: Validate resource access
    Auth-->>Gateway: Permission granted
    Gateway-->>Client: 200 OK + resource data
```

### Security Architecture

```mermaid
graph TB
    subgraph "Security Layers"
        subgraph "Network Security"
            TLS[TLS 1.3 Encryption]
            Firewall[Application Firewall]
            DDoS[DDoS Protection]
        end
        
        subgraph "Application Security"
            InputVal[Input Validation]
            SQLInj[SQL Injection Prevention]
            XSS[XSS Protection]
            CSRF[CSRF Protection]
        end
        
        subgraph "Authentication Security"
            MFA[Multi-Factor Auth]
            PassPolicy[Password Policy]
            AccLock[Account Lockout]
            SessMan[Session Management]
        end
        
        subgraph "Authorization Security"
            RBAC[Role-Based Access Control]
            PermCheck[Permission Validation]
            TokenVal[Token Validation]
            AppIso[Application Isolation]
        end
        
        subgraph "Data Security"
            Encryption[Data Encryption]
            Hashing[Password Hashing]
            KeyMgmt[Key Management]
            DataMask[Data Masking]
        end
        
        subgraph "Monitoring Security"
            AuditLog[Audit Logging]
            Anomaly[Anomaly Detection]
            AlertSys[Alert System]
            Forensics[Security Forensics]
        end
    end
    
    TLS --> InputVal
    Firewall --> SQLInj
    DDoS --> XSS
    
    InputVal --> MFA
    SQLInj --> PassPolicy
    XSS --> AccLock
    CSRF --> SessMan
    
    MFA --> RBAC
    PassPolicy --> PermCheck
    AccLock --> TokenVal
    SessMan --> AppIso
    
    RBAC --> Encryption
    PermCheck --> Hashing
    TokenVal --> KeyMgmt
    AppIso --> DataMask
    
    Encryption --> AuditLog
    Hashing --> Anomaly
    KeyMgmt --> AlertSys
    DataMask --> Forensics
```

### Database Schema Architecture

```mermaid
erDiagram
    USERS {
        string id PK
        string email UK
        string password_hash
        boolean email_verified
        boolean mfa_enabled
        string mfa_secret
        json backup_codes
        int failed_login_attempts
        datetime locked_until
        datetime last_login_at
        datetime created_at
        datetime updated_at
    }
    
    APPLICATIONS {
        string id PK
        string name
        string description
        string client_id UK
        string client_secret_hash
        json redirect_uris
        json scopes
        boolean active
        datetime created_at
        datetime updated_at
    }
    
    ROLES {
        string id PK
        string application_id FK
        string name
        string description
        json permissions
        datetime created_at
        datetime updated_at
    }
    
    PERMISSIONS {
        string id PK
        string application_id FK
        string name
        string description
        string resource
        string action
        datetime created_at
        datetime updated_at
    }
    
    USER_APPLICATION_ROLES {
        string id PK
        string user_id FK
        string application_id FK
        string role_id FK
        datetime assigned_at
        string assigned_by FK
    }
    
    SESSIONS {
        string id PK
        string user_id FK
        string application_id FK
        string access_token_hash
        string refresh_token_hash
        string ip_address
        string user_agent
        datetime expires_at
        datetime created_at
        datetime last_activity_at
    }
    
    PASSWORD_RESETS {
        string id PK
        string user_id FK
        string token_hash
        datetime expires_at
        boolean used
        datetime created_at
    }
    
    AUDIT_EVENTS {
        string id PK
        string user_id FK
        string application_id FK
        string type
        string action
        string resource
        string ip_address
        string user_agent
        json metadata
        boolean success
        string error_message
        datetime timestamp
    }
    
    RATE_LIMITS {
        string id PK
        string key
        int count
        datetime window_start
        datetime expires_at
    }
    
    USERS ||--o{ USER_APPLICATION_ROLES : "has roles in apps"
    APPLICATIONS ||--o{ USER_APPLICATION_ROLES : "has users with roles"
    ROLES ||--o{ USER_APPLICATION_ROLES : "assigned to users"
    APPLICATIONS ||--o{ ROLES : "defines"
    APPLICATIONS ||--o{ PERMISSIONS : "defines"
    USERS ||--o{ SESSIONS : "has"
    APPLICATIONS ||--o{ SESSIONS : "scoped to"
    USERS ||--o{ PASSWORD_RESETS : "requests"
    USERS ||--o{ AUDIT_EVENTS : "generates"
    APPLICATIONS ||--o{ AUDIT_EVENTS : "context for"
```

### System Integration Architecture

#### 1. Client Integration Patterns

```mermaid
graph TB
    subgraph "Client Applications"
        WebApp[Web Application<br/>React/Vue/Angular]
        MobileApp[Mobile Application<br/>iOS/Android]
        BackendAPI[Backend API<br/>Microservices]
        ThirdPartyApp[Third-Party Application<br/>External Integration]
    end
    
    subgraph "Integration Methods"
        RESTAPI[REST API<br/>HTTP/HTTPS]
        GraphQLAPI[GraphQL API<br/>Query Language]
        WebhookAPI[Webhook API<br/>Event Notifications]
        SDKAPI[SDK Integration<br/>Language Libraries]
    end
    
    subgraph "Auth Server Core"
        AuthCore[Authentication Server<br/>Clean Architecture]
    end
    
    subgraph "Security Layers"
        TLS[TLS Encryption]
        JWT[JWT Tokens]
        OAuth[OAuth 2.0 Flow]
        RateLimit[Rate Limiting]
    end
    
    WebApp --> RESTAPI
    MobileApp --> SDKAPI
    BackendAPI --> GraphQLAPI
    ThirdPartyApp --> WebhookAPI
    
    RESTAPI --> TLS
    SDKAPI --> JWT
    GraphQLAPI --> OAuth
    WebhookAPI --> RateLimit
    
    TLS --> AuthCore
    JWT --> AuthCore
    OAuth --> AuthCore
    RateLimit --> AuthCore
    
    style WebApp fill:#e1f5fe
    style MobileApp fill:#e1f5fe
    style BackendAPI fill:#e1f5fe
    style ThirdPartyApp fill:#e1f5fe
    style AuthCore fill:#ff9999
    style TLS fill:#c8e6c9
    style JWT fill:#c8e6c9
    style OAuth fill:#c8e6c9
    style RateLimit fill:#c8e6c9
```

#### 2. Microservices Ecosystem

```mermaid
graph TB
    subgraph "External World"
        Users[End Users]
        AdminPanel[Admin Panel]
        Monitoring[Monitoring Systems]
    end
    
    subgraph "API Gateway Layer"
        Gateway[API Gateway<br/>Kong/Nginx]
        LoadBalancer[Load Balancer<br/>HAProxy]
    end
    
    subgraph "Auth Server Cluster"
        AuthServer1[Auth Server Instance 1<br/>Clean Architecture]
        AuthServer2[Auth Server Instance 2<br/>Clean Architecture]
        AuthServer3[Auth Server Instance 3<br/>Clean Architecture]
    end
    
    subgraph "Supporting Services"
        EmailService[Email Service<br/>Notification System]
        AuditService[Audit Service<br/>Security Logging]
        MetricsService[Metrics Service<br/>Performance Monitoring]
    end
    
    subgraph "Data Layer"
        PrimaryDB[(Primary Database<br/>PostgreSQL)]
        ReplicaDB[(Read Replica<br/>PostgreSQL)]
        CacheDB[(Cache Layer<br/>Redis Cluster)]
        BackupStorage[(Backup Storage<br/>S3/GCS)]
    end
    
    Users --> Gateway
    AdminPanel --> Gateway
    Monitoring --> MetricsService
    
    Gateway --> LoadBalancer
    LoadBalancer --> AuthServer1
    LoadBalancer --> AuthServer2
    LoadBalancer --> AuthServer3
    
    AuthServer1 --> EmailService
    AuthServer1 --> AuditService
    AuthServer1 --> MetricsService
    
    AuthServer2 --> EmailService
    AuthServer2 --> AuditService
    AuthServer2 --> MetricsService
    
    AuthServer3 --> EmailService
    AuthServer3 --> AuditService
    AuthServer3 --> MetricsService
    
    AuthServer1 --> PrimaryDB
    AuthServer1 --> ReplicaDB
    AuthServer1 --> CacheDB
    
    AuthServer2 --> PrimaryDB
    AuthServer2 --> ReplicaDB
    AuthServer2 --> CacheDB
    
    AuthServer3 --> PrimaryDB
    AuthServer3 --> ReplicaDB
    AuthServer3 --> CacheDB
    
    PrimaryDB --> BackupStorage
    CacheDB --> BackupStorage
    
    style AuthServer1 fill:#ff9999
    style AuthServer2 fill:#ff9999
    style AuthServer3 fill:#ff9999
    style Gateway fill:#99ccff
    style LoadBalancer fill:#99ccff
    style EmailService fill:#99ff99
    style AuditService fill:#99ff99
    style MetricsService fill:#99ff99
```

#### 3. Deployment Architecture Visualization

```mermaid
graph TB
    subgraph "Cloud Infrastructure"
        subgraph "CDN Layer"
            CloudFlare[CloudFlare CDN<br/>Global Distribution]
            EdgeCache[Edge Caching<br/>Static Assets]
        end
        
        subgraph "Load Balancing Layer"
            ALB[Application Load Balancer<br/>AWS ALB/GCP LB]
            WAF[Web Application Firewall<br/>Security Rules]
            SSL[SSL Termination<br/>Certificate Management]
        end
        
        subgraph "Container Orchestration"
            K8sCluster[Kubernetes Cluster<br/>Container Management]
            AuthPods[Auth Server Pods<br/>Auto-scaling]
            ServiceMesh[Service Mesh<br/>Istio/Linkerd]
        end
        
        subgraph "Data Persistence"
            DBCluster[Database Cluster<br/>High Availability]
            CacheCluster[Redis Cluster<br/>Distributed Cache]
            StorageVolumes[Persistent Volumes<br/>Data Storage]
        end
        
        subgraph "Observability"
            LogAggregation[Log Aggregation<br/>ELK/Fluentd]
            MetricsCollection[Metrics Collection<br/>Prometheus]
            Tracing[Distributed Tracing<br/>Jaeger/Zipkin]
            Alerting[Alerting System<br/>AlertManager]
        end
        
        subgraph "Security & Compliance"
            SecretManagement[Secret Management<br/>Vault/K8s Secrets]
            NetworkPolicies[Network Policies<br/>Firewall Rules]
            Compliance[Compliance Monitoring<br/>SOC2/GDPR]
        end
    end
    
    CloudFlare --> ALB
    EdgeCache --> ALB
    ALB --> WAF
    WAF --> SSL
    SSL --> K8sCluster
    
    K8sCluster --> AuthPods
    K8sCluster --> ServiceMesh
    ServiceMesh --> AuthPods
    
    AuthPods --> DBCluster
    AuthPods --> CacheCluster
    AuthPods --> StorageVolumes
    
    AuthPods --> LogAggregation
    AuthPods --> MetricsCollection
    AuthPods --> Tracing
    
    MetricsCollection --> Alerting
    LogAggregation --> Alerting
    
    AuthPods --> SecretManagement
    K8sCluster --> NetworkPolicies
    AuthPods --> Compliance
    
    style AuthPods fill:#ff9999
    style K8sCluster fill:#99ccff
    style DBCluster fill:#99ff99
    style CacheCluster fill:#99ff99
    style LogAggregation fill:#ffcc99
    style MetricsCollection fill:#ffcc99
    style SecretManagement fill:#ff99cc
```

#### 4. Security Architecture Layers

```mermaid
graph TB
    subgraph "Defense in Depth Security Model"
        subgraph "Perimeter Security"
            DDoSProtection[DDoS Protection<br/>CloudFlare/AWS Shield]
            WAFRules[WAF Rules<br/>OWASP Top 10]
            GeoBlocking[Geo-blocking<br/>Country Restrictions]
        end
        
        subgraph "Network Security"
            VPC[Virtual Private Cloud<br/>Network Isolation]
            Subnets[Private Subnets<br/>Database Isolation]
            SecurityGroups[Security Groups<br/>Firewall Rules]
            NACLs[Network ACLs<br/>Subnet Level Security]
        end
        
        subgraph "Application Security"
            InputValidation[Input Validation<br/>Schema Validation]
            SQLInjectionPrevention[SQL Injection Prevention<br/>Parameterized Queries]
            XSSProtection[XSS Protection<br/>Content Security Policy]
            CSRFProtection[CSRF Protection<br/>Token Validation]
        end
        
        subgraph "Authentication Security"
            MFAEnforcement[MFA Enforcement<br/>TOTP/SMS/Email]
            PasswordPolicy[Password Policy<br/>Complexity Rules]
            AccountLockout[Account Lockout<br/>Brute Force Protection]
            SessionManagement[Session Management<br/>Secure Cookies]
        end
        
        subgraph "Authorization Security"
            RBACEnforcement[RBAC Enforcement<br/>Role-Based Access]
            PermissionValidation[Permission Validation<br/>Fine-grained Control]
            TokenValidation[Token Validation<br/>JWT Verification]
            APIRateLimiting[API Rate Limiting<br/>Abuse Prevention]
        end
        
        subgraph "Data Security"
            EncryptionAtRest[Encryption at Rest<br/>Database Encryption]
            EncryptionInTransit[Encryption in Transit<br/>TLS 1.3]
            KeyManagement[Key Management<br/>Rotation & Storage]
            DataMasking[Data Masking<br/>PII Protection]
        end
        
        subgraph "Monitoring & Compliance"
            SecurityMonitoring[Security Monitoring<br/>SIEM Integration]
            AuditLogging[Audit Logging<br/>Compliance Trails]
            ThreatDetection[Threat Detection<br/>Anomaly Detection]
            IncidentResponse[Incident Response<br/>Automated Alerts]
        end
    end
    
    DDoSProtection --> WAFRules
    WAFRules --> GeoBlocking
    GeoBlocking --> VPC
    
    VPC --> Subnets
    Subnets --> SecurityGroups
    SecurityGroups --> NACLs
    NACLs --> InputValidation
    
    InputValidation --> SQLInjectionPrevention
    SQLInjectionPrevention --> XSSProtection
    XSSProtection --> CSRFProtection
    CSRFProtection --> MFAEnforcement
    
    MFAEnforcement --> PasswordPolicy
    PasswordPolicy --> AccountLockout
    AccountLockout --> SessionManagement
    SessionManagement --> RBACEnforcement
    
    RBACEnforcement --> PermissionValidation
    PermissionValidation --> TokenValidation
    TokenValidation --> APIRateLimiting
    APIRateLimiting --> EncryptionAtRest
    
    EncryptionAtRest --> EncryptionInTransit
    EncryptionInTransit --> KeyManagement
    KeyManagement --> DataMasking
    DataMasking --> SecurityMonitoring
    
    SecurityMonitoring --> AuditLogging
    AuditLogging --> ThreatDetection
    ThreatDetection --> IncidentResponse
    
    style DDoSProtection fill:#ff6b6b
    style WAFRules fill:#ff6b6b
    style VPC fill:#4ecdc4
    style Subnets fill:#4ecdc4
    style InputValidation fill:#45b7d1
    style SQLInjectionPrevention fill:#45b7d1
    style MFAEnforcement fill:#96ceb4
    style PasswordPolicy fill:#96ceb4
    style RBACEnforcement fill:#feca57
    style PermissionValidation fill:#feca57
    style EncryptionAtRest fill:#ff9ff3
    style EncryptionInTransit fill:#ff9ff3
    style SecurityMonitoring fill:#54a0ff
    style AuditLogging fill:#54a0ff
```

#### 5. Scalability Architecture

```mermaid
graph TB
    subgraph "Horizontal Scaling Strategy"
        subgraph "Auto-Scaling Groups"
            ASG[Auto Scaling Group<br/>Dynamic Scaling]
            ScalingPolicies[Scaling Policies<br/>CPU/Memory Based]
            HealthChecks[Health Checks<br/>Application Monitoring]
        end
        
        subgraph "Load Distribution"
            GlobalLB[Global Load Balancer<br/>Geographic Distribution]
            RegionalLB[Regional Load Balancer<br/>Zone Distribution]
            ServiceLB[Service Load Balancer<br/>Pod Distribution]
        end
        
        subgraph "Database Scaling"
            ReadReplicas[Read Replicas<br/>Query Distribution]
            Sharding[Database Sharding<br/>Horizontal Partitioning]
            ConnectionPooling[Connection Pooling<br/>Resource Optimization]
        end
        
        subgraph "Cache Scaling"
            CacheCluster[Redis Cluster<br/>Distributed Caching]
            CachePartitioning[Cache Partitioning<br/>Data Distribution]
            CacheReplication[Cache Replication<br/>High Availability]
        end
        
        subgraph "Performance Optimization"
            CDNDistribution[CDN Distribution<br/>Global Edge Caching]
            CompressionOptimization[Compression<br/>Response Optimization]
            DatabaseIndexing[Database Indexing<br/>Query Optimization]
        end
    end
    
    ASG --> ScalingPolicies
    ScalingPolicies --> HealthChecks
    HealthChecks --> GlobalLB
    
    GlobalLB --> RegionalLB
    RegionalLB --> ServiceLB
    ServiceLB --> ReadReplicas
    
    ReadReplicas --> Sharding
    Sharding --> ConnectionPooling
    ConnectionPooling --> CacheCluster
    
    CacheCluster --> CachePartitioning
    CachePartitioning --> CacheReplication
    CacheReplication --> CDNDistribution
    
    CDNDistribution --> CompressionOptimization
    CompressionOptimization --> DatabaseIndexing
    
    style ASG fill:#ff9999
    style GlobalLB fill:#99ccff
    style ReadReplicas fill:#99ff99
    style CacheCluster fill:#ffcc99
    style CDNDistribution fill:#ff99cc
```

### Technology Stack

```mermaid
graph TB
    subgraph "Frontend Integration"
        WebSDK[Web SDK]
        MobileSDK[Mobile SDK]
        RESTAPI[REST API]
        GraphQL[GraphQL API]
    end
    
    subgraph "Backend Technologies"
        NodeJS[Node.js / TypeScript]
        Express[Express.js Framework]
        Helmet[Helmet.js Security]
        CORS[CORS Middleware]
    end
    
    subgraph "Authentication & Security"
        JWT[JSON Web Tokens]
        Bcrypt[Bcrypt Hashing]
        Crypto[Node.js Crypto]
        Speakeasy[Speakeasy TOTP]
    end
    
    subgraph "Database Technologies"
        PostgreSQL[PostgreSQL Database]
        Prisma[Prisma ORM]
        Redis[Redis Cache]
        Migrations[Database Migrations]
    end
    
    subgraph "Testing Framework"
        Jest[Jest Testing]
        FastCheck[Fast-Check PBT]
        Supertest[API Testing]
        TestContainers[Test Containers]
    end
    
    subgraph "DevOps & Monitoring"
        Docker[Docker Containers]
        Kubernetes[Kubernetes]
        Prometheus[Prometheus Metrics]
        Winston[Winston Logging]
    end
    
    WebSDK --> RESTAPI
    MobileSDK --> RESTAPI
    RESTAPI --> Express
    GraphQL --> Express
    
    Express --> NodeJS
    Express --> Helmet
    Express --> CORS
    
    NodeJS --> JWT
    NodeJS --> Bcrypt
    NodeJS --> Crypto
    NodeJS --> Speakeasy
    
    NodeJS --> PostgreSQL
    NodeJS --> Prisma
    NodeJS --> Redis
    Prisma --> Migrations
    
    NodeJS --> Jest
    Jest --> FastCheck
    Jest --> Supertest
    Jest --> TestContainers
    
    NodeJS --> Docker
    Docker --> Kubernetes
    NodeJS --> Prometheus
    NodeJS --> Winston
```

### Permission Architecture

The authentication server implements a sophisticated multi-layered permission system that supports global permissions, role-based permissions, user-specific permissions, and application-specific permissions with inheritance and override capabilities.

#### Permission Hierarchy and Types

```mermaid
graph TB
    subgraph "Permission Hierarchy"
        subgraph "Global Level"
            GlobalPerms[Global Permissions]
            SystemRoles[System Roles]
            SuperAdmin[Super Admin]
        end
        
        subgraph "Application Level"
            AppPerms[Application Permissions]
            AppRoles[Application Roles]
            AppAdmin[App Admin]
        end
        
        subgraph "User Level"
            UserPerms[Direct User Permissions]
            UserRoles[User Role Assignments]
            UserOverrides[User Permission Overrides]
        end
        
        subgraph "Resource Level"
            ResourcePerms[Resource-Specific Permissions]
            ActionPerms[Action-Based Permissions]
            ContextPerms[Context-Aware Permissions]
        end
    end
    
    GlobalPerms --> AppPerms
    SystemRoles --> AppRoles
    SuperAdmin --> AppAdmin
    
    AppPerms --> UserPerms
    AppRoles --> UserRoles
    AppAdmin --> UserOverrides
    
    UserPerms --> ResourcePerms
    UserRoles --> ActionPerms
    UserOverrides --> ContextPerms
    
    style GlobalPerms fill:#ff9999
    style AppPerms fill:#99ccff
    style UserPerms fill:#99ff99
    style ResourcePerms fill:#ffcc99
```

#### Permission Resolution Flow

```mermaid
flowchart TD
    Start([Permission Check Request]) --> CheckGlobal{Check Global Permissions}
    
    CheckGlobal -->|Has Global Admin| GrantAccess[Grant Access]
    CheckGlobal -->|No Global Admin| CheckApp{Check Application Context}
    
    CheckApp -->|No App Context| CheckSystemRole{Check System Roles}
    CheckApp -->|Has App Context| CheckAppAdmin{Check App Admin Role}
    
    CheckAppAdmin -->|Is App Admin| GrantAccess
    CheckAppAdmin -->|Not App Admin| CheckAppRole{Check App-Specific Roles}
    
    CheckAppRole -->|Has Required Role| CheckRolePerms{Check Role Permissions}
    CheckAppRole -->|No Required Role| CheckDirectPerms{Check Direct User Permissions}
    
    CheckRolePerms -->|Role Has Permission| CheckOverrides{Check User Overrides}
    CheckRolePerms -->|Role Lacks Permission| CheckDirectPerms
    
    CheckDirectPerms -->|Has Direct Permission| CheckOverrides
    CheckDirectPerms -->|No Direct Permission| CheckInheritance{Check Permission Inheritance}
    
    CheckOverrides -->|Override Denies| DenyAccess[Deny Access]
    CheckOverrides -->|Override Allows| GrantAccess
    CheckOverrides -->|No Override| CheckContext{Check Context Rules}
    
    CheckContext -->|Context Allows| GrantAccess
    CheckContext -->|Context Denies| DenyAccess
    CheckContext -->|No Context Rule| CheckDefault{Check Default Policy}
    
    CheckInheritance -->|Inherited Permission| GrantAccess
    CheckInheritance -->|No Inherited Permission| DenyAccess
    
    CheckSystemRole -->|Has System Role| GrantAccess
    CheckSystemRole -->|No System Role| DenyAccess
    
    CheckDefault -->|Default Allow| GrantAccess
    CheckDefault -->|Default Deny| DenyAccess
    
    GrantAccess --> LogAccess[Log Access Grant]
    DenyAccess --> LogDeny[Log Access Denial]
    
    LogAccess --> End([End])
    LogDeny --> End
```

#### Permission Data Model Architecture

```mermaid
erDiagram
    GLOBAL_PERMISSIONS {
        string id PK
        string name UK
        string description
        string category
        int priority
        boolean system_level
        datetime created_at
        datetime updated_at
    }
    
    SYSTEM_ROLES {
        string id PK
        string name UK
        string description
        json global_permissions
        int hierarchy_level
        boolean is_system_role
        datetime created_at
        datetime updated_at
    }
    
    APPLICATION_PERMISSIONS {
        string id PK
        string application_id FK
        string name
        string description
        string resource_type
        string action_type
        json conditions
        boolean inheritable
        datetime created_at
        datetime updated_at
    }
    
    APPLICATION_ROLES {
        string id PK
        string application_id FK
        string name
        string description
        json permissions
        int hierarchy_level
        boolean is_default
        datetime created_at
        datetime updated_at
    }
    
    USER_GLOBAL_ROLES {
        string id PK
        string user_id FK
        string system_role_id FK
        datetime assigned_at
        string assigned_by FK
        datetime expires_at
    }
    
    USER_APPLICATION_ROLES {
        string id PK
        string user_id FK
        string application_id FK
        string application_role_id FK
        datetime assigned_at
        string assigned_by FK
        datetime expires_at
    }
    
    USER_DIRECT_PERMISSIONS {
        string id PK
        string user_id FK
        string application_id FK
        string permission_id FK
        string permission_type
        boolean granted
        json conditions
        datetime assigned_at
        string assigned_by FK
        datetime expires_at
    }
    
    PERMISSION_OVERRIDES {
        string id PK
        string user_id FK
        string application_id FK
        string resource_type
        string action_type
        string override_type
        boolean allow_override
        json conditions
        string reason
        datetime created_at
        string created_by FK
        datetime expires_at
    }
    
    RESOURCE_PERMISSIONS {
        string id PK
        string application_id FK
        string resource_id
        string resource_type
        string owner_id FK
        json access_rules
        boolean public_access
        datetime created_at
        datetime updated_at
    }
    
    USERS ||--o{ USER_GLOBAL_ROLES : "has global roles"
    SYSTEM_ROLES ||--o{ USER_GLOBAL_ROLES : "assigned to users"
    USERS ||--o{ USER_APPLICATION_ROLES : "has app roles"
    APPLICATION_ROLES ||--o{ USER_APPLICATION_ROLES : "assigned to users"
    APPLICATIONS ||--o{ APPLICATION_ROLES : "defines"
    APPLICATIONS ||--o{ APPLICATION_PERMISSIONS : "defines"
    USERS ||--o{ USER_DIRECT_PERMISSIONS : "has direct permissions"
    APPLICATION_PERMISSIONS ||--o{ USER_DIRECT_PERMISSIONS : "granted to users"
    USERS ||--o{ PERMISSION_OVERRIDES : "has overrides"
    APPLICATIONS ||--o{ RESOURCE_PERMISSIONS : "contains resources"
    USERS ||--o{ RESOURCE_PERMISSIONS : "owns resources"
```

#### Permission Types and Scopes

```mermaid
graph TB
    subgraph "Global Permissions"
        subgraph "System Administration"
            SysAdmin[system.admin]
            UserMgmt[user.management]
            AppMgmt[application.management]
            AuditView[audit.view]
        end
        
        subgraph "Cross-Application"
            MultiApp[multi.app.access]
            GlobalRead[global.read]
            GlobalWrite[global.write]
        end
    end
    
    subgraph "Application-Specific Permissions"
        subgraph "Application Management"
            AppAdmin[app.admin]
            AppConfig[app.configure]
            AppUsers[app.user.manage]
            AppRoles[app.role.manage]
        end
        
        subgraph "Resource Permissions"
            ResCreate[resource.create]
            ResRead[resource.read]
            ResUpdate[resource.update]
            ResDelete[resource.delete]
            ResShare[resource.share]
        end
        
        subgraph "Feature Permissions"
            FeatureA[feature.a.access]
            FeatureB[feature.b.access]
            FeatureC[feature.c.admin]
        end
    end
    
    subgraph "User-Level Permissions"
        subgraph "Profile Management"
            ProfileRead[profile.read]
            ProfileUpdate[profile.update]
            ProfileDelete[profile.delete]
        end
        
        subgraph "Data Access"
            OwnData[own.data.access]
            SharedData[shared.data.access]
            PublicData[public.data.access]
        end
    end
    
    subgraph "Resource-Level Permissions"
        subgraph "Document Permissions"
            DocOwner[document.owner]
            DocEditor[document.editor]
            DocViewer[document.viewer]
            DocCommenter[document.commenter]
        end
        
        subgraph "Dynamic Permissions"
            TimeBasedPerm[time.based.access]
            LocationPerm[location.based.access]
            ContextPerm[context.based.access]
        end
    end
```

#### Permission Inheritance Model

```mermaid
graph TB
    subgraph "Inheritance Hierarchy"
        GlobalLevel[Global Level Permissions]
        SystemLevel[System Role Permissions]
        AppLevel[Application Level Permissions]
        RoleLevel[Role-Based Permissions]
        UserLevel[Direct User Permissions]
        ResourceLevel[Resource-Specific Permissions]
        OverrideLevel[Permission Overrides]
    end
    
    GlobalLevel -->|Inherits Down| SystemLevel
    SystemLevel -->|Inherits Down| AppLevel
    AppLevel -->|Inherits Down| RoleLevel
    RoleLevel -->|Inherits Down| UserLevel
    UserLevel -->|Inherits Down| ResourceLevel
    
    OverrideLevel -->|Can Override| GlobalLevel
    OverrideLevel -->|Can Override| SystemLevel
    OverrideLevel -->|Can Override| AppLevel
    OverrideLevel -->|Can Override| RoleLevel
    OverrideLevel -->|Can Override| UserLevel
    OverrideLevel -->|Can Override| ResourceLevel
    
    subgraph "Inheritance Rules"
        Rule1[Higher level permissions are inherited by lower levels]
        Rule2[Lower level permissions can be more restrictive]
        Rule3[Explicit denials override inherited permissions]
        Rule4[User overrides take precedence over role permissions]
        Rule5[Resource-level permissions are most specific]
    end
    
    style OverrideLevel fill:#ff6666
    style GlobalLevel fill:#ff9999
    style ResourceLevel fill:#99ff99
```

#### Permission Evaluation Engine

```mermaid
graph TB
    subgraph "Permission Evaluation Components"
        subgraph "Input Processing"
            RequestParser[Request Parser]
            ContextExtractor[Context Extractor]
            UserResolver[User Resolver]
            ResourceResolver[Resource Resolver]
        end
        
        subgraph "Permission Resolution"
            GlobalResolver[Global Permission Resolver]
            AppResolver[Application Permission Resolver]
            RoleResolver[Role Permission Resolver]
            UserResolver2[User Permission Resolver]
            ResourceResolver2[Resource Permission Resolver]
        end
        
        subgraph "Evaluation Engine"
            PermissionMerger[Permission Merger]
            ConflictResolver[Conflict Resolver]
            ContextEvaluator[Context Evaluator]
            PolicyEngine[Policy Engine]
        end
        
        subgraph "Output Generation"
            DecisionMaker[Decision Maker]
            AuditLogger[Audit Logger]
            CacheManager[Cache Manager]
            ResponseBuilder[Response Builder]
        end
    end
    
    RequestParser --> ContextExtractor
    ContextExtractor --> UserResolver
    UserResolver --> ResourceResolver
    
    ResourceResolver --> GlobalResolver
    GlobalResolver --> AppResolver
    AppResolver --> RoleResolver
    RoleResolver --> UserResolver2
    UserResolver2 --> ResourceResolver2
    
    ResourceResolver2 --> PermissionMerger
    PermissionMerger --> ConflictResolver
    ConflictResolver --> ContextEvaluator
    ContextEvaluator --> PolicyEngine
    
    PolicyEngine --> DecisionMaker
    DecisionMaker --> AuditLogger
    AuditLogger --> CacheManager
    CacheManager --> ResponseBuilder
```

#### Permission Caching Strategy

```mermaid
graph TB
    subgraph "Multi-Level Caching"
        subgraph "L1 Cache - In-Memory"
            UserPermCache[User Permission Cache]
            RolePermCache[Role Permission Cache]
            AppPermCache[Application Permission Cache]
        end
        
        subgraph "L2 Cache - Redis"
            DistributedCache[Distributed Permission Cache]
            SessionCache[Session-Based Cache]
            ComputedPermCache[Computed Permission Cache]
        end
        
        subgraph "L3 Cache - Database"
            MaterializedViews[Materialized Permission Views]
            IndexedQueries[Indexed Permission Queries]
            PrecomputedPerms[Precomputed Permission Sets]
        end
    end
    
    subgraph "Cache Invalidation"
        UserChange[User Role Change]
        RoleChange[Role Permission Change]
        AppChange[Application Permission Change]
        TimeExpiry[Time-Based Expiry]
    end
    
    UserPermCache --> DistributedCache
    RolePermCache --> DistributedCache
    AppPermCache --> DistributedCache
    
    DistributedCache --> MaterializedViews
    SessionCache --> IndexedQueries
    ComputedPermCache --> PrecomputedPerms
    
    UserChange --> UserPermCache
    RoleChange --> RolePermCache
    AppChange --> AppPermCache
    TimeExpiry --> DistributedCache
    
    style UserPermCache fill:#ffcccc
    style DistributedCache fill:#ccffcc
    style MaterializedViews fill:#ccccff
```

#### Permission Type Definitions

##### 1. Global Permissions
Global permissions apply across all applications and are typically reserved for system administrators.

**Examples:**
- `system.admin` - Full system administration access
- `user.management.global` - Manage users across all applications
- `application.create` - Create new applications
- `audit.view.all` - View all audit logs across applications
- `security.configure` - Configure system-wide security settings

**Use Cases:**
- Platform administrators managing the entire auth server
- Security teams monitoring all applications
- DevOps teams managing system configuration

##### 2. System Role Permissions
System roles are predefined roles with specific global permissions.

**System Roles:**
- **Super Admin**: All global permissions
- **Platform Admin**: Application and user management
- **Security Admin**: Security configuration and audit access
- **Support Admin**: Read-only access to user data and logs
- **Auditor**: Read-only access to audit logs

##### 3. Application-Specific Permissions
Permissions scoped to a specific application, defined by application owners.

**Examples:**
- `blog.post.create` - Create blog posts in the blog application
- `ecommerce.order.view` - View orders in the e-commerce application
- `crm.contact.delete` - Delete contacts in the CRM application
- `analytics.dashboard.access` - Access analytics dashboard

**Structure:**
```typescript
{
  applicationId: "app-123",
  name: "post.create",
  resource: "posts",
  action: "create",
  conditions: {
    requiresApproval: false,
    maxPerDay: 10
  }
}
```

##### 4. Role-Based Permissions
Permissions bundled into roles for easier management.

**Application Role Examples:**
- **Admin Role**: All application permissions
- **Editor Role**: Create, read, update permissions
- **Viewer Role**: Read-only permissions
- **Contributor Role**: Create and read permissions

**Role Definition:**
```typescript
{
  applicationId: "app-123",
  name: "Editor",
  permissions: [
    "post.create",
    "post.read",
    "post.update",
    "comment.moderate"
  ],
  hierarchyLevel: 2
}
```

##### 5. Direct User Permissions
Permissions assigned directly to a user, bypassing roles.

**Use Cases:**
- Temporary elevated access
- Special user privileges
- Exception handling
- Beta feature access

**Example:**
```typescript
{
  userId: "user-456",
  applicationId: "app-123",
  permissionId: "feature.beta.access",
  granted: true,
  expiresAt: "2024-12-31T23:59:59Z",
  conditions: {
    ipWhitelist: ["192.168.1.0/24"]
  }
}
```

##### 6. Permission Overrides
Explicit allow or deny rules that override inherited permissions.

**Override Types:**
- **Explicit Allow**: Grant permission even if role doesn't have it
- **Explicit Deny**: Deny permission even if role has it
- **Conditional Override**: Apply based on context (time, location, etc.)

**Example:**
```typescript
{
  userId: "user-456",
  applicationId: "app-123",
  resourceType: "sensitive-data",
  actionType: "delete",
  overrideType: "explicit_deny",
  allowOverride: false,
  reason: "User is not authorized for data deletion",
  conditions: {
    alwaysApply: true
  }
}
```

##### 7. Resource-Level Permissions
Permissions on specific resource instances (e.g., specific documents, records).

**Examples:**
- Document ownership and sharing
- Record-level access control
- Object-level permissions

**Resource Permission Model:**
```typescript
{
  resourceId: "doc-789",
  resourceType: "document",
  ownerId: "user-456",
  accessRules: {
    "user-123": ["read", "comment"],
    "user-456": ["read", "write", "delete", "share"],
    "role-editor": ["read", "write"],
    "public": ["read"]
  },
  publicAccess: false
}
```

##### 8. Context-Aware Permissions
Permissions that depend on contextual factors.

**Context Types:**
- **Time-Based**: Access only during business hours
- **Location-Based**: Access only from specific IP ranges
- **Device-Based**: Access only from approved devices
- **Condition-Based**: Access based on custom conditions

**Example:**
```typescript
{
  permissionId: "financial.data.access",
  conditions: {
    timeWindow: {
      start: "09:00",
      end: "17:00",
      timezone: "UTC",
      daysOfWeek: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"]
    },
    ipWhitelist: ["10.0.0.0/8"],
    requiresMFA: true,
    deviceTrust: "high"
  }
}
```

#### Permission Evaluation Examples

##### Example 1: Simple Permission Check
```typescript
// User: john@example.com
// Application: blog-app
// Action: Create a blog post

// Step 1: Check global permissions
globalPermissions = [] // No global permissions

// Step 2: Check application admin role
isAppAdmin = false

// Step 3: Check application roles
userRoles = ["Editor"] // User has Editor role in blog-app
rolePermissions = ["post.create", "post.read", "post.update"]

// Step 4: Check direct permissions
directPermissions = [] // No direct permissions

// Step 5: Check overrides
overrides = [] // No overrides

// Step 6: Evaluate
hasPermission("post.create") = true // From Editor role
```

##### Example 2: Complex Permission Check with Override
```typescript
// User: jane@example.com
// Application: financial-app
// Action: Delete financial record
// Time: 2:00 AM

// Step 1: Check global permissions
globalPermissions = [] // No global permissions

// Step 2: Check application roles
userRoles = ["Admin"] // User has Admin role
rolePermissions = ["record.delete", "record.read", "record.update"]

// Step 3: Check overrides
overrides = [{
  actionType: "delete",
  overrideType: "explicit_deny",
  conditions: {
    timeWindow: { start: "09:00", end: "17:00" }
  }
}]

// Step 4: Evaluate context
currentTime = "02:00" // Outside allowed time window

// Step 5: Final decision
hasPermission("record.delete") = false // Denied by time-based override
```

##### Example 3: Multi-Application Permission Check
```typescript
// User: admin@example.com
// Applications: [app1, app2, app3]
// Action: View user data across all apps

// Step 1: Check global permissions
globalPermissions = ["user.management.global"]

// Step 2: Evaluate
hasPermission("user.view", "app1") = true // From global permission
hasPermission("user.view", "app2") = true // From global permission
hasPermission("user.view", "app3") = true // From global permission

// Global permissions grant access across all applications
```

##### Example 4: Resource-Level Permission Check
```typescript
// User: bob@example.com
// Resource: document-123
// Action: Edit document

// Step 1: Check resource ownership
resourceOwner = "alice@example.com" // Bob is not the owner

// Step 2: Check resource access rules
resourceAccessRules = {
  "bob@example.com": ["read", "comment"],
  "alice@example.com": ["read", "write", "delete", "share"]
}

// Step 3: Check application role permissions
userRoles = ["Editor"] // Has general edit permission

// Step 4: Evaluate
hasPermission("document.edit", "document-123") = false
// Resource-level permissions are more specific and deny edit access
// Bob only has read and comment access to this specific document
```

### API Architecture

```mermaid
graph TB
    subgraph "Public API Endpoints"
        subgraph "Authentication Endpoints"
            Register[POST /auth/register]
            Login[POST /auth/login]
            Logout[POST /auth/logout]
            Refresh[POST /auth/refresh]
            Verify[POST /auth/verify-email]
            ForgotPwd[POST /auth/forgot-password]
            ResetPwd[POST /auth/reset-password]
        end
        
        subgraph "User Management Endpoints"
            GetProfile[GET /users/profile]
            UpdateProfile[PUT /users/profile]
            ChangePwd[PUT /users/change-password]
            EnableMFA[POST /users/mfa/enable]
            VerifyMFA[POST /users/mfa/verify]
            GetActivity[GET /users/activity]
        end
        
        subgraph "Token Management Endpoints"
            ValidateToken[POST /tokens/validate]
            RevokeToken[POST /tokens/revoke]
            TokenInfo[GET /tokens/info]
        end
    end
    
    subgraph "Application Management API"
        subgraph "Application Endpoints"
            CreateApp[POST /applications]
            GetApp[GET /applications/:id]
            UpdateApp[PUT /applications/:id]
            DeleteApp[DELETE /applications/:id]
            ListApps[GET /applications]
        end
        
        subgraph "Permission Endpoints"
            CreatePerm[POST /applications/:id/permissions]
            GetPerms[GET /applications/:id/permissions]
            UpdatePerm[PUT /permissions/:id]
            DeletePerm[DELETE /permissions/:id]
        end
        
        subgraph "Role Management Endpoints"
            CreateRole[POST /applications/:id/roles]
            GetRoles[GET /applications/:id/roles]
            AssignRole[POST /applications/:id/users/:userId/roles]
            RemoveRole[DELETE /applications/:id/users/:userId/roles/:roleId]
            GetUserRoles[GET /applications/:id/users/:userId/roles]
        end
    end
    
    subgraph "Admin API Endpoints"
        subgraph "User Administration"
            ListUsers[GET /admin/users]
            GetUser[GET /admin/users/:id]
            UpdateUser[PUT /admin/users/:id]
            DeleteUser[DELETE /admin/users/:id]
            LockUser[POST /admin/users/:id/lock]
            UnlockUser[POST /admin/users/:id/unlock]
        end
        
        subgraph "System Administration"
            GetStats[GET /admin/stats]
            GetAuditLogs[GET /admin/audit-logs]
            GetSessions[GET /admin/sessions]
            RevokeSessions[POST /admin/sessions/revoke]
            SystemHealth[GET /admin/health]
        end
    end
```

## Components and Interfaces

### Clean Architecture Component Organization

#### Dependency Flow Rules
1. **Entities** have no dependencies on outer layers
2. **Use Cases** depend only on Entities and interfaces
3. **Interface Adapters** depend on Use Cases and Entities
4. **Frameworks & Drivers** depend on Interface Adapters

### 1. Entities Layer Components

#### Core Domain Entities

```typescript
// Core business entities with pure domain logic
interface User {
  readonly id: UserId
  readonly email: Email
  readonly profile: UserProfile
  
  changePassword(currentPassword: string, newPassword: Password): void
  enableMFA(secret: TOTPSecret): void
  hasPermission(permission: Permission, context: PermissionContext): boolean
  assignRole(role: Role, application: Application): void
}

interface Application {
  readonly id: ApplicationId
  readonly name: string
  readonly owner: User
  
  addPermission(permission: Permission): void
  createRole(name: string, permissions: Permission[]): Role
  hasUser(user: User): boolean
}

interface Permission {
  readonly resource: string
  readonly action: string
  readonly conditions: PermissionCondition[]
  
  matches(resource: string, action: string, context: PermissionContext): boolean
  isGrantedTo(user: User, application: Application): boolean
}

interface Role {
  readonly id: RoleId
  readonly name: string
  readonly permissions: Permission[]
  readonly application: Application
  
  hasPermission(permission: Permission): boolean
  addPermission(permission: Permission): void
}
```

#### Domain Services
```typescript
interface PasswordService {
  hash(plainPassword: string): Promise<Password>
  verify(plainPassword: string, hashedPassword: Password): Promise<boolean>
  generateResetToken(): string
}

interface TokenService {
  generateAccessToken(user: User, application?: Application): Token
  generateRefreshToken(user: User): Token
  validateToken(token: string): TokenPayload
}

interface PermissionService {
  checkPermission(user: User, permission: Permission, context: PermissionContext): Promise<boolean>
  resolveUserPermissions(user: User, application: Application): Permission[]
  evaluatePermissionHierarchy(permissions: Permission[]): Permission[]
}
```

### 2. Use Cases Layer Components

#### Authentication Use Cases
```typescript
interface RegisterUserUseCase {
  execute(request: RegisterUserRequest): Promise<RegisterUserResponse>
}

interface AuthenticateUserUseCase {
  execute(request: AuthenticateUserRequest): Promise<AuthenticateUserResponse>
}

interface RefreshTokenUseCase {
  execute(request: RefreshTokenRequest): Promise<RefreshTokenResponse>
}

interface ResetPasswordUseCase {
  execute(request: ResetPasswordRequest): Promise<ResetPasswordResponse>
}
```

#### Authorization Use Cases
```typescript
interface CheckPermissionUseCase {
  execute(request: CheckPermissionRequest): Promise<CheckPermissionResponse>
}

interface AssignRoleUseCase {
  execute(request: AssignRoleRequest): Promise<AssignRoleResponse>
}

interface CreateApplicationUseCase {
  execute(request: CreateApplicationRequest): Promise<CreateApplicationResponse>
}

interface ManagePermissionsUseCase {
  execute(request: ManagePermissionsRequest): Promise<ManagePermissionsResponse>
}
```

#### User Management Use Cases
```typescript
interface UpdateProfileUseCase {
  execute(request: UpdateProfileRequest): Promise<UpdateProfileResponse>
}

interface EnableMFAUseCase {
  execute(request: EnableMFARequest): Promise<EnableMFAResponse>
}

interface ViewActivityUseCase {
  execute(request: ViewActivityRequest): Promise<ViewActivityResponse>
}
```

### 3. Interface Adapters Layer Components

#### Controllers (HTTP Adapters)
```typescript
interface AuthController {
  register(req: HttpRequest): Promise<HttpResponse>
  login(req: HttpRequest): Promise<HttpResponse>
  logout(req: HttpRequest): Promise<HttpResponse>
  verifyEmail(req: HttpRequest): Promise<HttpResponse>
  forgotPassword(req: HttpRequest): Promise<HttpResponse>
  resetPassword(req: HttpRequest): Promise<HttpResponse>
}

interface UserController {
  getProfile(req: HttpRequest): Promise<HttpResponse>
  updateProfile(req: HttpRequest): Promise<HttpResponse>
  changePassword(req: HttpRequest): Promise<HttpResponse>
  enableMFA(req: HttpRequest): Promise<HttpResponse>
  verifyMFA(req: HttpRequest): Promise<HttpResponse>
  getAccountActivity(req: HttpRequest): Promise<HttpResponse>
}

interface ApplicationController {
  createApplication(req: HttpRequest): Promise<HttpResponse>
  getApplication(req: HttpRequest): Promise<HttpResponse>
  updateApplication(req: HttpRequest): Promise<HttpResponse>
  deleteApplication(req: HttpRequest): Promise<HttpResponse>
  managePermissions(req: HttpRequest): Promise<HttpResponse>
}
```

#### Presenters (Response Formatters)
```typescript
interface AuthPresenter {
  presentRegistration(response: RegisterUserResponse): HttpResponse
  presentLogin(response: AuthenticateUserResponse): HttpResponse
  presentError(error: DomainError): HttpResponse
}

interface UserPresenter {
  presentProfile(response: UpdateProfileResponse): HttpResponse
  presentActivity(response: ViewActivityResponse): HttpResponse
  presentMFASetup(response: EnableMFAResponse): HttpResponse
}
```

#### Repository Interfaces (defined in Use Cases, implemented in Interface Adapters)
```typescript
interface UserRepository {
  save(user: User): Promise<void>
  findById(id: UserId): Promise<User | null>
  findByEmail(email: Email): Promise<User | null>
  findByApplication(application: Application): Promise<User[]>
  delete(id: UserId): Promise<void>
}

interface ApplicationRepository {
  save(application: Application): Promise<void>
  findById(id: ApplicationId): Promise<Application | null>
  findByOwner(owner: User): Promise<Application[]>
  findAll(): Promise<Application[]>
  delete(id: ApplicationId): Promise<void>
}

interface PermissionRepository {
  save(permission: Permission): Promise<void>
  findByApplication(application: Application): Promise<Permission[]>
  findByRole(role: Role): Promise<Permission[]>
  findByUser(user: User, application: Application): Promise<Permission[]>
}
```

#### Gateway Interfaces (External Service Adapters)
```typescript
interface EmailGateway {
  sendVerificationEmail(user: User, token: string): Promise<void>
  sendPasswordResetEmail(user: User, token: string): Promise<void>
  sendSecurityAlert(user: User, event: SecurityEvent): Promise<void>
}

interface CacheGateway {
  set(key: string, value: any, ttl: number): Promise<void>
  get(key: string): Promise<any | null>
  delete(key: string): Promise<void>
  exists(key: string): Promise<boolean>
}

interface AuditGateway {
  log(event: AuditEvent): Promise<void>
  query(criteria: AuditCriteria): Promise<AuditEvent[]>
}
```

### 2. Services

#### AuthService
Core authentication business logic.

```typescript
interface AuthService {
  registerUser(email: string, password: string): Promise<User>
  authenticateUser(email: string, password: string): Promise<AuthResult>
  verifyEmailToken(token: string): Promise<void>
  sendVerificationEmail(user: User): Promise<void>
  handleFailedLogin(userId: string): Promise<void>
  checkAccountLock(userId: string): Promise<boolean>
  initiatePasswordReset(email: string): Promise<void>
  resetPassword(token: string, newPassword: string): Promise<void>
}
```

#### TokenService
Manages token lifecycle with application context.

```typescript
interface TokenService {
  generateTokenPair(user: User, applicationId?: string): Promise<TokenPair>
  refreshTokens(refreshToken: string): Promise<TokenPair>
  validateAccessToken(token: string, applicationId?: string): Promise<TokenPayload>
  revokeToken(token: string): Promise<void>
  revokeAllUserTokens(userId: string, applicationId?: string): Promise<void>
  revokeApplicationTokens(applicationId: string): Promise<void>
}
```

#### EmailService
Handles email notifications and verification.

```typescript
interface EmailService {
  sendVerificationEmail(user: User, token: string): Promise<void>
  sendPasswordResetEmail(user: User, token: string): Promise<void>
  sendSecurityAlert(user: User, event: SecurityEvent): Promise<void>
  sendMFASetupEmail(user: User, backupCodes: string[]): Promise<void>
}
```

#### MFAService
Multi-factor authentication management.

```typescript
interface MFAService {
  generateTOTPSecret(userId: string): Promise<TOTPSetup>
  verifyTOTPToken(userId: string, token: string): Promise<boolean>
  generateBackupCodes(userId: string): Promise<string[]>
  verifyBackupCode(userId: string, code: string): Promise<boolean>
}
```

#### ApplicationService
Application management and configuration.

```typescript
interface ApplicationService {
  createApplication(name: string, description: string, clientId: string): Promise<Application>
  getApplication(applicationId: string): Promise<Application>
  getApplicationByClientId(clientId: string): Promise<Application>
  updateApplication(applicationId: string, data: UpdateApplicationData): Promise<Application>
  deleteApplication(applicationId: string): Promise<void>
  validateApplicationAccess(clientId: string, clientSecret: string): Promise<Application>
}
```

#### RoleService
Role and permission management with application context.

```typescript
interface RoleService {
  createRole(applicationId: string, name: string, permissions: string[]): Promise<Role>
  assignUserRole(applicationId: string, userId: string, roleId: string): Promise<void>
  removeUserRole(applicationId: string, userId: string, roleId: string): Promise<void>
  getUserRoles(userId: string, applicationId?: string): Promise<Role[]>
  checkPermission(userId: string, permission: string, applicationId: string): Promise<boolean>
  getUserPermissions(userId: string, applicationId: string): Promise<Permission[]>
}
```

#### PermissionService
Permission management and validation.

```typescript
interface PermissionService {
  createPermission(applicationId: string, name: string, description: string, resource: string, action: string): Promise<Permission>
  getApplicationPermissions(applicationId: string): Promise<Permission[]>
  validatePermission(userId: string, applicationId: string, resource: string, action: string): Promise<boolean>
  getPermissionsByRole(roleId: string): Promise<Permission[]>
}
```

### 3. Security Components

#### JWTManager
JWT token generation and validation.

```typescript
interface JWTManager {
  generateAccessToken(payload: TokenPayload): string
  generateRefreshToken(payload: TokenPayload): string
  verifyToken(token: string): TokenPayload
  decodeToken(token: string): TokenPayload | null
}
```

#### PasswordHasher
Secure password hashing.

```typescript
interface PasswordHasher {
  hash(password: string): Promise<string>
  verify(password: string, hash: string): Promise<boolean>
  needsRehash(hash: string): boolean
}
```

#### Encryptor
Data encryption for sensitive information.

```typescript
interface Encryptor {
  encrypt(data: string): string
  decrypt(encrypted: string): string
  encryptObject<T>(obj: T): string
  decryptObject<T>(encrypted: string): T
}
```

#### RateLimiter
Request rate limiting.

```typescript
interface RateLimiter {
  checkLimit(key: string, limit: number, window: number): Promise<boolean>
  increment(key: string, window: number): Promise<number>
  reset(key: string): Promise<void>
}
```

### 4. Repositories

#### UserRepository
User data persistence.

```typescript
interface UserRepository {
  create(user: CreateUserDTO): Promise<User>
  findById(id: string): Promise<User | null>
  findByEmail(email: string): Promise<User | null>
  update(id: string, data: UpdateUserDTO): Promise<User>
  delete(id: string): Promise<void>
  incrementFailedLogins(id: string): Promise<number>
  resetFailedLogins(id: string): Promise<void>
}
```

#### TokenRepository
Token storage and management.

```typescript
interface TokenRepository {
  storeRefreshToken(token: RefreshTokenData): Promise<void>
  findRefreshToken(token: string): Promise<RefreshTokenData | null>
  revokeToken(token: string): Promise<void>
  revokeUserTokens(userId: string): Promise<void>
  cleanExpiredTokens(): Promise<number>
}
```

#### SessionRepository
Session management.

```typescript
interface SessionRepository {
  create(session: CreateSessionDTO): Promise<Session>
  findById(id: string): Promise<Session | null>
  findByUserId(userId: string): Promise<Session[]>
  update(id: string, data: UpdateSessionDTO): Promise<Session>
  delete(id: string): Promise<void>
  deleteUserSessions(userId: string): Promise<void>
}
```

#### AuditRepository
Security audit logging.

```typescript
interface AuditRepository {
  log(event: AuditEvent): Promise<void>
  findByUserId(userId: string, limit: number): Promise<AuditEvent[]>
  findByApplicationId(applicationId: string, limit: number): Promise<AuditEvent[]>
  findByType(type: string, from: Date, to: Date): Promise<AuditEvent[]>
  findSuspiciousActivity(criteria: SuspiciousActivityCriteria): Promise<AuditEvent[]>
}
```

#### ApplicationRepository
Application data persistence.

```typescript
interface ApplicationRepository {
  create(application: CreateApplicationDTO): Promise<Application>
  findById(id: string): Promise<Application | null>
  findByClientId(clientId: string): Promise<Application | null>
  update(id: string, data: UpdateApplicationDTO): Promise<Application>
  delete(id: string): Promise<void>
  findAll(): Promise<Application[]>
}
```

#### PermissionRepository
Permission data persistence.

```typescript
interface PermissionRepository {
  create(permission: CreatePermissionDTO): Promise<Permission>
  findById(id: string): Promise<Permission | null>
  findByApplicationId(applicationId: string): Promise<Permission[]>
  findByRoleId(roleId: string): Promise<Permission[]>
  update(id: string, data: UpdatePermissionDTO): Promise<Permission>
  delete(id: string): Promise<void>
}
```

#### UserApplicationRoleRepository
User-Application-Role relationship management.

```typescript
interface UserApplicationRoleRepository {
  assignRole(userId: string, applicationId: string, roleId: string): Promise<void>
  removeRole(userId: string, applicationId: string, roleId: string): Promise<void>
  findUserRoles(userId: string, applicationId?: string): Promise<UserApplicationRole[]>
  findApplicationUsers(applicationId: string): Promise<UserApplicationRole[]>
  removeAllUserRoles(userId: string, applicationId?: string): Promise<void>
  removeAllApplicationRoles(applicationId: string): Promise<void>
}
```

## Data Models

### User
```typescript
interface User {
  id: string
  email: string
  passwordHash: string
  emailVerified: boolean
  mfaEnabled: boolean
  mfaSecret: string | null  // encrypted
  backupCodes: string[] | null  // encrypted
  failedLoginAttempts: number
  lockedUntil: Date | null
  lastLoginAt: Date | null
  createdAt: Date
  updatedAt: Date
}
```

### Application
```typescript
interface Application {
  id: string
  name: string
  description: string
  clientId: string  // unique identifier for API access
  clientSecret: string  // hashed secret for authentication
  redirectUris: string[]  // allowed redirect URIs
  scopes: string[]  // available scopes for this application
  active: boolean
  createdAt: Date
  updatedAt: Date
}
```

### Permission
```typescript
interface Permission {
  id: string
  applicationId: string
  name: string  // e.g., "read_users", "write_posts"
  description: string
  resource: string  // e.g., "users", "posts", "settings"
  action: string  // e.g., "read", "write", "delete", "admin"
  createdAt: Date
  updatedAt: Date
}
```

### UserApplicationRole
```typescript
interface UserApplicationRole {
  id: string
  userId: string
  applicationId: string
  roleId: string
  assignedAt: Date
  assignedBy: string  // user ID who assigned the role
}
```

### PasswordReset
```typescript
interface PasswordReset {
  id: string
  userId: string
  token: string  // hashed
  expiresAt: Date
  used: boolean
  createdAt: Date
}
```

### Session
```typescript
interface Session {
  id: string
  userId: string
  accessToken: string
  refreshToken: string
  ipAddress: string
  userAgent: string
  expiresAt: Date
  createdAt: Date
  lastActivityAt: Date
}
```

### Role
```typescript
interface Role {
  id: string
  applicationId: string  // roles are application-specific
  name: string
  description: string
  permissions: string[]  // array of permission IDs
  createdAt: Date
  updatedAt: Date
}
```

### AuditEvent
```typescript
interface AuditEvent {
  id: string
  userId: string | null
  applicationId: string | null  // which application the event relates to
  type: string  // 'login', 'logout', 'register', 'permission_check', etc.
  action: string
  resource: string | null  // what resource was accessed
  ipAddress: string
  userAgent: string
  metadata: object
  success: boolean
  errorMessage: string | null
  timestamp: Date
}
```

### TokenPayload
```typescript
interface TokenPayload {
  sub: string  // user id
  email: string
  applicationId?: string  // application context for the token
  roles: ApplicationRole[]  // roles with application context
  permissions: ApplicationPermission[]  // permissions with application context
  iat: number
  exp: number
  type: 'access' | 'refresh'
}

interface ApplicationRole {
  applicationId: string
  roleId: string
  roleName: string
}

interface ApplicationPermission {
  applicationId: string
  permissionId: string
  resource: string
  action: string
}
```

## Correctness Properties

*A property is a characteristic or behavior that should hold true across all valid executions of a system—essentially, a formal statement about what the system should do. Properties serve as the bridge between human-readable specifications and machine-verifiable correctness guarantees.*

### Property Reflection

After analyzing all acceptance criteria, several properties can be consolidated to eliminate redundancy:

- **Serialization/Deserialization**: Multiple criteria (2.5, 3.4/3.5, 4.4/4.5, 5.4/5.5, 6.4/6.5, 7.4/7.5, 8.4, 10.4/10.5) all test round-trip serialization. These can be combined into comprehensive round-trip properties for each data type.
- **Token Generation/Validation**: Criteria 6.4 and 6.5 can be combined into a single JWT round-trip property.
- **Rate Limiting**: Criteria 9.3 and 9.4 can be combined into a single property about rate limit data consistency.

### Core Properties

**Property 1: User registration with valid credentials**
*For any* valid email and password combination, registering a user should create a new account with properly encrypted credentials and trigger email verification
**Validates: Requirements 1.1, 1.4**

**Property 2: Duplicate email prevention**
*For any* email address, attempting to register multiple users with the same email should reject subsequent registrations
**Validates: Requirements 1.2**

**Property 3: Input validation consistency**
*For any* invalid email format or weak password, the registration system should reject the input with specific validation errors
**Validates: Requirements 1.3**

**Property 4: Email verification enforcement**
*For any* unverified user account, authentication attempts should be rejected until email verification is completed
**Validates: Requirements 1.5**

**Property 5: Successful authentication token generation**
*For any* valid user credentials, successful authentication should return both access and refresh tokens with correct user claims
**Validates: Requirements 2.1**

**Property 6: Failed authentication handling**
*For any* invalid credentials, authentication should be rejected and the failed login counter should be incremented
**Validates: Requirements 2.2**

**Property 7: Account locking after failed attempts**
*For any* user account, exceeding the maximum failed login attempts should lock the account and prevent further authentication
**Validates: Requirements 2.3**

**Property 8: Session creation and audit logging**
*For any* successful authentication, a new session should be created and an audit event should be logged with complete metadata
**Validates: Requirements 2.4**

**Property 9: Credential parsing round-trip**
*For any* valid user credentials, parsing then serializing should produce equivalent credential data
**Validates: Requirements 2.5**

**Property 10: Password reset token generation**
*For any* valid email address, requesting password reset should generate a secure reset token and send reset instructions
**Validates: Requirements 3.1**

**Property 11: Password reset token validation**
*For any* valid password reset token within expiration window, the system should allow password changes
**Validates: Requirements 3.2**

**Property 12: Password reset token invalidation**
*For any* used password reset token, subsequent attempts to use the same token should be rejected
**Validates: Requirements 3.3**

**Property 13: Password reset token security**
*For any* password reset token, storing should hash the token and validation should verify both hash and expiration
**Validates: Requirements 3.4, 3.5**

**Property 14: Role assignment and session invalidation**
*For any* user and role combination, assigning a role should update permissions and invalidate existing sessions
**Validates: Requirements 4.1**

**Property 15: Permission consistency after role changes**
*For any* user with modified roles, all subsequent permission checks should reflect the new role assignments immediately
**Validates: Requirements 4.2**

**Property 16: Permission validation accuracy**
*For any* user, permission, and application combination, permission checks should accurately reflect current application-specific role assignments
**Validates: Requirements 4.3**

**Property 17: Role data round-trip**
*For any* role configuration, storing then retrieving role data should preserve all role information and user associations
**Validates: Requirements 4.4, 4.5**

**Property 18: MFA setup completeness**
*For any* user enabling MFA, the system should generate secure TOTP secrets and backup codes, storing them encrypted
**Validates: Requirements 5.1**

**Property 19: MFA enforcement during authentication**
*For any* MFA-enabled user, authentication should require both password and valid MFA token verification
**Validates: Requirements 5.2**

**Property 20: Invalid MFA token rejection**
*For any* invalid MFA token, authentication should be rejected and a security event should be logged
**Validates: Requirements 5.3**

**Property 21: MFA data encryption round-trip**
*For any* MFA configuration data, encrypting then decrypting should preserve all sensitive information securely
**Validates: Requirements 5.4, 5.5**

**Property 22: Token validation accuracy**
*For any* JWT token, validation should correctly verify signature, expiration, and return appropriate user information for valid tokens
**Validates: Requirements 6.1, 6.3**

**Property 23: Invalid token rejection**
*For any* expired or malformed token, validation should reject the token with appropriate error codes
**Validates: Requirements 6.2**

**Property 24: JWT round-trip consistency**
*For any* user claims with application context, generating a JWT then parsing it should preserve all claim information including application-specific roles and permissions
**Validates: Requirements 6.4, 6.5**

**Property 25: Refresh token exchange**
*For any* valid refresh token, the token refresh process should generate new access and refresh tokens while invalidating the old refresh token
**Validates: Requirements 7.1, 7.2**

**Property 26: Invalid refresh token handling**
*For any* expired or invalid refresh token, the system should require full re-authentication
**Validates: Requirements 7.3**

**Property 27: Refresh token encryption round-trip**
*For any* refresh token data, encrypting then decrypting should preserve token integrity and associated metadata
**Validates: Requirements 7.4, 7.5**

**Property 28: Comprehensive audit logging**
*For any* authentication event, the system should log complete metadata including timestamp, IP address, user agent, and event details
**Validates: Requirements 8.1**

**Property 29: Security alert triggering**
*For any* suspicious activity pattern, the system should trigger appropriate security alerts and protective measures
**Validates: Requirements 8.2**

**Property 30: Structured log formatting**
*For any* audit log entry, the format should be structured and searchable with all required security context
**Validates: Requirements 8.3**

**Property 31: Audit log data round-trip**
*For any* audit log entry, storing then querying should preserve all log information and support filtering by search criteria
**Validates: Requirements 8.4, 8.5**

**Property 32: Rate limiting enforcement**
*For any* request pattern exceeding configured thresholds, rate limiting should be applied with appropriate HTTP status codes
**Validates: Requirements 9.1**

**Property 33: Rate limit tracking accuracy**
*For any* series of requests, the system should accurately track request counts per IP address and user account
**Validates: Requirements 9.2**

**Property 34: Rate limit data consistency**
*For any* rate limiting counter, storing then retrieving should maintain accurate request counts and expiration times
**Validates: Requirements 9.3, 9.4**

**Property 35: Rate limit processing continuity**
*For any* rate-limited endpoint, requests within allowed limits should continue to be processed normally
**Validates: Requirements 9.5**

**Property 36: Account update validation**
*For any* account information update, the system should validate changes and persist them securely
**Validates: Requirements 10.1**

**Property 37: Password change security**
*For any* password change request, the system should verify the current password and enforce all password policy requirements
**Validates: Requirements 10.2**

**Property 38: Account activity display**
*For any* user account, viewing account activity should display recent authentication events and security actions accurately
**Validates: Requirements 10.3**

**Property 39: Application registration security**
*For any* application registration request, the system should create unique client credentials and store configuration securely
**Validates: Requirements 10.1**

**Property 40: Application authentication validation**
*For any* application authentication request, the system should validate client credentials and generate application-scoped tokens
**Validates: Requirements 10.2**

**Property 41: Application permission definition**
*For any* application permission definition, the system should store permission structures with proper resource and action mappings
**Validates: Requirements 10.3**

**Property 42: Application data round-trip**
*For any* application configuration data, storing then retrieving should preserve all application settings and permission structures
**Validates: Requirements 10.4, 10.5**

**Property 43: User-application role assignment**
*For any* user and application combination, assigning a role should create proper associations and update token permissions
**Validates: Requirements 11.1**

**Property 44: Application-specific permission validation**
*For any* user permission check within an application context, validation should reflect application-specific role assignments
**Validates: Requirements 11.2**

**Property 45: Multi-application permission isolation**
*For any* user accessing multiple applications, permissions should be properly isolated and scoped to each application
**Validates: Requirements 11.3**

**Property 46: Role assignment data round-trip**
*For any* user-application-role relationship, storing then retrieving should preserve all assignment data with audit trails
**Validates: Requirements 11.4, 11.5**

**Property 47: Account data round-trip**
*For any* user account data, updating then retrieving should preserve all account settings and information consistently
**Validates: Requirements 12.4, 12.5**

## Error Handling

### Error Categories

1. **Validation Errors**: Input validation failures, format errors, constraint violations
2. **Authentication Errors**: Invalid credentials, expired tokens, account lockouts
3. **Authorization Errors**: Insufficient permissions, role violations, access denials
4. **Rate Limiting Errors**: Request throttling, quota exceeded, temporary blocks
5. **System Errors**: Database failures, external service unavailability, internal server errors
6. **Security Errors**: Suspicious activity detection, token tampering, encryption failures

### Error Response Format

```typescript
interface ErrorResponse {
  error: {
    code: string
    message: string
    details?: object
    timestamp: string
    requestId: string
  }
}
```

### Error Handling Strategy

- **Graceful Degradation**: System continues operating with reduced functionality when non-critical services fail
- **Circuit Breaker Pattern**: Prevent cascading failures when external services are unavailable
- **Retry Logic**: Automatic retry with exponential backoff for transient failures
- **Security-First**: Never expose sensitive information in error messages
- **Comprehensive Logging**: All errors logged with sufficient context for debugging
- **User-Friendly Messages**: Clear, actionable error messages for client applications

## Testing Strategy

### Dual Testing Approach

The authentication server requires both unit testing and property-based testing to ensure comprehensive coverage:

- **Unit Tests**: Verify specific examples, edge cases, and integration points between components
- **Property-Based Tests**: Verify universal properties that should hold across all inputs using generated test data

### Property-Based Testing Framework

**Framework**: We will use **fast-check** for JavaScript/TypeScript property-based testing, which provides:
- Comprehensive data generators for various input types
- Shrinking capabilities to find minimal failing examples
- Integration with popular testing frameworks like Jest

**Configuration**: Each property-based test will run a minimum of 100 iterations to ensure thorough coverage of the input space.

**Test Tagging**: Each property-based test must include a comment explicitly referencing the correctness property from this design document using the format: `**Feature: auth-server, Property {number}: {property_text}**`

### Unit Testing Focus Areas

- **Authentication Flow Integration**: End-to-end authentication scenarios
- **Error Boundary Testing**: Specific error conditions and edge cases
- **External Service Integration**: OAuth provider interactions, email service integration
- **Security Component Integration**: Token generation, encryption, rate limiting coordination

### Property-Based Testing Implementation

Each correctness property listed above must be implemented as a single property-based test. The tests will:

1. Generate random valid inputs using fast-check generators
2. Execute the system functionality
3. Verify the expected properties hold true
4. Use shrinking to find minimal counterexamples when properties fail

### Test Coverage Requirements

- **Functional Coverage**: All authentication flows, token operations, and user management features
- **Security Coverage**: All security-critical paths including encryption, validation, and access control
- **Error Coverage**: All error conditions and recovery scenarios
- **Performance Coverage**: Rate limiting and resource management under load

