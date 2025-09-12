using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using WebApplication2.Models; // your User model namespace

namespace WebApplication2
{
    public partial class Default : Page
    {
        protected async void Page_Load(object sender, EventArgs e) // run every time page loads
        {
            if (!IsPostBack) //checks if the page is loading for the first time
            {
                await LoadUsers();
            }
        }

        private async Task LoadUsers()
        {
            string apiUrl = "http://localhost:52210/api/users"; // replace with your port

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear(); //clear any old header
                client.DefaultRequestHeaders.Accept.Add( //tells API: I want JSON back
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl); //sends get request

                if (response.IsSuccessStatusCode)
                {
                    var users = await response.Content.ReadAsAsync<List<User>>(); //Deserializes JSON response into a list of User objects
                    RepeaterUsers.DataSource = users; //Assigns the list to your GridView
                    RepeaterUsers.DataBind(); //Tells the GridView to render the data as HTML 
                }
                else
                {
                    // In case of error, show message
                    RepeaterUsers.DataSource = new List<User> {
                        new User { Id = 0, Username = "Error", Email = "API not reachable" }
                    };
                    RepeaterUsers.DataBind();
                }
            }

        }
        protected async void searchButton(object sender, EventArgs e)
        {
            if(int.TryParse(TextUserId.Text, out int id)) //converts textUserId to integer and return true, store value in id
            {
                errorMessage.Text = "";
                await GetOneUser(id);
            }
            else
            {
                errorMessage.Text = "Please Enter a number";
                GridViewOneUser.DataSource = null;
                GridViewOneUser.DataBind();
            }
        }

        private async Task GetOneUser(int id)
        {
            string apiUrl = $"http://localhost:52210/api/users/{id}";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadAsAsync<User>();
                    GridViewOneUser.DataSource = new List<User> { user };
                    GridViewOneUser.DataBind();
                }
                else
                {
                    GridViewOneUser.DataSource = new List<User> { new User { Id = 0,
                        Username = "User Not Found", Email = "" }
                    };
                  
                    GridViewOneUser.DataBind();               
                }
            }

        }
        protected async void createUserButton(object sender, EventArgs e)
        {
            string username = Username.Text.Trim();
            string email = Email.Text.Trim();
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
            {
                createErrorMessage.Text = "please enter both username and email";
                createSuccessMessage.Text = "";
                return;
            }
            var newUser = new User
            {
                Username = username,
                Email = email,
            };
            createErrorMessage.Text = "";
            await createNewUser(newUser);
        }
        private async Task createNewUser(User newUser)
        {
            string apiUrl = "http://localhost:52210/api/users";
            using (HttpClient client = new HttpClient()) 
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, newUser);
                if (response.IsSuccessStatusCode)
                {
                    await LoadUsers();
                    createSuccessMessage.Text = "User created successfully";
                }
                else
                {
                    createErrorMessage.Text = $"error: {response.StatusCode}";
                }
            }
        }
    }
}
