﻿namespace Cloth.API.Models.Responses.Cloth;

public class SizeClothResponse
{
    public Guid SizeId { get; set; }

    public string Size { get; set; }

    public int QuantityInStock { get; set; }
}