
using System.Globalization;
using ExpenseTracker;

class Program
{
    static void Main()
    {
        Console.WriteLine("""
========================================
          WELCOME TO MY MONTHLY BUDGET
========================================
""");

        decimal totalSpent = 0m;
        decimal monthlySpent = 0m;
        decimal highestExpense = 0m;
        decimal? monthlyBudget = null;

        int expenseCount = 0;

        string highestDescription = "";

        decimal foodTotal = 0m;
        decimal transportTotal = 0m;
        decimal utilitiesTotal = 0m;
        decimal entertainmentTotal = 0m;
        decimal otherTotal = 0m;

        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("------------- MAIN MENU -------------");
            Console.WriteLine("1) Add an expense");
            Console.WriteLine("2) View summary");
            Console.WriteLine("3) Set monthly budget");
            Console.WriteLine("4) Exit");
            Console.WriteLine("-------------------------------------");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice.Trim())
            {
                case "1":
                    AddExpense();
                    break;

                case "2":
                    ViewSummary();
                    break;

                case "3":
                    MyMonthlyBudget();
                    break;

                case "4":
                    running = false;
                    //Console.WriteLine();
                    //Console.WriteLine("Thank you for using MyBudget!");
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Try Again. Please choose 1, 2, 3, or 4.");
                    break;
            }
        }
    }
}

// Budget entry and evaluation
void AddExpense()
{
    Console.WriteLine("\n========== Add An Expense ==========");

    string description;

    while (true)
    {
        Console.Write("Enter description: ");
        description = Console.ReadLine()?.Trim() ?? "";

        if (!string.IsNullOrWhiteSpace(description))
            break;

        Console.WriteLine("Description cannot be empty. Try again.");
    }

    decimal amount;

    while (true)
    {
        Console.Write("Enter amount: ");
        string amountInput = Console.ReadLine() ?? "";

        if (!decimal.TryParse(
            amountInput,
            NumberStyles.Number,
            CultureInfo.CurrentCulture,
            out decimal enteredAmount))
        {
            Console.WriteLine("Invalid number. Please try again.");
            continue;
        }

        try
        {
            amount = BudgetRules.ValidateAmount(enteredAmount);
            break;
        }
        catch (InvalidExpenseException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    string? category;

    while (true)
    {
        Console.WriteLine("\nAvailable categories:");
        Console.WriteLine("Food, Transport, Utilities, Entertainment, Other");
        Console.Write("Enter category: ");

        category = BudgetRules.NormalizeCategory(Console.ReadLine());

        if (category != null)
            break;

        Console.WriteLine("Invalid category. Please try again.");
    }

    DateTime expenseDate;

    while (true)
    {
        Console.Write("Enter date (yyyy-mm-dd), or press Enter for today: ");

        string dateInput = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(dateInput))
        {
            expenseDate = DateTime.Today;
            break;
        }

        if (!DateTime.TryParseExact(
            dateInput.Trim(),
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out expenseDate))
        {
            Console.WriteLine("Invalid date format. Use yyyy-mm-dd.");
            continue;
        }

        if (expenseDate.Date > DateTime.Today)
        {
            Console.WriteLine("Future dates are not allowed.");
            continue;
        }

        break;
    }

    Console.Write("Enter optional note (press Enter to skip): ");

    string? note = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(note))
        note = null;
    else
        note = note.Trim();

    // Update overall totals.
    totalSpent += amount;
    expenseCount++;

    // Update current-month spending only.
    if (expenseDate.Year == DateTime.Today.Year &&
        expenseDate.Month == DateTime.Today.Month)
    {
        monthlySpent += amount;
    }

    // Update highest expense.
    if (amount > highestExpense)
    {
        highestExpense = amount;
        highestDescription = description;
    }

    // Update matching category total.
    switch (category)
    {
        case "Food":
            foodTotal += amount;
            break;

        case "Transport":
            transportTotal += amount;
            break;

        case "Utilities":
            utilitiesTotal += amount;
            break;

        case "Entertainment":
            entertainmentTotal += amount;
            break;

        case "Other":
            otherTotal += amount;
            break;
    }

    string sizeBand = BudgetRules.ClassifyAmount(amount);

    Console.WriteLine("\nExpense added successfully!");
    Console.WriteLine($"Description: {description}");
    Console.WriteLine($"Amount: {BudgetRules.FormatCurrency(amount)}");
    Console.WriteLine($"Category: {category}");
    Console.WriteLine($"Date: {expenseDate:yyyy-MM-dd}");
    Console.WriteLine($"Size classification: {sizeBand}");

    if (note != null)
        Console.WriteLine($"Note: {note}");

    Console.WriteLine($"Running total: {BudgetRules.FormatCurrency(totalSpent)}");
}

// ViewSummary function
void ViewSummary()
{
    Console.WriteLine("\n========== EXPENSE SUMMARY ==========");

    if (expenseCount == 0)
    {
        Console.WriteLine("No expenses have been recorded yet.");
    }
    else
    {
        decimal average = totalSpent / expenseCount;

        Console.WriteLine($"Number of expenses: {expenseCount}");
        Console.WriteLine($"Total spent: {BudgetRules.FormatCurrency(totalSpent)}");
        Console.WriteLine($"Average expense: {BudgetRules.FormatCurrency(average)}");
        Console.WriteLine($"Highest expense: {BudgetRules.FormatCurrency(highestExpense)}");
        Console.WriteLine($"Highest expense description: {highestDescription}");
    }

    Console.WriteLine("\n-------- CATEGORY BREAKDOWN --------");

    Console.WriteLine($"Food: {BudgetRules.FormatCurrency(foodTotal)}");
    Console.WriteLine($"Transport: {BudgetRules.FormatCurrency(transportTotal)}");
    Console.WriteLine($"Utilities: {BudgetRules.FormatCurrency(utilitiesTotal)}");
    Console.WriteLine($"Entertainment: {BudgetRules.FormatCurrency(entertainmentTotal)}");
    Console.WriteLine($"Other: {BudgetRules.FormatCurrency(otherTotal)}");

    Console.WriteLine("\n---------- MONTHLY BUDGET ----------");

    if (monthlyBudget.HasValue)
    {
        decimal remaining = monthlyBudget.Value - monthlySpent;

        Console.WriteLine($"Monthly budget: {BudgetRules.FormatCurrency(monthlyBudget.Value)}");
        Console.WriteLine($"Spent this month: {BudgetRules.FormatCurrency(monthlySpent)}");
        Console.WriteLine($"Remaining: {BudgetRules.FormatCurrency(remaining)}");

        Console.WriteLine(
            $"Status: {BudgetRules.BudgetStatus(remaining, monthlyBudget.Value)}");
    }
    else
    {
        Console.WriteLine("No monthly budget has been set.");
    }

    Console.WriteLine("====================================");
}
// MonthlyBudget
void MyMonthlyBudget()
{
    Console.WriteLine("\n========== Set Monthly Budget ==========");

    while (true)
    {
        Console.Write("Enter your monthly budget: ");

        string budgetInput = Console.ReadLine() ?? "";

        if (!decimal.TryParse(
            budgetInput,
            NumberStyles.Number,
            CultureInfo.CurrentCulture,
            out decimal enteredBudget))
        {
            Console.WriteLine("Invalid number. Please try again.");
            continue;
        }

        try
        {
            monthlyBudget = BudgetRules.ValidateAmount(enteredBudget);

            decimal remaining = monthlyBudget.Value - monthlySpent;

            Console.WriteLine(
                $"Monthly budget set to: {BudgetRules.FormatCurrency(monthlyBudget.Value)}");

            Console.WriteLine(
                $"Already spent this month: {BudgetRules.FormatCurrency(monthlySpent)}");

            Console.WriteLine(
                $"Remaining: {BudgetRules.FormatCurrency(remaining)}");

            Console.WriteLine(
                $"Budget status: {BudgetRules.BudgetStatus(remaining, monthlyBudget.Value)}");

            break;
        }
        catch (InvalidExpenseException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
