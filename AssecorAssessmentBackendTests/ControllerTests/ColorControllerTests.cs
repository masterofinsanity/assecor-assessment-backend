using AssecorAssessmentBackend.Api;
using AssecorAssessmentBackend.Models;
using AssecorAssessmentBackend.Services;
using Moq;

namespace AssecorAssessmentBackendTests.ControllerTests;

public sealed class ColorControllerTests
{

    private readonly List<Color> _colors = [
        new Color()
        {
            Id = 1,
            Name = "blau"
        },
        new Color()
        {
            Id = 2,
            Name = "schwarz"
        }
    ];
    
    [Fact]
    public async Task TestGetAll()
    {
        var mockService = new Mock<IColorService>();

        mockService.Setup(s => s.GetColors(null))
        .Returns(Task.FromResult(_colors));

        var controller = new ColorsController(mockService.Object);

        var result = await controller.Get();
        
        Assert.IsAssignableFrom<List<Color>>(result);
    }
    
    [Fact]
    public async Task TestGetAllByName()
    {
        var mockService = new Mock<IColorService>();

        mockService.Setup(s => s.GetColors("schwarz"))
            .Returns(Task.FromResult(_colors));

        var controller = new ColorsController(mockService.Object);

        var result = await controller.Get("schwarz");
        
        Assert.IsAssignableFrom<List<Color>>(result);
    }
    
}