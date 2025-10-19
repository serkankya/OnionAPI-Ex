﻿using Project.Application.DTOs.BrandDTOs;

namespace Project.Application.Features.Products.Queries
{
	public class GetAllProductsQueryResponse 
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Discount { get; set; }
		public decimal Price { get; set; }
		public BrandDto Brand { get; set; }
	}
}
