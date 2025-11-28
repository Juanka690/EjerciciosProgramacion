using EjerciciosProgramacion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EjerciciosProgramacion.Controllers;

public class ExercisesController : Controller
{
    private static readonly List<TaskItem> _tasks = new();
    private static readonly List<Expense> _expenses = new();
    private static readonly List<Booking> _bookings = new();
    private static readonly List<Note> _notes = new();
    private static readonly List<CalendarEvent> _events = new();
    private static readonly List<Recipe> _recipes = new();
    private static readonly SurveyPlatformViewModel _survey = new();
    private static readonly Random _random = new();

    private const string MemorySessionKey = "memory-game";
    private const string StopwatchSessionKey = "stopwatch-state";

    public IActionResult Index() => RedirectToAction("Index", "Home");

    public IActionResult TaskList()
    {
        return View(new TaskListViewModel { Tasks = _tasks.ToList() });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddTask(TaskListViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.NewTask.Title))
        {
            ModelState.AddModelError("NewTask.Title", "La tarea es obligatoria");
        }

        if (!ModelState.IsValid)
        {
            model.Tasks = _tasks.ToList();
            return View("TaskList", model);
        }

        _tasks.Add(new TaskItem
        {
            Title = model.NewTask.Title,
            Category = model.NewTask.Category
        });

        return RedirectToAction(nameof(TaskList));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleTask(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task is not null)
        {
            task.Done = !task.Done;
        }

        return RedirectToAction(nameof(TaskList));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteTask(Guid id)
    {
        _tasks.RemoveAll(t => t.Id == id);
        return RedirectToAction(nameof(TaskList));
    }

    public IActionResult TipCalculator() => View(new TipCalculatorModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult TipCalculator(TipCalculatorModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        return View(model);
    }

    public IActionResult PasswordGenerator() => View(new PasswordGeneratorModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PasswordGenerator(PasswordGeneratorModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var builder = new StringBuilder();
        var chars = "abcdefghijklmnopqrstuvwxyz";
        if (model.IncludeUppercase)
        {
            chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }

        if (model.IncludeNumbers)
        {
            chars += "0123456789";
        }

        if (model.IncludeSymbols)
        {
            chars += "!@#$%^&*()_-+=[]{}";
        }

        for (int i = 0; i < model.Length; i++)
        {
            builder.Append(chars[_random.Next(chars.Length)]);
        }

        model.GeneratedPassword = builder.ToString();
        return View(model);
    }

    public IActionResult ExpenseManager()
    {
        return View(new ExpenseManagerViewModel
        {
            Expenses = _expenses.OrderByDescending(e => e.Date).ToList()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ExpenseManager(ExpenseManagerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Expenses = _expenses.OrderByDescending(e => e.Date).ToList();
            return View(model);
        }

        _expenses.Add(new Expense
        {
            Description = model.NewExpense.Description,
            Amount = model.NewExpense.Amount,
            Category = model.NewExpense.Category,
            Date = model.NewExpense.Date
        });

        return RedirectToAction(nameof(ExpenseManager));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveExpense(Guid id)
    {
        _expenses.RemoveAll(e => e.Id == id);
        return RedirectToAction(nameof(ExpenseManager));
    }

    public IActionResult BookingSystem()
    {
        return View(new BookingSystemViewModel
        {
            Bookings = _bookings.OrderBy(b => b.Date).ToList()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BookingSystem(BookingSystemViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Bookings = _bookings.OrderBy(b => b.Date).ToList();
            return View(model);
        }

        _bookings.Add(new Booking
        {
            Client = model.NewBooking.Client,
            Service = model.NewBooking.Service,
            Date = model.NewBooking.Date,
            Notes = model.NewBooking.Notes
        });

        return RedirectToAction(nameof(BookingSystem));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CancelBooking(Guid id)
    {
        _bookings.RemoveAll(b => b.Id == id);
        return RedirectToAction(nameof(BookingSystem));
    }

    public IActionResult NotesManager(string? search)
    {
        var filtered = string.IsNullOrWhiteSpace(search)
            ? _notes
            : _notes.Where(n => n.Title.Contains(search, StringComparison.OrdinalIgnoreCase)
                              || (n.Category?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
                              || n.Content.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

        return View(new NotesManagerViewModel
        {
            Notes = filtered.OrderBy(n => n.Title).ToList(),
            Search = search
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult NotesManager(NotesManagerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Notes = _notes.ToList();
            return View(model);
        }

        _notes.Add(new Note
        {
            Title = model.NewNote.Title,
            Category = model.NewNote.Category,
            Content = model.NewNote.Content
        });

        return RedirectToAction(nameof(NotesManager));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteNote(Guid id)
    {
        _notes.RemoveAll(n => n.Id == id);
        return RedirectToAction(nameof(NotesManager));
    }

    public IActionResult EventCalendar()
    {
        return View(new EventCalendarViewModel
        {
            Events = _events.OrderBy(e => e.Date).ToList()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EventCalendar(EventCalendarViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Events = _events.OrderBy(e => e.Date).ToList();
            return View(model);
        }

        _events.Add(new CalendarEvent
        {
            Title = model.NewEvent.Title,
            Date = model.NewEvent.Date,
            Location = model.NewEvent.Location
        });

        return RedirectToAction(nameof(EventCalendar));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteEvent(Guid id)
    {
        _events.RemoveAll(e => e.Id == id);
        return RedirectToAction(nameof(EventCalendar));
    }

    public IActionResult RecipePlatform(string? search)
    {
        var filtered = string.IsNullOrWhiteSpace(search)
            ? _recipes
            : _recipes.Where(r => r.Title.Contains(search, StringComparison.OrdinalIgnoreCase)
                               || (r.Category?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
                               || (r.Ingredients?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)).ToList();

        return View(new RecipePlatformViewModel
        {
            Recipes = filtered.OrderBy(r => r.Title).ToList(),
            Search = search
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RecipePlatform(RecipePlatformViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Recipes = _recipes.ToList();
            return View(model);
        }

        _recipes.Add(new Recipe
        {
            Title = model.NewRecipe.Title,
            Category = model.NewRecipe.Category,
            Ingredients = model.NewRecipe.Ingredients,
            Instructions = model.NewRecipe.Instructions
        });

        return RedirectToAction(nameof(RecipePlatform));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteRecipe(Guid id)
    {
        _recipes.RemoveAll(r => r.Id == id);
        return RedirectToAction(nameof(RecipePlatform));
    }

    public IActionResult MemoryGame()
    {
        var state = LoadMemoryGame();
        return View(state);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ResetMemoryGame()
    {
        HttpContext.Session.Remove(MemorySessionKey);
        return RedirectToAction(nameof(MemoryGame));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PlayMemoryGame(int firstChoice, int secondChoice)
    {
        var state = LoadMemoryGame();
        if (firstChoice == secondChoice || firstChoice < 1 || secondChoice < 1 ||
            firstChoice > state.Cards.Count || secondChoice > state.Cards.Count)
        {
            state.Messages.Add("Selecciona dos posiciones diferentes dentro del rango válido.");
            SaveMemoryGame(state);
            return View("MemoryGame", state);
        }

        var first = state.Cards[firstChoice - 1];
        var second = state.Cards[secondChoice - 1];

        first.Revealed = second.Revealed = true;
        state.Moves++;

        if (first.Value == second.Value)
        {
            first.Matched = second.Matched = true;
            state.Messages.Add($"¡Encontraste un par de {first.Value}!");
        }
        else
        {
            state.Messages.Add("No coinciden, intenta de nuevo.");
            first.Revealed = second.Revealed = false;
        }

        SaveMemoryGame(state);
        return View("MemoryGame", state);
    }

    public IActionResult SurveyPlatform()
    {
        return View(_survey);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SurveyPlatform(Guid selectedOptionId)
    {
        var option = _survey.Options.FirstOrDefault(o => o.Id == selectedOptionId);
        if (option is not null)
        {
            option.Votes++;
        }

        return View(_survey);
    }

    public IActionResult Stopwatch()
    {
        var state = LoadStopwatchState();
        return View(ToViewModel(state));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Stopwatch(string actionType)
    {
        var state = LoadStopwatchState();
        var now = DateTime.UtcNow;

        switch (actionType)
        {
            case "start":
                if (!state.Running)
                {
                    state.Running = true;
                    state.StartTime = now;
                }
                break;
            case "pause":
                if (state.Running)
                {
                    state.Elapsed += now - state.StartTime;
                    state.Running = false;
                }
                break;
            case "reset":
                state = new StopwatchState();
                break;
            case "lap":
                var elapsed = state.Elapsed + (state.Running ? now - state.StartTime : TimeSpan.Zero);
                state.Laps.Add(elapsed);
                break;
        }

        SaveStopwatchState(state);
        return RedirectToAction(nameof(Stopwatch));
    }

    private MemoryGameState LoadMemoryGame()
    {
        var state = HttpContext.Session.Get<MemoryGameState>(MemorySessionKey);
        if (state is not null)
        {
            return state;
        }

        var values = new[] { "A", "B", "C", "D" };
        var cards = values.SelectMany(v => new[] { v, v })
            .Select((value, index) => new MemoryCard { Position = index + 1, Value = value })
            .OrderBy(_ => _random.Next())
            .ToList();

        state = new MemoryGameState { Cards = cards };
        SaveMemoryGame(state);
        return state;
    }

    private void SaveMemoryGame(MemoryGameState state)
    {
        HttpContext.Session.Set(MemorySessionKey, state);
    }

    private StopwatchState LoadStopwatchState()
    {
        var state = HttpContext.Session.Get<StopwatchState>(StopwatchSessionKey);
        return state ?? new StopwatchState();
    }

    private void SaveStopwatchState(StopwatchState state)
    {
        HttpContext.Session.Set(StopwatchSessionKey, state);
    }

    private StopwatchViewModel ToViewModel(StopwatchState state)
    {
        var now = DateTime.UtcNow;
        var elapsed = state.Elapsed + (state.Running ? now - state.StartTime : TimeSpan.Zero);

        return new StopwatchViewModel
        {
            Elapsed = elapsed,
            Running = state.Running,
            Laps = state.Laps.ToList()
        };
    }
}
