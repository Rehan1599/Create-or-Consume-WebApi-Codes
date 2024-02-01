using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_With_SingleSp.Models
{
    public class Employee
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}