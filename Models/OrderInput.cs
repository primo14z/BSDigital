namespace BSDigital.Models;

public class OrderInput
{
    public string Type { get; set; }
    public double Amount { get; set; }

    public OrderInput(string type, double amount)
    {
        Type = type;
        Amount = amount;
    }
}