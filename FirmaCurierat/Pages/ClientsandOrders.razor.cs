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
    public partial class ClientsandOrdersComponent : ComponentBase
    {
        public List<Models.FirmaCurierat.Clienti> clients;

        protected DataBaseManagement.DataManagement dataHelper
        {
            get;
            set;
        }
        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            dataHelper = new DataBaseManagement.DataManagement();
            // SqlConnection scn = new SqlConnection();
            string ConnectionString = @"Data Source=DESKTOP-I3NIEPL\SQLEXPRESS;Initial Catalog=login_database;database=CurieratVladProiect;integrated security=SSPI";
            clients = new List<FirmaCurierat.Models.FirmaCurierat.Clienti>();
            string sqlCommand = "select * from  clienti";
            clients = await dataHelper.LoadData<FirmaCurierat.Models.FirmaCurierat.Clienti, dynamic>(sqlCommand, new { }, ConnectionString);
        }
        public async Task goToAdd(MouseEventArgs args)
        {
            
            NavigationManager.NavigateTo("/addOrdersAndClients");
            await InvokeAsync(() => { StateHasChanged(); });
        }
        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, object data)
        {

            try
            {
                if (await DialogService.Confirm("Do you want to delete this record?") == true)
                {
                  //  List<Models.FirmaCurierat.Soferi> drivers2 = new List<Models.FirmaCurierat.Soferi>();
                   // drivers2 = drivers;
                   // drivers = new List<Models.FirmaCurierat.Soferi>();
                    FirmaCurierat.Models.FirmaCurierat.Clienti data2 = new Models.FirmaCurierat.Clienti();
                   // drivers2.RemoveAll(d => d.id_sofer == data2.id_sofer);
                    data2 = (Models.FirmaCurierat.Clienti)data;
                    SqlConnection scn = new SqlConnection();
                    scn.ConnectionString = @"Data Source=DESKTOP-I3NIEPL\SQLEXPRESS;Initial Catalog=login_database;database=CurieratVladProiect;integrated security=SSPI";
                    SqlCommand scmd = new SqlCommand("select id_comanda from comenzi where id_client = @id", scn);
                    scmd.Parameters.Clear();
                    scmd.Parameters.AddWithValue("@id", data2.id_client);
                    scn.Open();
                    int deletType = Convert.ToInt32(scmd.ExecuteScalar());
                    scmd = new SqlCommand("delete from tipul_comenzii where id_comanda = @id", scn);
                    scmd.Parameters.AddWithValue("@id", deletType);
                    scmd.ExecuteNonQuery();
                    scmd = new SqlCommand("delete from comenzi where id_comanda = @id", scn);
                    scmd.Parameters.AddWithValue("@id", deletType);
                    scmd.ExecuteNonQuery();
                    scmd = new SqlCommand("delete from clienti where id_client = @id", scn);
                    scmd.Parameters.AddWithValue("@id", data2.id_client);
                    scmd.ExecuteNonQuery();
                    this.StateHasChanged();

                }
            }
            catch (System.Exception rlvMailerDeleteMailException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Mail");
            }
        }
    }
}
