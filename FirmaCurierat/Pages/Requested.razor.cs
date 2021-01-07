using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaCurierat.Pages
{
    public partial class RequestedComponent : ComponentBase
    {
        public class infoForTheGrid
        {
            public string nume
            {
                get;
                set;
            }
            public string awb
            {
                get;
                set;
            }
            public int valoare_comanda
            {
                get;
                set;
            }
        }
        protected DataBaseManagement.DataManagement dataHelper
        {
            get;
            set;
        }
        public List<infoForTheGrid> interestList;
        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            interestList = new List<infoForTheGrid>();
            dataHelper = new DataBaseManagement.DataManagement();
            string ServerName = Environment.MachineName;

            string database = "CurieratVladProiect";
            string ConnectionString = String.Format(@"Server={0}\SQLEXPRESS;Initial Catalog={1};
                                               Integrated Security = SSPI", ServerName, database);


            string sqlCommand = "select a.nume, b.awb, b.valoare_comanda from clienti a " +
                "inner join comenzi b on a.id_client = b.id_client where b.valoare_comanda >= ( select avg(valoare_comanda) from comenzi) order by " +
                "b.valoare_comanda asc";
            interestList = await dataHelper.LoadData<infoForTheGrid, dynamic>(sqlCommand, new { }, ConnectionString);
        }
        }
}
