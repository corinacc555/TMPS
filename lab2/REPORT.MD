# Raport de Laborator 2: Creational Design Patterns & SOLID

**Tema:** Sistem de gestionare a comenzilor pentru o Florărie
**Disciplina:** Tehnici și Mecanisme de Proiectare Software (TMPS)
**Scopul lucrării:** Analiza, înțelegerea și implementarea șabloanelor de proiectare creaționale (Creational Design Patterns) într-o aplicație scrisă în C#, extinzând structura bazată pe principiile SOLID.

## 1. Descrierea Proiectului

Proiectul simulează sistemul informatic al unei florării. Aplicația permite crearea și gestiunea comenzilor de flori (fie prin pachete predefinite speciale, fie prin comenzi personalizate de la tastatură), oferind selecția dinamică a modalităților de livrare și de plată.

Proiectul este structurat după o arhitectură curată orientată pe domenii (Domain-Driven Design), izolând entitățile, fabricile, builder-ele și serviciile în module dedicate.

## 2. Privire de Ansamblu: Șabloanele Creaționale (Creational Patterns)

Șabloanele creaționale au rolul de a abstractiza și controla procesul de instanțiere a obiectelor, făcând sistemul independent de modul în care obiectele sale sunt create, compuse și reprezentate.

| Șablon (Pattern) | Clasificare | Descriere pe scurt |
|---|---|---|
| Singleton | Obiect | Garantează că o clasă are o singură instanță globală în tot programul. |
| Factory Method | Clasă | Definește o interfață de creare, lăsând subclasele să decidă ce obiect concret instanțiază. |
| Abstract Factory | Obiect | Oferă o interfață pentru crearea familiilor de obiecte înrudite/dependente. |
| Builder | Obiect | Separă construcția unui obiect complex de reprezentarea sa, asamblându-l pas cu pas. |
| Prototype | Obiect | Permite crearea de obiecte noi prin clonarea (copierea) unei instanțe existente. |
| Object Pool | Obiect | Reciclează și reutilizează obiecte scumpe de creat (ex: conexiuni BD, thread-uri) dintr-un container partajat, în loc să le distrugă și recreeze repetat. |

## 3. Structura Proiectului (Domain-Driven Architecture)

Codul a fost organizat pe foldere tematice pentru a asigura o lizibilitate maximă și o mentenanță ușoară:

```text
FlorarieSOLID/
├── main.cs (sau Program.cs)    # Punctul de intrare (Meniul interactiv)
│
├── Core/                        # Entitățile și setările de bază
│   ├── StoreConfiguration.cs    # (Singleton) Setările unice ale florăriei
│   ├── Flower.cs                # Modelul pentru o floare
│   ├── OrderItem.cs             # Rând de pe factură (Floare + Cantitate)
│   └── FlowerOrder.cs           # (SRP) Logica datelor comenzii
│
├── Delivery/                    # Modulul de livrare
│   ├── IDeliveryCost.cs         # (OCP) Interfața pentru livrare
│   ├── DeliveryMethods.cs       # Implementări concrete (Standard / Express)
│   └── DeliveryFactories.cs     # (Factory Method) Fabricile de livrare
│
├── Payments/                    # Modulul de procesare a plăților
│   ├── PaymentInterfaces.cs     # (ISP) Interfețe segregate pentru Cash și Card
│   └── PaymentTypes.cs          # Tipuri de comenzi (InStore / Online)
│
├── Builders/                    # Modulul de asamblare a comenzilor complexe
│   ├── IOrderBuilder.cs         # Interfața builder-ului
│   ├── CustomOrderBuilder.cs    # (Builder) Implementarea construcției pas cu pas
│   └── OrderDirector.cs         # (Director) Ghidul pentru pachete predefinite
│
└── Utils/                       # Utilitare
    └── InvoicePrinter.cs        # (SRP) Afișarea și formatarea facturii
```

## 4. Design Patterns Implementate în Proiect

Pentru această lucrare am ales 3 șabloane de bază, integrându-le natural în fluxul aplicației:

### A. Singleton Pattern

**Unde este aplicat:** `Core/StoreConfiguration.cs`

**Mecanism:** Constructorul este privatizat (`private StoreConfiguration()`), împiedicând utilizarea operatorului `new` din exterior. Accesul se realizează exclusiv prin proprietatea statică `Instance`.

**Utilitate:** Numele și configurația florăriei sunt unice. Se evită alocarea inutilă de memorie pentru mai multe instanțe identice.

### B. Factory Method Pattern

**Unde este aplicat:** `Delivery/DeliveryFactories.cs`

**Mecanism:** Clasa abstractă `DeliveryCreator` definește metoda abstractă `FactoryMethod()`. Subclasele concrete (`StandardDeliveryCreator`, `ExpressDeliveryCreator`) suprapun această metodă pentru a instanția tipul corespunzător de livrare.

**Utilitate:** Aplicația nu este cuplată direct de clasele de livrare concretă. Dacă se dorește adăugarea unei metode de livrare noapte sau prin dronă, se va crea doar o nouă fabrică.

### C. Builder Pattern (+ Director)

**Unde este aplicat:** `Builders/CustomOrderBuilder.cs` și `Builders/OrderDirector.cs`

**Mecanism:** `CustomOrderBuilder` oferă metode pas-cu-pas (`BuildRoses`, `BuildTulips`, `BuildDelivery`) pentru a adăuga componente la un `FlowerOrder`. Clasa `OrderDirector` conține o rețetă predefinită (`ConstructValentinesBouquet`) apelând automat pașii necesari pe builder.

**Utilitate:** Permite asamblarea simplă a pachetelor de flori complexe și flexibilitate totală atunci când clientul dorește o comandă personalizată.

## 5. Principiile SOLID Integrat (Moștenite din Lab 1)

**SRP (Single Responsibility Principle):** `FlowerOrder` calculează doar totalul și gestiunea articolelor, iar `InvoicePrinter` se ocupă exclusiv de formatarea și afișarea textului pe consolă.

**OCP (Open/Closed Principle):** Interfața `IDeliveryCost` permite extinderea opțiunilor de livrare fără a modifica codul existent de calcul al comenzii.

**ISP (Interface Segregation Principle):** Interfețele `ICardPayment` și `ICashPayment` sunt segregate. Clasa `OnlineOrder` implementează doar plata cu cardul, fără a conține metode inutile de plata cash.

## 6. Instrucțiuni de Rulare

Deschideți terminalul în folderul proiectului.

Executați comanda:

```bash
dotnet run
```

Utilizați meniul interactiv pentru a alege între un pachet predefinit (Valentine's Day) sau o comandă personalizată pas cu pas.

## 7. Concluzie

În urma efectuării acestei lucrări de laborator, am extins arhitectura sistemului de gestionare a florăriei, integrând cu succes cele trei șabloane de proiectare creaționale (Singleton, Factory Method și Builder).

Prin implementarea Singleton, am securizat și eficientizat gestionarea memoriei pentru configurația globală a magazinului. Factory Method a permis decuplarea logicii de instanțiere pentru metodele de livrare, respectând riguros principiul Open/Closed, iar șablonul Builder a rezolvat problema creării obiectelor complexe, separând clar reprezentarea de asamblarea pas cu pas.

Organizarea structurală a proiectului pe domenii logice asigură un nivel înalt de mentenanță și claritate. Rezultatul final este un cod curat, modular, ușor de testat și pregătit pentru viitoare extinderi.
