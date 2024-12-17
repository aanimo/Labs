using System;

class Student
{
    private string _name;
    public int Age { get; set; }
    public Subject FavoriteSubject { get; set; }
    private Game FavoriteGame;

    public Student(string name, int Age, Subject favoriteSubject, Game favoriteGame)
    {
        this._name = name;
        this.Age = Age;
        this.FavoriteSubject = favoriteSubject;
        this.FavoriteGame = favoriteGame;
    }

    public Student(string name, Subject favoriteSubject, Game favoriteGame)
    {
        this._name = name;
        Age = 0;
        this.FavoriteSubject = favoriteSubject;
        this.FavoriteGame = favoriteGame;
    }

    public string WriteInfo()
    {
        return "Студент " + _name + ", возраст " + Age;
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

    public string GetFavorites()
    {
        return $"Любимый предмет: {FavoriteSubject.Name}, любимая игра: {FavoriteGame.GetName()}";
    }

    public static void ChangeStudentName(ref Student student, string newName)
    {
        student.SetName(newName);
    }

    public static void ChangeAgeByValue(int age)
    {
        age += 1;
    }

    static void Main(string[] args)
    {
        Subject math = new("Математика");
        Game football = new("Футбол");

        Student Oleg = new("Олег", 25, math, football);
        Student Venya = new("Веня", math, football);

        Console.WriteLine(Oleg.WriteInfo());
        Oleg.BecomeOlder();

        Console.WriteLine(Venya.WriteInfo());
        Oleg.SetName("Рома");

        Console.WriteLine(Oleg.WriteInfo());

        Console.WriteLine("Имя студента: " + Oleg.GetName());

        Console.WriteLine(Oleg.GetFavorites());

        int age = Oleg.Age;
        ChangeAgeByValue(age);
        Console.WriteLine($"Возраст после попытки изменения по значению: {Oleg.Age}");

        ChangeStudentName(ref Oleg, "Андрей");
        Console.WriteLine($"Имя после изменения по ссылке: {Oleg.GetName()}");
    }
}

class Subject
{
    public string Name { get; set; }

    public Subject(string name)
    {
        Name = name;
    }
}

class Game
{
    private string name;

    public Game(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return name;
    }
}
