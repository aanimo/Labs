using laba8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public class StudentPresenter
{
    private readonly IStudentView _view;
    private readonly List<Student> _students;
    private readonly StudentModel _studentModel;

    public StudentPresenter(IStudentView view)
    {
        _view = view;
        _students = new List<Student>();
        _studentModel = new StudentModel();
        _view.StudentAdded += OnStudentAdded;
        _view.StudentUpdated += OnStudentUpdated;
        _view.StudentDeleted += OnStudentDeleted;
    }

    public void LoadStudents()
    {
        _students.Clear();
        _students.AddRange(_studentModel.GetAllStudents());
        _view.DisplayStudents(_students);
    }

    private void OnStudentAdded(object sender, Student student)
    {
        if (_students.Any(s => s.RecordBook == student.RecordBook))
        {
            MessageBox.Show("Студент с таким номером зачетной книжки уже существует!");
            return;
        }

        _studentModel.AddStudent(student);
        _students.Add(student);
        _view.DisplayStudents(_students);
    }



    private void OnStudentUpdated(object sender, Student student)
    {
        _studentModel.UpdateStudent(student);
        var existingStudent = _students.FirstOrDefault(s => s.RecordBook == student.RecordBook);
        if (existingStudent != null)
        {
            existingStudent.FullName = student.FullName;
            existingStudent.Department = student.Department;
            existingStudent.Specification = student.Specification;
            existingStudent.DateOfAdmission = student.DateOfAdmission;
            existingStudent.Group = student.Group;
        }
        _view.DisplayStudents(_students);
    }

    private void OnStudentDeleted(object sender, string recordBook)
    {
        _studentModel.DeleteStudent(recordBook);
        var studentToDelete = _students.FirstOrDefault(s => s.RecordBook == recordBook);
        if (studentToDelete != null)
        {
            _students.Remove(studentToDelete);
        }
        _view.DisplayStudents(_students);
    }
}