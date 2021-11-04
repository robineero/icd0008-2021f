using System;
using System.Linq;
using DAL;
using Domain;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var db = new AppDbContext())
            {
                Console.WriteLine($"Posts count: {db.Posts.Count()}");

                int iterations = 1000;
                for (int i = 0; i < iterations; i++)
                {
                    db.Posts.Add(new Post()
                    {
                        Title = $"Title {i} - {Guid.NewGuid()}",
                        Body = $"body {i} - {Guid.NewGuid()}"
                    });
                }
                db.SaveChanges();
                Console.WriteLine($"Posts count: {db.Posts.Count()}");
                
                // Update
                iterations = db.Posts.Count();
                for (int i = 0; i < iterations; i++)
                {
                    var post = db.Posts.FirstOrDefault(x => x.Id == i);
                    if (post != null)
                    {
                        post.Title = $"Uus pealkiri {Guid.NewGuid()}";
                        db.SaveChanges();
                    }
                }
                
                // Delete

                foreach (var px in db.Posts)
                {
                    db.Remove(px);
                }
                db.SaveChanges();


            }
        }
    }
}