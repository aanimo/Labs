using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using lab8;


public class StudentModel
{
    private string connectionString = "Server=localhost;Database=StudentDB;User ID=root;Password=bahek;";

    public List<Student> GetAllStudents()
    {
        List<Student> students = new List<Student>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string query = "SELECT * FROM Student";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                RecordBook = reader["RecordBook"].ToString(),
                                FullName = reader["FullName"].ToString(),
                                Department = reader["Department"].ToString(),
                                Specification = reader["Specification"].ToString(),
                                DateOfAdmission = Convert.ToDateTime(reader["DateOfAdmission"]),
                                Group = reader["Group"].ToString()
                            });
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось установить соединение с базой данных.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message);
            }
        }

        return students;
    }

    public void AddStudent(Student student)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Student (RecordBook, FullName, Department, Specification, DateOfAdmission, `Group`) " +
                               "VALUES (@RecordBook, @FullName, @Department, @Specification, @DateOfAdmission, @Group)";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add("@RecordBook", MySqlDbType.VarChar).Value = student.RecordBook;
                command.Parameters.Add("@FullName", MySqlDbType.VarChar).Value = student.FullName;
                command.Parameters.Add("@Department", MySqlDbType.VarChar).Value = student.Department;
                command.Parameters.Add("@Specification", MySqlDbType.VarChar).Value = student.Specification;
                command.Parameters.Add("@DateOfAdmission", MySqlDbType.Date).Value = student.DateOfAdmission;
                command.Parameters.Add("@Group", MySqlDbType.VarChar).Value = student.Group;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062)
                    {
                        MessageBox.Show("Студент с таким RecordBook уже существует!");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении студента: " + ex.Message);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при подключении к базе данных: " + ex.Message);
        }
    }

    public void UpdateStudent(Student student)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Student SET FullName = @FullName, Department = @Department, Specification = @Specification, " +
                               "DateOfAdmission = @DateOfAdmission, `Group` = @Group WHERE RecordBook = @RecordBook";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.Add("@RecordBook", MySqlDbType.VarChar).Value = student.RecordBook;
                command.Parameters.Add("@FullName", MySqlDbType.VarChar).Value = student.FullName;
                command.Parameters.Add("@Department", MySqlDbType.VarChar).Value = student.Department;
                command.Parameters.Add("@Specification", MySqlDbType.VarChar).Value = student.Specification;
                command.Parameters.Add("@DateOfAdmission", MySqlDbType.Date).Value = student.DateOfAdmission;
                command.Parameters.Add("@Group", MySqlDbType.VarChar).Value = student.Group;

                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при обновлении студента: " + ex.Message);
        }
    }

    public void DeleteStudent(string recordBook)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Student WHERE RecordBook = @RecordBook";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@RecordBook", MySqlDbType.VarChar).Value = recordBook;

                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при удалении студента: " + ex.Message);
        }
    }
}
