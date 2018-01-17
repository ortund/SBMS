﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData
{
    public class Style : TableMeta
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CompletionTime { get; set; }
        public decimal Commission { get; set; }
        public byte[] Image { get; set; }
        public string ImageType { get; set; }

        public virtual Store Store { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
