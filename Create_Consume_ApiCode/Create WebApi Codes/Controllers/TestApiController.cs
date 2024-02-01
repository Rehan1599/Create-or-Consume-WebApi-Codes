using Api_With_SingleSp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Api_With_SingleSp.Controllers
{
    public class TestApiController : ApiController
    {
        Employee emp = new Employee();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString);

        //-----------------------Get (Select[Read Data]) Method--------------------
        [HttpGet]
        public List<Employee> Data_Get()
        {
            SqlDataAdapter sda = new SqlDataAdapter("spTestApi", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable data = new DataTable();
            sda.Fill(data);
            List<Employee> Emp_List = new List<Employee>();

            if(data.Rows.Count > 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    Employee emp = new Employee();

                    emp.Id = Convert.ToInt32(data.Rows[i]["Id"]);
                    emp.Name= data.Rows[i]["Name"].ToString();
                    emp.Salary = Convert.ToInt32(data.Rows[i]["Salary"]);
                    emp.Gender = data.Rows[i]["Gender"].ToString();
                    emp.Country = data.Rows[i]["Country"].ToString();
                    emp.State = data.Rows[i]["State"].ToString();
                    emp.City = data.Rows[i]["City"].ToString();

                    Emp_List.Add(emp);
                }
            
            }

            if (Emp_List.Count > 0)
            {
                return Emp_List;
            }
            else
            {
                return null;
            }
        }


        //-----------------------Get (Select By Id[Read Data]) Method--------------------
        [HttpGet]
        public Employee Data_GetById( int id)
        {

            SqlDataAdapter sda = new SqlDataAdapter("spTestApi", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@cmd_type", "By_Id");
            sda.SelectCommand.Parameters.AddWithValue("@Id", id);

            DataTable data = new DataTable();
            sda.Fill(data);

            Employee emp = new Employee();
            if (data.Rows.Count > 0)
            {
               
                    emp.Id = Convert.ToInt32(data.Rows[0]["Id"]);
                    emp.Name = data.Rows[0]["Name"].ToString();
                    emp.Salary = Convert.ToInt32(data.Rows[0]["Salary"]);
                    emp.Gender = data.Rows[0]["Gender"].ToString();
                emp.Country = data.Rows[0]["Country"].ToString();
                emp.State = data.Rows[0]["State"].ToString();
                emp.City = data.Rows[0]["City"].ToString();
            }

            if (emp != null)
            {
                return emp;
            }
            else
            {
                return null;
            }

        }




        //-----------------------Post (Insert) Method--------------------
        [HttpPost]
        public string Data_Post(Employee emp)
        {
            string msg = "";
            if (emp != null)
            {
                SqlCommand cmd = new SqlCommand("spTestApi", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmd_type", "Insert");
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Country", emp.Country);
                cmd.Parameters.AddWithValue("@State", emp.State);
                cmd.Parameters.AddWithValue("@City", emp.City);
                con.Open();

               int row = cmd.ExecuteNonQuery();
                con.Close();
                if (row > 0)
                {
                    return msg = "Insert SuccessFully";
                }
                else
                {
                    return msg = "Insertion Failed";
                }
               
            }
            return msg;
        }


        //-----------------------Get (Update By Id[Update Data]) Method--------------------

        [HttpPut]
        public string Data_Put(int id, Employee emp)
        {
            string msg = "";
            if (emp != null)
            {
                SqlCommand cmd = new SqlCommand("spTestApi", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmd_type", "Update");
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Country", emp.Country);
                cmd.Parameters.AddWithValue("@State", emp.State);
                cmd.Parameters.AddWithValue("@City", emp.City);
                con.Open();

                int row = cmd.ExecuteNonQuery();
                con.Close();
                if (row > 0)
                {
                    return msg = "Update SuccessFully";
                }
                else
                {
                    return msg = "Updation Failed";
                }

            }
            return msg;
        }

        //-----------------------Get (Delete By Id[Delete Data]) Method--------------------

        [HttpDelete]
        public string Data_Delete(int id)
        {
                string msg = "";
          
                SqlCommand cmd = new SqlCommand("spTestApi", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmd_type", "Delete");
                cmd.Parameters.AddWithValue("@Id", id);
               
                con.Open();

                int row = cmd.ExecuteNonQuery();
                con.Close();
                if (row > 0)
                {
                    return msg = "Data Deleted SuccessFully";
                }
                else
                {
                    return msg = "Data Deletion Failed";
                }

                return msg;
        }




        // CRUD operations for Country, State, and City




        // GET: api/TestApi/Countries
        [HttpGet]
        [Route("api/TestApi/Countries")]
        public List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Country", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Country country = new Country
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CountryName = reader["Country"].ToString()
                    };
                    countries.Add(country);
                }
            }
            finally
            {
                con.Close();
            }

            return countries;
        }




        // GET: api/TestApi/StatesByCountry/{countryId}
        [HttpGet]
        [Route("api/TestApi/StatesByCountry/{countryId}")]
        public List<State> GetStatesByCountry(int countryId)
        {
            List<State> states = new List<State>();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM State WHERE Country_Id = @CountryId", con);
                cmd.Parameters.AddWithValue("@CountryId", countryId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    State state = new State
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        StateName = reader["State"].ToString(),
                        CountryId = Convert.ToInt32(reader["Country_Id"])
                    };
                    states.Add(state);
                }
            }
            finally
            {
                con.Close();
            }

            return states;
        }

        // GET: api/TestApi/CitiesByState/{stateId}
        [HttpGet]
        [Route("api/TestApi/CitiesByState/{stateId}")]
        public List<City> GetCitiesByState(int stateId)
        {
            List<City> cities = new List<City>();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM City WHERE State_Id = @StateId", con);
                cmd.Parameters.AddWithValue("@StateId", stateId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    City city = new City
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CityName = reader["City"].ToString(),
                        StateId = Convert.ToInt32(reader["State_Id"])
                    };
                    cities.Add(city);
                }
            }
            finally
            {
                con.Close();
            }

            return cities;
        }

    }


}
