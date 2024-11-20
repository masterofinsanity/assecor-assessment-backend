using System.Globalization;
using assecor_assessment_backend.DataSources;
using assecor_assessment_backend.DataSources.Csv;
using assecor_assessment_backend.DataStorage;
using assecor_assessment_backend.Extensions;
using assecor_assessment_backend.Global;
using assecor_assessment_backend.Models;
using assecor_assessment_backend.Services;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

/* ------Using csv as a data source------ */

var csvLocation = builder.Configuration.GetValue<string>("CsvFileLocation");

builder.Services.AddSingleton<IApplicationDataSource>(new CsvDataSource(csvLocation, new CsvDataSource.Options
{
    ReaderConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        Delimiter = ",",
        HasHeaderRecord = false,
        TrimOptions = TrimOptions.Trim,
        
        //Skip invalid rows to prevent any exceptions
        ShouldSkipRecord = record =>
        {
            if (record.Row.ColumnCount < 4)
            {
                return true;
            }

            return false;
        }
    },
    ConfigureContext = context =>
    {
        context.RegisterClassMap<PersonMap>();
    }
}));

/* ------Using File as a source for the persons / people------ */

builder.Services.AddSingleton<IApplicationDataStorage, FileDataStorage>();

/* ------Uncomment & deactivate previous call to use in memory db instead (should be changed to persistent db):------ */

/*
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("InMemoryDb");
});
builder.Services.AddScoped<IApplicationDataStorage, DatabaseDataStorage>();
*/

//Services built on top of implementations hidden behind interface:

builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IPersonsService, PersonsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

//Adding default colors to selected data storage:

app.AddDefaultColors();

/* ------Uncomment if database is selected to import data from file------ */
//app.ImportPeople();

app.Run();
