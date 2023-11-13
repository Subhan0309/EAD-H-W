using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

Console.WriteLine("Hello, World!");
using (var context = new MyContext())
{
    // Create and save new Students
    Console.WriteLine("Adding new students");

    var student = new Student
    {
        FirstName = "Subhan",
        LastName = "Sajjad",
        EmailAddress = "subhanSajjad@pucit.com",
        PhoneNumber = "123-456-7890",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString()),
        BirthDate = new DateTime(1990, 5, 15), 
        Address = "123 Main St, Cityville"    
    };

    context.Students.Add(student);

    var student1 = new Student
    {
        FirstName = "Amna",
        LastName = "Mazhar",
        EmailAddress = "amna@pucit.com",
        PhoneNumber = "789-123-56789",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString()),
        BirthDate = new DateTime(1992, 8, 21),
        Address = "456 Oak St, Townsville"    
    };

    context.Students.Add(student1);
    context.SaveChanges();

    // Display all Students from the database
    var students = (from s in context.Students
                    orderby s.FirstName
                    select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in students)
    {
        string name = stdnt.FirstName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}, BirthDate: {2}, Address: {3}",
            stdnt.ID, name, stdnt.BirthDate, stdnt.Address);
    }

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();



    Console.WriteLine("Updating student information");
    var studentToUpdate = context.Students.FirstOrDefault(s => s.ID == 15);

    if (studentToUpdate != null)
    {
        Console.WriteLine("Updating student information");

        studentToUpdate.EmailAddress = "sajjadsubhan@grifith.com";
        studentToUpdate.PhoneNumber = "9557-654-3210";
        context.SaveChanges();
    }
    // Display all Students from the database after updating
    var studentss = (from s in context.Students
                     orderby s.FirstName
                     select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in studentss)
    {
        string name = stdnt.FirstName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}, BirthDate: {2}, Address: {3}",
           stdnt.ID, name, stdnt.BirthDate, stdnt.Address);
    }

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();


    Console.WriteLine("Deleting a student");
    var studentToDelete = context.Students.FirstOrDefault(s => s.ID == 33);

    if (studentToDelete != null)
    {
        context.Students.Remove(studentToDelete);
        context.SaveChanges();

    }

    // Display all Students from the database after deleting
    var studentsss = (from s in context.Students
                      orderby s.FirstName
                      select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in studentsss)
    {
        string name = stdnt.FirstName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}, Email:{2}, PhoneNO:{3}", stdnt.ID, name, stdnt.EmailAddress, stdnt.PhoneNumber);
    }
}



public enum Grade
{
    A, B, C, D, F
}

public class Enrollment
{
    public int EnrollmentID { get; set; }
    public int CourseID { get; set; }
    public int StudentID { get; set; }
    public Grade? Grade { get; set; }

    public virtual Course? Course { get; set; }
    public virtual Student? Student { get; set; }
}

public class Student
{
    public int ID { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Address { get; set; }

    public virtual ICollection<Enrollment>? Enrollments { get; set; }
}

public class Course
{
    public int CourseID { get; set; }
    public string? Title { get; set; }
    public int Credits { get; set; }

    public virtual ICollection<Enrollment>? Enrollments { get; set; }
}

public class MyContext : DbContext
{
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Enrollment> Enrollments { get; set; }
    public virtual DbSet<Student> Students { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    // Configure your database connection here
    //    optionsBuilder.UseSqlServer("your_connection_string");
    //}
}
