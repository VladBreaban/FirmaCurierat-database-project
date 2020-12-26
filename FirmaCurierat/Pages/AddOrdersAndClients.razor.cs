using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaCurierat.Pages
{
    public class AddOrdersAndClientsComponent:ComponentBase
    {
        protected Models.FirmaCurierat.Clienti client;
        protected Models.FirmaCurierat.Comenzi comanda;
        protected List<Models.FirmaCurierat.TipComenzi> tip;
        protected Models.FirmaCurierat.TipComenzi tip_selected;
        protected List<Models.FirmaCurierat.Dispeceri> coordsList;
        protected DataBaseManagement.DataManagement dataHelper;
        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            client = new Models.FirmaCurierat.Clienti();
            comanda = new Models.FirmaCurierat.Comenzi();
            tip = new List<Models.FirmaCurierat.TipComenzi>();
            coordsList = new List<Models.FirmaCurierat.Dispeceri>();
            tip_selected = new Models.FirmaCurierat.TipComenzi();
            // SqlConnection scn = new SqlConnection();
            string ConnectionString = @"Data Source=DESKTOP-I3NIEPL\SQLEXPRESS;Initial Catalog=login_database;database=CurieratVladProiect;integrated security=SSPI";
            coordsList = new List<Models.FirmaCurierat.Dispeceri>();
            string sqlCommand = "select * from  dispeceri";
            dataHelper = new DataBaseManagement.DataManagement();
            coordsList = await dataHelper.LoadData<Models.FirmaCurierat.Dispeceri, dynamic>(sqlCommand, new { }, ConnectionString);
            sqlCommand = "select * from tip_comenzi";
            tip = await dataHelper.LoadData<Models.FirmaCurierat.TipComenzi, dynamic>(sqlCommand, new { }, ConnectionString);

        }

        protected async Task registerOrder(MouseEventArgs args)
        {

        }
    }
}
