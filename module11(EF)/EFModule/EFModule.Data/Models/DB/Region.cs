﻿using System;
using System.Collections.Generic;

namespace EFModule.Data.Models.DB
{
    public partial class Region
    {
        public Region()
        {
            Territories = new HashSet<Territories>();
        }

        public int RegionId { get; set; }
        public string RegionDescription { get; set; }
        public DateTime DateOfEstablishment { get; set; }

        public virtual ICollection<Territories> Territories { get; set; }
    }
}
