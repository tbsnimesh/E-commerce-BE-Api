# PROJECT_NOTES.md

> **Read this file before doing any work on the project.**
> This is the shared context between the human learner, the mentor (Claude in chat), and the executor (Claude Code).

---

## Project Summary

A learning-oriented e-commerce application built as a guided apprenticeship.
**Goal:** the learner understands every line of code and every architectural decision. Working software is a side effect of the learning, not the primary objective.

The learner is a beginner. Explanations matter more than speed.

---

## Tech Stack (locked in)

- **Backend:** ASP.NET Core Web API on .NET 10
- **ORM:** Entity Framework Core (code-first, with migrations)
- **Database:** SQL Server LocalDB (development)
- **Frontend:** Angular 21 — **standalone components, signals, modern control flow (@if / @for)**. No NgModules unless absolutely required by a library.
- **API communication:** Angular HttpClient + minimal RxJS
- **OS:** Windows
- **Editor:** VS Code (primary)

---

## Architectural Decisions

### Backend folder layout (pragmatic layered, not full Clean Architecture)
backend/ShopApi/
├── Domain/Entities/         # POCOs, no EF attributes where avoidable
├── Data/                    # AppDbContext, Configurations/, Migrations/, Seed/
├── Services/                # Interfaces + implementations (business logic)
├── Api/
│   ├── Controllers/
│   ├── Dtos/
│   ├── Mapping/             # manual mapping (no AutoMapper unless asked)
│   └── Validators/
├── Infrastructure/
│   ├── ExceptionHandling/
│   ├── Auth/
│   └── Logging/
└── Program.cs

### Frontend folder layout
frontend/src/app/
├── core/                    # singletons: services, interceptors, guards, models
├── shared/                  # reusable dumb UI: components, pipes, directives
├── features/                # one folder per feature: products, cart, orders, auth
├── app.routes.ts
├── app.config.ts
└── app.component.ts
## Conventions and Constraints

These are deliberate. Do not override them without asking the learner.

- **No Repository pattern wrapping EF Core.** `DbContext` already is a repository + unit-of-work. Services use `AppDbContext` directly.
- **No AutoMapper / Mapperly.** Manual mapping between entities and DTOs in `Api/Mapping/`. We will revisit if mapping becomes painful.
- **DTOs always for API input/output.** Never expose EF entities through controllers.
- **Async everywhere for I/O.** All EF calls use `Async` variants and `await`.
- **Validation at the API boundary** (DTOs, FluentValidation TBD).
- **Global exception handling via `IExceptionHandler` middleware.** No `try/catch` in controllers for cross-cutting error handling.
- **`ProblemDetails`** is the standard error response shape.
- **`ILogger<T>` via DI.** Serilog to be added later.
- **JWT auth** via ASP.NET Core Identity (introduced at milestone 9, not before).
- **CORS:** explicit allowed origins; never `AllowAnyOrigin()`.
- **Secrets:** .NET user-secrets in development. Never in source control.
- **Angular:** standalone components only. Signals for state. `inject()` over constructor DI where it reads cleaner. `OnPush` change detection by default once we get there.
- **No premature abstractions.** YAGNI. Add a layer only when a concrete pain motivates it.

---

## Milestone Status

- [x] **M0** — Planning, architecture, decisions
- [ ] **M1** — Project setup: scaffold backend + frontend, CORS, smoke test, first Git commit
- [ ] **M2** — Database, entities, first migration, seed data
- [ ] **M3** — Products API end-to-end (list with pagination, get by id, DTOs, services, Swagger)
- [ ] **M4** — Cross-cutting backend: global exception handler, ProblemDetails, Serilog, FluentValidation
- [ ] **M5** — Angular product list + detail (components, signals, HttpClient, routing, loading/error states)
- [ ] **M6** — Cross-cutting frontend: error interceptor, loading interceptor, global ErrorHandler
- [ ] **M7** — Categories + filtering/sorting on products
- [ ] **M8** — Cart (storage strategy TBD)
- [ ] **M9** — Auth (Identity + JWT, login/register, auth interceptor, guards)
- [ ] **M10** — Orders end-to-end
- [ ] **M11** — Testing (a few unit tests both sides)
- [ ] **M12** — Polish: lazy loading, env config, README, deploy notes

---

## Instructions for Claude Code (executor)

1. **Always read this file first.** It is the source of truth for stack, layout, and conventions.
2. **Stick strictly to the scope of the current task** as given by the learner. Do not invent extra files, features, or abstractions.
3. **Do not skip ahead** to future milestones, even if it seems efficient. The learner needs to encounter concepts in order.
4. **Explain what you did** after each task in plain, beginner-friendly language. Mention the file(s) changed and why.
5. **Ask before assuming** when a prompt is ambiguous.
6. **Prefer modern patterns only.** No outdated NgModule-heavy Angular. No deprecated .NET patterns.
7. **Do not add packages** (NuGet or npm) without explicit approval. List them first, wait for go-ahead.
8. **Never commit on the learner's behalf** unless explicitly asked. The learner writes their own commit messages — it is part of the learning.

---

## Open Questions / Decisions Deferred

- Cart storage: browser (localStorage) vs server-side. Decide at M8.
- Logging backend (Serilog sinks): console only for dev; file/Seq TBD.
- Deployment target: not yet decided.

---

## Change Log

- 2026-06-01 — Initial notes created. M0 complete.