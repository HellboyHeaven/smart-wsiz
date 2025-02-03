using Core.Interfaces;
using System.Linq.Expressions;

namespace Core.Abstracts;
public record FilterCommand<T> : ICommand
{
    public IEnumerable<Expression<Func<T, bool>>> Filters { get; } = new List<Expression<Func<T, bool>>>();

    public void AddFilter(Expression<Func<T, bool>> filter)
    {
        Filters.Append(filter);
    }

}
