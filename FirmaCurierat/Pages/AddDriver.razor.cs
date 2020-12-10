using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
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

    }
}
