using System;

class Student
{
    private string _name;
    public int Age { get; set; }

    // Статическое поле для подсчёта количества студентов
    public static int StudentCount { get; private set; }

    // Статическое свойство для хранения имени университета
    public static string UniversityName { get; set; }

    // Статический конструктор
    static Student()
    {
        UniversityName = "СГУ";
        StudentCount = 0;
    }

    // Конструктор с двумя параметрами
    public Student(string name, int Age)
    {
        this._name = name;
        this.Age = Age;
        StudentCount++; // Увеличиваем счётчик при создании нового студента
    }

    // Конструктор с одним параметром
    public Student(string name)
    {
        this._name = name;
        Age = 0;
        StudentCount++; // Увеличиваем счётчик
    }

    // Метод WriteInfo теперь возвращает строку
    public string WriteInfo()
    {
        return $"Студент {_name}, возраст {Age}, школа {UniversityName}";
    }

    // Метод BecomeOlder увеличивает возраст на 1
    public void BecomeOlder()
    {
        Age++;
        Console.WriteLine("Студент " + _name + ", возраст " + Age);
    }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        this._name = name;
    }

    // Статический метод для вывода общего количества студентов
    public static void ShowStudentCount()
    {
        Console.WriteLine($"Общее количество студентов: {StudentCount}");
    }

    static void Main(string[] args)
    {
        // Создаём три объекта класса Student различными способами
        Student Oleg = new("Олег", 25);
        Student Venya = new("Веня");
        Student Anna = new("Анна", 19);

        // Демонстрация работы WriteInfo и BecomeOlder
        Console.WriteLine(Oleg.WriteInfo());
        Oleg.BecomeOlder();

        Console.WriteLine(Venya.WriteInfo());
        Venya.BecomeOlder();

        Console.WriteLine(Anna.WriteInfo());

        // Демонстрация работы статического метода
        Student.ShowStudentCount();

        // Демонстрация работы геттера GetName
        Console.WriteLine("Имя студента: " + Oleg.GetName());

        // Использование статического класса для работы с учениками
        StudentHelper.ShowSchoolName();
    }
}

// Статический класс для работы со студентами
static class StudentHelper
{
    // Статический метод для вывода имени школы
    public static void ShowSchoolName()
    {
        Console.WriteLine($"Школа: {Student.UniversityName}");
    }
}
