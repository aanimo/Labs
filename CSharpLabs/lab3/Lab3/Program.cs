using System;

class Student
{
    private string name;
    public int Age { get; set; }

    // Статическое поле
    public static int TotalStudents;

    // Статическое свойство
    public static string University { get; set; }

    // Статический метод
    public static void DisplayTotalStudents()
    {
        Console.WriteLine($"Всего студентов: {TotalStudents}");
    }

    // Статический конструктор
    static Student()
    {
        University = "СГУ";
        TotalStudents = 0;
    }

    public Student(string name, int age)
    {
        this.name = name;
        this.Age = age;
        TotalStudents++; // Увеличение количества студентов при создании объекта
    }

    public Student(string name)
    {
        this.name = name;
        Age = 0;
        TotalStudents++; // Увеличение количества студентов при создании объекта
    }

    public void WriteInfo()
    {
        Console.WriteLine($"Студент {name}, возраст {Age}, университет: {University}");
    }

    public void BecomeOlder()
    {
        Console.WriteLine($"Студент {name}, возраст {Age + 1}");
    }

    public string GetName()
    {
        return name;
    }

    public void SetName(string _name)
    {
        this.name = _name;
    }

    // Тестирование программы
    static void Main(string[] args)
    {
        // Создание объектов разными способами
        Student Oleg = new("Олег", 25);
        Student Venya = new("Веня");
        Student Anonymous = new Student("Неизвестный");

        // Использование методов
        Oleg.WriteInfo();
        Oleg.BecomeOlder();

        Venya.WriteInfo();
        Oleg.SetName("Рома");
        Oleg.BecomeOlder();

        // Использование статического метода
        Student.DisplayTotalStudents();

        // Создание статического класса и взаимодействие
        StudentHelper.GraduationMessage(Oleg);
        StudentHelper.GraduationMessage(Venya);

        Console.ReadLine();
    }
}

// Статический класс
static class StudentHelper
{
    public static void GraduationMessage(Student student)
    {
        Console.WriteLine($"{student.GetName()} закончил университет {Student.University}.");
    }
}
