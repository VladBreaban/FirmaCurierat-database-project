﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            try
            {
                SqlConnection scn = new SqlConnection();
                scn.ConnectionString = @"Data Source=DESKTOP-I3NIEPL\SQLEXPRESS;Initial Catalog=login_database;database=CurieratVladProiect;integrated security=SSPI";
                SqlCommand scmd = new SqlCommand("insert into clienti (nume,prenume,adresa,mail) values (@nam,@pre,@adr,@mail); SELECT SCOPE_IDENTITY()", scn);
              scmd.Parameters.Clear();

                scmd.Parameters.AddWithValue("@nam", client.nume);
                scmd.Parameters.AddWithValue("@pre", client.prenume);
                //scmd.Parameters.AddWithValue("@an", driver.an_angajare);
                scmd.Parameters.AddWithValue("@adr", client.adresa);
                scmd.Parameters.AddWithValue("@mail", client.mail);
                scn.Open();
                int inserted_Client = Convert.ToInt32(scmd.ExecuteScalar());
                scmd = new SqlCommand("insert into comenzi (data_livrare,awb,id_dispecer,id_client) values (@date,@awb,@id1,@id2);SELECT SCOPE_IDENTITY()", scn);
                scmd.Parameters.AddWithValue("@date", comanda.data_livrare);
                scmd.Parameters.AddWithValue("@awb", comanda.awb);
                //scmd.Parameters.AddWithValue("@an", driver.an_angajare);
                scmd.Parameters.AddWithValue("@id1", comanda.id_dispecer);
                scmd.Parameters.AddWithValue("@id2", inserted_Client);
                int inserted_Order = Convert.ToInt32(scmd.ExecuteScalar());
                scmd = new SqlCommand("insert into tipul_comenzii (id_comanda, id_tip) values (@id1, @id2)", scn);
                scmd.Parameters.Clear();
                scmd.Parameters.AddWithValue("@id1", inserted_Order);
                scmd.Parameters.AddWithValue("@id2", tip_selected.id_tip);

                scmd.ExecuteNonQuery();
            }
            catch(Exception e )
            {
                throw;
            }
        
        }
    }
}
