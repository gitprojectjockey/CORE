using AutoMapper;
using LibraryData.Entities;
using LMS.DataTransfer.Objects;

namespace LMS.DataTransfer.ObjectMaps
{
    public class LMSMaps : ILMSMaps
    {
        private static bool _initialized;
        public LMSMaps()
        {
            _initialized = false;
        }

        public  bool Initialized { get => _initialized;}

        public void Configure()
        {
            _initialized = true;
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Patron, PatronDto>().PreserveReferences();
                cfg.CreateMap<LibraryBranch, LibraryBranchDto>().PreserveReferences();
                cfg.CreateMap<CheckoutHistory, CheckoutHistoryDto>().PreserveReferences();
                cfg.CreateMap<Checkout, CheckoutDto>().PreserveReferences();
                cfg.CreateMap<Hold, HoldDto>().PreserveReferences();
                cfg.CreateMap<Book, BookDto>().PreserveReferences();
                cfg.CreateMap<LibraryCard, LibraryCardDto>().PreserveReferences();
                cfg.CreateMap<BranchHour, BranchHourDto>().PreserveReferences();
            });
            Mapper.AssertConfigurationIsValid();
        }
    }
}
