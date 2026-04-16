# 🚗 SmartParking SaaS

> Real-time parking reservation platform built with .NET 8, Blazor, MAUI, and PostgreSQL.

---

## ✨ Highlights

- Real-time availability management (no overbooking)
- Full booking lifecycle (Pending → Active → Completed → Expired → Cancelled)
- Payment handling (Pending / Paid)
- Operator dashboard (KPIs + quick actions)
- Mobile-first driver experience

---

## 🇬🇧 Overview

SmartParking is a SaaS platform designed to manage parking reservations in real time.

It focuses on solving real-world problems such as:
- preventing overbooking
- handling pending payments
- managing parking capacity
- providing an operational dashboard for monitoring and actions

---

## 🧩 Architecture Overview

This project follows a **modular architecture inspired by Clean Architecture principles**:

- Domain-driven design for business rules
- Application layer for use cases and handlers
- Infrastructure layer for persistence (EF Core)
- Separate UI layers:
  - Blazor Server (Operator Dashboard)
  - MAUI Blazor Hybrid (Driver App)

The system is designed to be:
- scalable
- maintainable
- ready for real-time extensions (SignalR)

---

## ⚠️ Domain Challenges

This project is not a simple CRUD application.

It addresses real-world constraints:

- Preventing overbooking under concurrent requests
- Managing pending vs confirmed payments
- Handling booking lifecycle transitions safely
- Keeping parking availability consistent at all times

---

## 🛠️ Technical Stack

- .NET 8 / ASP.NET Core Web API
- Blazor Server (Admin UI)
- .NET MAUI Blazor Hybrid (Mobile)
- Entity Framework Core
- PostgreSQL

---

## ⚙️ Features

### 🚗 Driver (Mobile)
- Search nearby parking
- View availability in real time
- Reserve a parking spot
- Payment flow

### 🧑‍💼 Operator (Admin)
- Dashboard with KPIs
- Booking lifecycle management
- Parking availability management
- Payment monitoring
- Quick actions (cancel, complete, expire)

---

## 📸 Screenshots

### Operator Dashboard
![Dashboard](docs/screenshots/dashboard.png)

### Bookings Management
![Bookings](docs/screenshots/bookings.png)

### Parking Details
![Parking](docs/screenshots/parking-details.png)

### Payments
![Payments](docs/screenshots/payments.png)

### Mobile App (Driver)
![Mobile](docs/screenshots/mobile-search.png)

### Mobile App (Driver)
![Mobile](docs/screenshots/mobile-availability.png)

### Mobile App (Driver)
![Mobile](docs/screenshots/mobile-booking-details.png)

---

## 💡 Why this project matters

This project demonstrates the design of a **real SaaS system**:

- not just UI or CRUD
- but full product thinking
- domain modeling
- operational workflows

It reflects how production systems are built in practice.

---

## 🚀 Current Status

Work in progress.

Main features implemented:
- Search + booking flow
- Operator dashboard (KPIs + quick actions)
- Booking admin actions (cancel, complete, expire)
- Auto-refresh + filters + URL sync

---

## 🔮 Next Steps

- SignalR real-time updates
- Payment reconciliation improvements
- Advanced filtering & analytics
- Anti-fraud patterns (future extension)

---

## 👤 Author

Rachid Bariz  
Senior Full-Stack .NET Architect  
Product-driven engineer building SaaS platforms

---

---

# 🇫🇷 Présentation

SmartParking est une plateforme SaaS de réservation de parking en temps réel.

Elle permet de gérer des problématiques métier concrètes :
- éviter le surbooking
- gérer les paiements en attente
- contrôler la capacité des parkings
- fournir un dashboard opérateur exploitable

---

## 🧠 Concepts clés

- Gestion de disponibilité en temps réel
- Cycle de vie des réservations
- Gestion des paiements (Pending / Paid)
- Dashboard opérateur (cockpit)
- Expérience mobile côté utilisateur

---

## 🏗️ Architecture

- Backend : ASP.NET Core Web API (.NET 8)
- Application : Clean Architecture
- Domaine : règles métier
- Infrastructure : EF Core
- Admin : Blazor Server
- Mobile : .NET MAUI Blazor Hybrid
- Base de données : PostgreSQL

---

## ⚙️ Fonctionnalités

### 🚗 Driver (Mobile)
- Recherche de parking
- Visualisation de la disponibilité
- Réservation
- Paiement

### 🧑‍💼 Opérateur (Admin)
- Dashboard KPI
- Gestion des réservations
- Gestion des parkings
- Suivi des paiements
- Actions rapides

---

## 🔥 Points forts

Ce projet traite de vrais sujets métier :

- gestion des conflits de réservation
- synchronisation disponibilité / paiement
- gestion de capacité en temps réel

---

## 🚀 Statut

Projet en cours de développement.

Fonctionnalités principales déjà en place :
- recherche + réservation
- dashboard opérateur
- actions admin sur bookings
- auto-refresh + filtres avancés

---

## 🔮 Prochaines étapes

- SignalR temps réel
- amélioration des paiements
- analytics avancés
- extension anti-fraude

---

## 👤 Auteur

Rachid Bariz  
Architecte logiciel .NET Senior  
Approche orientée produit et SaaS