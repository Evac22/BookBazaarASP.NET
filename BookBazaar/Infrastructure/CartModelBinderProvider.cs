using BookBazaar.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookBazaar.Infrastructure
{
    public class CartModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.Metadata.ModelType == typeof(Cart) ? new CartModelBinder() : null;
        }
    }
}
