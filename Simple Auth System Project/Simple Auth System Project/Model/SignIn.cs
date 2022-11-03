﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Auth_System_project.Model
   
{
    public class SignInRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
       
    }

    public class SignInResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}