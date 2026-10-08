# Laboratorul 1 - Principii SOLID

## Scopul laboratorului

Scopul laboratorului este analiza principiilor SOLID si implementarea a trei dintre ele intr-o aplicatie simpla pentru gestionarea unei comenzi de flori.

Au fost alese urmatoarele principii:

- SRP - Single Responsibility Principle
- OCP - Open/Closed Principle
- ISP - Interface Segregation Principle

Aplicatia permite utilizatorului sa aleaga flori, cantitati, metoda de livrare si metoda de plata. La final este afisat sumarul comenzii si suma finala.

## Cum se ruleaza

Este necesar sa fie instalat .NET SDK. Proiectul foloseste .NET 10.

Din PowerShell se ruleaza:

```powershell
cd D:\TMPS\lab1
dotnet run
```

Pentru verificarea compilarii fara pornirea programului:

```powershell
dotnet build
```

### Utilizarea programului

1. La fiecare intrebare despre floare sunt afisate variantele acceptate: `Trandafir`, `Bujor`, `Lalea` si `Crizantema`.
2. Se introduce numele exact al florii si cantitatea dorita.
3. Se scrie `gata` pentru finalizarea listei de produse.
4. Se alege livrarea standard sau express.
5. Se alege plata cash sau cu cardul online.
6. Programul afiseaza sumarul comenzii, costul produselor, costul livrarii si suma finala.

## Implementarea principiilor SOLID

### 1. SRP - Single Responsibility Principle

SRP inseamna ca o clasa trebuie sa aiba o singura responsabilitate principala si un singur motiv important pentru a fi modificata.

In proiect, SRP este implementat in `SRP.cs`:

- `Flower` reprezinta o floare si pretul acesteia.
- `FlowerOrder` gestioneaza produsele din comanda si calculeaza totalul florilor.
- `InvoicePrinter` are responsabilitatea de a afisa sumarul comenzii.

Responsabilitatile sunt separate: comanda nu se ocupa de afisare, iar afisarea nu gestioneaza datele comenzii.

### 2. OCP - Open/Closed Principle

OCP inseamna ca un modul trebuie sa fie deschis pentru extindere, dar inchis pentru modificari. Astfel, putem adauga comportamente noi fara sa schimbam codul existent.

In `OCP.cs` exista interfata `IDeliveryCost`, care defineste metoda `CalculateCost()`.

Implementarea contine doua tipuri de livrare:

- `StandardDelivery` - cost de 15 lei.
- `ExpressDelivery` - cost de 30 lei.

In `main.cs`, programul lucreaza cu tipul general `IDeliveryCost`. Pentru a adauga un nou tip de livrare, de exemplu `SameDayDelivery`, se poate crea o noua clasa care implementeaza interfata, fara modificarea claselor existente.

### 3. ISP - Interface Segregation Principle

ISP inseamna ca o clasa nu trebuie obligata sa implementeze metode pe care nu le foloseste. Este mai bine sa existe interfete mici si specializate.

In `ISP.cs` sunt definite doua interfete separate:

- `ICardPayment` pentru plata cu cardul.
- `ICashPayment` pentru plata cash.

`InStoreOrder` poate implementa ambele metode de plata, iar `OnlineOrder` implementeaza doar `ICardPayment`. Astfel, o comanda online nu este obligata sa implementeze plata cash.

In `main.cs`, utilizatorul alege metoda de plata, iar programul apeleaza implementarea potrivita.

## Structura proiectului

- `main.cs` - punctul de intrare si fluxul interactiv al aplicatiei.
- `SRP.cs` - clasele pentru flori, comanda si afisarea sumarului.
- `OCP.cs` - interfata si clasele pentru metodele de livrare.
- `ISP.cs` - interfetele si clasele pentru metodele de plata.
- `TMPS.csproj` - configuratia proiectului .NET.

## Concluzie

Laboratorul arata cum principiile SOLID pot face codul mai clar, mai usor de intretinut si mai simplu de extins. Separarea responsabilitatilor prin SRP, folosirea interfetelor pentru extinderea livrarii prin OCP si definirea unor interfete mici pentru plata prin ISP reduc dependentele dintre componente. Aplicatia poate fi extinsa cu flori, metode de livrare sau metode de plata noi fara modificari majore in clasele existente.
