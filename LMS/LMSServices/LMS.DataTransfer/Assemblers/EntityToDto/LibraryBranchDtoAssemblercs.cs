using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public class LibraryBranchDtoAssembler : DTOAssemblerBase<LibraryBranchDto, LibraryBranch>
    {
        public override LibraryBranchDto AssembleDTO(LibraryBranch source)
        {
            return Mapper.Map<LibraryBranch, LibraryBranchDto>(source);
        }
    }
}
