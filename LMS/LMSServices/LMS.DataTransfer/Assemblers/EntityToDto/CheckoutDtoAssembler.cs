using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;

namespace LMS.DataTransfer.Assemblers.EntityToDto
{
    public sealed class CheckoutDtoAssembler : DTOAssemblerBase<CheckoutDto, Checkout>
    {
        public override CheckoutDto AssembleDTO(Checkout source)
        {
            return Mapper.Map<Checkout, CheckoutDto>(source);
        }
    }
}
