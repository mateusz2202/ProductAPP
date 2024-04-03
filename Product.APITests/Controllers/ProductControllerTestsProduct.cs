using Xunit;
using Product.Shared.Wrapper;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Authorization.Policy;
using Product.Application.Features.Queries.GetAllProduct;
using Product.Application.Features.Queries;
using Product.APITests;
using Product.Application.Features.Commands.AddProduct;
using FluentAssertions;
using Product.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Product.API.Controllers.Tests;


public class ProductControllerTestsProduct
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public ProductControllerTestsProduct()
    {
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices((conf, services) =>
                {
                    services.AddMvc(options => options.Filters.Add(new FakeUserFilter()));
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                    //sql server change to memoryDatabase
                    var dbContextOption = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ProductDbContext>));

                    if (dbContextOption != null)
                        services.Remove(dbContextOption);

                    services.AddDbContext<ProductDbContext>(options => options.UseInMemoryDatabase("ProductDb"));

                });
            });

        _client = _factory.CreateClient();
    }

  

    [Fact]
    public async Task GetAllProductsTest()
    {
        var tasks = Enumerable.Range(0, 10).Select(_ => CreateProduct());
        await Task.WhenAll(tasks);

        var response = await _client.GetAsync("Product");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var operations = (await response.Content.ReadFromJsonAsync<Result<List<GetAllProductsResponse>>>());

        operations.Data.Should().NotBeNull();
        operations.Data.Should().HaveCountGreaterThan(9);

        await Task.CompletedTask;
    }


    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetProductByIdTest_NotFound(int id)
    {
        var response = await _client.GetAsync($"/Product/id/{id}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        await Task.CompletedTask;
    }

    [Fact]
    public async Task GetProductnByIdTest_Ok()
    {
        var createdProduct = await CreateProduct();

        var response = await _client.GetAsync($"/Product/id/{createdProduct.Id}");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var product = await response.Content.ReadFromJsonAsync<Result<GetProductResponse>>();

        product.Data.Should().NotBeNull();
        product.Data.Should().BeEquivalentTo(createdProduct);

        await Task.CompletedTask;
    }

   
    private async Task<GetProductResponse> CreateProduct()
    {
        var ranodmCode = RandomGenerator.RandomString(4);

        var createdProduct = new AddProductCommand(ranodmCode, "testName",3);

        var responseCreated = await _client.PostAsJsonAsync("/Product", createdProduct);
        responseCreated.EnsureSuccessStatusCode();

        var createdProductResult = await responseCreated.Content.ReadFromJsonAsync<Result<int>>();

        var response = await _client.GetAsync($"/Product/id/{createdProductResult.Data}");
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var product = await response.Content.ReadFromJsonAsync<Result<GetProductResponse>>();

        product.Data.Should().NotBeNull();
        product.Data.Should().BeEquivalentTo(new
        {
            Code = ranodmCode,
            Name = "testName",
            Price = 3
        });

        return product.Data;
    }

    
}