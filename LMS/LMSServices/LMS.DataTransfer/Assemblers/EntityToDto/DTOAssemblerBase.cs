namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public abstract class DTOAssemblerBase<TDto, TEntity>
    {
       public abstract TDto AssembleDTO(TEntity source);
    }
}
