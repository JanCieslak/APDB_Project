using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APDB_AdvertApi.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public Guid Token { get; set; }
    }
}
