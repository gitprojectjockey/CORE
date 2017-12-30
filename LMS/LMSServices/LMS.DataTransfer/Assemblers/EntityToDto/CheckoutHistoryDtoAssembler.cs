using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using System.Collections.Generic;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public sealed class CheckoutHistoryDtoAssembler : DTOAssemblerBase<IEnumerable<CheckoutHistoryDto>,IEnumerable<CheckoutHistory>>
    {
        public override IEnumerable<CheckoutHistoryDto> AssembleDTO(IEnumerable<CheckoutHistory>source)
        {
            return Mapper.Map<IEnumerable<CheckoutHistory>, IEnumerable<CheckoutHistoryDto>>(source);
        }
    }
}
