using System;

// Базовый абстрактный класс Person
abstract class Person
{
    public abstract string Name { get; set; } // Абстрактное поле

    // Абстрактный метод
    public abstract void DisplayInfo();
}

class Student : Person
{
    private string _name;
    public override string Name
    {
        get { return _name; }
        set { _name = value; }
    }

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

    public virtual string WriteInfo()
    {
        return $"Студент {Name}, возраст {Age}";
    }

    // Переопределение абстрактного метода DisplayInfo
    public override void DisplayInfo()
    {
        Console.WriteLine(WriteInfo());
    }

    // Переопределение метода ToString() класса Object
    public override string ToString()
    {
        return $"Студент: {Name}, возраст: {Age}";
    }

    // Метод BecomeOlder увеличивает возраст на 1
    public void BecomeOlder()
    {
        Age++;
        Console.WriteLine("Студент " + Name + ", возраст " + Age);
    }

    public string GetName()
    {
        return Name;
    }

    public void SetName(string name)
    {
        this.Name = name;
    }

    // Демонстрация скрытия метода
    public new string ToString(bool detailed)
    {
        return $"Детали: Студент {Name}, возраст {Age}";
    }
}

class ITStudent : Student
{
    public string FavoriteLanguage { get; set; }

    public ITStudent(string name, int age, string favoriteLanguage)
        : base(name, age)
    {
        this.FavoriteLanguage = favoriteLanguage;
    }

    // Переопределение метода WriteInfo
    public override string WriteInfo()
    {
        return $"IT-Студент {Name}, возраст {Age}, любимый язык программирования: {FavoriteLanguage}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаём объекты классов Student и ITStudent
        Student Oleg = new("Олег", 25);
        ITStudent Marina = new("Марина", 22, "C#");

        // Демонстрация работы WriteInfo и переопределения
        Console.WriteLine(Oleg.WriteInfo());
        Oleg.BecomeOlder();

        Console.WriteLine(Marina.WriteInfo());
        Marina.BecomeOlder();

        // Демонстрация работы метода ToString() и его скрытия
        Console.WriteLine(Oleg.ToString());
        Console.WriteLine(Oleg.ToString(true)); // Вызов скрытого метода

        // Демонстрация работы переопределённого метода ToString()
        Console.WriteLine(Marina.ToString());

        // Демонстрация работы абстрактного метода через базовый класс
        Oleg.DisplayInfo();
        Marina.DisplayInfo();
    }
}
