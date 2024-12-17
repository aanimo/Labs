using laba8;
using System;
using System.Collections.Generic;

public interface IStudentView
{

    event EventHandler<Student> StudentAdded;
    event EventHandler<Student> StudentUpdated;
    event EventHandler<string> StudentDeleted;

    void DisplayStudents(List<Student> students);
}