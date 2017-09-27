namespace StudentSystem.Client
{
    using StudentSystem.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System;

    public class Program
    {
        static void Main()
        {
            using (var db = new StudentDbContext())
            {
                db.Database.Migrate();
                //var seedDatabase = new SeedDatabase();
                //seedDatabase.Seed(db);
                //ListStudentsHomework(db);
                //ListCoursesAndResourses(db);
                //ListAllCoursesWithMoreThan5Resourses(db);
                //ListCoursesForTask4(db);
                //ListStudentsForTask5(db);
                //ListCoursesWithResources31(db);
                ListStudentCountCourses32(db);
            }
        }

        private static void ListStudentCountCourses32(StudentDbContext db)
        {
            var studentData = db.Students
                .Where(s => s.Courses.Any())
                .Select(s => new
                {
                    s.Name,
                    CoursesCountEnroled = s.Courses.Count,
                    TotalNumberOfresources = s.Courses.Sum(c => c.Course.Resources.Count),
                    TotalNumberOfLicenses = s.Courses.Sum(c => c.Course.Resources.Sum(r => r.Licenses.Count))
                })
                 .OrderByDescending(s => s.CoursesCountEnroled)
                 .ThenByDescending(s => s.TotalNumberOfresources)
                 .ThenBy(s => s.Name);

            foreach (var student in studentData)
            {
                Console.WriteLine(new string('-', 20));
                Console.WriteLine($"Student name: {student.Name}");
                Console.WriteLine($"Number of courses: {student.CoursesCountEnroled}");
                Console.WriteLine($"Number of resources: {student.TotalNumberOfresources}");
                Console.WriteLine($"Number of licenses: {student.TotalNumberOfLicenses}");

            }
        }

        private static void ListCoursesWithResources31(StudentDbContext db)
        {
            var coursesData = db.Courses
                .Select(c => new
                {
                    c.Name,
                    ResourcesCount = c.Resources.Count,
                    Resources = c.Resources.Select(r => new
                    {
                        r.Name,
                        LicensesCount = r.Licenses.Count,
                        LicensesNames = r.Licenses.Select(l => new
                        {
                            l.Name
                        })
                    })
                    .OrderByDescending(r => r.LicensesCount)
                    .ThenBy(r => r.Name)
                }
                )
                .OrderByDescending(c => c.ResourcesCount)
                .ThenBy(c => c.Name);

            foreach (var course in coursesData)
            {
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine($"Course name: {course.Name}");
                foreach (var resource in course.Resources)
                {
                    Console.WriteLine("rrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr");
                    Console.WriteLine($"Resource name: {resource.Name}");
                    foreach (var license in resource.LicensesNames)
                    {
                        Console.WriteLine($"License name: {license.Name}");
                    }
                }
            }
        }

        private static void ListStudentsForTask5(StudentDbContext db)
        {
            var studentsData = db.Students
                .Select(s => new
                {
                    s.Name,
                    NumberOfCourses = s.Courses.Count,
                    TotalPrice = (s.Courses.Count == 0 ? 0 : s.Courses.Sum(c => c.Course.Price)),
                    AveragePrice = (s.Courses.Count == 0 ? 0 : s.Courses.Average(c => c.Course.Price))
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.NumberOfCourses)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var student in studentsData)
            {
                if (student.NumberOfCourses > 0)
                {
                    Console.WriteLine("-------------------------");
                    Console.WriteLine($"Name:               | {student.Name}");
                    Console.WriteLine($"Number of courses:  | {student.NumberOfCourses}");
                    Console.WriteLine($"Total price:        | {student.TotalPrice:f2}");
                    Console.WriteLine($"Average price       | {student.AveragePrice:f2}");

                }
                else
                {
                    Console.WriteLine("-------------------------");
                    Console.WriteLine($"Name:               | {student.Name}");
                    Console.WriteLine($"Number of courses:  | {0}");
                    Console.WriteLine($"Total price:        | {0}");
                    Console.WriteLine($"Average price       | {0}");

                }
            }
        }

        private static void ListCoursesForTask4(StudentDbContext db)
        {
            var firstStartingDate = db
                .Courses
                .OrderBy(c => c.StartDate)
                .Select(c => c.StartDate)
                .FirstOrDefault();

            var lastStartingDate = db
                    .Courses
                    .OrderByDescending(c => c.StartDate)
                    .Select(c => c.StartDate)
                    .FirstOrDefault();

            var diferenceInDays = lastStartingDate.Date.Subtract(firstStartingDate.Date).Days;


            var calculatedDate = firstStartingDate.AddDays((diferenceInDays / 3)*2);

            var activeCourses = db.Courses
                .Where(c => c.StartDate <= calculatedDate && c.EndDate > calculatedDate)
                .Select(c => new
                {
                    c.Name,
                    Count = c.Students.Count,
                    c.StartDate,
                    c.EndDate,
                    Duration = c.EndDate.Date.Subtract(c.StartDate.Date).Days
                }
                )
                .OrderByDescending(c => c.Count)
                .ThenByDescending(c => c.Duration)
                .ToList();
            foreach (var course in activeCourses)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(course.Name);
                Console.WriteLine($"Start date: {course.StartDate.Date.ToShortDateString()}");
                Console.WriteLine($"End date: {course.EndDate.Date.ToShortDateString()}");
                Console.WriteLine($"Duration: {course.Duration}");
                Console.WriteLine($"Students enroled: {course.Count}");
            }
        }

        private static void ListAllCoursesWithMoreThan5Resourses(StudentDbContext db)
        {
            var selectedCourses = db.Courses
                .Where(c => c.Resources.Count > 5)
                .OrderByDescending(r => r.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new
                {
                    c.Name,
                    c.Resources.Count
                }
                );
            foreach (var course in selectedCourses)
            {
                Console.WriteLine(course.Name);
                Console.WriteLine($"Resource count: {course.Count}");
            }
        }

        private static void ListCoursesAndResourses(StudentDbContext db)
        {
            var coursesAndResourses = db.Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    Resourses = c.Resources
                        .Select(r => new
                        {
                            r.Name,
                            r.ResourceType,
                            Url = r.Url
                        })
                });
            foreach (var course in coursesAndResourses)
            {
                Console.WriteLine(course.Name);
                Console.WriteLine($"--{course.Description}");
                foreach (var resource in course.Resourses)
                {
                    Console.WriteLine($"---{resource.Name}");
                    Console.WriteLine($"-----{resource.ResourceType.ToString()}");
                    Console.WriteLine($"-------{resource.Url}");
                }
            }
        }

        private static void ListStudentsHomework(StudentDbContext db)
        {
            var studentsHomework = db.Students
                .Select(s => new
                {
                    s.Name,
                    Homeworks = s.Homeworks
                    .Select(h => new
                    {
                        h.Content,
                        h.ContentType
                    })
                }
                );
            foreach (var student in studentsHomework)
            {
                System.Console.WriteLine(student.Name);
                foreach (var homework in student.Homeworks)
                {
                    Console.WriteLine($"--{homework.Content}");
                    Console.WriteLine($"---{homework.ContentType.ToString()}");
                }
            }
        }
    }
}
