using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace DataAccess
{
    public partial interface INorthwindEntities : IDisposable
    {
        int SaveChanges();

        int SaveChanges(bool acceptChangesDuringSave);

        int SaveChanges(SaveOptions options);
    }
}
