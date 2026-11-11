# Rune

Rune is a digital gaming platform inspired by Steam and Epic Games Store.

The project aims to bring game discovery, purchasing, community features, news, events, workshop and gaming tools together in a single platform.

## Features

- Game Store
- Explore
- Community
- News
- Events
- Workshop
- Tools
- Desktop Application

## Technologies

- C#
- .NET
- ASP.NET Core
- Entity Framework Core
- HTML
- CSS
- JavaScript
- Bootstrap
- PostgreSQL
- Git & GitHub

## Current State

The store catalog, game details, search, release notes, and browser-local wishlist are available.
The wishlist is saved in the current browser and is not synced to an account. Account login,
payments, gifting, and several community links are not active yet; unfinished links are marked
as coming soon in the interface. The displayed app version is read from `Version.txt`.

## Database Setup

The application looks for its PostgreSQL connection string in `ConnectionStrings:DATABASE_URL`,
then falls back to `DATABASE_URL`.
For local development, keep credentials outside the repository with .NET User Secrets:

```powershell
dotnet user-secrets set "ConnectionStrings:DATABASE_URL" "Host=localhost;Port=5432;Database=rune;Username=rune;Password=your-local-password"
```

For deployment, set `ConnectionStrings__DATABASE_URL` to an Npgsql connection string, or set
`DATABASE_URL` to an Npgsql connection string or a `postgres://` / `postgresql://` URL supplied
by the hosting provider. The application applies pending EF Core migrations at startup before
seeding its initial data. The PostgreSQL database must already exist, and the application's
database user needs permission to apply schema changes.

## Goal

To build a comprehensive gaming ecosystem that brings players, developers and gaming communities together.
