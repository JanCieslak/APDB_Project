using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace APDB_AdvertApi.Models
{
    public class Campaign
    {
        public int IdCampaign { get; set; }
        public int IdClient { get; set; }
        public virtual Client Client { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float PricePerSquareMeter { get; set; }
        public int FromIdBuilding { get; set; }
        public virtual Building FromBuilding { get; set; }
        public int ToIdBuilding { get; set; }
        public virtual Building ToBuilding { get; set; }
        public virtual ICollection<Banner> Banners { get; set; }
    }
}