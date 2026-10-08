# Laboratorul 1 - Principii SOLID

## Scopul laboratorului

Scopul laboratorului este analiza principiilor SOLID si implementarea a trei dintre ele intr-o aplicatie simpla pentru gestionarea unei comenzi de flori. Principiile alese sunt SRP (Single Responsibility Principle), OCP (Open/Closed Principle) si ISP (Interface Segregation Principle).

Aplicatia permite utilizatorului sa aleaga flori, cantitati, metoda de livrare si metoda de plata. La final este afisat sumarul comenzii si suma finala.

## Ce reprezinta SOLID

SOLID este un set de principii folosite in programarea orientata pe obiecte pentru a obtine cod mai organizat, mai usor de inteles si mai simplu de modificat. Principiile ajuta la reducerea dependintelor dintre clase si la separarea responsabilitatilor.

In cadrul acestui laborator nu au fost implementate toate cele cinci principii SOLID. Au fost selectate SRP, OCP si ISP deoarece se potrivesc natural cu domeniul unei florarii: exista date despre comanda, mai multe variante de livrare si mai multe metode de plata.

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

La fiecare intrebare despre floare sunt afisate variantele acceptate: `Trandafir`, `Bujor`, `Lalea` si `Crizantema`. Se introduce numele exact al florii si cantitatea dorita, iar cuvantul `gata` finalizeaza lista de produse. Apoi se alege livrarea standard sau express si metoda de plata, cash sau card online. La final, programul afiseaza sumarul comenzii, costul produselor, costul livrarii si suma finala.

Programul foloseste un catalog construit in `main.cs`, in care fiecare floare are un nume si un pret. Comenzile sunt adaugate intr-un obiect `FlowerOrder`. Pentru fiecare produs se retin floarea si cantitatea, iar totalul florilor este calculat pe baza pretului si a cantitatii.

Dupa finalizarea produselor, utilizatorul alege tipul de livrare. Aplicatia retine alegerea prin interfata `IDeliveryCost`, fara ca restul programului sa depinda direct de o singura clasa de livrare. La final este aleasa metoda de plata, iar `InvoicePrinter` afiseaza toate informatiile importante intr-un singur sumar.

Un exemplu de interactiune este:

```text
Floare: Trandafir
Cantitate: 2
Floare: Lalea
Cantitate: 1
Finalizare: gata
Livrare: Express
Plata: Card online
```

Pentru acest exemplu, totalul florilor este 120 lei, costul livrarii este 30 lei, iar suma finala este 150 lei.

## Implementarea principiilor SOLID

### 1. SRP - Single Responsibility Principle

SRP inseamna ca o clasa trebuie sa aiba o singura responsabilitate principala si un singur motiv important pentru a fi modificata.

In proiect, SRP este implementat in `SRP.cs`. Clasa `Flower` reprezinta o floare si pretul acesteia, `FlowerOrder` gestioneaza produsele din comanda si calculeaza totalul, iar `InvoicePrinter` are responsabilitatea de a afisa sumarul comenzii.

Responsabilitatile sunt separate: comanda nu se ocupa de afisare, iar afisarea nu gestioneaza datele comenzii.

### 2. OCP - Open/Closed Principle

OCP inseamna ca un modul trebuie sa fie deschis pentru extindere, dar inchis pentru modificari. Astfel, putem adauga comportamente noi fara sa schimbam codul existent.

In `OCP.cs` exista interfata `IDeliveryCost`, care defineste metoda `CalculateCost()`. Aceasta este implementata de `StandardDelivery`, cu un cost de 15 lei, si `ExpressDelivery`, cu un cost de 30 lei.

In `main.cs`, programul lucreaza cu tipul general `IDeliveryCost`. Pentru a adauga un nou tip de livrare, de exemplu `SameDayDelivery`, se poate crea o noua clasa care implementeaza interfata, fara modificarea claselor existente.

### 3. ISP - Interface Segregation Principle

ISP inseamna ca o clasa nu trebuie obligata sa implementeze metode pe care nu le foloseste. Este mai bine sa existe interfete mici si specializate.

In `ISP.cs` sunt definite doua interfete separate: `ICardPayment` pentru plata cu cardul si `ICashPayment` pentru plata cash.

`InStoreOrder` poate implementa ambele metode de plata, iar `OnlineOrder` implementeaza doar `ICardPayment`. Astfel, o comanda online nu este obligata sa implementeze plata cash.

In `main.cs`, utilizatorul alege metoda de plata, iar programul apeleaza implementarea potrivita.

## Structura proiectului

Fisierul `main.cs` este punctul de intrare si contine fluxul interactiv al aplicatiei. `SRP.cs` contine clasele pentru flori, comanda si afisarea sumarului, `OCP.cs` contine interfata si clasele pentru livrare, iar `ISP.cs` contine interfetele si clasele pentru plata. Configuratia proiectului .NET se afla in `TMPS.csproj`.

Fluxul aplicatiei porneste din `main.cs`, unde sunt citite datele de la utilizator. Datele sunt transmise catre clasele din celelalte fisiere: `FlowerOrder` calculeaza valoarea produselor, o clasa de livrare calculeaza transportul, iar clasele de plata confirma metoda aleasa. `InvoicePrinter` reuneste rezultatele si afiseaza suma finala.

## Verificare si testare

Testarea actuala se face prin rularea interactiva a aplicatiei. Pentru un test complet se pot introduce mai multe flori, inclusiv aceeasi floare de mai multe ori, apoi se poate verifica totalul afisat. Trebuie verificata si alegerea ambelor tipuri de livrare, precum si a ambelor metode de plata.

Comanda `dotnet build` verifica daca fisierele C# se compileaza, iar `dotnet run` porneste aplicatia. Proiectul nu contine inca teste automate; rezultatele sunt verificate prin raspunsurile afisate in consola.

## Posibilitati de extindere

Aplicatia poate fi dezvoltata prin adaugarea unor flori noi in catalog, fara modificarea claselor care gestioneaza comanda. Pentru o metoda noua de livrare se poate crea o clasa noua care implementeaza `IDeliveryCost`. In acelasi mod, o metoda noua de plata poate implementa interfata potrivita, fara ca aceasta sa fie adaugata in toate clasele existente.

Ca imbunatatiri viitoare, aplicatia ar putea valida cantitatile zero sau negative, ar putea permite eliminarea unui produs din comanda si ar putea salva comenzile intr-un fisier sau intr-o baza de date.

## Concluzie

Laboratorul arata cum principiile SOLID pot face codul mai clar, mai usor de intretinut si mai simplu de extins. Separarea responsabilitatilor prin SRP, folosirea interfetelor pentru extinderea livrarii prin OCP si definirea unor interfete mici pentru plata prin ISP reduc dependentele dintre componente. Aplicatia poate fi extinsa cu flori, metode de livrare sau metode de plata noi fara modificari majore in clasele existente.
