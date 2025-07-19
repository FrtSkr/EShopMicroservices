
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSuccess);
public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id));
            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);
        })
            .WithName("DeleteProduct")
            .WithSummary("Delete a product by ID")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithDescription("Delete a product by ID");
    }
}

