﻿using Diabetes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Core.Interfaces
{
    
        public interface ITokenService
        {
            Task<string> CreateToken(AppUser user);
        }
    

}
