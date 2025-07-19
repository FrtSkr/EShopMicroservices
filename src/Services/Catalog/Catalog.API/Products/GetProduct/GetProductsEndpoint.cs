using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProduct;

public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products", async (ISender sender) =>
                {
                    var result = await sender.Send(new GetProductsQuery());
                    var response = result.Adapt<GetProductResponse>();

                    return Results.Ok(response);

                })
                .WithName("GetProducts")
                .Produces<GetProductsEndpoint>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Get Products");
    }
}
