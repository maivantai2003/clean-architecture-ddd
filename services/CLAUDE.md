# Project Instructions — Microservices

## 1. Project Overview
- Project type: .NET microservices.
- Architecture: Clean Architecture + DDD where applicable.
- Each service owns its business logic and data.
- Verify the actual structure before making architectural assumptions.

## 2. Architecture Rules
- Domain must not depend on Application, Infrastructure, or WebAPI.
- Application contains use cases, business orchestration, abstractions, and application-level validation.
- Infrastructure implements persistence, messaging, external integrations, and other technical concerns.
- WebAPI handles HTTP concerns, authentication integration, middleware, and request/response mapping.
- Repository interfaces belong to Application when that matches the established project convention.
- Do not introduce unnecessary abstractions or force every pattern into every service.

## 3. Microservices Boundaries
- Do not access another service's database directly.
- Do not reference another service's internal Domain or Infrastructure project.
- Use explicit API contracts or integration events for cross-service communication.
- Keep service-specific data and business rules within their owning service.
- Put code in BuildingBlocks only when it is genuinely reusable and does not couple business domains.

## 4. Coding Standards
- Follow existing naming, folder structure, and dependency injection conventions.
- Use async/await for asynchronous I/O.
- Propagate CancellationToken through asynchronous operations where applicable.
- Avoid duplicated business rules and unnecessary abstractions.
- Never silently swallow exceptions.
- Add or update tests for behavior changes.

## 5. Persistence and Messaging
- Follow the service's existing EF Core and transaction conventions.
- Use Outbox/Inbox and idempotent consumers where required by the messaging design.
- Do not assume a database transaction spans multiple services.
- Never publish integration events before the required persistence transaction has committed unless the established design explicitly supports it.

## 6. Security
- Enforce authorization on the server, not only in the frontend.
- Follow the existing authentication and RBAC permission model.
- Validate input and enforce resource-level access where needed.
- Never hardcode secrets or place credentials in source files.
- Do not expose sensitive data in logs, prompts, or API responses.rules

## 7. Change Workflow
1. Inspect relevant files and existing patterns before coding.
2. Explain the proposed approach and affected projects.
3. Identify compatibility, security, and migration risks.
4. Implement the smallest coherent change.
5. Build and run relevant tests.
6. Report modified files, commands executed, results, and remaining risks.

## 8. Safety Constraints
- Do not delete files, reset Git state, or overwrite user changes without explicit approval.
- Do not run production deployments or destructive database operations without explicit approval.
- Do not add packages or change public API contracts without explaining the impact.
- If requirements or architecture are unclear, ask before making a risky assumption.

## 9. Repository-Specific Commands
- Document verified build and test commands here.
- Do not invent solution paths, project names, or commands.
