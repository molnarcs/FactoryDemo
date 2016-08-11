using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace DataAccess
{
    public partial class NorthwindEntitiesMock
    {
        public int SaveChanges()
        {
            return 1;
        }

        public int SaveChanges(bool acceptChangesDuringSave)
        {
            return 1;
        }

        public int SaveChanges(SaveOptions options)
        {
            return 1;
        }

        public void Dispose()
        {
        }
    }
}
