using System;

class Student
{
    private string name;
    public int Age { get; set; }
    public Student(string name, int Age)
    {
        this.name = name;
        this.Age = Age;
    }

    public Student(string _name)
    {
        this.name = _name;
        Age = 0;
    }

    public void WriteInfo()
    {
        Console.WriteLine("Студент " + name + ", возраст " + Age);
    }
    public void BecomeOlder()
    {
        Console.WriteLine("Студент " + name + ", возраст " + (Age + 1));
    }

    public string GetName()
    {
        return name;
    }
    public void SetName(string _name)
    {
        this.name = _name;
    }


    static void Main(string[] args)
    {
        Student Oleg = new("Олег", 25);
        Student Venya = new("Веня");

        Oleg.WriteInfo();
        Oleg.BecomeOlder();

        Venya.WriteInfo();
        Oleg.SetName("Рома");
        Oleg.BecomeOlder();

    }
}

