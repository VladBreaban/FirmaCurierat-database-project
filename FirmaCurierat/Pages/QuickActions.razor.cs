using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaCurierat.Pages
{
    public class QuickActionsComponent:ComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }
        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        public async Task viewDrivers(MouseEventArgs args)
        {
            UriHelper.NavigateTo("/employees");
         
        }
        public async Task viewOrders(MouseEventArgs args)
        {
            UriHelper.NavigateTo("/view-number");
          
        }
        public async Task viewGeneral(MouseEventArgs args)
        {
            UriHelper.NavigateTo("/view-general");
       
        }
    }
}
