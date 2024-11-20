using System.ComponentModel.DataAnnotations;

namespace AssecorAssessmentBackend.Models;

public sealed class Color
{
    public uint Id { get; set; }

    [RegularExpression("^[0-9a-fA-F]+$")] 
    public string Name { get; set; } = string.Empty;
}