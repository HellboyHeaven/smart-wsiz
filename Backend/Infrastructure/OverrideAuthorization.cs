using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure;
/// <summary>
/// https://github.com/dotnet/aspnetcore/issues/8149#issuecomment-471927034
/// </summary>
public class OverrideFilter : ActionFilterAttribute
{
    public Type Type { get; set; }
}

public class OverrideFilterProvider : IFilterProvider
{
    public int Order => 1;

    public void OnProvidersExecuted(FilterProviderContext context) { }

    public void OnProvidersExecuting(FilterProviderContext context)
    {
        if (context.ActionContext.ActionDescriptor.FilterDescriptors != null)
        {
            //Check whether the method has any OverrideFilter
            var overrideFilters = context.Results.Where(filterItem => filterItem.Filter is OverrideFilter).ToList();
            foreach (var overrideFilter in overrideFilters)
            {
                //Remove the filters of the corresponding type, but with smaller scope
                context.Results.RemoveAll(filterItem =>
                    filterItem.Descriptor.Filter.GetType() == ((OverrideFilter)overrideFilter.Filter).Type &&
                    filterItem.Descriptor.Scope < overrideFilter.Descriptor.Scope);
            }
        }
    }
}

public class OverrideAuthorization : OverrideFilter
{
    public OverrideAuthorization()
    {
        Type = typeof(AuthorizeFilter);
    }
}

/// <summary>
/// https://stackoverflow.com/questions/16606281/linq-to-remove-certain-elements-from-a-ilistt-based-on-a-ilistint
/// </summary>
public static class IListExt
{
    public static int RemoveAll<T>(this IList<T> list, Predicate<T> match)
    {
        int count = 0;

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (match(list[i]))
            {
                ++count;
                list.RemoveAt(i);
            }
        }

        return count;
    }
}