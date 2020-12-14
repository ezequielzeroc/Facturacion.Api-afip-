using Facturacion.Data.Contracts;
using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Data.Repositories
{
    public class DocumentTypeRepository:IDocumentTypeRepository
    {
        private readonly EasyStcokDBContext _dbContext;
        public DocumentTypeRepository(EasyStcokDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Domain.DocumentType>> GetTypes()
        {
            List<Domain.DocumentType> documentTypes;
            try
            {
                documentTypes = await _dbContext.DocumentTypes.ToListAsync();
                return documentTypes;
            }
            catch (Exception e )
            {
                return null;
            }
        }

    }
}
