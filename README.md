# Szybkie testy API zamiast niestabilnych testów UI

## 📋 Informacje ogólne

**Projekt**: WDI 2026 Presentation API Tests  
**Konferencja**: [Warszawskie Dni Informatyki](https://warszawskiedniinformatyki.pl/)  
**Autor**: Damian Brzęczek  
**Tytuł prezentacji**: *"Szybkie testy API zamiast niestabilnych testów UI"*

---

## 🎯 Cel projektu

Projekt demonstruje praktyczne podejście do testowania API poprzez:

- **BDD (Behavior-Driven Development)** z użyciem **SpecFlow/Reqnroll**
- **Szybkie, stabilne testy** jako alternatywę dla niestabilnych testów UI
- **Best practices** w automatyzacji testów API

Projekt pokazuje, dlaczego testowanie na poziomie API jest bardziej efektywne, szybsze i bardziej niezawodne niż testowanie przez UI.

---

## 🏗️ Struktura projektu

```
WDI2026.Presentation.API/
├── WDI.Presentation.API.Tests/                    # Projekt testów
│   ├── Features/
│   │   └── Procducts.feature                      # Scenariusze BDD (SpecFlow/Reqnroll)
│   ├── Steps/
│   │   └── ProductsSteps.cs                       # Implementacja kroków testów
│   └── ImplicitUsings.cs                          # Globalne using statements
│
├── WDI2026.Presentation.API.Client/               # Klient API
│   ├── ApiClient.cs                               # Główna klasa klienta
│   ├── ApiAuthenticator.cs                        # Obsługa autentykacji JWT
│   └── Models/
│       ├── Product.cs
│       ├── LoginResponse.cs
│       └── Created.cs
│
└── WDI2026.Presentation.API.Common/               # Wspólne komponenty
    ├── Clients/
    │   ├── CustomRestClient.cs                    # Rozszerzenie RestSharp
    │   └── Base/BaseRestClient.cs
    ├── Logging/
    │   ├── StepsLogger.cs                         # Logger dla SpecFlow
    │   └── ApiClientLogging.cs
    ├── Configuration/
    │   ├── TestConfiguration.cs
    │   ├── Settings/
    │   │   ├── AppSettings.cs
    │   │   └── Clients/ShopApiClientSettings.cs
    │   └── Hooks/
    │       ├── DependencyInjectionConfiguration.cs
    │       └── AfterScenarioHooks.cs
    └── appSettings.*.json                         # Konfiguracja dla różnych środowisk
```

---

## ✨ Technologie i biblioteki

- **Framework**: .NET 10 (C# 14.0)
- **Testing Frameworks**:
  - [NUnit](https://nunit.org/) - testowanie jednostkowe
  - [Reqnroll](https://reqnroll.net/) - BDD (Behavior-Driven Development)
  - [Shouldly](https://shouldly.io/) - fluent assertions
- **API Client**: [RestSharp](https://restsharp.dev/) - HTTP client
- **Autentykacja**: JWT (JSON Web Tokens)
- **Logging**: Custom `IStepsLogger` zintegrowany z SpecFlow
- **Dependency Injection**: Wbudowany IoC
- **Parallel Execution**: Wsparcie dla równoległego uruchamiania testów

---

## 🧪 Scenariusze testowe

Projekt zawiera testy dla operacji CRUD na zasobie `Product`:

### Feature: Product Management (`Procducts.feature`)

1. **Scenario: Create product and verify it can be retrieved**
   - Tworzy nowy produkt
   - Pobiera utworzony produkt
   - Weryfikuje, że dane się zgadzają

2. **Scenario: Update product description and verify update**
   - Tworzy produkt
   - Aktualizuje opis
   - Weryfikuje, że zmiana została zaaplikowana

3. **Scenario: Creating invalid product returns validation error**
   - Próbuje stworzyć produkt z nieprawidłowymi danymi (np. ujemna cena)
   - Weryfikuje, że API zwraca błąd walidacji

---

## 🚀 Jak uruchomić projekt

### Wymagania

- .NET 10 SDK
- Visual Studio 2022/2026 lub VS Code
- Uruchomiona instancja API na `https://localhost:7167/`

### Instalacja zależności

```powershell
dotnet restore
```

### Uruchomienie wszystkich testów

```powershell
dotnet test
```

### Uruchomienie testów SpecFlow/Reqnroll

```powershell
dotnet test --filter "FullyQualifiedName~Products"
```

### Uruchomienie testów z wynikami w raporcie HTML

```powershell
dotnet test --logger "html;logfilename=TestResults.html"
```

### 📊 Raport Reqnroll

Projekt jest skonfigurowany do automatycznego generowania raportu Reqnroll w formacie HTML:

- **Ścieżka raportu**: `specFlowReport/reqnroll_report.html`
- **Plik konfiguracji**: `reqnroll.json`
- **Automatyczne generowanie**: Raport jest tworzony automatycznie po każdym uruchomieniu testów

Po uruchomieniu testów otwórz wygenerowany plik HTML w przeglądarce:

```powershell
# Linux/macOS
open specFlowReport/reqnroll_report.html

# Windows
start specFlowReport/reqnroll_report.html
```

**Zawartość raportu:**
- Szczegóły każdego scenariusza (Given, When, Then)
- Status wykonania (Passed ✅ / Failed ❌ / Skipped ⊘)
- Czas wykonania testu
- Logi i ślady błędów (w przypadku niepowodzeń)
- Statystyki ogólne

---

## 🔑 Kluczowe koncepty

### 1. **Autentykacja JWT w ApiClient**

`ApiAuthenticator` automatycznie:
- Pobiera token JWT z endpointa `/api/v1/account/login`
- Dodaje token do każdego żądania w nagłówku `Authorization: bearer {token}`
- Obsługuje odnawianie tokenów

### 2. **BDD z SpecFlow/Reqnroll**

Scenariusze w pliku `.feature` są mapowane do metod w `ProductsSteps`:

```gherkin
Given a new product with name "ProductTestOlek12", description "TestByOlek12", price 44 and stock 3
When I create the product
Then the product should be created successfully and retrieving it should match the original
```

### 3. **Logging i Tracing**

`StepsLogger` integruje się z Reqnroll i loguje:
- Każde żądanie HTTP
- Status odpowiedzi
- Czasowe znaczniki
- Dodatkowy kontekst scenariusza

### 4. **Równoległa egzekucja testów**

Testy są oznaczone atrybutem:
```csharp
[Parallelizable(ParallelScope.All)]
```

Co umożliwia szybkie uruchomienie niezależnych testów w sposób równoległy.

---

## 📊 Korzyści testowania API zamiast UI

| Aspekt | Testy UI | Testy API |
|--------|----------|-----------|
| **Szybkość** | Wolne (10-60s per test) | Szybkie (100-500ms per test) |
| **Stabilność** | Kruche (zmiany DOM, elementy, itp.) | Stabilne (kontrakty API) |
| **Niezawodność** | Wrażliwe na timing, sieć | Dokładne, powtarzalne |
| **Pokrycie** | Testuje UI + logika | Testuje logikę biznesową |
| **Łatwość utrzymania** | Wymaga ciągłych zmian | Stabilne zmiany |
| **Równoległa eksekucja** | Trudne | Łatwe |
| **Debugowanie** | Skomplikowane | Proste |

---

## 📚 Materiały dodatkowe

- [Reqnroll (SpecFlow Open Source) Documentation](https://docs.reqnroll.net/latest/)
- [NUnit Documentation](https://docs.nunit.org/)
- [RestSharp Documentation](https://restsharp.dev/docs/intro)
- [Shouldly Assertions Documentation](https://docs.shouldly.org/)

---

## 👤 Autor

**Damian Brzęczek**

---

## 📝 Licencja

Projekt stworzony na potrzeby prezentacji dla Warszawskich Dni Informatyki 2026.

---

## 🤝 Kontakt

Pytania? Sugestie? Zapraszam do dyskusji! 🎤

---

**Last Updated**: 2026  
**Status**: ✅ Demo-ready
