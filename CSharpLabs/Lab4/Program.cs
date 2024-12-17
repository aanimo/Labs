using System;
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

    public override string WriteInfo()
    {
        return $"IT-Студент {Name}, возраст {Age}, любимый язык программирования: {FavoriteLanguage}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Student Oleg = new("Олег", 25);
        ITStudent Marina = new("Марина", 22, "C#");

        Console.WriteLine(Oleg.WriteInfo());
        Oleg.BecomeOlder();

        Console.WriteLine(Marina.WriteInfo());
        Marina.BecomeOlder();

        Console.WriteLine(Oleg.ToString());
        Console.WriteLine(Oleg.ToString(true));

        Console.WriteLine(Marina.ToString());

        Oleg.DisplayInfo();
        Marina.DisplayInfo();
    }
}
