
using System; 
using System.Globalization; 

/* Here started my program */

class Program
{
    // Declaring and initializing data-processing variables
    static decimal TotalExpenses = 0m;
    static int ExpenseCount = 0;
    static decimal HighestExpense = 0m; 
    static decimal LowestExpense = 0m; 
    static decimal FoodTotal = 0m;
    static decimal TransportTotal = 0m;
    static decimal UtilitiesTotal = 0m;
    static decimal EntertainmentTotal = 0m;
    static decimal OtherExpenseTotal = 0m;
    static decimal MyMonthlyBudget = 0m;

    // Expense categories
    public enum ExpenseCategoryType
    {
        Food,
        Utilities,
        Transport,
        Entertainment,
        Other
    }

    
    static void Main()
    {
        Console.WriteLine("==============================================");
        Console.WriteLine("       Welcome to my Budget Expense Tracker");
        Console.WriteLine("==============================================");

        bool exect = true;

        while (exect)
        {
            Console.WriteLine("\nPlease choose an option:");
            Console.WriteLine("1) Add an expense");
            Console.WriteLine("2) View summary");
            Console.WriteLine("3) Set monthly budget");
            Console.WriteLine("4) Exit");
            Console.Write("Enter your choice: ");

            string option = Console.ReadLine() ?? "";

            switch (option)
            {
                case "1":
                    Console.WriteLine("Add an expense selected.");
                    AddExpense();
                    break;

                case "2":
                    Console.WriteLine("View summary selected.");
                    ViewSummary();
                    break;

                case "3":
                    Console.WriteLine("Set monthly budget selected.");
                    EnterMonthlyBudget();
                    break;

                case "4":
                    Console.WriteLine("Thank you for using MyBudget!");
                    exect = false;
                    break;

                default:
                    Console.WriteLine("Enter a number between 1 and 4.");
                    break;
            }
        }
    }

        static void AddExpense()
    {
        // Get and validate description.
        string description;

        while (true)
        {
            Console.Write("Enter expense description: ");
            description = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(description))
            {
                description = description.Trim();
                break;
            }

            Console.WriteLine("Error: Enter a valid expense description.");
        }

        // Get and validate expense amount.
        decimal amount;

        while (true)
        {
            Console.Write("Enter expense amount: ");
            string input = Console.ReadLine() ?? "";

            if (!decimal.TryParse(
                input,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out amount))
            {
                Console.WriteLine("Error: Enter a valid number.");
                continue;
            }

            if (amount <= 0m || amount > 5000m)
            {
                Console.WriteLine(
                    "Error: Amount must be greater than $0 " +
                    "and no more than $5,000.");
                continue;
            }

            break;
        }

                ExpenseCategoryType category;

        while (true)
        {
            Console.Write(
                "Enter category (Food, Transport, Utilities, " +
                "Entertainment, Other): ");

            string TypeCategory = Console.ReadLine()?.Trim() ?? "";

            if (Enum.TryParse<ExpenseCategoryType>(
                    TypeCategory, true, out category)
                && Enum.IsDefined(typeof(ExpenseCategoryType), category))
            {
                break;
            }

            Console.WriteLine("Error: Invalid category. Please try again.");
        }

        // Get and validate expense date.
        DateTime expenseDate;

        while (true)
        {
            Console.Write(
                "Enter date (yyyy-MM-dd), or press Enter for today: ");

            string dateInput = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(dateInput))
            {
                expenseDate = DateTime.Today;
                break;
            }

            if (!DateTime.TryParseExact(
                dateInput,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out expenseDate))
            {
                Console.WriteLine("Error: Use the format yyyy-MM-dd.");
                continue;
            }

            if (expenseDate.Date > DateTime.Today)
            {
                Console.WriteLine("Error: Future dates are not allowed.");
                continue;
            }

            break;
        }

        // Get optional note.
        Console.Write("Enter optional note (or press Enter to skip): ");
        string? note = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(note))
        {
            note = null;
        }
        else
        {
            note = note.Trim();
        }

        if (ExpenseCount == 0)
        {
            HighestExpense = amount;
            LowestExpense = amount;
        }
        else
        {
            if (amount > HighestExpense)
            {
                HighestExpense = amount;
            }

            if (amount < LowestExpense)
            {
                LowestExpense = amount;
            }
        }

        TotalExpenses += amount;
        ExpenseCount++;

        
        switch (category)
        {
            case ExpenseCategoryType.Food:
                FoodTotal += amount;
                break;

            case ExpenseCategoryType.Transport:
                TransportTotal += amount;
                break;

            case ExpenseCategoryType.Utilities:
                UtilitiesTotal += amount;
                break;

            case ExpenseCategoryType.Entertainment:
                EntertainmentTotal += amount;
                break;

            case ExpenseCategoryType.Other:
                OtherExpenseTotal += amount;
                break;
        }

        // Confirm the expense.
        Console.WriteLine("\nExpense added successfully!");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"Amount:      {amount:C}");
        Console.WriteLine($"Category:    {category}");
        Console.WriteLine($"Date:        {expenseDate:yyyy-MM-dd}");

        if (note != null)
        {
            Console.WriteLine($"Note:        {note}");
        }

        Console.WriteLine($"Total spent: {TotalExpenses:C}");

                if (amount < 50m)
        {
            Console.WriteLine("Expense size: Small");
        }
        else if (amount < 200m)
        {
            Console.WriteLine("Expense size: Medium");
        }
        else
        {
            Console.WriteLine("Expense size: Large");
        }
    }
    static void ViewSummary()
    {
        Console.WriteLine("\n========== EXPENSE SUMMARY ==========");
        Console.WriteLine($"Expense count:       {ExpenseCount}");
        Console.WriteLine($"Total expenses:      {TotalExpenses:C}");

        if (ExpenseCount > 0)
        {
            Console.WriteLine($"Highest expense:     {HighestExpense:C}");
            Console.WriteLine($"Lowest expense:      {LowestExpense:C}");
        }
        else
        {
            Console.WriteLine("No expenses have been entered yet.");
        }

        Console.WriteLine("\nCategory totals:");
        Console.WriteLine($"Food:                {FoodTotal:C}");
        Console.WriteLine($"Transport:           {TransportTotal:C}");
        Console.WriteLine($"Utilities:            {UtilitiesTotal:C}");
        Console.WriteLine($"Entertainment:        {EntertainmentTotal:C}");
        Console.WriteLine($"Other:                {OtherExpenseTotal:C}");

        Console.WriteLine($"\nMonthly budget:       {MyMonthlyBudget:C}");

        if (MyMonthlyBudget > 0m)
        {
            decimal remainingBudget = MyMonthlyBudget - TotalExpenses;
            Console.WriteLine($"Budget remaining:     {remainingBudget:C}");

            if (remainingBudget < 0m)
            {
                Console.WriteLine("Over Expense of your Budget.");
            }
        }
        else
        {
            Console.WriteLine("Set a monthly budget to track remaining funds.");
        }

        Console.WriteLine("=====================================");
    }

       static void EnterMonthlyBudget()
    {
        decimal budget;

        while (true)
        {
            Console.Write("Enter your monthly budget: ");
            string input = Console.ReadLine() ?? "";

            if (!decimal.TryParse(
                input,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out budget))
            {
                Console.WriteLine("Error: Enter a valid number.");
                continue;
            }

            if (budget <= 0m || budget > 1000000m)
            {
                Console.WriteLine(
                    "Error: Budget must be greater than $0 " +
                    "and no more than $1,000,000.");
                continue;
            }

            break;
        }

        MyMonthlyBudget = budget;

        Console.WriteLine($"Monthly budget set to {MyMonthlyBudget:C}");

        decimal remainingBudget = MyMonthlyBudget - TotalExpenses;
        Console.WriteLine($"Budget remaining: {remainingBudget:C}");

        if (remainingBudget < 0m)
        {
            Console.WriteLine("Warning: You have exceeded your budget.");
        }
    }
}
