﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Core.Common
{
    public class RepositoryResponse <T> 
    {
        public T? Data { get; set; }
        public int OperationStatusCode { get; set; }
        public string? Message { get; set; }

    }
}
