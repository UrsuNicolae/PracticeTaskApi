using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Error
    {
        public bool Success { get; set; } = true;

        public int Status_Code { get; set; }

        public string Status_Message { get; set; }
    }
}
