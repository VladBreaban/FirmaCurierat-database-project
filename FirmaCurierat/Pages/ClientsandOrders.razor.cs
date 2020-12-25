using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using System;
using System.Collections.Generic;
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
    }
}
