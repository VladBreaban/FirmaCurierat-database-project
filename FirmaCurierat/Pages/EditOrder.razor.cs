﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaCurierat.Pages
{
    public class EditOrderComponent : ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }
        [Parameter]
        public dynamic client_id { get; set; }
        protected Models.FirmaCurierat.Clienti client;
        protected Models.FirmaCurierat.Comenzi comanda;
        protected List<Models.FirmaCurierat.TipComenzi> tip;
        protected List<Models.FirmaCurierat.Comenzi> comenzi;
        protected Models.FirmaCurierat.TipComenzi tip_selected;
        protected List<Models.FirmaCurierat.Dispeceri> coordsList;
        protected DataBaseManagement.DataManagement dataHelper;
        public List<Models.FirmaCurierat.Clienti> clients
        {
            get;
            set;
        }
        protected async Task loadClientsandOrders()
        {
            tip = new List<Models.FirmaCurierat.TipComenzi>();
            tip_selected = new Models.FirmaCurierat.TipComenzi();
            comenzi = new List<Models.FirmaCurierat.Comenzi>();
            dataHelper = new DataBaseManagement.DataManagement();
            clients = new List<Models.FirmaCurierat.Clienti>();
            client = new Models.FirmaCurierat.Clienti();
            SqlConnection scn = new SqlConnection();
            scn.ConnectionString = @"Data Source=DESKTOP-I3NIEPL\SQLEXPRESS;Initial Catalog=login_database;database=CurieratVladProiect;integrated security=SSPI";
            scn.Open();
            string ConnectionString = @"Data Source=DESKTOP-I3NIEPL\SQLEXPRESS;Initial Catalog=login_database;database=CurieratVladProiect;integrated security=SSPI";

            comanda = new Models.FirmaCurierat.Comenzi();
            SqlCommand scmd = new SqlCommand("select * from  comenzi where id_client = @id", scn);
            scmd.Parameters.Clear();

            scmd.Parameters.AddWithValue("@id", client_id);

            using (SqlDataReader reader = scmd.ExecuteReader())
            {

                if (reader.Read())
                {
                    comanda.awb = (string)reader["awb"];
                    comanda.data_livrare = (string)reader["data_livrare"];
                    comanda.id_client = (int)reader["id_client"];
                    comanda.id_comanda = (int)reader["id_comanda"];
                    comanda.id_dispecer = (int)reader["id_dispecer"];

                }
            }
            scmd = new SqlCommand("select id_tip from tipul_comenzii where id_comanda = @id;;SELECT SCOPE_IDENTITY()", scn);
            scmd.Parameters.AddWithValue("@id", comanda.id_comanda);
            int tip_id = Convert.ToInt32(scmd.ExecuteScalar());
            scmd = new SqlCommand("select * from tip_comenzi where id_tip = @id", scn);
            scmd.Parameters.Clear();
            scmd.Parameters.AddWithValue("@id", tip_id);
          
            using (SqlDataReader reader = scmd.ExecuteReader())
            {

                if (reader.Read())
                {
                 tip_selected.id_tip = (int)reader["id_tip"];
                    tip_selected.specificatii = (string)reader["specificatii"];
                    tip_selected.tip = (string)reader["tip"];

                }
            }
            string sqlCommand = "select * from tip_comenzi";
            tip = await dataHelper.LoadData<Models.FirmaCurierat.TipComenzi, dynamic>(sqlCommand, new { }, ConnectionString);
   
        }
  
        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            try
            {
                await loadClientsandOrders();


            }
            catch (Exception e)
            {
                throw;
            }


        }
        protected async Task updateOrder()
        {
            SqlConnection scn = new SqlConnection();
            scn.ConnectionString = @"Data Source=DESKTOP-I3NIEPL\SQLEXPRESS;Initial Catalog=login_database;database=CurieratVladProiect;integrated security=SSPI";

            SqlCommand scmd = new SqlCommand("select id_comanda from comenzi where id_client = @id;SELECT SCOPE_IDENTITY()", scn);
            scmd.Parameters.AddWithValue("@id", client_id);
            scn.Open();
            int order_id = Convert.ToInt32(scmd.ExecuteScalar());
            scmd = new SqlCommand("update comenzi SET data_livrare = @dat, awb = @awb where id_comanda = @id", scn);
            scmd.Parameters.Clear();

            scmd.Parameters.AddWithValue("@dat", comanda.data_livrare);
            scmd.Parameters.AddWithValue("@awb", comanda.awb);
            scmd.Parameters.AddWithValue("@id", order_id);
            int inserted_Client = Convert.ToInt32(scmd.ExecuteScalar());
            scmd = new SqlCommand("update tipul_comenzii set id_tip=@id1 where id_comanda=@id1", scn);
            scmd.Parameters.Clear();
            scmd.Parameters.AddWithValue("@id1", tip_selected.id_tip);
            scmd.Parameters.AddWithValue("@id2", order_id);
            scmd.ExecuteNonQuery();

           
        }
      
        protected async Task update(MouseEventArgs args)
        {
            try
            {

                await this.updateOrder();
         
                UriHelper.NavigateTo("/clientsandOrders");
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}