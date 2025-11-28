using System.ComponentModel.DataAnnotations;

namespace EjerciciosProgramacion.Models;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Category { get; set; }

    public bool Done { get; set; }
}

public class TaskListViewModel
{
    public List<TaskItem> Tasks { get; set; } = new();
    public TaskItem NewTask { get; set; } = new();
}

public class TipCalculatorModel
{
    [Range(0, double.MaxValue)]
    public decimal Amount { get; set; }

    [Range(0, 100)]
    public int TipPercentage { get; set; } = 10;

    [Range(1, 100)]
    public int People { get; set; } = 1;

    public decimal TotalTip => Math.Round(Amount * TipPercentage / 100, 2);
    public decimal TotalWithTip => Math.Round(Amount + TotalTip, 2);
    public decimal PerPerson => People > 0 ? Math.Round(TotalWithTip / People, 2) : 0;
}

public class PasswordGeneratorModel
{
    [Range(4, 64)]
    public int Length { get; set; } = 12;

    public bool IncludeNumbers { get; set; } = true;

    public bool IncludeSymbols { get; set; } = true;

    public bool IncludeUppercase { get; set; } = true;

    public string? GeneratedPassword { get; set; }
}

public class Expense
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = "General";

    [Range(0, double.MaxValue)]
    public decimal Amount { get; set; }

    public DateTime Date { get; set; } = DateTime.Today;
}

public class ExpenseManagerViewModel
{
    public Expense NewExpense { get; set; } = new();
    public List<Expense> Expenses { get; set; } = new();
}

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Client { get; set; } = string.Empty;

    [Required]
    public string Service { get; set; } = string.Empty;

    public DateTime Date { get; set; } = DateTime.Today.AddDays(1);

    public string? Notes { get; set; }
}

public class BookingSystemViewModel
{
    public Booking NewBooking { get; set; } = new();
    public List<Booking> Bookings { get; set; } = new();
}

public class Note
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Category { get; set; }

    public string Content { get; set; } = string.Empty;
}

public class NotesManagerViewModel
{
    public Note NewNote { get; set; } = new();
    public string? Search { get; set; }
    public List<Note> Notes { get; set; } = new();
}

public class CalendarEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Title { get; set; } = string.Empty;

    public DateTime Date { get; set; } = DateTime.Today;

    public string? Location { get; set; }
}

public class EventCalendarViewModel
{
    public CalendarEvent NewEvent { get; set; } = new();
    public List<CalendarEvent> Events { get; set; } = new();
}

public class Recipe
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Category { get; set; }

    public string? Ingredients { get; set; }

    public string? Instructions { get; set; }
}

public class RecipePlatformViewModel
{
    public Recipe NewRecipe { get; set; } = new();
    public string? Search { get; set; }
    public List<Recipe> Recipes { get; set; } = new();
}

public class MemoryCard
{
    public int Position { get; set; }
    public string Value { get; set; } = string.Empty;
    public bool Revealed { get; set; }
    public bool Matched { get; set; }
}

public class MemoryGameState
{
    public List<MemoryCard> Cards { get; set; } = new();
    public List<string> Messages { get; set; } = new();
    public int Moves { get; set; }
}

public class SurveyOption
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public int Votes { get; set; }
}

public class SurveyPlatformViewModel
{
    public string Question { get; set; } = "¿Cuál es tu lenguaje de programación favorito?";
    public List<SurveyOption> Options { get; set; } = new()
    {
        new() { Text = "C#" },
        new() { Text = "Java" },
        new() { Text = "Python" },
        new() { Text = "JavaScript" }
    };
    public Guid SelectedOptionId { get; set; }
}

public class StopwatchState
{
    public bool Running { get; set; }
    public DateTime StartTime { get; set; }
    public TimeSpan Elapsed { get; set; }
    public List<TimeSpan> Laps { get; set; } = new();
}

public class StopwatchViewModel
{
    public TimeSpan Elapsed { get; set; }
    public bool Running { get; set; }
    public List<TimeSpan> Laps { get; set; } = new();
}
