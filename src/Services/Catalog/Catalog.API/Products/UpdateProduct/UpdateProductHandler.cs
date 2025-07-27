
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand
(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageFile,
    List<string> Category
) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductComamandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductComamandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
    }
}

internal class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle called with  {@command}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if(product is null)
        {
            throw new ProductNotFoundException();
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;
        product.ImageFile = command.ImageFile;
        product.Category = command.Category;
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);


        return new UpdateProductResult(true);

    }
}

