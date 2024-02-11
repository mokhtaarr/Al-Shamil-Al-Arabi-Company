using App.Dal;
using App.Dal.Model;
using App.VM;
using AutoMapper;
using System.Configuration;
using System.Data.Entity;

namespace App.Service.AutoMapperConfigration
{
    public class AutomapperWebProfile : Profile
    {
        public AutomapperWebProfile()
        {
            Categories();
            CompanyWord();
            AboutUs();
            Services();
            SubServices();
            Features();
            SubFeatures();
            Portfolio();
            SubPortfolio();
            Portfolio();
            PortfolioType();
            SubPortfolio();
            Team();
            TeamMembers();
            ContactUs();
            LogosTechnology();
        }
        public void Categories()
        {
            CreateMap<CategoryVM, Category>()
                .ForMember(dest => dest.AllCategories, act => act.Ignore())
                .ForMember(dest => dest.Categories, act => act.Ignore());

            CreateMap<Category, CategoryVM>();
                //.ForMember(dest => dest.Icon, act => act .MapFrom(src => ConfigurationManager.AppSettings["CategoryIconsVirtualPath"] + src.Icon ));
        }
        public void LogosTechnology()
        {
            CreateMap<LogosTechnologyVM, LogosTechnology>();
            CreateMap<LogosTechnology, LogosTechnologyVM>();
        }
        public void CompanyWord()
        {
            CreateMap<CompanyWordVM, CompanyWord>();
            CreateMap<CompanyWord, CompanyWordVM>();
        }
        public void AboutUs()
        {
            CreateMap<AboutUsVM, AboutUs>();
            CreateMap<AboutUs, AboutUsVM>();
        }
        public void Services()
        {
            CreateMap<ServicesVM, Services>()
                .ForMember(dest => dest.SubServices, act => act.Ignore());

            CreateMap<Services, ServicesVM>();
        }
        public void SubServices()
        {
            CreateMap<SubServicesVM, SubServices>()
                .ForMember(dest => dest.Services, act => act.Ignore());

            CreateMap<SubServices, SubServicesVM>();
        }
        public void Features()
        {
            CreateMap<FeaturesVM, Features>()
                .ForMember(dest => dest.SubFeatures, act => act.Ignore());

            CreateMap<Features, FeaturesVM>();
        }
        public void SubFeatures()
        {
            CreateMap<SubFeaturesVM, SubFeatures>()
                .ForMember(dest => dest.Features, act => act.Ignore());

            CreateMap<SubFeatures, SubFeaturesVM>();
        }
        public void Portfolio()
        {
            CreateMap<PortfolioVM, Portfolio>()
                .ForMember(dest => dest.SubPortfolios, act => act.Ignore());

            CreateMap<Portfolio, PortfolioVM>();
        }
        public void PortfolioType()
        {
            CreateMap<PortfolioTypesVM, PortfolioType>()
                .ForMember(dest => dest.SubPortfolios, act => act.Ignore());

            CreateMap<PortfolioType, PortfolioTypesVM>();
        }
        public void SubPortfolio()
        {
            CreateMap<SubPortfolioVM, SubPortfolios>()
                .ForMember(dest => dest.Portfolio, act => act.Ignore());

            CreateMap<SubPortfolios, SubPortfolioVM>();
        }
        public void Team()
        {
            CreateMap<TeamVM, Team>()
                .ForMember(dest => dest.TeamMembers, act => act.Ignore());

            CreateMap<Team, TeamVM>();
        }
        public void TeamMembers()
        {
            CreateMap<TeamMembersVM, TeamMembers>()
                .ForMember(dest => dest.Team, act => act.Ignore());

            CreateMap<TeamMembers, TeamMembersVM>();
        }
        public void ContactUs()
        {
            CreateMap<ContactUsVM, ContactUs>();

            CreateMap<ContactUs, ContactUsVM>();
        }

        #region OldCode
        //        public void MApperForEmployeeLicenceDate()
        //        {
        //            CreateMap<EmployeeLicenceDataVM, EmployeeLicenceData>()
        //                .ForMember(dest => dest.LicenceKindHR, act => act.Ignore())
        //                .ForMember(dest => dest.LicenceTypeHR, act => act.Ignore())
        //                .ForSourceMember(x => x.LicenceKindHREnName, xx => xx.Ignore())
        //                .ForSourceMember(x => x.LicenceKindHRName, xx => xx.Ignore())
        //                .ForSourceMember(x => x.LicenceTypeHREnName, xx => xx.Ignore())
        //                .ForSourceMember(x => x.LicenceTypeHRName, xx => xx.Ignore())
        //                .ForSourceMember(x => x.SourceAreaEnName, xx => xx.Ignore())
        //                .ForSourceMember(x => x.SourceAreaName, xx => xx.Ignore());


        //            CreateMap<EmployeeLicenceData, EmployeeLicenceDataVM>()
        //              .ForMember(dest => dest.LicenceKindHRId, act => act.MapFrom(x => x.LicenceKindHR.ID))
        //              .ForMember(dest => dest.LicenceTypeHRId, act => act.MapFrom(x => x.LicenceTypeHR.ID))

        //               .ForMember(dest => dest.LicenceTypeHRName, act => act
        //              .MapFrom(src => (src.LicenceTypeHR == null) ? "-" : src.LicenceTypeHR.Name))

        //              .ForMember(dest => dest.LicenceTypeHREnName, act => act
        //              .MapFrom(src => (src.LicenceTypeHR == null) ? "-" : src.LicenceTypeHR.EnName))

        //               .ForMember(dest => dest.LicenceKindHRName, act => act
        //              .MapFrom(src => (src.LicenceKindHR == null) ? "-"
        //    : src.LicenceKindHR.Name))

        //              .ForMember(dest => dest.LicenceKindHREnName, act => act
        //              .MapFrom(src => (src.LicenceKindHR == null) ? "-"
        //    : src.LicenceKindHR.EnName))

        //              .ForMember(dest => dest.SourceAreaName, act => act
        //              .MapFrom(src => (src.Area == null) ? "-"
        //    : src.Area.Name))
        //              .ForMember(dest => dest.SourceAreaEnName, act => act
        //              .MapFrom(src => (src.Area == null) ? "-"
        //    : src.Area.EnName));
        //        }
        //        public void EmployeeJobData()
        //        {
        //            CreateMap<EmployeeJobDataVM, EmployeeJobData>()
        //    .ForMember(dest => dest.CarrerField, act => act.Ignore())
        //    .ForMember(dest => dest.JobName, act => act.Ignore())
        //    .ForMember(dest => dest.JobFunction, act => act.Ignore())
        //    .ForMember(dest => dest.Employee, act => act.Ignore())
        //    .ForMember(dest => dest.JobDegree, act => act.Ignore())
        //    .ForMember(dest => dest.JobLevel, act => act.Ignore())
        //                            .ForSourceMember(x => x.CarrerFieldEnName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.CarrerFieldEnName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.JobNameEnName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.JobNameName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.JobFunctionEnName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.JobFunctionName, xx => xx.Ignore())

        //                            .ForSourceMember(x => x.JobDegreeEnName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.JobDegreeName, xx => xx.Ignore())

        //                            .ForSourceMember(x => x.JobLevelName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.JobLevelEnName, xx => xx.Ignore());


        //            CreateMap<EmployeeJobData, EmployeeJobDataVM>()

        //              .ForMember(dest => dest.CarrerFieldEnName, act => act.MapFrom(x => x.CarrerField.EnName))
        //              .ForMember(dest => dest.CarrerFieldName, act => act.MapFrom(x => x.CarrerField.Name))



        //              .ForMember(dest => dest.JobNameName, act => act.MapFrom(x => x.JobName.Name))
        //              .ForMember(dest => dest.JobNameEnName, act => act.MapFrom(x => x.JobName.EnName))

        //               .ForMember(dest => dest.JobFunctionEnName, act => act
        //              .MapFrom(src => (src.JobFunction == null) ? "-"
        //    : src.JobFunction.EnName))
        //                .ForMember(dest => dest.JobFunctionName, act => act
        //              .MapFrom(src => (src.JobFunction == null) ? "-"
        //    : src.JobFunction.Name))

        //              .ForMember(dest => dest.JobDegreeName, act => act.MapFrom(x => x.JobDegree.Name))
        //              .ForMember(dest => dest.JobDegreeEnName, act => act.MapFrom(x => x.JobDegree.ENName))

        //              .ForMember(dest => dest.JobLevelName, act => act.MapFrom(x => x.JobLevel.Name))
        //              .ForMember(dest => dest.JobLevelEnName, act => act.MapFrom(x => x.JobLevel.EnName));

        //        }

        //        public void MapperForemployeeQualification()
        //        {
        //            CreateMap<EmployeeQualificationVM, EmployeeQualification>()
        //                            .ForMember(dest => dest.Employee, act => act.Ignore())
        //                            .ForMember(dest => dest.CertificationGrade, act => act.Ignore())
        //                            .ForMember(dest => dest.CertificationSpecDepart, act => act.Ignore())
        //                            .ForMember(dest => dest.CertificationSpecific, act => act.Ignore())
        //                            .ForMember(dest => dest.CertificationType, act => act.Ignore())

        //                            .ForSourceMember(x => x.CertificationGradeName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.CertificationGradeENName, xx => xx.Ignore())

        //                            .ForSourceMember(x => x.CertificationSpecDepartName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.CertificationSpecDepartEnName, xx => xx.Ignore())

        //                            .ForSourceMember(x => x.CertificationSpecificName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.CertificationSpecificEnName, xx => xx.Ignore())

        //                            .ForSourceMember(x => x.CertificationTypeEnName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.CertificationTypeName, xx => xx.Ignore());

        //            CreateMap<EmployeeQualification, EmployeeQualificationVM>()
        //              .ForMember(dest => dest.EmployeeId, act => act.MapFrom(x => x.Employee.ID))

        //              .ForMember(dest => dest.CertificationGradeName, act => act
        //              .MapFrom(src => (src.CertificationGrade == null) ? "-"
        //    : src.CertificationGrade.Name))

        //              .ForMember(dest => dest.CertificationGradeENName, act => act
        //              .MapFrom(src => (src.CertificationGrade == null) ? "-"
        //    : src.CertificationGrade.NameEng))
        //              .ForMember(dest => dest.CertificationSpecDepartName, act => act
        //              .MapFrom(src => (src.CertificationSpecDepart == null) ? "-"
        //    : src.CertificationSpecDepart.Name))

        //              .ForMember(dest => dest.CertificationSpecDepartEnName, act => act
        //              .MapFrom(src => (src.CertificationSpecDepart == null) ? "-"
        //    : src.CertificationSpecDepart.EnName))

        //              .ForMember(dest => dest.CertificationTypeName, act => act
        //              .MapFrom(src => (src.CertificationType == null) ? "-"
        //    : src.CertificationType.Name))

        //              .ForMember(dest => dest.CertificationTypeEnName, act => act
        //              .MapFrom(src => (src.CertificationType == null) ? "-"
        //    : src.CertificationType.EnName))


        //              .ForMember(dest => dest.CertificationSpecificName, act => act
        //              .MapFrom(src => (src.CertificationSpecific == null) ? "-"
        //    : src.CertificationSpecific.Name))

        //              .ForMember(dest => dest.CertificationSpecificEnName, act => act
        //              .MapFrom(src => (src.CertificationSpecific == null) ? "-"
        //    : src.CertificationSpecific.EnName));



        //        }
        //        public void MapperForEmployeeContractDuration()
        //        {
        //            CreateMap<EmployeeContractDurationVM, EmployeeContractDuration>()
        //                            .ForMember(dest => dest.Employee, act => act.Ignore())
        //                            .ForSourceMember(x => x.EmployeeStatusEnName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.EmployeeStatusKindEnName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.EmployeeStatusKindName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.EmployeeStatusName, xx => xx.Ignore())

        //                            .ForSourceMember(x => x.EmployeeStatusKindName, xx => xx.Ignore());



        //            CreateMap<EmployeeContractDuration, EmployeeContractDurationVM>()

        //                       .ForMember(dest => dest.EmployeeStatusKindName, act => act
        //              .MapFrom(src => (src.EmployeeStatusKind == null) ? "-"
        //    : src.EmployeeStatusKind.Name))

        //              .ForMember(dest => dest.EmployeeStatusKindEnName, act => act
        //              .MapFrom(src => (src.EmployeeStatusKind == null) ? "-"
        //    : src.EmployeeStatusKind.EnName))

        //               .ForMember(dest => dest.EmployeeStatusName, act => act
        //              .MapFrom(src => (src.EmployeeStatu == null) ? "-"
        //    : src.EmployeeStatu.Name))

        //              .ForMember(dest => dest.EmployeeStatusEnName, act => act
        //              .MapFrom(src => (src.EmployeeStatu == null) ? "-"
        //    : src.EmployeeStatu.EnName));


        //        }

        //        public void MapperForEmployeeExperience()
        //        {
        //            CreateMap<EmployeeExperienceVM, EmployeeExperience>()
        //                            .ForMember(dest => dest.Employee, act => act.Ignore());

        //            CreateMap<EmployeeExperience, EmployeeExperienceVM>();

        //        }
        //        public void Countery()
        //        {
        //            CreateMap<CountryVM, Country>()
        //                            .ForMember(dest => dest.Areas, act => act.Ignore())
        //                            .ForMember(dest => dest.Employees, act => act.Ignore());

        //            CreateMap<Country, CountryVM>();
        //        }
        //        public void Area()
        //        {
        //            CreateMap<AreaVM, Area>()
        //                            .ForMember(dest => dest.Country, act => act.Ignore())
        //                            .ForMember(dest => dest.Employees, act => act.Ignore())
        //                            .ForSourceMember(x => x.CountryEnName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.CountryName, xx => xx.Ignore());

        //            CreateMap<Area, AreaVM>()
        //              .ForMember(dest => dest.CountryName, act => act.MapFrom(x => x.Country.Name))
        //              .ForMember(dest => dest.CountryEnName, act => act.MapFrom(x => x.Country.EnName))
        //              .ForMember(dest => dest.CountryId, act => act.MapFrom(x => x.Country.ID));

        //        }
        //        public void Nationality()
        //        {
        //            CreateMap<NationalityVM, Nationality>()

        //                            .ForMember(dest => dest.Employees, act => act.Ignore());

        //            CreateMap<Nationality, NationalityVM>();

        //        }

        //        public void BloodGroup()
        //        {
        //            CreateMap<BloodGroupVM, BloodGroup>()
        //                            .ForMember(dest => dest.Employees, act => act.Ignore());
        //            CreateMap<BloodGroup, BloodGroupVM>();

        //        }
        //        public void Religion()
        //        {
        //            CreateMap<ReligionVM, Religion>()
        //                            .ForMember(dest => dest.Employees, act => act.Ignore());
        //            CreateMap<Religion, ReligionVM>();
        //        }
        //        public void LicenceTypeHR()
        //        {
        //            CreateMap<LicenceTypeHRVM, LicenceTypeHR>()
        //                            .ForMember(dest => dest.EmployeeLicenceDatas, act => act.Ignore())
        //                            .ForMember(dest => dest.LicenceKindHRs, act => act.Ignore());
        //            CreateMap<LicenceTypeHR, LicenceTypeHRVM>();
        //        }

        //        public void Department()
        //        {
        //            CreateMap<DepartmentVM, Department>()
        //                            .ForMember(dest => dest.department, act => act.Ignore())
        //                            .ForMember(dest => dest.Departments, act => act.Ignore())
        //                            .ForMember(dest => dest.EmployeeDepartments, act => act.Ignore())
        //                            .ForSourceMember(x => x.ParentName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.ParentEnName, xx => xx.Ignore());
        //            CreateMap<Department, DepartmentVM>()
        //              .ForMember(dest => dest.ParentName, act => act.MapFrom(x => x.department.Name))
        //              .ForMember(dest => dest.ParentEnName, act => act.MapFrom(x => x.department.EnName))
        //              .ForMember(dest => dest.ParentId, act => act.MapFrom(x => x.department.ID));

        //        }
        //        public void ArchiveSettingHR()
        //        {
        //            CreateMap<ArchiveSettingHRVM, ArchiveSettingHR>()
        //                            .ForMember(dest => dest.archiveSettingHRs, act => act.Ignore())
        //                            .ForMember(dest => dest.Parent, act => act.Ignore())
        //                            .ForSourceMember(x => x.ParentName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.ParentEnName, xx => xx.Ignore());
        //            CreateMap<ArchiveSettingHR, ArchiveSettingHRVM>()
        //              .ForMember(dest => dest.ParentName, act => act.MapFrom(x => x.Parent.Name))
        //              .ForMember(dest => dest.ParentEnName, act => act.MapFrom(x => x.Parent.EnName));

        //        }
        //        public void CertificationSpecific()
        //        {
        //            CreateMap<CertificationSpecificVM, CertificationSpecific>()
        //                            .ForMember(dest => dest.CertificationSpecDeparts, act => act.Ignore())
        //                            .ForMember(dest => dest.EmployeeQualifications, act => act.Ignore())
        //                            .ForMember(dest => dest.CertificationType, act => act.Ignore())
        //                            .ForSourceMember(x => x.CertificationTypeEnName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.CertificationTypeName, xx => xx.Ignore());
        //            CreateMap<CertificationSpecific, CertificationSpecificVM>()
        //              .ForMember(dest => dest.CertificationTypeName, act => act.MapFrom(x => x.CertificationType.Name))
        //              .ForMember(dest => dest.CertificationTypeEnName, act => act.MapFrom(x => x.CertificationType.EnName))
        //              .ForMember(dest => dest.CertificationTypeID, act => act.MapFrom(x => x.CertificationType.ID));

        //        }
        //        public void CertificationSpecDepart()
        //        {
        //            CreateMap<CertificationSpecDepartVM, CertificationSpecDepart>()
        //                            .ForMember(dest => dest.EmployeeQualifications, act => act.Ignore())
        //                            .ForMember(dest => dest.CertificationSpecific, act => act.Ignore())
        //                            .ForSourceMember(x => x.CertificationSpecificEnName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.CertificationSpecificName, xx => xx.Ignore());
        //            CreateMap<CertificationSpecDepart, CertificationSpecDepartVM>()
        //              .ForMember(dest => dest.CertificationSpecificName, act => act.MapFrom(x => x.CertificationSpecific.Name))
        //              .ForMember(dest => dest.CertificationSpecificEnName, act => act.MapFrom(x => x.CertificationSpecific.EnName))
        //              .ForMember(dest => dest.CertificationSpecificID, act => act.MapFrom(x => x.CertificationSpecific.ID));

        //        }
        //        public void Employee()
        //        {
        //            CreateMap<EmployeeVM, Employee>()
        //                            .ForMember(dest => dest.Area, act => act.Ignore())
        //                            .ForMember(dest => dest.Country, act => act.Ignore())

        //                            .ForMember(dest => dest.Religion, act => act.Ignore())

        //                            .ForMember(dest => dest.Nationality, act => act.Ignore())
        //                            .ForMember(dest => dest.MotivationEmployees, act => act.Ignore())
        //                            .ForMember(dest => dest.MaritalStatu, act => act.Ignore())
        //                            .ForMember(dest => dest.EmployeeLicenceDatas, act => act.Ignore())
        //                            .ForMember(dest => dest.EmployeeJobDatas, act => act.Ignore())
        //                            .ForMember(dest => dest.EmployeeExperiences, act => act.Ignore())
        //                            .ForMember(dest => dest.EmployeeContractDurations, act => act.Ignore())
        //                            .ForMember(dest => dest.EmployeeDepartments, act => act.Ignore())
        //                            .ForMember(dest => dest.EmployeeGuid, act => act.Ignore())
        //                            .ForMember(dest => dest.BloodGroup, act => act.Ignore())
        //.ForMember(destination => destination.Gender, opt => opt.MapFrom(source => (int?)source.Gender));
        //            ////.ForMember(dest => dest.Gender, act => act.Condition(src =>
        //            ////(src.Gender != null)));


        //            CreateMap<Employee, EmployeeVM>()
        //              .ForMember(dest => dest.Gender, act => act.MapFrom(x => (Gender)x.Gender))
        //                            .ForSourceMember(x => x.EmployeeGuid, xx => xx.Ignore())
        //.ForMember(destination => destination.Gender, opt => opt.MapFrom(source => (Gender?)source.Gender));



        //        }


        //        public void CertificationType()
        //        {
        //            CreateMap<CertificationTypeVM, CertificationType>()
        //                            .ForMember(dest => dest.EmployeeQualifications, act => act.Ignore())
        //                            .ForMember(dest => dest.CertificationSpecifics, act => act.Ignore());


        //            CreateMap<CertificationType, CertificationTypeVM>();

        //        }
        //        public void MaritalStatu()
        //        {
        //            CreateMap<MaritalStatuVM, MaritalStatu>()
        //                            .ForMember(dest => dest.Employees, act => act.Ignore());


        //            CreateMap<CertificationType, CertificationTypeVM>();

        //        }
        //        public void JobName()
        //        {
        //            CreateMap<JobNameVM, JobName>()
        //                            .ForMember(dest => dest.JobFunctions, act => act.Ignore())
        //                            .ForMember(dest => dest.EmployeeJobDatas, act => act.Ignore());


        //            CreateMap<JobName, JobNameVM>();

        //        }
        //        public void CarrerField()
        //        {
        //            CreateMap<CarrerFieldVM, CarrerField>()
        //                            .ForMember(dest => dest.EmployeeJobDatas, act => act.Ignore());


        //            CreateMap<CarrerField, CarrerFieldVM>();

        //        }
        //        public void MotivationType()
        //        {
        //            CreateMap<MotivationTypeVM, MotivationType>()
        //                            .ForMember(dest => dest.MotivationEmployees, act => act.Ignore());
        //            CreateMap<MotivationType, MotivationTypeVM>();

        //        }

        //        public void JobFunction()
        //        {
        //            CreateMap<JobFunctionVM, JobFunction>()
        //                      .ForMember(dest => dest.EmployeeJobDatas, act => act.Ignore())
        //                      .ForMember(dest => dest.JobName, act => act.Ignore())
        //                            .ForSourceMember(x => x.JobNameEnName, xx => xx.Ignore())
        //                            .ForSourceMember(x => x.JobNameName, xx => xx.Ignore());


        //            CreateMap<JobFunction, JobFunctionVM>()
        //              .ForMember(dest => dest.JobNameEnName, act => act.MapFrom(x => x.JobName.EnName))
        //              .ForMember(dest => dest.JobNameName, act => act.MapFrom(x => x.JobName.Name));

        //        }
        #endregion

        public static void Run()
        {
            Mapper.Initialize(a =>
            {
                a.AddProfile<AutomapperWebProfile>();
            });
        }
    }
}
