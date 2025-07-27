namespace Catalog.API.Products.GetProductById;

public record class GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record class GetProductByIdResult(Product Product);

public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle called with  {@query}", query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if(product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }
        return new GetProductByIdResult(product);
    }
}

