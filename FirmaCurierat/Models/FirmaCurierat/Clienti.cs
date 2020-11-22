﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaCurierat.Models.FirmaCurierat
{
    [Table("clienti", Schema = "dbo")]
    public class Clienti
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_client
        {
            get;
            set;
        }

        public string nume
        {
            get;
            set;
        }
        public string adresa
        {
            get;
            set;
        }
    }
}
