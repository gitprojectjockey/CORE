﻿using Microsoft.AspNetCore.Identity;
using System;

namespace PostalZipService.Services.Identity
{
    // Note: The advantage of inheriting from framework classes is that you can add your own 
    // custom properties(e.g.Age in my case) to user entity.Also by inheriting database context, you could modify the database schema, if required.
    public class AppIdentityUser : IdentityUser
    {
        public int Age { get; set; }

        public DateTime BirthDate{ get; set; }
    }
}