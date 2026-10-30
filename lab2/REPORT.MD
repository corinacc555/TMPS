Raport de Laborator 2: Creational Design Patterns & SOLID

Tema: Sistem de gestionare a comenzilor pentru o Florărie
Scopul lucrării: Analiza, înțelegerea și implementarea a cel puțin 3 șabloane de proiectare creaționale (Creational Design Patterns), extinzând o aplicație care respectă principiile SOLID.

1. Descrierea Proiectului

Proiectul simulează sistemul de comenzi al unei florării. Clientul poate comanda buchete de flori (fie pachete predefinite, fie comenzi personalizate manual), poate alege o metodă de livrare și o metodă de plată.

Aplicația este construită pe o arhitectură curată (Domain-Driven), separând logica de business în module independente pentru a fi ușor de întreținut și extins.

2. Structura Proiectului (Domain-Driven)

Codul a fost organizat pe foldere în funcție de rolul componentelor, respectând Single Responsibility Principle la nivel de arhitectură:

Program.cs - Punctul de intrare (Meniul interactiv)

Core/

StoreConfiguration.cs - (Singleton) Setările magazinului

Flower.cs - Entitatea de bază a unei flori

OrderItem.cs - Element de pe factură (Floare + Cantitate)

FlowerOrder.cs - (SRP) Logica principală a comenzii

Delivery/

IDeliveryCost.cs - (OCP) Interfața pentru livrare

DeliveryMethods.cs - Implementările livrării (Standard/Express)

DeliveryFactories.cs - (Factory Method) Fabricile de livrare

Payments/

PaymentInterfaces.cs - (ISP) Interfețe separate pentru Cash și Card

PaymentTypes.cs - Implementările comenzilor (Online/Fizic)

Builders/

IOrderBuilder.cs - Interfața constructorului de comenzi

CustomOrderBuilder.cs - (Builder) Implementarea pas cu pas

OrderDirector.cs - (Director) Asistentul pentru comenzi complexe predefinite

Utils/

InvoicePrinter.cs - (SRP) Clasă dedicată exclusiv afișării textului

3. Creational Design Patterns Implementate

Pentru acest laborator am ales 3 șabloane creaționale de bază care se ocupă cu instanțierea controlată a claselor:

A. Singleton Pattern

Concept: Asigură că o clasă are o singură instanță în tot programul și oferă un punct de acces global la aceasta.

Implementare: Core/StoreConfiguration.cs

Explicație: Florăria are un singur nume ("Florăria SOLID & Patterns"). Am privatizat constructorul (private StoreConfiguration()) și am oferit acces doar prin proprietatea statică Instance. Astfel, datele magazinului sunt unice și nu se ocupă memorie inutil cu instanțe multiple.

B. Factory Method Pattern

Concept: Definește o interfață pentru crearea unui obiect, dar lasă subclasele să decidă ce clasă să instanțieze.

Implementare: Delivery/DeliveryFactories.cs

Explicație: În loc să folosesc instanțierea directă în logica aplicației, am creat DeliveryCreator (clasa abstractă) și subclase concrete (ExpressDeliveryCreator). Când clientul alege livrarea, aplicația cere fabricii să producă instanța corectă prin apelarea metodei FactoryMethod().

C. Builder Pattern

Concept: Separă construcția unui obiect complex de reprezentarea sa, permițând asamblarea pas cu pas.

Implementare: Builders/CustomOrderBuilder.cs și Builders/OrderDirector.cs

Explicație: O comandă (FlowerOrder) a devenit un obiect complex. Am folosit un Builder care are metode specifice pentru fiecare pas (BuildRoses, BuildDelivery). Adițional, am implementat clasa OrderDirector care funcționează ca un rețetar: la comanda "ConstructValentinesBouquet", directorul dictează automat builder-ului toți pașii necesari pentru a asambla buchetul predefinit.

4. Principiile SOLID Respectate (Din Lab 1)

SRP (Single Responsibility Principle):

Separarea clară a datelor matematice (FlowerOrder) de logica de printare pe ecran (InvoicePrinter). Clasa de facturare nu face calcule, doar afișează.

OCP (Open/Closed Principle):

Interfața IDeliveryCost. Dacă florăria adaugă Livrare VIP cu Drona, vom crea doar o clasă nouă care extinde interfața, fără să modificăm codul claselor de livrare existente sau logica de bază a comenzii. Sistemul este deschis pentru extindere și închis pentru modificare.

ISP (Interface Segregation Principle):

Plățile au fost separate în două interfețe mici: ICardPayment și ICashPayment. Comanda online (OnlineOrder) implementează DOAR plata cu cardul, nefiind obligată să aibă metode irelevante (cum ar fi plata cu bani gheață).

5. Instrucțiuni de Rulare

Deschideți un terminal în rădăcina proiectului.

Compilați și rulați aplicația folosind comanda dotnet run.

Urmați instrucțiunile interactive din meniul afișat în consolă.