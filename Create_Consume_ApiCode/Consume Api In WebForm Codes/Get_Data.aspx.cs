using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Text;
//using System.Data.SqlClient;
//using System.Data;
//using System.Configuration;

public partial class Get_Data : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetEmployees();
            CountryDropdown();
            //StateDropdown();
            //CityDropdown();
        }
    }


    void Text_Clear()
    {
        Id_HiddenField.Value = "";
        Name_Box.Text = "";
        Salary_Box.Text = "";
        ddlCountry.ClearSelection();
        ddlState.ClearSelection();
        ddlCity.ClearSelection();
        Gender_List.ClearSelection();

    }


    protected void Reset_Button_Click(object sender, EventArgs e)
    {
        Text_Clear();
    }

    //---------------Bind Country DropDownList Method For Api --------------


    protected void CountryDropdown()
    {
        try
        {
            // API endpoint URL to retrieve countries
            string apiUrl = "http://localhost:53923/api/TestApi/Countries";

            // Create HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                // Send GET request to API endpoint
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read response content as string
                    string responseData = response.Content.ReadAsStringAsync().Result;

                    // Deserialize JSON response to list of Country objects
                    List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(responseData);

                    // Bind countries to dropdown list
                    ddlCountry.DataSource = countries;
                    ddlCountry.DataTextField = "CountryName"; // Set the display text
                    ddlCountry.DataValueField = "Id"; // Set the value
                    ddlCountry.DataBind();

                    // Add a default option
                    ddlCountry.Items.Insert(0, new ListItem("-- Select Country --", "0"));
                }
                else
                {
                    // Handle unsuccessful response
                    lblMessage.Text = "Failed to retrieve countries. Status code: " + response.StatusCode;
                    lblMessage.CssClass = "text-danger";
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions
            lblMessage.Text = "An error occurred: " + ex.Message;
            lblMessage.CssClass = "text-danger";
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        StateDropdown();
    }


    //---------------Bind State Drodownlist Method For Api --------------

    protected void StateDropdown()
    {
        try
        {
            // Get the selected country ID from the dropdown list
            int selectedCountryId = Convert.ToInt32(ddlCountry.SelectedValue);

            // API endpoint URL to retrieve states by country ID
            string apiUrl = $"http://localhost:53923/api/TestApi/StatesByCountry/{selectedCountryId}";

            // Create HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                // Send GET request to API endpoint
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read response content as string
                    string responseData = response.Content.ReadAsStringAsync().Result;

                    // Deserialize JSON response to list of State objects
                    List<State> states = JsonConvert.DeserializeObject<List<State>>(responseData);

                    // Bind states to dropdown list
                    ddlState.DataSource = states;
                    ddlState.DataTextField = "StateName"; // Set the display text
                    ddlState.DataValueField = "Id"; // Set the value
                    ddlState.DataBind();

                    // Add a default option
                    ddlState.Items.Insert(0, new ListItem("-- Select State --", "0"));
                }
                else
                {
                    // Handle unsuccessful response
                    lblMessage.Text = "Failed to retrieve states. Status code: " + response.StatusCode;
                    lblMessage.CssClass = "text-danger";
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions
            lblMessage.Text = "An error occurred: " + ex.Message;
            lblMessage.CssClass = "text-danger";
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        CityDropdown();
    }



    //---------------Bind City Drodownlist Method For Api --------------

    protected void CityDropdown()
    {
        try
        {
            // Get the selected state ID from the dropdown list
            int selectedStateId = Convert.ToInt32(ddlState.SelectedValue);

            // API endpoint URL to retrieve cities by state ID
            string apiUrl = $"http://localhost:53923/api/TestApi/CitiesByState/{selectedStateId}";

            // Create HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                // Send GET request to API endpoint
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read response content as string
                    string responseData = response.Content.ReadAsStringAsync().Result;

                    // Deserialize JSON response to list of City objects
                    List<City> cities = JsonConvert.DeserializeObject<List<City>>(responseData);

                    // Bind cities to dropdown list
                    ddlCity.DataSource = cities;
                    ddlCity.DataTextField = "CityName"; // Set the display text
                    ddlCity.DataValueField = "Id"; // Set the value
                    ddlCity.DataBind();

                    // Add a default option
                    ddlCity.Items.Insert(0, new ListItem("-- Select City --", "0"));
                }
                else
                {
                    // Handle unsuccessful response
                    lblMessage.Text = "Failed to retrieve cities. Status code: " + response.StatusCode;
                    lblMessage.CssClass = "text-danger";
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions
            lblMessage.Text = "An error occurred: " + ex.Message;
            lblMessage.CssClass = "text-danger";
        }
    }



    //---------------Bind GridView Code Or Method For Api --------------
    protected void GetEmployees()
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:53923/api/");
            HttpResponseMessage response = client.GetAsync("TestApi").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var employees = JsonConvert.DeserializeObject<List<Employee>>(data);
                Emp_GridView.DataSource = employees;
                Emp_GridView.DataBind(); // Ensure data binding
            }
            else
            {
                Response.Write("Data Not Bind");
            }
        }
        catch (Exception ex)
        {
            // Log the exception or display an error message
            Response.Write($"An error occurred: {ex.Message}");
        }

    }
    //--------------- GridView Method End --------------




    //---------------Insert Event Or Method For Api Start --------------

    protected async void InsertEmployee()
    {
        try
        {
            // Create a new Employee object with data from your form controls
            Employee newEmployee = new Employee
            {
                Name = Name_Box.Text, // Assuming you have a textbox named txtName for name input
                Salary = Convert.ToInt32(Salary_Box.Text), // Assuming you have a textbox named txtSalary for salary input
                Gender = Gender_List.SelectedItem.Text, // Assuming you have a dropdownlist named ddlGender for gender selection
                Country = ddlCountry.SelectedItem.Text,
                State = ddlState.SelectedItem.Text,
                City = ddlCity.SelectedItem.Text
            };

            // Serialize the Employee object to JSON
            string json = JsonConvert.SerializeObject(newEmployee);

            // Create a StringContent object with the JSON data
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Set the URL of your API endpoint for inserting data
                string url = "http://localhost:53923/api/TestApi/Data_Post";

                // Send a POST request to the API endpoint
                HttpResponseMessage response = await client.PostAsync(url, data);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Display a success message
                    lblMessage.Text = "Data inserted successfully!";
                    lblMessage.CssClass = "text-success";
                    Text_Clear();
                    GetEmployees();
                }
                else
                {
                    // Display an error message
                    lblMessage.Text = "Failed to insert data. Status code: " + response.StatusCode;
                    lblMessage.CssClass = "text-danger";
                }
            }
        }
        catch (Exception ex)
        {
            // Display an error message if an exception occurs
            lblMessage.Text = "An error occurred: " + ex.Message;
            lblMessage.CssClass = "text-danger";
        }
    }

    protected void Insert_Button_Click(object sender, EventArgs e)
    {
        if(Id_HiddenField.Value == "")
        {
            InsertEmployee();
        }
        else
        {
            lblMessage.Text = "User Alredy Id Exist... You're Selected Row For Delete Or Update Data Not For Insert.";
            lblMessage.CssClass = "text-danger";
        }


    }

    //------------ Insert Event End ---------------

    //------------ Selected Index Chenge For Sending Data GriddView To TextBox

    protected void Emp_GridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = Emp_GridView.SelectedRow;

        Label Id_lbl = (Label)row.FindControl("Label1");
        Label Name_lbl = (Label)row.FindControl("Label2");
        Label Salary_lbl = (Label)row.FindControl("Label3");
        Label Gender_lbl = (Label)row.FindControl("Label4");
        Label Country_lbl = (Label)row.FindControl("Label5");
        Label State_lbl = (Label)row.FindControl("Label6");
        Label City_lbl = (Label)row.FindControl("Label7");

        Id_HiddenField.Value = Id_lbl.Text;
        Name_Box.Text = Name_lbl.Text;
        Salary_Box.Text = Salary_lbl.Text;
        Gender_List.SelectedItem.Text = Gender_lbl.Text;
        ddlCountry.SelectedItem.Text = Country_lbl.Text;
        ddlState.SelectedItem.Text = State_lbl.Text;
        ddlCity.SelectedItem.Text = City_lbl.Text;
    }


    //------------ Selected index change Event End ---------------



    //------------ Update Event Code  For Api---------------


    protected async void UpdateEmployee()
    {

        try
        {
            // Create a new Employee object with updated data from your form controls
            Employee updatedEmployee = new Employee
            {
                Id = Convert.ToInt32(Id_HiddenField.Value), // Assuming you have a textbox named txtEmployeeId for employee ID input
                Name = Name_Box.Text, // Assuming you have a textbox named txtName for name input
                Salary = Convert.ToInt32(Salary_Box.Text), // Assuming you have a textbox named txtSalary for salary input
                Gender = Gender_List.SelectedItem.Text, // Assuming you have a dropdownlist named ddlGender for gender selection
                Country = ddlCountry.SelectedItem.Text,
                State = ddlState.SelectedItem.Text,
                City = ddlCity.SelectedItem.Text
            };

            // Serialize the updated Employee object to JSON
            string json = JsonConvert.SerializeObject(updatedEmployee);

            // Create a StringContent object with the JSON data
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Set the URL of your API endpoint for updating data
                string url = "http://localhost:53923/api/TestApi/Data_Put?id=" + updatedEmployee.Id;

                // Send a PUT request to the API endpoint
                HttpResponseMessage response = await client.PutAsync(url, data);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Display a success message
                    lblMessage.Text = "Data updated successfully!";
                    lblMessage.CssClass = "text-success";
                    Text_Clear();
                    GetEmployees();
                }
                else
                {
                    // Display an error message
                    lblMessage.Text = "Failed to update data. Status code: " + response.StatusCode;
                    lblMessage.CssClass = "text-danger";
                }
            }
        }
        catch (Exception ex)
        {
            // Display an error message if an exception occurs
            lblMessage.Text = "An error occurred: " + ex.Message;
            lblMessage.CssClass = "text-danger";
        }


    }


    protected void Update_Button_Click(object sender, EventArgs e)
    {
        if (Id_HiddenField.Value != "")
        {
            UpdateEmployee();
        }
        else
        {
            lblMessage.Text = "Please Seletct Row For Update Data...";
            lblMessage.CssClass = "text-danger";
        }
    }



    protected void Delete_Button_Click(object sender, EventArgs e)
    {
        if (Id_HiddenField.Value != "")
        {
            DeleteEmployee();
        }
        else
        {
            lblMessage.Text = "Please Seletct Row For Delete Data...";
            lblMessage.CssClass = "text-danger";
        }
     
    }

    protected async void DeleteEmployee()
    {
        try
        {
            // Get the employee ID to delete
            int employeeIdToDelete = Convert.ToInt32(Id_HiddenField.Value); // Assuming txtEmployeeId is the textbox for employee ID input

            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Set the URL of the API endpoint for deleting data
                string url = "http://localhost:53923/api/TestApi/Data_Delete?id=" + employeeIdToDelete;

                // Send a DELETE request to the API endpoint
                HttpResponseMessage response = await client.DeleteAsync(url);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Display a success message
                    lblMessage.Text = "Employee deleted successfully!";
                    lblMessage.CssClass = "text-success";
                    Text_Clear();
                    GetEmployees();
                }
                else
                {
                    // Display an error message
                    lblMessage.Text = "Failed to delete employee. Status code: " + response.StatusCode;
                    lblMessage.CssClass = "text-danger";
                }
            }
        }
        catch (Exception ex)
        {
            // Display an error message if an exception occurs
            lblMessage.Text = "An error occurred: " + ex.Message;
            lblMessage.CssClass = "text-danger";
        }
    }

    protected void Emp_GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            // Get the employee ID to delete from the GridView's DataKeys collection
            GridViewRow row = Emp_GridView.Rows[e.RowIndex];
            Label id = (Label)row.FindControl("Label1");

            int employeeIdToDelete = Convert.ToInt32(id.Text);

            // Call the method to delete the employee using your API
            string deleteUrl = $"http://localhost:53923/api/TestApi/Data_Delete?id={employeeIdToDelete}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.DeleteAsync(deleteUrl).Result;
                response.EnsureSuccessStatusCode(); // Ensure success status code

                string responseData = response.Content.ReadAsStringAsync().Result;

                // Check if deletion was successful
                if (responseData.Contains("Data Deleted SuccessFully"))
                {
                    // Rebind the GridView after deletion
                    GetEmployees();
                    // Optionally display a success message
                    lblMessage.Text = "Employee deleted successfully!";
                    lblMessage.CssClass = "text-success";
                }
                else
                {
                    // Display error message
                    lblMessage.Text = "Failed to delete employee.";
                    lblMessage.CssClass = "text-danger";
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions here
            lblMessage.Text = "An error occurred: " + ex.Message;
            lblMessage.CssClass = "text-danger";
        }
    }



}