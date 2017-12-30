using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using System.Collections.Generic;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public class LibraryBranchesDtoAssembler : DTOAssemblerBase<IEnumerable<LibraryBranchDto>, IEnumerable<LibraryBranch>>
    {
        public override IEnumerable<LibraryBranchDto> AssembleDTO(IEnumerable<LibraryBranch> source)
        {
            return Mapper.Map<IEnumerable<LibraryBranch>, IEnumerable<LibraryBranchDto>>(source);
        }
    }
}
