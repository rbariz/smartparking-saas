# 1. SmartParking SaaS

Real-time parking reservation platform built with .NET 8, Blazor, MAUI, and PostgreSQL.

---

## 2. Highlights

- Real-time availability management (no overbooking)
- Full booking lifecycle (Pending → Active → Completed → Expired → Cancelled)
- Payment handling (Pending / Paid)
- Operator dashboard (KPIs + quick actions)
- Mobile-first driver experience

---

## 3. Overview

SmartParking is a SaaS platform designed to manage parking reservations in real time.

It focuses on solving real-world problems such as:
- preventing overbooking
- handling pending payments
- managing parking capacity
- providing an operational dashboard for monitoring and actions

---

## 4. Architecture Overview

This project follows a modular architecture inspired by Clean Architecture principles:

- Domain-driven design for business rules
- Application layer for use cases and handlers
- Infrastructure layer for persistence (EF Core)

Separate UI layers:
- Blazor Server (Operator Dashboard)
- MAUI Blazor Hybrid (Driver App)

The system is designed to be:
- scalable
- maintainable
- ready for real-time extensions (SignalR)

---

## 5. Domain Challenges

This project is not a simple CRUD application.

It addresses real-world constraints:

- Preventing overbooking under concurrent requests
- Managing pending vs confirmed payments
- Handling booking lifecycle transitions safely
- Keeping parking availability consistent at all times

---

## 6. Technical Stack

- .NET 8 / ASP.NET Core Web API
- Blazor Server (Admin UI)
- .NET MAUI Blazor Hybrid (Mobile)
- Entity Framework Core
- PostgreSQL

---

## 7. Features

### 7.1 Driver (Mobile)

- Search nearby parking
- View availability in real time
- Reserve a parking spot
- Payment flow

### 7.2 Operator (Admin)

- Dashboard with KPIs
- Booking lifecycle management
- Parking availability management
- Payment monitoring
- Quick actions (cancel, complete, expire)

---

## 8. Screenshots

### 8.1 Operator Dashboard
![Dashboard](docs/screenshots/dashboard.png)

### 8.2 Bookings Management
![Bookings](docs/screenshots/bookings.png)

### 8.3 Parking Details
![Parking](docs/screenshots/parking-details.png)

### 8.4 Payments
![Payments](docs/screenshots/payments.png)

### 8.5 Mobile - Search
![Mobile](docs/screenshots/mobile-search.png)

### 8.6 Mobile - Availability
![Mobile](docs/screenshots/mobile-availability.png)

### 8.7 Mobile - Booking Details
![Mobile](docs/screenshots/mobile-booking-details.png)

---

## 9. Why this project matters

This project demonstrates the design of a real SaaS system:

- not just UI or CRUD
- but full product thinking
- domain modeling
- operational workflows

It reflects how production systems are built in practice.

---

## 10. Current Status

Work in progress.

Main features implemented:
- Search and booking flow
- Operator dashboard (KPIs + quick actions)
- Booking admin actions (cancel, complete, expire)
- Auto-refresh with filters and URL synchronization

---

## 11. Next Steps

- SignalR real-time updates
- Payment reconciliation improvements
- Advanced filtering and analytics
- Anti-fraud patterns (future extension)

---

## 12. Author

Rachid Bariz  
Senior Full-Stack .NET Architect  
Product-driven engineer building SaaS platforms

---

---

# 13. Présentation (FR)

SmartParking est une plateforme SaaS de réservation de parking en temps réel.

Elle permet de gérer des problématiques métier concrètes :
- éviter le surbooking
- gérer les paiements en attente
- contrôler la capacité des parkings
- fournir un dashboard opérateur exploitable

---

## 14. Concepts clés

- Gestion de disponibilité en temps réel
- Cycle de vie des réservations
- Gestion des paiements (Pending / Paid)
- Dashboard opérateur (cockpit)
- Expérience mobile côté utilisateur

---

## 15. Architecture

- Backend : ASP.NET Core Web API (.NET 8)
- Application : Clean Architecture
- Domaine : règles métier
- Infrastructure : EF Core
- Admin : Blazor Server
- Mobile : .NET MAUI Blazor Hybrid
- Base de données : PostgreSQL

---

## 16. Fonctionnalités

### 16.1 Driver (Mobile)

- Recherche de parking
- Visualisation de la disponibilité
- Réservation
- Paiement

### 16.2 Opérateur (Admin)

- Dashboard KPI
- Gestion des réservations
- Gestion des parkings
- Suivi des paiements
- Actions rapides

---

## 17. Points forts

Ce projet traite de vrais sujets métier :

- gestion des conflits de réservation
- synchronisation disponibilité / paiement
- gestion de capacité en temps réel

---

## 18. Statut

Projet en cours de développement.

Fonctionnalités principales déjà en place :
- recherche + réservation
- dashboard opérateur
- actions admin sur bookings
- auto-refresh et filtres avancés

---

## 19. Prochaines étapes

- SignalR temps réel
- amélioration des paiements
- analytics avancés
- extension anti-fraude

---

## Internationalization (i18n)

The mobile application supports full bilingual localization (English / French).

### Key features

- UI localization using `IStringLocalizer`
- Language switching from Profile page (EN / FR)
- Persistent language preference using MAUI `Preferences`
- Culture applied at startup for consistent rendering
- Fully localized pages and components:
  - Search
  - Results
  - Parking details
  - Booking & Payment flows
  - OTP Authentication
  - Profile

### Domain status localization

Domain statuses returned by the API (e.g. `open`, `pending_payment`) are translated on the client using a centralized `StatusMapper`.

This ensures:
- consistent translations
- clean separation between API data and UI labels
- correct styling via raw status values

### Culture-safe API calls

All API query strings use `InvariantCulture` to avoid issues with:
- decimal formatting (`,` vs `.`)
- date serialization

Example fix:

csharp
request.Latitude.ToString(CultureInfo.InvariantCulture)

This resolves an issue where search results failed under French culture.

Supported languages
🇬🇧 English (default)
🇫🇷 French

## 20. Auteur

Rachid Bariz  
Architecte logiciel .NET Senior  
Approche orientée produit et SaaS
