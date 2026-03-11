namespace SIMFranchise.Mappings
{
    using AutoMapper;
    using SIMFranchise.DTOs; // Aapke DTOs ka namespace
    using SIMFranchise.DTOs.Company;
    using SIMFranchise.DTOs.Franchise;
    using SIMFranchise.DTOs.Franchise.SIMFranchise.DTOs.Franchise;
    using SIMFranchise.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Company Mapping
            CreateMap<Company, CompanyResponseDto>();
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<CompanyUpdateDto, Company>();

            // Franchises Mappings
            CreateMap<Franchise, FranchiseResponseDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name)); // Company Name nikalne ke liye
            CreateMap<FranchiseCreateDto, Franchise>();
            CreateMap<FranchiseUpdateDto, Franchise>();
        }
    }
}
