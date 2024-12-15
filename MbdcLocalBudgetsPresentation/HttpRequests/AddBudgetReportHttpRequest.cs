using System.ComponentModel.DataAnnotations;

namespace MbdcLocalBudgetsPresentation.HttpRequests;

public class AddBudgetReportHttpRequest
{
    [Required]
    [MaxLength(100)]
    public string City { get; set; } = string.Empty;
    [Required]
    [Range(1970, 2100)]
    public int Year { get; set; }
    [Required]
    [Range(0, long.MaxValue)]
    public long PlannedExpenses { get; set; }
    [Required]
    [Range(0, long.MaxValue)]
    public long ActualExpenses { get; set; }
    [Required]
    [Range(0, long.MaxValue)]
    public long PlannedIncome { get; set; }
    [Required]
    [Range(0, long.MaxValue)]
    public long ActualIncome { get; set; }
}