using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace lab7
{
    public partial class Form1 : Form
    {
        private List<Student> students = new List<Student>();
        private BindingSource studentBindingSource = new BindingSource();

        private Dictionary<string, List<string>> specifications = new Dictionary<string, List<string>>()
        {
            { "Институт точных наук и информационных технологий", new List<string> { "Прикладная информатика", "Математика и компьютерные науки" } },
            { "Институт экономики и управления", new List<string> { "Менеджмент", "Туризм" } }
        };

        public Form1()
        {
            InitializeComponent();
            InitializeComboBoxes();
            studentBindingSource.DataSource = students;
            dataGridViewStudents.DataSource = studentBindingSource;

            dataGridViewStudents.SelectionChanged += DataGridViewStudents_SelectionChanged;
        }

        private void InitializeComboBoxes()
        {
            departmentComboBox.Items.AddRange(specifications.Keys.ToArray());
            departmentComboBox.SelectedIndexChanged += DepartmentComboBox_SelectedIndexChanged;

            if (departmentComboBox.Items.Count > 0)
                departmentComboBox.SelectedIndex = 0;
        }

        private void DepartmentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            specificationComboBox.Items.Clear();

            if (departmentComboBox.SelectedItem != null)
            {
                string selectedDepartment = departmentComboBox.SelectedItem.ToString();

                if (specifications.TryGetValue(selectedDepartment, out List<string> specList))
                {
                    specificationComboBox.Items.AddRange(specList.ToArray());
                }

                specificationComboBox.SelectedIndex = -1;
            }
        }

        private void DataGridViewStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewStudents.CurrentRow == null || dataGridViewStudents.CurrentRow.DataBoundItem == null)
            {
                recordBookTextBox.Clear();
                fullNameTextBox.Clear();
                departmentComboBox.SelectedIndex = -1;
                specificationComboBox.Items.Clear();
                groupTextBox.Clear();
                dateOfAdmissionPicker.Value = DateTime.Now;
                return;
            }

            var student = (Student)dataGridViewStudents.CurrentRow.DataBoundItem;

            recordBookTextBox.Text = student.RecordBook;
            fullNameTextBox.Text = student.FullName;
            departmentComboBox.SelectedItem = student.Department;
            groupTextBox.Text = student.Group;
            dateOfAdmissionPicker.Value = student.DateOfAdmission;

            if (student.Department != null && specifications.ContainsKey(student.Department))
            {
                specificationComboBox.Items.Clear();
                specificationComboBox.Items.AddRange(specifications[student.Department].ToArray());
                specificationComboBox.SelectedItem = student.Specification;
            }
        }



        private void AddStudent(object sender, EventArgs e)
        {
            if (students.Any(s => s.RecordBook == recordBookTextBox.Text))
            {
                MessageBox.Show("Студент с таким номером зачетки уже существует!");
                return;
            }

            Student newStudent = new Student
            {
                RecordBook = recordBookTextBox.Text,
                FullName = fullNameTextBox.Text,
                Department = departmentComboBox.SelectedItem?.ToString(),
                Specification = specificationComboBox.SelectedItem?.ToString(),
                DateOfAdmission = dateOfAdmissionPicker.Value,
                Group = groupTextBox.Text
            };

            students.Add(newStudent);
            studentBindingSource.ResetBindings(false);

            ClearFields();
        }

        private void ClearFields()
        {
            recordBookTextBox.Clear();
            fullNameTextBox.Clear();
            departmentComboBox.SelectedIndex = -1;
            specificationComboBox.Items.Clear();
            groupTextBox.Clear();
            dateOfAdmissionPicker.Value = DateTime.Now;
        }


        private void UpdateStudent(object sender, EventArgs e)
        {
            if (dataGridViewStudents.CurrentRow == null) return;

            var student = (Student)dataGridViewStudents.CurrentRow.DataBoundItem;

            if (recordBookTextBox.Text != student.RecordBook &&
                students.Any(s => s.RecordBook == recordBookTextBox.Text))
            {
                MessageBox.Show("Студент с таким номером зачетки уже существует!");
                return;
            }

            student.RecordBook = recordBookTextBox.Text;
            student.FullName = fullNameTextBox.Text;
            student.Department = departmentComboBox.SelectedItem?.ToString();
            student.Specification = specificationComboBox.SelectedItem?.ToString();
            student.DateOfAdmission = dateOfAdmissionPicker.Value;
            student.Group = groupTextBox.Text;

            studentBindingSource.ResetBindings(false);
        }

        private void DeleteStudent(object sender, EventArgs e)
        {
            if (dataGridViewStudents.CurrentRow == null) return;

            var student = (Student)dataGridViewStudents.CurrentRow.DataBoundItem;
            students.Remove(student);

            studentBindingSource.ResetBindings(false);
        }
    }

    public class Student
    {
        public string RecordBook { get; set; }  // Номер зачетки
        public string FullName { get; set; }    // ФИО студента
        public string Department { get; set; }  // Институт
        public string Specification { get; set; } // Направление
        public DateTime DateOfAdmission { get; set; }  // Дата зачисления
        public string Group { get; set; }  // Группа
    }
}
