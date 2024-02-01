using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_With_SingleSp.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
    }

    public class State
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
    }
}