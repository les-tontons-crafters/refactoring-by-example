using System;

namespace TellDontAskKata.Main.Domain;

internal static class Extensions
{
    public static decimal Round(this decimal amount) => decimal.Round(amount, 2, MidpointRounding.ToPositiveInfinity);
}