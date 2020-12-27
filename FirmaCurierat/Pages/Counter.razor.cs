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
    public class CounterComponent:ComponentBase
    {
        public List<Models.FirmaCurierat.Soferi> drivers;

        protected DataBaseManagement.DataManagement dataHelper
        {
            get;
            set;
        }
        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        public NavigationManager UriHelper { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            dataHelper = new DataBaseManagement.DataManagement();
            // SqlConnection scn = new SqlConnection();
            string ConnectionString = @"Data Source=DESKTOP-I3NIEPL\SQLEXPRESS;Initial Catalog=login_database;database=CurieratVladProiect;integrated security=SSPI";
            drivers = new List<FirmaCurierat.Models.FirmaCurierat.Soferi>();
            string sqlCommand = "select * from  soferi";
            drivers = await dataHelper.LoadData<FirmaCurierat.Models.FirmaCurierat.Soferi, dynamic>(sqlCommand, new { }, ConnectionString);
        }

     protected Syncfusion.Blazor.Grids.SfGrid<FirmaCurierat.Models.FirmaCurierat.Soferi> grid0
        {
            get;
            set;
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, object data)
        {

            try
            {
                if (await DialogService.Confirm("Do you want to delete this record?") == true)
                {
                  List<Models.FirmaCurierat.Soferi> drivers2 = new List<Models.FirmaCurierat.Soferi>();
                    drivers2 = drivers;
                    drivers = new List<Models.FirmaCurierat.Soferi>();
                    FirmaCurierat.Models.FirmaCurierat.Soferi data2 = new Models.FirmaCurierat.Soferi();
                    drivers2.RemoveAll(d => d.id_sofer == data2.id_sofer);
                    data2 = (Models.FirmaCurierat.Soferi)data;
                    SqlConnection scn = new SqlConnection();
                    scn.ConnectionString = @"Data Source=DESKTOP-I3NIEPL\SQLEXPRESS;Initial Catalog=login_database;database=CurieratVladProiect;integrated security=SSPI";
                    SqlCommand scmd = new SqlCommand("delete from soferi where id_sofer =  @id", scn);
                    scmd.Parameters.Clear();
                    scmd.Parameters.AddWithValue("@id", data2.id_sofer);
                    scn.Open();
                    scmd.ExecuteNonQuery();
                    drivers = drivers2;
                    grid0.Refresh();
                    this.StateHasChanged();
                
                }
            }
            catch (System.Exception rlvMailerDeleteMailException)
            {
                NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Mail");
            }
        }
        protected async Task goToAdd(MouseEventArgs args)
        {
           
            UriHelper.NavigateTo("/addDriver");
            await InvokeAsync(() => { StateHasChanged(); });
        }
    }
}
