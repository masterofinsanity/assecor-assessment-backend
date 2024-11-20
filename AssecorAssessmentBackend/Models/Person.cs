using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CsvHelper.Configuration;

namespace assecor_assessment_backend.Models;

public sealed class Person
{
    public long Id { get; set; }

    [RegularExpression("^([A-Za-zŽžÀ-ÿ\\-\\s]+)$")] 
    public string FirstName { get; set; } = string.Empty;

    [RegularExpression("^([A-Za-zŽžÀ-ÿ\\-\\s]+)$")]
    public string LastName { get; set; } = string.Empty;
    
    [RegularExpression("^([0-9]{5})$")]
    public string PostalCode { get; set; } = string.Empty;

    [RegularExpression("^([A-Za-zŽžÀ-ÿ0-9\\-\\s]+)$")]
    public string City { get; set; } = string.Empty;

    [JsonIgnore]
    public uint? ColorId { get; set; }

    private Color? _color;

    [JsonIgnore]
    public Color? Color { get; set; }
    
    [JsonPropertyName("color")]
    [NotMapped]
    public string? ColorName => Color?.Name;
}