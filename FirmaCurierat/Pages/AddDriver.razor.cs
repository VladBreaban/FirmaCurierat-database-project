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
    public class AddDriverComponent : ComponentBase
    {
        Models.FirmaCurierat.Soferi _mail;
        protected Models.FirmaCurierat.Soferi driver;
       
        protected Models.FirmaCurierat.Masini cars;

        protected Models.FirmaCurierat.Dispeceri coords;
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NotificationService NotificationService { get; set; }
        protected List<Models.FirmaCurierat.Dispeceri> coordsList;
        protected List<Models.FirmaCurierat.Masini> carsList;
        protected DataBaseManagement.DataManagement dataHelper
        {
            get;
            set;
        }
        public int? id_masina;
        public int? id_coordonator;
        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            driver = new Models.FirmaCurierat.Soferi();
            cars = new Models.FirmaCurierat.Masini();
            carsList = new List<Models.FirmaCurierat.Masini>();
            coordsList = new List<Models.FirmaCurierat.Dispeceri>();
            // SqlConnection scn = new SqlConnection();
            string ConnectionString = @"Data Source=DESKTOP-I3NIEPL\SQLEXPRESS;Initial Catalog=login_database;database=CurieratVladProiect;integrated security=SSPI";
            coordsList = new List<Models.FirmaCurierat.Dispeceri>();
            carsList = new List<Models.FirmaCurierat.Masini>();
            string sqlCommand = "select * from  dispeceri";
            dataHelper = new DataBaseManagement.DataManagement();
            //  scn.Open();
           coordsList = await dataHelper.LoadData<Models.FirmaCurierat.Dispeceri, dynamic>(sqlCommand, new { }, ConnectionString);
            sqlCommand = "select * from  masini";
          carsList = await dataHelper.LoadData<Models.FirmaCurierat.Masini, dynamic>(sqlCommand, new { }, ConnectionString);
        }

        public async Task register(MouseEventArgs args)
        {
            try
            {
                 if(driver.nume == null || driver.prenume == null || driver.id_masina == null || driver.id_dispecer == null)
                {
                    NotificationService.Notify(NotificationSeverity.Success, $"All field are required!");
                    return;
                }
                SqlConnection scn = new SqlConnection();
                scn.ConnectionString = @"Data Source=DESKTOP-I3NIEPL\SQLEXPRESS;Initial Catalog=login_database;database=CurieratVladProiect;integrated security=SSPI";
                SqlCommand scmd = new SqlCommand("insert into soferi (nume,prenume,an_angajare,id_masina,id_dispecer) values (@nam,@pre,@an,@id1,@id2)", scn);
                scmd.Parameters.Clear();
                driver.an_angajare = 2020;
                scmd.Parameters.AddWithValue("@nam", driver.nume);
                scmd.Parameters.AddWithValue("@pre", driver.prenume);
                scmd.Parameters.AddWithValue("@an", driver.an_angajare);
                scmd.Parameters.AddWithValue("@id1", driver.id_masina);
                scmd.Parameters.AddWithValue("@id2", driver.id_dispecer);
                scn.Open();
                scmd.ExecuteNonQuery();
                NotificationService.Notify(NotificationSeverity.Success, $"Driver added!");
                Task.Delay(50);
                UriHelper.NavigateTo("/counter");
            } catch(Exception e)
            {
                throw;
            }
          
        }

        public async Task back(MouseEventArgs args)
        {

        }

    }
}
