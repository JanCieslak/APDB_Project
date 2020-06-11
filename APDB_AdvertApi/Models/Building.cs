using System.Collections.Generic;

namespace APDB_AdvertApi.Models
{
    public class Building
    {
        public int IdBuilding { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public float Height { get; set; }
        public virtual ICollection<Campaign> FromCampaigns { get; set; }
        public virtual ICollection<Campaign> ToCampaigns { get; set; }
    }
}