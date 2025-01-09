namespace MbdcLocalBudgetsDomain.Entities.Olap;

public class Locality : EfBaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public LocalityStatus Status { get; set; }
    public Guid? ParentDistrictId { get; set; }
    public District ParentDistrict { get; set; }
    public long? Population { get; set; }
    public ICollection<OlapBudgetReport> BudgetReports { get; set; }
}

public enum LocalityStatus
{
    None = 0,
    Area = 2,
    City = 3,
    MunicipalityDistrict = 4,
    Municipality = 5,
    CityAgglomeration = 6,
    Commune = 8,
    CommuneAgglomeration = 9
}
/*
 *În clasificatorul CUATM statutul localităţilor este indicat prin interme­diul cifrelor:

   2 – raion, unitate administrativ-teritorială de nivelul doi;

   3 – oraş;

   4 – sectorul municipiului;

   5 – mu­nicipiu;

   6 – localitate din componenţa oraşului (municipiului);

   8 – sat (comu­nă);

   9 – localitate din componenţa satului (comu­nei).
 */