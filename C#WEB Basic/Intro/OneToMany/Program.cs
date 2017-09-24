using System;
using System.Linq;

namespace OneToMany
{
    public class Program
    {
        static void Main()
        {
            using (var db = new MyDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Departments.Add(new Models.Department { Name = "Developers" });
                db.SaveChanges();

                db.Employes.Add(new Models.Person { Name = "Ivan", DepartmentId = db.Departments.FirstOrDefault().Id });
                db.SaveChanges();

                db.Employes.Add(new Models.Person { Name = "Pesho", Department = db.Departments.FirstOrDefault(), ManagerId = db.Employes.FirstOrDefault(p => p.Name == "Ivan").Id });
                db.SaveChanges();

                db.Departments.FirstOrDefault(d => d.Name == "Developers").Employes.Add(db.Employes.FirstOrDefault(p => p.Name == "Ivan"));
                db.Departments.FirstOrDefault(d => d.Name == "Developers").Employes.Add(db.Employes.FirstOrDefault(p => p.Name == "Pesho"));

                db.SaveChanges();
            }
        }
    }
}