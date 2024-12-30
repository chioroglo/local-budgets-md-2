using MbdcLocalBudgetsDomain.Entities.Olap;

namespace MbdcLocalBudgetsDomain.Dto;
public class ExcelLocalityDto
{
    public string Code { get; set; }
    public string ParentCode { get; set; }
    public string Status { get; set; }
    public string Name { get; set; }
    public LocalityStatus GetParsedStatus()
    {
        double.TryParse(Status, out var result);

        if (result == 0)
        {
            return LocalityStatus.None;
        }
        return (LocalityStatus)result;
    }
}