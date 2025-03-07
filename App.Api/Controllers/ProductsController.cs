using Microsoft.AspNetCore.Mvc;
using Services.Products;

namespace App.Api.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        CreateActionResult(await productService.GetAllAsync());

    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) =>
        CreateActionResult(await productService.GetPagedAsync(pageNumber, pageSize));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) =>
        CreateActionResult(await productService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request) =>
        CreateActionResult(await productService.CreateAsync(request));

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request) =>
        CreateActionResult(await productService.UpdateAsync(id, request));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        CreateActionResult(await productService.DeleteAsync(id));
}