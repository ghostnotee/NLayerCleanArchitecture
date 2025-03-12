using App.Application.Features.Categories;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Update;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers;

public class CategoriesController(ICategoryService categoryService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        CreateActionResult(await categoryService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) =>
        CreateActionResult(await categoryService.GetByIdAsync(id));

    [HttpPost("{id:int}/products")]
    public async Task<IActionResult> GetCategoryWithProducts(int id) =>
        CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));

    [HttpPost("products")]
    public async Task<IActionResult> GetCategoriesWithProductsAsync() =>
        CreateActionResult(await categoryService.GetCategoriesWithProductsAsync());

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request) =>
        CreateActionResult(await categoryService.CreateAsync(request));

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest request) =>
        CreateActionResult(await categoryService.UpdateAsync(id, request));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id) =>
        CreateActionResult(await categoryService.DeleteAsync(id));
}