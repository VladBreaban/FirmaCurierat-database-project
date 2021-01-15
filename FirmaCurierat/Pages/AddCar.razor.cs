using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaCurierat.Pages
{
    public class AddCarComponent : ComponentBase
    {
        public int an;
        public string marca;
        [Inject]
        protected NavigationManager UriHelper { get; set; }
        [Inject]
        protected NotificationService NotificationService { get; set; }
        public void insertCar(MouseEventArgs args)
        {
            SqlConnection scn = new SqlConnection();
          
            string ServerName = Environment.MachineName;

            string database = "CurieratVladProiect";
            string ConnectionString = String.Format(@"Server={0}\SQLEXPRESS;Initial Catalog={1};
                                               Integrated Security = SSPI", ServerName, database);
           

            scn.ConnectionString = ConnectionString;
            scn.Open();
            SqlCommand scmd = new SqlCommand("insert into masini (marca,an) values (@marca,@an)", scn);
            scmd.Parameters.Clear();
            scmd.Parameters.AddWithValue("@marca", marca);
            scmd.Parameters.AddWithValue("@an", an);
            scmd.ExecuteNonQuery();
            NotificationService.Notify(NotificationSeverity.Success, $"Car added!");
            UriHelper.NavigateTo("/quickactions");

        }
    }
}
