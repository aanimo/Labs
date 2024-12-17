using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba8
{
    public partial class Form1 : Form, IStudentView
    {
        private StudentPresenter presenter;
        private BindingSource studentBindingSource = new BindingSource();

        private Dictionary<string, List<string>> departmentSpecializations = new Dictionary<string, List<string>>
    {
        { "Институт точных наук и информационных технологий", new List<string> { "Прикладная информатика", "Математика и компьютерные науки" } },
        { "Институт экономики", new List<string> { "Экономика", "Финансы и кредит" } },
        { "Институт гуманитарных наук", new List<string> { "Философия", "История", "Социология" } }
    };

        public event EventHandler<Student> StudentAdded;
        public event EventHandler<Student> StudentUpdated;
        public event EventHandler<string> StudentDeleted;

        public Form1()
        {
            InitializeComponent();
            InitializeDepartmentComboBox();
            presenter = new StudentPresenter(this);
            studentBindingSource.DataSource = new List<Student>();
            dataGridViewStudents.DataSource = studentBindingSource;
            dataGridViewStudents.SelectionChanged += DataGridViewStudents_SelectionChanged;
            presenter.LoadStudents();

            departmentComboBox.SelectedIndexChanged += DepartmentComboBox_SelectedIndexChanged;
        }

        private void DepartmentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (departmentComboBox.SelectedItem == null)
            {
                specificationComboBox.DataSource = null;
                return;
            }

            var selectedDepartment = departmentComboBox.SelectedItem.ToString();

            if (departmentSpecializations.ContainsKey(selectedDepartment))
            {
                specificationComboBox.DataSource = departmentSpecializations[selectedDepartment];
            }
            else
            {
                specificationComboBox.DataSource = null;
            }
        }


        private void InitializeDepartmentComboBox()
        {
            departmentComboBox.Items.Add("Институт точных наук и информационных технологий");
            departmentComboBox.Items.Add("Институт экономики");
            departmentComboBox.Items.Add("Институт гуманитарных наук");
        }


        private void DataGridViewStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewStudents.CurrentRow == null) return;

            var student = dataGridViewStudents.CurrentRow.DataBoundItem as Student;
            if (student == null) return;

            recordBookTextBox.Text = student.RecordBook;
            fullNameTextBox.Text = student.FullName;
            departmentComboBox.SelectedItem = student.Department;
            specificationComboBox.SelectedItem = student.Specification;
            groupTextBox.Text = student.Group;
            dateOfAdmissionPicker.Value = student.DateOfAdmission;
        }

        private void AddStudent(object sender, EventArgs e)
        {
            var newStudent = new Student
            {
                RecordBook = recordBookTextBox.Text,
                FullName = fullNameTextBox.Text,
                Department = departmentComboBox.SelectedItem.ToString(),
                Specification = specificationComboBox.SelectedItem.ToString(),
                DateOfAdmission = dateOfAdmissionPicker.Value,
                Group = groupTextBox.Text
            };

            StudentAdded?.Invoke(this, newStudent);
            ClearInputFields();
        }

        private void UpdateStudent(object sender, EventArgs e)
        {
            var updatedStudent = new Student
            {
                RecordBook = recordBookTextBox.Text,
                FullName = fullNameTextBox.Text,
                Department = departmentComboBox.SelectedItem.ToString(),
                Specification = specificationComboBox.SelectedItem.ToString(),
                DateOfAdmission = dateOfAdmissionPicker.Value,
                Group = groupTextBox.Text
            };

            StudentUpdated?.Invoke(this, updatedStudent);
        }

        private void DeleteStudent(object sender, EventArgs e)
        {
            var recordBook = recordBookTextBox.Text;
            StudentDeleted?.Invoke(this, recordBook);
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            recordBookTextBox.Clear();
            fullNameTextBox.Clear();
            departmentComboBox.SelectedIndex = -1;
            specificationComboBox.SelectedIndex = -1;
            groupTextBox.Clear();
            dateOfAdmissionPicker.Value = DateTime.Now;
        }

        public void DisplayStudents(List<Student> students)
        {
            if (students == null) students = new List<Student>();
            studentBindingSource.DataSource = students;
            studentBindingSource.ResetBindings(false);
        }

    }
}
