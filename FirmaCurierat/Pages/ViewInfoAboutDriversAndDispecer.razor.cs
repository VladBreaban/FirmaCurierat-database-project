using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaCurierat.Pages
{
    public partial class ViewInfoAboutDriversAndDispecerComponent:ComponentBase
    {
        public class infoForTheGrid
        {
            public string nume
            {
                get;
                set;
            }
            public string prenume
            {
                get;
                set;

            }
            public string nume_dispecer
            {
                get;
                set;

            }
        }
        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        [Inject]
        protected NavigationManager UriHelper { get; set; }
        public List<infoForTheGrid> interestList;
        protected DataBaseManagement.DataManagement dataHelper
        {
            get;
            set;
        }
        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            dataHelper = new DataBaseManagement.DataManagement();
            interestList = new List<infoForTheGrid>();
            string ConnectionString = @"Data Source=DESKTOP-I3NIEPL\SQLEXPRESS;Initial Catalog=login_database;database=CurieratVladProiect;integrated security=SSPI";
            //string sqlCommand = "select a.nume, a.prenume, b.id_comanda from clienti a " +
            //      "inner join comenzi b on a.id_client = b.id_client" +
            //      " where a.oras= " + "'" + tara.nume + "'";
            string sqlCommand = "select a.nume_dispecer, b.nume, b.prenume from dispeceri a " +
                "inner join soferi b on a.id_dispecer = b.id_dispecer";
            interestList = await dataHelper.LoadData<infoForTheGrid, dynamic>(sqlCommand, new { }, ConnectionString);

        }

    }
}
