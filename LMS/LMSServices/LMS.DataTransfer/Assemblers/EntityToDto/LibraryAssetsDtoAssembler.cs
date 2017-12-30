using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using System.Collections.Generic;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public class LibraryAssetsDtoAssembler : DTOAssemblerBase<IEnumerable<LibraryAssetDto>,IEnumerable<LibraryAsset>>
    {
        public override IEnumerable<LibraryAssetDto> AssembleDTO(IEnumerable<LibraryAsset> source)
        {
            return Mapper.Map<IEnumerable<LibraryAsset>, IEnumerable<LibraryAssetDto>>(source);
        }
    }
}
