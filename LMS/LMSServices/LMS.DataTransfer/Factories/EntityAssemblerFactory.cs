using LMS.DataTransfer.Assemblers.DtoToEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LMS.DataTransfer.Factories
{
    public static class EntityAssemblerFactory<TEntity, TDto> where TEntity : class where TDto : class
    {
        public static EntityAssemblerBase<TEntity, TDto> MakeAssembler()
        {
            EntityAssemblerBase<TEntity, TDto> assembler = null;
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
                    assembler = new PatronEntityAssembler() as EntityAssemblerBase<TEntity, TDto>;
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
