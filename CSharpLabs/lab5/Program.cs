using System;

interface IPerson
{
    string Name { get; set; }
    void DisplayInfo();
}

interface ISpecialist : IPerson
{
    string Specialty { get; set; }
    void ShowSpecialization();
}

class Subject
{
    public string SubjectName { get; set; }
    public Subject(string subjectName)
    {
        SubjectName = subjectName;
    }

    public override string ToString()
    {
        return SubjectName;
    }
}

class Student : IPerson, ICloneable, IComparable<Student>
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Age { get; set; }
    public Subject FavoriteSubject { get; set; }

    public Student(string name, int age, Subject favoriteSubject)
    {
        this._name = name;
        this.Age = age;
        this.FavoriteSubject = favoriteSubject;
    }

    public Student(string name, Subject favoriteSubject)
    {
        this._name = name;
        Age = 0;
        this.FavoriteSubject = favoriteSubject;
    }

    public virtual string WriteInfo()
    {
        return $"Студент {Name}, возраст {Age}, любимый предмет: {FavoriteSubject}";
    }

    public void DisplayInfo()
    {
        Console.WriteLine(WriteInfo());
    }

    public override string ToString()
    {
        return $"Студент: {Name}, возраст: {Age}, любимый предмет: {FavoriteSubject}";
    }

    public void BecomeOlder()
    {
        Age++;
        Console.WriteLine($"Студент {Name}, возраст {Age}");
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
        return $"Детали: Студент {Name}, возраст {Age}, любимый предмет: {FavoriteSubject}";
    }

    public object Clone()
    {
        return new Student(this.Name, this.Age, new Subject(this.FavoriteSubject.SubjectName));
    }

    public int CompareTo(Student other)
    {
        if (other == null) return 1;
        return this.Age.CompareTo(other.Age);
    }
}

class ITStudent : Student, ISpecialist
{
    public string FavoriteLanguage { get; set; }
    public string Specialty { get; set; }

    public ITStudent(string name, int age, string favoriteLanguage, Subject favoriteSubject, string specialty)
        : base(name, age, favoriteSubject)
    {
        this.FavoriteLanguage = favoriteLanguage;
        this.Specialty = specialty;
    }

    public override string WriteInfo()
    {
        return $"IT-Студент {Name}, возраст {Age}, любимый язык программирования: {FavoriteLanguage}, любимый предмет: {FavoriteSubject}, специальность: {Specialty}";
    }

    void ISpecialist.ShowSpecialization()
    {
        Console.WriteLine($"Специальность: {Specialty}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Subject math = new("Математика");
        Subject programming = new("Программирование");

        Student Oleg = new("Олег", 25, math);
        ITStudent Marina = new("Марина", 22, "C#", programming, "Software Engineering");

        Console.WriteLine(Oleg.WriteInfo());
        Oleg.BecomeOlder();

        Console.WriteLine(Marina.WriteInfo());
        Marina.BecomeOlder();

        Console.WriteLine(Oleg.ToString());
        Console.WriteLine(Oleg.ToString(true));

        Console.WriteLine(Marina.ToString());

        Oleg.DisplayInfo();
        Marina.DisplayInfo();

        ISpecialist specialist = Marina;
        specialist.ShowSpecialization();

        Student clonedOleg = (Student)Oleg.Clone();
        Console.WriteLine($"Cloned: {clonedOleg.WriteInfo()}");

        Console.WriteLine($"Comparison: {Oleg.CompareTo(Marina)}");
    }
}
