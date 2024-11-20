using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace AssecorAssessmentBackend.DataSources;

public sealed class CsvDataSource : IApplicationDataSource
{
    public sealed class Options
    {
        public CultureInfo CultureInfo { get; set; } = CultureInfo.InvariantCulture;
        public Action<CsvContext> ConfigureContext { get; set; } = (c) => { };
        
        public CsvConfiguration? ReaderConfiguration { get; set; } 
    }
    
    private readonly string _path;
    
    private readonly Options _options;

    public CsvDataSource(string csvFilePath, Options options)
    {
        _path = csvFilePath;
        _options = options;

        if (!File.Exists(_path))
        {
            throw new FileNotFoundException($"File {_path} not found");
        }
    }
    
    public IEnumerable<T> GetAll<T>()
    {
        using var reader = CreateReader();
        
        return reader.GetRecords<T>().ToList();
    }

    public async IAsyncEnumerable<T> AsAsyncEnumerable<T>()
    {
        using var reader = CreateReader();

        await foreach (var element in reader.GetRecordsAsync<T>())
        {
            yield return element;
        };
    }

    private CsvReader CreateReader()
    {
        var reader = new StreamReader(_path);

        CsvReader csv;

        if (_options.ReaderConfiguration == null)
        {
            csv = new CsvReader(reader, _options.CultureInfo);
        }
        else
        {
            csv = new CsvReader(reader, _options.ReaderConfiguration);
        }
        
        _options.ConfigureContext.Invoke(csv.Context);
        
        return csv;
    }
    
}
