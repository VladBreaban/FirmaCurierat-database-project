using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using System;
using System.Collections.Generic;
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

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {

            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    
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
