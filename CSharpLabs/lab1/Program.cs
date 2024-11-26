using System;

class Student
{
    private string _name;
    public int Age { get; set; }

    public Student(string name, int Age)
    {
        this._name = name;
        this.Age = Age;
    }

    public Student(string name)
    {
        this._name = name;
        Age = 0;
    }

    public string WriteInfo()
    {
        return "Студент " + _name + ", возраст " + Age;
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

    static void Main(string[] args)
    {
        Student Oleg = new("Олег", 25);
        Student Venya = new("Веня");

        // Демонстрация работы WriteInfo
        Console.WriteLine(Oleg.WriteInfo());
        Oleg.BecomeOlder();

        Console.WriteLine(Venya.WriteInfo());
        Oleg.SetName("Рома");

        // Повторный вызов после изменения имени
        Console.WriteLine(Oleg.WriteInfo());
        Oleg.BecomeOlder();

        // Демонстрация работы геттера GetName
        Console.WriteLine("Имя студента: " + Oleg.GetName());
    }
}
