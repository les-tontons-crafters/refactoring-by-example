namespace TellDontAskKata.Main.Commands;

/// <summary>
/// Represents an item dedicated to order creation.
/// </summary>
/// <param name="Name">The name of the item.</param>
/// <param name="Quantity">The quantity of the item.</param>
public record CreateOrderItem(string Name, int Quantity);