using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
​

namespace ef_demo
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var context = new TestContext();

            const string SSN = "1337080919";

            var newPerson = new Person
            {
                SSN = SSN,
                FirstName = "HTL",
                LastName = "Student",
                DateOfBirth = new DateTime(2019, 09, 08),
                IsAwesome = true,
                Awesomeness = 6.77,
                Addresses = new List<Address>{
                    new Address{
                        SSN = SSN,
                        AddressNo = 1,
                        Country = "Austria",
                        City = "Leonding",
                        Street = "Limesstr.",
                        StreetNo = 12
                    }
                }
            };

            await context.Person.AddAsync(newPerson);

            await context.SaveChangesAsync();

            var src = context.Person.Include(p => p.Addresses);

            var queryRes1 = await src
                .Where(p => p.IsAwesome)
                .SelectMany(p => p.Addresses)
                .Select(a => a.City)
                .Distinct()
                .OrderBy(n => n)
                .ToListAsync();

            var queryRes2 = await src
                .GroupBy(p => p.IsAwesome)
                .Select(g => new { Awesome = g.Key, Count = g.Count() })
                .ToDictionaryAsync(t => t.Awesome, t => t.Count);
            Console.WriteLine("Cities with awesome people:");
            foreach (var city in queryRes1)
            {
                Console.WriteLine(city);
            }
            Console.WriteLine($"{Environment.NewLine}In total there are {queryRes2[true]} awesome and {queryRes2[false]} not awesome people");

        }

    }

}