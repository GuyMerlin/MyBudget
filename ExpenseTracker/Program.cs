namespace  ExpenseTracker;

public static class BudgetRules
{
    public const decimal MaxAmount = 10_000m;
    public const decimal NearLimitFraction = 0.10m;

    public static decimal ValidateAmount(decimal amount)
    {
        if (amount <= 0)
            throw new InvalidExpenseException("Amount must be greater than zero.");

        if (amount > MaxAmount)
            throw new InvalidExpenseException("Amount exceeds the maximum allowed limit.");

        return decimal.Round(amount, 2);
    }

    public static string ClassifyAmount(decimal amount)
    {
        if (amount <= 0)
            throw new InvalidExpenseException("Amount must be positive.");

        return amount switch
        {
            < 10m => "Micro",
            < 50m => "Small",
            < 200m => "Medium",
            _ => "Large"
        };
    }

    public static string? NormalizeCategory(string? input)
    {
        return input?.Trim().ToLowerInvariant() switch
        {
            "food"  => "Food",
            "transport"  => "Transport",
            "utilities"  => "Utilities",
            "entertainment"  => "Entertainment",
            "other"  => "Other",
            _ => null
        };
    }

    public static string BudgetStatus(decimal remaining, decimal monthlyLimit)
    {
        if (monthlyLimit <= 0)
            throw new InvalidExpenseException("Monthly budget must be positive.");

        if (remaining < 0)
            return "OVER BUDGET";

        if (remaining < monthlyLimit * NearLimitFraction)
            return "Almost out";

        return "On track";
    }

    public static string FormatCurrency(decimal amount)
        => FormatCurrency(amount, "$");

    public static string FormatCurrency(decimal amount, string currencySymbol)
    {
        return $"{currencySymbol}{amount:0.00}";
    }
}

public sealed class InvalidExpenseException(string message) : Exception(message);

