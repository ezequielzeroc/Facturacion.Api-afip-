using Facturacion.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Data.Contracts
{
    public interface IDocumentTypeRepository
    {
        Task<List<DocumentType>> GetTypes();

    }
}
