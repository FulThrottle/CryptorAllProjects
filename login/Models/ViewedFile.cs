﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptorLogin.Models
{
    public class ViewedFile
    {
        public string FilePath { get; set; }

        public string FileName { get; set; }
        
        public DateTime ViewedDate { get; set; }
    }
}
