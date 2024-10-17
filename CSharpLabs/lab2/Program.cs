using System;

class Student
{
    private string _name;
    public int Age { get; set; }
    public Subject FavoriteSubject { get; set; } // Поле public
    private Game FavoriteGame; // Поле private

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

    // Метод для получения любимых предмета и игры
    public string GetFavorites()
    {
        return $"Любимый предмет: {FavoriteSubject.Name}, любимая игра: {FavoriteGame.GetName()}";
    }

    // Метод для демонстрации передачи параметра по ссылке
    public static void ChangeStudentName(ref Student student, string newName)
    {
        student.SetName(newName);
    }

    // Метод для демонстрации передачи параметра по значению
    public static void ChangeAgeByValue(int age)
    {
        age += 1; // Изменение произойдет только в локальной копии
    }

    static void Main(string[] args)
    {
        // Создаем объекты любимого предмета и игры
        Subject math = new("Математика");
        Game football = new("Футбол");

        // Создаем студентов
        Student Oleg = new("Олег", 25, math, football);
        Student Venya = new("Веня", math, football);

        // Демонстрация работы WriteInfo и BecomeOlder
        Console.WriteLine(Oleg.WriteInfo());
        Oleg.BecomeOlder();

        Console.WriteLine(Venya.WriteInfo());
        Oleg.SetName("Рома");

        // Повторный вызов после изменения имени
        Console.WriteLine(Oleg.WriteInfo());

        // Демонстрация работы геттера GetName
        Console.WriteLine("Имя студента: " + Oleg.GetName());

        // Демонстрация получения любимых предмета и игры
        Console.WriteLine(Oleg.GetFavorites());

        // Демонстрация передачи параметра по значению
        int age = Oleg.Age;
        ChangeAgeByValue(age);
        Console.WriteLine($"Возраст после попытки изменения по значению: {Oleg.Age}");

        // Демонстрация передачи параметра по ссылке
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
    private string name; // Поле private

    public Game(string name)
    {
        this.name = name;
    }

    // Метод для получения имени игры
    public string GetName()
    {
        return name;
    }
}
