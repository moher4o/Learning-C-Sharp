using System;

namespace ManyToMany
{
  public  class Program
    {
        static void Main()
        {
            using (var db = new MyDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

        }
    }
}
