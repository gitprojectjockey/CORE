using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;

namespace LMS.DataTransfer.Assemblers.DtoToEntity
{
    public class PatronEntityAssembler : EntityAssemblerBase<Patron,PatronDto>
    {
        public override Patron AssembleEntity(PatronDto source)
        {
            return Mapper.Map<PatronDto, Patron>(source);
        }
    }
}
