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