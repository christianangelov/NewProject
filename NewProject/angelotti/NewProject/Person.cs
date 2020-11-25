using System;
using System.Collections.Generic;

namespace NewProject
{
    public class Person
    {
        public string SSN { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAwesome { get; set; }
        public double Awesomeness { get; set; }
        public decimal Wealth { get; set; }
        public List<Address> Addresses { get; set; }
    }
}