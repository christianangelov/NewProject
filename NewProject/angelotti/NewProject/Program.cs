using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ef_demo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace NewProject
{
    internal class Program
    {
        private static async Task Main(string[] args)
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
                Addresses = new List<Address>
                {
                    new Address
                    {
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

            IIncludableQueryable<Person, List<Address>> src = context.Person.Include(p => p.Addresses);
            List<string> queryRes1 = await src
                .Where(p => p.IsAwesome)
                .SelectMany(p => p.Addresses)
                .Select(a => a.City)
                .Distinct()
                .OrderBy(n => n)
                .ToListAsync();
            Dictionary<bool, int> queryRes2 = await src
                .GroupBy(p => p.IsAwesome)
                .Select(g => new {Awesome = g.Key, Count = g.Count()})
                .ToDictionaryAsync(t => t.Awesome, t => t.Count);

            Console.WriteLine("Cities with awesome people:");
            foreach (string city in queryRes1)
            {
                Console.WriteLine(city);
            }

            Console.WriteLine(
                $"{Environment.NewLine}In total there are {queryRes2[true]} awesome and {queryRes2[false]} not awesome people");
        }
    }
}