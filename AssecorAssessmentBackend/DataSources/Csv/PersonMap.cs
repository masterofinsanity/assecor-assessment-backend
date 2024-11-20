using AssecorAssessmentBackend.Models;
using CsvHelper.Configuration;

namespace AssecorAssessmentBackend.DataSources.Csv;

public sealed class PersonMap : ClassMap<Person>
{
    public PersonMap()
    {
        Map(m => m.LastName).Index(0);
        Map(m => m.FirstName).Index(1);
        Map(m => m.PostalCode).Index(2).Convert(args =>
        {
            var value = args.Row.GetField(2);

            if (value == null)
            {
                return "";
            }
            
            var spaceIndex = value.IndexOf(' ');

            if (spaceIndex <= 0)
            {
                return "";
            }
            
            return value.Substring(0, spaceIndex);
        });
        Map(m => m.City).Index(2).Convert(args =>
        {
            var value = args.Row.GetField(2);

            if (value == null)
            {
                return "";
            }
            
            var spaceIndex = value.IndexOf(' ');

            if (spaceIndex < 0)
            {
                return "";
            }
            
            return value.Substring(spaceIndex + 1);
        });
        Map(m => m.ColorId).Index(3);
        Map(m => m.Id).Index(0).Convert(args => args.Row.Context.Parser?.RawRow ?? 0);
    }
}