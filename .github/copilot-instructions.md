# GitHub Copilot Project Instructions  
These instructions define the coding rules and structure Copilot must follow across this repository.  
The project uses **.NET Core**, **C#**, **Clean Architecture**, **Domain-Driven Design (DDD)**, **ABP Framework**, and **modular SaaS architecture**.

---

## 🚀 Coding Style Guidelines (C# / .NET)
- Always follow **Microsoft C# coding conventions**.
- Use **PascalCase** for classes, methods, properties.
- Use **camelCase** for private fields & local variables.
- Always use `async` + `await` properly.
- Avoid `Task.Result` or `.Wait()`.
- Avoid nested `if/else` — use guard clauses.
- Write **immutable** classes where possible.
- Prefer **Composition over Inheritance**.
- Avoid static state.
- Use Dependency Injection everywhere.
- Follow **SOLID principles** strictly.

---

## 🏛 Clean Architecture Rules
- Domain layer: **No external dependencies**.
- Application layer: Only domain references, no infrastructure references.
- Infrastructure layer: EF Core, Redis, Blob Storage, etc.
- API layer: Only uses Application Layer.

Copilot must **never violate layer boundaries**.

---

## 🧱 ABP Framework Rules
- Entities must derive from ABP base classes (`AggregateRoot`, `FullAuditedAggregateRoot`, etc.)
- Use ABP Repository pattern, not EF DbContext directly.
- Always use:
  - `IUnitOfWork`
  - `IRepository<T>`
  - DTO + AutoMapper
  - ABP Permission system (unless custom).
- Use `BackgroundJob`, `EventBus`, and `DistributedEventBus` for async flows.

---

# 🔥 Code Smell Avoidance (Full List from RefactoringGuru)
**Copilot must avoid producing code that includes ANY of the following smells.**

## 👉 General Code Smells
- Bloaters  
- Object-Orientation Abusers  
- Change Preventers  
- Dispensables  
- Couplers  

---

## 1️⃣ Bloaters (AVOID)
Copilot must avoid generating:
- Long Method  
- Large Class  
- Primitive Obsession  
- Long Parameter List  
- Data Clumps  
- Data Class  
- Lazy Class  
- Speculative Generality  

---

## 2️⃣ Object-Orientation Abusers
Copilot must avoid:
- Switch Statements (use polymorphism instead)
- Temporary Field  
- Refused Bequest  
- Alternative Classes with Different Interfaces  

---

## 3️⃣ Change Preventers
Copilot must never produce code with:
- Divergent Change  
- Shotgun Surgery  
- Parallel Inheritance Hierarchies  

---

## 4️⃣ Dispensables
Copilot should remove or avoid:
- Comments (over-use; self-documenting code preferred)
- Duplicate Code  
- Dead Code  
- Data Class  
- Lazy Class  
- Speculative Generality  

---

## 5️⃣ Couplers
Copilot must avoid:
- Feature Envy  
- Inappropriate Intimacy  
- Message Chains  
- Middle Man  
- Insider Trading  

---

# 🧬 Refactoring Rules (Copilot MUST follow)
For any generated code, Copilot should prefer:

### ✔ Extract Method  
### ✔ Extract Class  
### ✔ Introduce Parameter Object  
### ✔ Replace Nested Conditional with Guard Clauses  
### ✔ Replace Conditionals with Polymorphism  
### ✔ Remove Middle Man  
### ✔ Replace Magic Numbers with Constants  
### ✔ Introduce Null Object  
### ✔ Replace Inheritance with Delegation  
### ✔ Encapsulate Field  
### ✔ Hide Delegate  

---

# 🧠 Design Patterns to Prefer
Copilot must prioritize using:

### Structural
- Adapter  
- Facade  
- Composite  
- Decorator  

### Behavioral
- Strategy  
- State  
- Chain of Responsibility  
- Command  
- Observer  
- Template Method  

### Creational
- Factory Method  
- Abstract Factory  
- Builder  
- Prototype  
- Singleton (only when necessary, extremely limited use)

---

# 📦 Domain-Driven Design Rules
- Use Value Objects where appropriate.
- Entities must have business invariants.
- Use Domain Events for side-effects.
- Never place business logic in controllers.
- Use Services for domain operations.
- Use Aggregates to enforce invariants.
- Use Repositories only in Application layer.

---

# 📡 API Design Rules
- Use consistent REST/RESTful naming.
- Use DTO — never return entities.
- Use FluentValidation or ABP Validation.
- Always return proper HTTP statuses.
- Avoid fat controllers → move logic to services.

---

# 🔐 Security Rules
- Validate ALL user inputs.
- No sensitive data in logs.
- Use ABP permission system or custom RBAC.
- Use `IStringLocalizer` for all message strings.
- Sanitize user-generated content.

---

# 📊 Database & EF Core Rules
- Use Migrations properly.
- Avoid N+1 queries.
- Always use `AsNoTracking()` for read-only queries.
- Prefer Specification pattern.
- Avoid circular relationships.
- Use soft-delete properly for ABP entities.

---

# 🧪 Testing Rules
- Use xUnit.
- Use Moq / NSubstitute for mocks.
- Avoid testing internal ABP components.
- Write unit tests for domain services.
- Write integration tests for repository behavior.

---

# ✔ Final Summary
Copilot must generate:

- Clean, maintainable, SOLID C# code  
- Code free of all **RefactoringGuru Code Smells**  
- Code following **DDD**, **Clean Architecture**, **ABP Framework**  
- Proper layering with no cross-layer leaks  
- Highly readable, highly testable code  

Copilot MUST refuse or auto-correct any structure that introduces code smells.

