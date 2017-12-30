namespace LMS.DataTransfer.Assemblers.DtoToEntity
{
    public abstract class EntityAssemblerBase<TEntity,TDto>
    {
        public abstract TEntity AssembleEntity(TDto source);
    }
}
