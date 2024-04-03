using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Commands.AddProduct;
using Product.Application.Features.Queries.GetAllProduct;
using Product.Application.Features.Queries.GetByCode;
using Product.Application.Features.Queries.GetById;

namespace Product.API.Controllers;


[Route("[controller]")]
[ApiController]
//[Authorize]
public class ProductController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet()]
    public async Task<IActionResult> GetAllProducts()
        => Ok(await _mediator.Send(new GetAllProductsQuery()));

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetProductnById([FromRoute] int id)
        => Ok(await _mediator.Send(new GetProductByIdQuery(id)));
  

    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetProductnByCode([FromRoute] string code)
        => Ok(await _mediator.Send(new GetProductByCodeQuery(code)));    

    [HttpPost()]
    public async Task<IActionResult> AddProduct(AddProductCommand addProductCommand)
        => Ok(await _mediator.Send(addProductCommand));
  

}
