﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Core.Model
{
    public class LoginResponse
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
