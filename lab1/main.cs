using System;
using System.Collections.Generic;
using FlorarieSOLID;

// 1. Creăm un dicționar (catalog) pentru a găsi ușor florile după nume
Dictionary<string, Flower> catalog = new Dictionary<string, Flower>(StringComparer.OrdinalIgnoreCase) {
    {"Trandafir", new Flower("Trandafir", 50.0)},
    {"Bujor", new Flower("Bujor", 65.0)},
    {"Lalea", new Flower("Lalea", 20.0)},
    {"Crizantema", new Flower("Crizantemă", 30.0)}
};

FlowerOrder comandaMea = new FlowerOrder();

Console.WriteLine("=== BINE AI VENIT LA FLORĂRIE ===");
Console.WriteLine("--------------------------------------------------------------------------------------");

// 2. Buclă interactivă pentru adăugarea florilor
while (true)
{
    Console.WriteLine("\nFlori disponibile: Trandafir (50 lei), Bujor (65 lei), Lalea (20 lei), Crizantema (30 lei)");
    Console.Write("\nScrie numele florii (sau scrie 'gata' pentru a finaliza comanda): ");
    string numeFloare = Console.ReadLine();

    // Dacă utilizatorul scrie "gata", oprim bucla
    if (numeFloare.ToLower() == "gata")
    {
        break;
    }

    // Verificăm dacă floarea introdusă există în catalog
    if (catalog.ContainsKey(numeFloare))
    {
        Console.Write($"Câte fire de {numeFloare} dorești? ");
        string cantitateText = Console.ReadLine();
        
        // Convertim textul în număr (și ne asigurăm că a introdus un număr valid)
        if (int.TryParse(cantitateText, out int cantitate))
        {
            comandaMea.AddFlower(catalog[numeFloare], cantitate);
            Console.WriteLine($"-> Am adăugat {cantitate} x {numeFloare} în coș!");
        }
        else
        {
            Console.WriteLine("Eroare: Te rog să introduci un număr valid pentru cantitate.");
        }
    }
    else
    {
        Console.WriteLine("Nu avem această floare în catalog. Încearcă din nou.");
    }
}

// 3. Alegerea metodei de livrare de la tastatură (Demonstrație OCP)
Console.WriteLine("\nCum dorești livrarea?");
Console.WriteLine("1 - Standard (15 lei)");
Console.WriteLine("2 - Express (30 lei)");
Console.Write("Alegerea ta (1/2): ");
string alegereLivrare = Console.ReadLine();

IDeliveryCost livrare;
if (alegereLivrare == "2") {
    livrare = new ExpressDelivery();
} else {
    livrare = new StandardDelivery();
}

// 4. Alegerea metodei de plată (Demonstrație ISP)
Console.WriteLine("\nCum dorești să plătești?");
Console.WriteLine("1 - Cash la magazin");
Console.WriteLine("2 - Cu cardul online");
Console.Write("Alegerea ta (1/2): ");
string alegerePlata = Console.ReadLine();

string metodaPlata;
if (alegerePlata == "1") {
    InStoreOrder comandaFizica = new InStoreOrder();
    comandaFizica.PayWithCash(); // Folosim interfața de cash
    metodaPlata = "Cash la magazin";
} else {
    OnlineOrder comandaWeb = new OnlineOrder();
    comandaWeb.PayWithCard(); // Folosim interfața de card online
    metodaPlata = "Card online";
}

// 5. Sumarul final al comenzii (Demonstrație SRP)
InvoicePrinter printer = new InvoicePrinter();
printer.PrintSummary(comandaMea, livrare, metodaPlata);