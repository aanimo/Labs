using System;

public class InvalidNameException : Exception
{
    public InvalidNameException(string message) : base(message) { }
}

public class InvalidAgeException : Exception
{
    public InvalidAgeException(string message) : base(message) { }
}

abstract class Person
{
    public abstract string Name { get; set; }
    public abstract void DisplayInfo();
}

class Student : Person
{
    private string _name;

    public override string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidNameException("Имя студента не может быть пустым или содержать только пробелы.");
            }
            _name = value;
        }
    }

    private int _age;
    public int Age
    {
        get => _age;
        set
        {
            if (value < 0)
            {
                throw new InvalidAgeException("Возраст студента не может быть отрицательным.");
            }
            _age = value;
        }
    }

    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public Student(string name) : this(name, 0) { }

    public virtual string WriteInfo()
    {
        return $"Студент {Name}, возраст {Age}";
    }

    public override void DisplayInfo()
    {
        Console.WriteLine(WriteInfo());
    }

    public override string ToString()
    {
        return $"Студент: {Name}, возраст: {Age}";
    }

    public void BecomeOlder()
    {
        Age++;
        Console.WriteLine($"Студент {Name}, возраст {Age}");
    }

    public void SetName(string name)
    {
        try
        {
            Name = name;
        }
        catch (InvalidNameException ex) when (name == null)
        {
            Console.WriteLine($"Ошибка: {ex.Message} (Имя не должно быть null)");
        }
        catch (InvalidNameException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}

class ITStudent : Student
{
    public string FavoriteLanguage { get; set; }

    public ITStudent(string name, int age, string favoriteLanguage) : base(name, age)
    {
        FavoriteLanguage = favoriteLanguage;
    }

    public override string WriteInfo()
    {
        return $"IT-Студент {Name}, возраст {Age}, любимый язык программирования: {FavoriteLanguage}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Student Oleg = new("Олег", 25);
            ITStudent Marina = new("Марина", 22, "C#");

            Console.WriteLine(Oleg.WriteInfo());
            Oleg.BecomeOlder();

            Console.WriteLine(Marina.WriteInfo());
            Marina.BecomeOlder();

            Console.WriteLine(Oleg.ToString());
            Console.WriteLine(Marina.ToString());

            Oleg.SetName("");
            Oleg.Age = -5;
        }
        catch (InvalidNameException ex)
        {
            Console.WriteLine($"Обнаружена ошибка имени: {ex.Message}");
        }
        catch (InvalidAgeException ex)
        {
            Console.WriteLine($"Обнаружена ошибка возраста: {ex.Message}");
        }

        Console.WriteLine("Работа завершена.");
    }
}
