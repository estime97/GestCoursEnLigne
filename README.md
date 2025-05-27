# GestCoursEnLigne
# 🎓 Plateforme de cours en ligne (.NET)

Une application web moderne permettant à des formateurs de créer des cours, à des étudiants de s'inscrire et de progresser via des leçons et des quiz interactifs.

---

## 🏗️ Technologies utilisées

- [.NET 8](https://dotnet.microsoft.com/)
- ASP.NET Core (Razor Pages ou Blazor Server)
- Entity Framework Core
- ASP.NET Identity
- SQL Server
- MudBlazor (UI)
- AutoMapper, FluentValidation
- (Optionnel) Clean Architecture

---

## 🚀 Fonctionnalités principales

### 🔐 Authentification & rôles
- Inscription et connexion sécurisées
- Gestion des rôles : Admin, Formateur, Étudiant
- Interface d'administration des utilisateurs

### 📚 Gestion des cours
- Création de cours par les formateurs
- Modules, leçons (PDF / Vidéos)
- Quiz pour tester les connaissances

### 👨‍🎓 Côté étudiant
- Navigation dans les cours
- Suivi de progression
- Résultats aux quiz

### 📊 Dashboards
- Étudiant : progression, scores
- Formateur : cours créés, statistiques
- Admin : utilisateurs, système

### 📁 Fichiers & médias
- Upload sécurisé de fichiers
- Affichage vidéos / téléchargement de ressources

---

## 📋 Roadmap du projet

👉 Voir [Roadmap complète](./Plateforme-cours-roadmap.md)

---

## 🛠️ Installation locale

```bash
git clone https://github.com/votre-utilisateur/plateforme-cours-dotnet.git
cd plateforme-cours-dotnet
dotnet ef database update
dotnet run

