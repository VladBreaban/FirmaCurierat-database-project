using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaCurierat.Pages
{
    public class AddOrdersAndClientsComponent:ComponentBase
    {
        protected Models.FirmaCurierat.Clienti client;
        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            client = new Models.FirmaCurierat.Clienti();
           
        }
    }
}
