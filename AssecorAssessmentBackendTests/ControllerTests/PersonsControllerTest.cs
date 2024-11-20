using assecor_assessment_backend.Api;
using assecor_assessment_backend.DTO;
using assecor_assessment_backend.Exceptions;
using assecor_assessment_backend.Models;
using assecor_assessment_backend.Services;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AssecorAssessmentBackendTests.ControllerTests;

public sealed class PersonsControllerTest
{

    private readonly Person[] _persons = [
        new Person { 
            Id = 1,
            LastName = "Zimmer", 
            FirstName = "Hans", 
            City = "Kuchen", 
            PostalCode = "73329", 
            ColorId = 1, 
            Color = new Color {
                Id = 1,
                Name = "blau"
            },
        },
        new Person { 
            Id = 1,
            LastName = "Hetfield", 
            FirstName = "James", 
            City = "?", 
            PostalCode = "66666", 
            ColorId = 2, 
            Color = new Color {
                Id = 2,
                Name = "black"
            },
        },
    ];
    
    [Fact]
    public void TestGetAll()
    {
        var mockService = new Mock<IPersonsService>();

        mockService.Setup(s => s.GetAllAsync())
        .Returns(new MockAsyncEnumerable<Person>(_persons));

        var controller = new PersonsController(mockService.Object);

        var result = controller.Get();
        
        Assert.IsAssignableFrom<IAsyncEnumerable<Person>>(result);
    }
    
    [Fact]
    public void TestGetByColorName()
    {
        var mockService = new Mock<IPersonsService>();

        mockService.Setup(s => s.GetByColorAsync("blau"))
            .Returns(new MockAsyncEnumerable<Person>(_persons));

        var controller = new PersonsController(mockService.Object);

        var result = controller.GetByColor("blau");
        
        Assert.IsAssignableFrom<IAsyncEnumerable<Person>>(result);
    }
    
    [Fact]
    public void TestGetByColorId()
    {
        var mockService = new Mock<IPersonsService>();

        mockService.Setup(s => s.GetByColorAsync(1))
            .Returns(new MockAsyncEnumerable<Person>(_persons));

        var controller = new PersonsController(mockService.Object);

        var result = controller.GetByColorId(1);
        
        Assert.IsAssignableFrom<IAsyncEnumerable<Person>>(result);
    }
    
    [Fact]
    public async Task TestCreation()
    {
        var mockService = new Mock<IPersonsService>();

        var newPerson = new NewPersonDTO();
        
        mockService.Setup(s => s.CreatePersonAsync(newPerson))
            .Returns(Task.FromResult(Either<Exception, Person>.Right(_persons[0])));

        var controller = new PersonsController(mockService.Object);

        var result = await controller.Post(newPerson);
        
        Assert.IsType<OkObjectResult>(result);
        
        var okResult = result as OkObjectResult;
        
        Assert.IsType<Person>(okResult.Value);
    }
    
    [Fact]
    public async Task TestCreationFailed()
    {
        var mockService = new Mock<IPersonsService>();

        var newPerson = new NewPersonDTO();

        var occurringException = new EntityNotFoundException<Color, string>("lila");
        
        mockService.Setup(s => s.CreatePersonAsync(newPerson))
            .Returns(Task.FromResult(Either<Exception, Person>.Left(occurringException)));

        var controller = new PersonsController(mockService.Object);

        var result = await controller.Post(newPerson);
        
        Assert.IsType<ObjectResult>(result);
        
        var objResult = result as ObjectResult;
        
        Assert.NotEqual(200, objResult.StatusCode);
        
        Assert.Equal(occurringException.Message, objResult.Value);
    }
}