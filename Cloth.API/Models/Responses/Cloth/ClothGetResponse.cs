﻿namespace Cloth.API.Models.Responses.Cloth;

public class ClothGetResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public Guid BrandId { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public List<GroupClothResponse> Groups { get; set; }

    public List<SizeClothResponse> Sizes { get; set; }
}