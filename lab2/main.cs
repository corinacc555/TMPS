using System;
using System.Collections.Generic;
using FlorarieSOLID;

namespace FlorarieSOLID;

public class Program
{
    public static void Main(string[] args)
    {
        // 1. SINGLETON: Numele unicității magazinului
        Console.WriteLine("======================================================");
        Console.WriteLine($"   BINE AȚI VENIT LA {StoreConfiguration.Instance.StoreName.ToUpper()}!");
        Console.WriteLine("======================================================\n");

        // Catalogul de flori disponibile
        Dictionary<string, Flower> catalog = new Dictionary<string, Flower>(StringComparer.OrdinalIgnoreCase)
        {
            { "1", new Flower("Trandafir", 50.0) },
            { "2", new Flower("Bujor", 65.0) },
            { "3", new Flower("Lalea", 20.0) },
            { "4", new Flower("Crizantemă", 30.0) }
        };

        Console.WriteLine("Dorești o comandă rapidă sau una personalizată?");
        Console.WriteLine("1 - Pachet predefinit: Buchet Valentine's Day (Folosește Builder + Director)");
        Console.WriteLine("2 - Comandă personalizată (Alegi tu fiecare floare)");
        Console.Write("Alegerea ta (1/2): ");
        string modComanda = Console.ReadLine();

        FlowerOrder comandaMea;

        if (modComanda == "1")
        {
            // BUILDER + DIRECTOR: Asamblare automată
            Console.WriteLine("\n--> Se pregătește buchetul predefinit de Valentine's Day...");
            IOrderBuilder builder = new CustomOrderBuilder();
            OrderDirector director = new OrderDirector();
            director.ConstructValentinesBouquet(builder);
            comandaMea = builder.GetResult();
        }
        else
        {
            // BUILDER MANUAL: Adăugarea elementelor de la tastatură
            comandaMea = new FlowerOrder();

            while (true)
            {
                Console.WriteLine("\n--- Alege o floare de adăugat în coș ---");
                Console.WriteLine("1 - Trandafir (50 lei)");
                Console.WriteLine("2 - Bujor (65 lei)");
                Console.WriteLine("3 - Lalea (20 lei)");
                Console.WriteLine("4 - Crizantemă (30 lei)");
                Console.WriteLine("0 - Am terminat de ales florile");
                Console.Write("Opțiunea ta: ");
                string opțiune = Console.ReadLine();

                if (opțiune == "0") break;

                if (catalog.ContainsKey(opțiune))
                {
                    Flower floareSelectata = catalog[opțiune];
                    Console.Write($"Câte bucăți de {floareSelectata.Name} dorești? ");
                    
                    if (int.TryParse(Console.ReadLine(), out int cantitate) && cantitate > 0)
                    {
                        comandaMea.AddFlower(floareSelectata, cantitate);
                        Console.WriteLine($"[OK] S-au adăugat {cantitate} x {floareSelectata.Name} în coș!");
                    }
                    else
                    {
                        Console.WriteLine("[!] Cantitate invalidă.");
                    }
                }
                else
                {
                    Console.WriteLine("[!] Opțiune inexistentă în catalog.");
                }
            }

            // FACTORY METHOD: Alegerea tipului de livrare
            Console.WriteLine("\n--- Cum dorești să fie livrată comanda? ---");
            Console.WriteLine("1 - Livrare Standard (15 lei)");
            Console.WriteLine("2 - Livrare Express (30 lei)");
            Console.Write("Alegerea ta (1/2): ");
            string opțiuneLivrare = Console.ReadLine();

            DeliveryCreator creatorLivrare;
            if (opțiuneLivrare == "2")
            {
                creatorLivrare = new ExpressDeliveryCreator();
            }
            else
            {
                creatorLivrare = new StandardDeliveryCreator();
            }

            // Instanțiem livrarea prin metoda Factory
            comandaMea.Delivery = creatorLivrare.FactoryMethod();
        }

        // SRP: Tipărirea facturii finale
        InvoicePrinter printer = new InvoicePrinter();
        printer.PrintInvoice(comandaMea);

        // ISP: Alegerea metodei de plată
        Console.WriteLine("--- Alege metoda de plată ---");
        Console.WriteLine("1 - Cash la casa de marcat");
        Console.WriteLine("2 - Cu cardul online");
        Console.Write("Alegerea ta (1/2): ");
        string optiunePlata = Console.ReadLine();

        Console.WriteLine();
        if (optiunePlata == "1")
        {
            InStoreOrder plataFizica = new InStoreOrder();
            plataFizica.PayWithCash();
        }
        else
        {
            OnlineOrder plataOnline = new OnlineOrder();
            plataOnline.PayWithCard();
        }
    }
}