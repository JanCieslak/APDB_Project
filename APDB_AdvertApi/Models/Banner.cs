using System.Text.RegularExpressions;

namespace APDB_AdvertApi.Models
{
    public class Banner
    {
        public int IdAdvertisement { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float Area { get; set; }
        public int IdCampaign { get; set; }
        public virtual Campaign Campaign { get; set; }
    }
}