# Arkance Project

 🚀 Technologies utilisées

* NET 9 (SDK version 13)
* API Minimal
* Entity Framework Core (EF Core) comme ORM
* Base de données PostgreSQL hébergée sur Supabase

---

 ⚙️ Installation et lancement

 1. Cloner le repository

```bash
git clone https://github.com/<ton-repository>/arkance.git
cd arkance
```

### 2. Installer les dépendances NuGet

Dans le dossier du projet, exécuter :

```bash
dotnet restore
```

### 3. Lancer l’application

Deux options :

Via le terminal (Visual Studio Code, PowerShell, etc.)**

```bash
dotnet run
```

  Via Visual Studio
  Exécuter en mode Debug directement depuis l’IDE.

---

## 📖 Documentation API

Une fois l’application lancée, accéder à **Swagger** pour tester les endpoints :
👉 [http://localhost:5219/swagger/index.html](http://localhost:5219/swagger/index.html)

---

## 🗄️ Base de données

* SGBD : PostgreSQL (hébergé sur [Supabase](https://supabase.com/))
* ORM : Entity Framework Core

---

## ✅ Tests et validation

* Les tests peuvent être effectués directement depuis Swagger.
* Vérifier que la connexion à la base de données Supabase est correctement configurée dans le fichier `appsettings.json`.
