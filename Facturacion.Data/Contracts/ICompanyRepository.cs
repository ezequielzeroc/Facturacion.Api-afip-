﻿using Facturacion.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Data.Contracts
{
    public interface ICompanyRepository
    {
        Task<CompanySettings> getSettings(int CompanyID);
        Task<bool> Update(CompanySettings settings);
    }
}
