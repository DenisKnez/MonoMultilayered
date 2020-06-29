
namespace Project.Model
{
    public class CompanyTypeModel : BaseModel, ICompanyTypeModel
    {
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}