using System;

class Student
{
    private string _name;
    public int Age { get; set; }
    public static int StudentCount { get; private set; }

    public static string UniversityName { get; set; }

    static Student()
    {
        UniversityName = "СГУ";
        StudentCount = 0;
    }

    public Student(string name, int Age)
    {
        this._name = name;
        this.Age = Age;
        StudentCount++;
    }

    public Student(string name)
    {
        this._name = name;
        Age = 0;
        StudentCount++;
    }

    public string WriteInfo()
    {
        return $"Студент {_name}, возраст {Age}, школа {UniversityName}";
    }

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

    public static void ShowStudentCount()
    {
        Console.WriteLine($"Общее количество студентов: {StudentCount}");
    }

    static void Main(string[] args)
    {
        Student Oleg = new("Олег", 25);
        Student Venya = new("Веня");
        Student Anna = new("Анна", 19);

        Console.WriteLine(Oleg.WriteInfo());
        Oleg.BecomeOlder();

        Console.WriteLine(Venya.WriteInfo());
        Venya.BecomeOlder();

        Console.WriteLine(Anna.WriteInfo());

        Student.ShowStudentCount();

        Console.WriteLine("Имя студента: " + Oleg.GetName());

        StudentHelper.ShowSchoolName();
    }
}

static class StudentHelper
{
    public static void ShowSchoolName()
    {
        Console.WriteLine($"Школа: {Student.UniversityName}");
    }
}
