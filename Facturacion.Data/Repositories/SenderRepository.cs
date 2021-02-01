using Facturacion.Data.Contracts;
using Facturacion.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Data.Repositories
{

    public class SenderRepository: ISender
    {
        private EasyStcokDBContext _dbContex;
        public SenderRepository(EasyStcokDBContext dbContext)
        {
            _dbContex = dbContext;
        }

        public async Task<bool> addSend(DocumentToSend document)
        {
            DocumentToSend newDocumentToSend;
            try
            {
                document.Date = DateTime.Now;
                document.Sent = false;
                document.TimesSent = 0;
                _dbContex.Entry(document).State = EntityState.Added;
                return await _dbContex.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}

