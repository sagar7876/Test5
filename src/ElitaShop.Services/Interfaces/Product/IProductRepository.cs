﻿using ElitaShop.DataAccess.Paginations;
public interface IProductRepository
{
    // Append
    Task<bool> CreateAsync(ProductCreateDto productCreateDto);

    // Update
    Task<bool> UpdateAsync(long productId, ProductUpdateDto productUpdateDto);

    // Delete
    Task<bool> DeleteAsync(long productId);

    // Get
    Task<IList<Product>> GetAllAsync(PaginationParams @params);
    Task<Product> GetById(long productId);
}