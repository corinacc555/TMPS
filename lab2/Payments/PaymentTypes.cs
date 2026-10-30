namespace FlorarieSOLID;
using System;

public class InStoreOrder : ICardPayment, ICashPayment 
{
    public void PayWithCard() => Console.WriteLine("[Plată] Card acceptat la POS-ul din magazin.");
    public void PayWithCash() => Console.WriteLine("[Plată] Cash acceptat la casa de marcat.");
}

public class OnlineOrder : ICardPayment 
{
    public void PayWithCard() => Console.WriteLine("[Plată] Tranzacție online cu cardul procesată securizat.");
}