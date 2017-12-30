using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;
using System.Collections.Generic;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public sealed class CheckoutsDtoAssembler : DTOAssemblerBase<IEnumerable<CheckoutDto>, IEnumerable<Checkout>>
    {
        public override IEnumerable<CheckoutDto> AssembleDTO(IEnumerable<Checkout>source)
        {
            return Mapper.Map<IEnumerable<Checkout>, IEnumerable<CheckoutDto>>(source);
        }
    }
}
