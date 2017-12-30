using LMS.DataTransfer.Assemblers.EntityToDto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LMS.DataTransfer.Factories
{
    public static class DTOAssemblerFactory<TDto, TEntity> where TDto : class where TEntity : class
    {
        public static DTOAssemblerBase<TDto, TEntity> MakeAssembler()
        {
            DTOAssemblerBase<TDto, TEntity> assembler = null;
            var attributeValue = string.Empty;

            var attribute = typeof(TDto).GetCustomAttributes(typeof(IdentityAttribute), true).FirstOrDefault() as IdentityAttribute;
            if (attribute == null && IsDTOCollection(typeof(TDto)))
            {
                attributeValue = GetTypeArgumentName(typeof(TDto));
            }
            else
            {
                attributeValue = attribute.Id;
            }

            switch (attributeValue)
            {
                case "PatronDto":
                    assembler = new PatronDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "BookDto":
                    assembler = new BookDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "LibraryAssetDto":
                    assembler = new LibraryAssetDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "LibraryBranchDto":
                    assembler = new LibraryBranchDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "LibraryCardDto":
                    assembler = new LibraryCardDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "CheckoutDto":
                    assembler = new CheckoutDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "BranchHourDto_Collection":
                    assembler = new LibraryBranchHoursDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "PatronDto_Collection":
                    assembler = new PatronsDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "LibraryCardDto_Collection":
                    assembler = new LibraryCardsDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "BookDto_Collection":
                    assembler = new BooksDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "LibraryAssetDto_Collection":
                    assembler = new LibraryAssetsDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "CheckoutHistoryDto_Collection":
                    assembler = new CheckoutHistoryDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "CheckoutDto_Collection":
                    assembler = new CheckoutsDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "HoldDto_Collection":
                    assembler = new HoldDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "LibraryBranchDto_Collection":
                    assembler = new LibraryBranchesDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;
                case "VideoDto_Collection":
                    assembler = new VideoDtoAssembler() as DTOAssemblerBase<TDto, TEntity>;
                    break;

            }
            return assembler;
        }

        private static bool IsDTOCollection(Type type)
        {
            return type.GetGenericTypeDefinition() == typeof(List<>) || type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }
        private static string GetTypeArgumentName(Type type)
        {
            return $"{type.GenericTypeArguments.FirstOrDefault().Name}_Collection";
        }
    }
}
