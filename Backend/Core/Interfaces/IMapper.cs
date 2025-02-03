namespace Core.Interfaces;

public interface IMapper<TResult, TParam>
{
    TResult Map(TParam request);
}

public interface IMapper<TEntity, TResponse, TRequest>

{
    TEntity Map(TRequest request);
    TResponse Map(TEntity entity);
    IEnumerable<TResponse> Map(IEnumerable<TEntity> entities);
}