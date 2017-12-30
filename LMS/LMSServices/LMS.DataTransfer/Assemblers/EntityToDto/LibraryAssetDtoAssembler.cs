using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public class LibraryAssetDtoAssembler : DTOAssemblerBase<LibraryAssetDto, LibraryAsset>
    {
        public override LibraryAssetDto AssembleDTO(LibraryAsset source)
        {
            return Mapper.Map<LibraryAsset, LibraryAssetDto>(source);
        }
    }
}
