using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Text.Json;
using System.Xml.Serialization;
using CsvHelper;
using System.Globalization;

namespace lab9
{
    public partial class Form1 : Form
    {
        private List<Student> students = new List<Student>();
        private int selectedIndex = -1;
        public class Student
        {
            public string FullName { get; set; }
            public string StudentId { get; set; }
            public string FieldOfStudy { get; set; }
        }

        private SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "JSON files (*.json)|*.json|XML files (*.xml)|*.xml|CSV files (*.csv)|*.csv",
            Title = "Сохранить файл"
        };

        private OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "JSON files (*.json)|*.json|XML files (*.xml)|*.xml|CSV files (*.csv)|*.csv",
            Title = "Открыть файл"
        };

        public Form1()
        {
            InitializeComponent();
            ConfigureListView();
            ConfigureComboBox();
            ConfigureFileDialogs();
        }

        private void ConfigureListView()
        {
            listViewStudents.View = View.Details;
            listViewStudents.Columns.Add("ФИО", 200);
            listViewStudents.Columns.Add("Номер студенческого", 150);
            listViewStudents.Columns.Add("Направление", 150);
            listViewStudents.FullRowSelect = true;

            listViewStudents.SelectedIndexChanged += ListViewStudents_SelectedIndexChanged;
        }

        private void ConfigureComboBox()
        {
            comboBoxFormat.Items.Add("JSON");
            comboBoxFormat.Items.Add("XML");
            comboBoxFormat.Items.Add("CSV");
            comboBoxFormat.SelectedIndex = 0;
            comboBoxSpecification.Items.Add("Информатика");
            comboBoxSpecification.Items.Add("Математика");
            comboBoxSpecification.Items.Add("Физика");
            comboBoxSpecification.SelectedIndex = 0;
        }

        private void ConfigureFileDialogs()
        {
            string format = comboBoxFormat.SelectedItem.ToString();

            switch (format)
            {
                case "JSON":
                    saveFileDialog.Filter = "JSON files (*.json)|*.json";
                    openFileDialog.Filter = "JSON files (*.json)|*.json";
                    break;
                case "XML":
                    saveFileDialog.Filter = "XML files (*.xml)|*.xml";
                    openFileDialog.Filter = "XML files (*.xml)|*.xml";
                    break;
                case "CSV":
                    saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                    openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                    break;
                default:
                    saveFileDialog.Filter = "All files (*.*)|*.*";
                    openFileDialog.Filter = "All files (*.*)|*.*";
                    break;
            }
        }

        private void comboBoxFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigureFileDialogs();
        }


        private void AddStudent(string fullName, string studentId, string fieldOfStudy)
        {
            var student = new Student
            {
                FullName = fullName,
                StudentId = studentId,
                FieldOfStudy = fieldOfStudy
            };

            students.Add(student);
            UpdateListView();
        }

        private void UpdateListView()
        {
            listViewStudents.Items.Clear();
            foreach (var student in students)
            {
                var item = new ListViewItem(student.FullName);
                item.SubItems.Add(student.StudentId);
                item.SubItems.Add(student.FieldOfStudy);
                listViewStudents.Items.Add(item);
            }
        }

        private void ListViewStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewStudents.SelectedItems.Count > 0)
            {
                selectedIndex = listViewStudents.SelectedItems[0].Index;
                var student = students[selectedIndex];

                textBoxFullName.Text = student.FullName;
                textBoxStudentBook.Text = student.StudentId;
                comboBoxSpecification.SelectedItem = student.FieldOfStudy;
            }
            else
            {
                selectedIndex = -1;
                textBoxFullName.Clear();
                textBoxStudentBook.Clear();
                comboBoxSpecification.SelectedIndex = 0;
            }
        }


        private void UpdateStudent(int index, string fullName, string studentId, string fieldOfStudy)
        {
            if (index >= 0 && index < students.Count)
            {
                students[index].FullName = fullName;
                students[index].StudentId = studentId;
                students[index].FieldOfStudy = fieldOfStudy;
                UpdateListView();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string fullName = textBoxFullName.Text.Trim();
            string studentId = textBoxStudentBook.Text.Trim();
            string fieldOfStudy = comboBoxSpecification.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrEmpty(studentId))
            {
                AddStudent(fullName, studentId, fieldOfStudy);

                textBoxFullName.Clear();
                textBoxStudentBook.Clear();
                comboBoxSpecification.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0 && selectedIndex < students.Count)
            {
                string fullName = textBoxFullName.Text.Trim();
                string studentId = textBoxStudentBook.Text.Trim();
                string fieldOfStudy = comboBoxSpecification.SelectedItem.ToString();

                if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrEmpty(studentId))
                {
                    UpdateStudent(selectedIndex, fullName, studentId, fieldOfStudy);

                    selectedIndex = -1;
                    textBoxFullName.Clear();
                    textBoxStudentBook.Clear();
                    comboBoxSpecification.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для обновления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewStudents.SelectedItems.Count > 0)
            {
                int index = listViewStudents.SelectedItems[0].Index;
                students.RemoveAt(index);
                UpdateListView();

                selectedIndex = -1;
                textBoxFullName.Clear();
                textBoxStudentBook.Clear();
                comboBoxSpecification.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveToFile()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                string format = comboBoxFormat.SelectedItem.ToString();

                switch (format)
                {
                    case "JSON":
                        SaveToJson(filePath);
                        break;
                    case "XML":
                        SaveToXml(filePath);
                        break;
                    case "CSV":
                        SaveToCsv(filePath);
                        break;
                    default:
                        MessageBox.Show("Неизвестный формат файла.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void LoadFromFile()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string format = comboBoxFormat.SelectedItem.ToString();

                switch (format)
                {
                    case "JSON":
                        LoadFromJson(filePath);
                        break;
                    case "XML":
                        LoadFromXml(filePath);
                        break;
                    case "CSV":
                        LoadFromCsv(filePath);
                        break;
                    default:
                        MessageBox.Show("Неизвестный формат файла.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void SaveToJson(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                var json = JsonSerializer.Serialize(students);
                writer.Write(json);
            }
        }

        private void LoadFromJson(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                var json = reader.ReadToEnd();
                students = JsonSerializer.Deserialize<List<Student>>(json);
                UpdateListView();
            }
        }

        private void SaveToXml(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                var serializer = new XmlSerializer(typeof(List<Student>));
                serializer.Serialize(writer, students);
            }
        }

        private void LoadFromXml(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                var serializer = new XmlSerializer(typeof(List<Student>));
                students = (List<Student>)serializer.Deserialize(reader);
                UpdateListView();
            }
        }

        private void SaveToCsv(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(students);
            }
        }

        private void LoadFromCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                students = new List<Student>(csv.GetRecords<Student>());
                UpdateListView();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveToFile();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadFromFile();
        }
    }
}
