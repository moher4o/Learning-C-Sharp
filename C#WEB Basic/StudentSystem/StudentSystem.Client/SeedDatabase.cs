using StudentSystem.Data;
using StudentSystem.Models.Models;
using System;
using System.Linq;

namespace StudentSystem.Client
{
   public class SeedDatabase
    {
        private static Random random = new Random();
        private DateTime currentDate = DateTime.Now;
        public void Seed(StudentDbContext db)
        {
            
            Console.Write("Adding data in database");
            SeedStudents(db);
            Console.Write(".");
            SeedCourses(db);
            Console.Write(".");
            SeedLicenses(db);
            Console.WriteLine(".");
        }

        private void SeedLicenses(StudentDbContext db)
        {
            var resourcesIds = db.Resources.Select(r => r.Id).ToArray();
            for (int i = 1; i < 250; i++)
            {
                db.Add(new License()
                {
                    Name = $"License Name {i}",
                    ResourceId = resourcesIds[random.Next(0, resourcesIds.Length)]
                });
                db.SaveChanges();
            }
        }

        private void SeedCourses(StudentDbContext db)
        {
            var studentsIds = db.Students.Select(s => s.Id).ToList();
            for (int i = 1; i < 11; i++)
            {
                int randomNumber = random.Next(1, 10);
                var course = new Course() { Name = $"Course Name {i}", Description = $"Description for Course N:{i}", StartDate = currentDate.AddDays(-i * randomNumber), EndDate = currentDate.AddDays(80 - 2 * randomNumber), Price = i * randomNumber };

                db.Courses.Add(course);
                db.SaveChanges();

                AddStudentsToCourse(db, studentsIds, course);

                AddResources(db, course);


            }
        }

        private static void AddStudentsToCourse(StudentDbContext db, System.Collections.Generic.List<int> studentsIds, Course course)
        {
            for (int j = 1; j < random.Next(2, 12); j++)
            {
                var studentId = studentsIds[random.Next(0, studentsIds.Count)];

                if (course.Students.Any(s => s.StudentId == studentId))
                {
                    j--;
                }
                else
                {
                    course.Students.Add(new StudentCourse() { StudentId = studentId });
                    db.SaveChanges();
                    var homeworksTypes = new[] { 0, 1, 2 };
                    db.Homeworks.Add(new HomeWork()
                    {
                        Content = $"Content for the homework {j}",
                        ContentType = (ContentType)homeworksTypes[random.Next(0, homeworksTypes.Length)],
                        SubmissionDate = course.EndDate.AddDays(-1),
                        CourseId = course.Id,
                        StudentId = studentId
                    });
                }
                
            }
        }

        private static void AddResources(StudentDbContext db, Course course)
        {
            var resourcesInCourse = random.Next(2, 20);
            var types = new[] { 0, 1, 2, 999 };
            for (int j = 0; j < resourcesInCourse; j++)
            {
                course.Resources.Add(new Resource()
                {
                    Name = $"Resource Name {j}",
                    Url = $"Url {j}",
                    ResourceType = (ResourceType)types[random.Next(0, types.Length)]
                });
                db.SaveChanges();
            }
        }

        private void SeedStudents(StudentDbContext db)
        {
            for (int i = 1; i < 26; i++)
            {
                int randomNumber = random.Next(1, i);
                db.Students.Add(new Student() { Name = $"Student Name {i}", PhoneNumber = $"+3598882{random.Next(0, 9)}456{random.Next(0, 9)}", BirthDate = currentDate.AddYears(-20).AddDays(randomNumber*20), RegistrationDate = currentDate.AddMonths(randomNumber) });

                db.SaveChanges();
            }
        }
    }
}
