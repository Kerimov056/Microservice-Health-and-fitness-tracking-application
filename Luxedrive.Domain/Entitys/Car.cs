using Activictiy.Domain.Entitys.Common;
using Activictiy.Domain.Enums;

namespace Activictiy.Domain.Entitys;

public class Car : BaseEntity
{
    public string Marka { get; set; }
    public string Model { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }
    public bool isReserv { get; set; } = false;
    public bool isCampaigns { get; set; } = false;
    public string? CampaignName { get; set; }
    public decimal? CampaignsPrice { get; set; }
    public decimal? CampaignsInterest { get; set; }
    public DateTime? PickUpCampaigns { get; set; }
    public DateTime? ReturnCampaigns { get; set; }
    public CampaignsStatus Status { get; set; }

    public Guid carCatogoryId { get; set; }
    public CarCatogory carCatogory { get; set; }
    public List<CarImage> images { get; set; }
    public List<CarComment>? carComments { get; set; }

}
