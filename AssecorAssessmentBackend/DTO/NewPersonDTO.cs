using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using assecor_assessment_backend.Models;

namespace assecor_assessment_backend.DTO;

public sealed class NewPersonDTO
{
    [RegularExpression("^([A-Za-zŽžÀ-ÿ\\-\\s]+)$")] 
    public string FirstName { get; set; } = string.Empty;

    [RegularExpression("^([A-Za-zŽžÀ-ÿ\\-\\s]+)$")]
    public string LastName { get; set; } = string.Empty;
    
    [RegularExpression("^([0-9]{5})$")]
    public string PostalCode { get; set; } = string.Empty;

    [RegularExpression("^([A-Za-zŽžÀ-ÿ0-9\\-\\s]+)$")]
    public string City { get; set; } = string.Empty;
    
    public string? Color { get; set; }

    public static explicit operator Person(NewPersonDTO person)
    {
        var result = new Person();
        
        result.FirstName = person.FirstName;
        result.LastName = person.LastName;
        result.PostalCode = person.PostalCode;
        result.City = person.City;
        return result;
    }
}