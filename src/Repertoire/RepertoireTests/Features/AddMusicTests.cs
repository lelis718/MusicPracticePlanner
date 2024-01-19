using Microsoft.AspNetCore.Http;
using Moq;
using MusicPracticePlanner.Base.ServicePrimitives.Integration;
using RepertoireDomain;
using RepertoireDomain.Entities;
using RepertoireService;
using StorageService;

namespace RepertoireTests;

public class AddMusicTests
{
    private readonly Mock<IStorageService> fileServerMock;
    private readonly Mock<IRepertoireRepository> repertoryRepositoryMock;
    private readonly Mock<IBus> busMock;

    public AddMusicTests()
    {
        fileServerMock = new();
        repertoryRepositoryMock = new();
        busMock = new();
    }

    [Fact]
    public async Task AssertThatMusicWasAddedCorrectlyForAnExistentRepertoire()
    {
        // Arrange
        var fileMock = new Mock<IFormFile>();

        var studentId = Guid.NewGuid();
        var existantRepertory = Repertoire.Create(studentId);

        fileServerMock.Setup(x=>x.Save(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>())).ReturnsAsync(new FileReference("/some/file/reference"));
        repertoryRepositoryMock.Setup(x=> x.FindRepertoireByStudent(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(existantRepertory);
        
        var cmd = new AddMusic.Command(Guid.NewGuid(), "Some test music", fileMock.Object);
        var handlerToTest = new AddMusic.Handler(fileServerMock.Object, repertoryRepositoryMock.Object, busMock.Object);

        // Act
        Guid result = await handlerToTest.Handle(cmd, CancellationToken.None);

        // Assert
        Assert.NotEqual(result, Guid.Empty);
        Assert.NotEmpty(existantRepertory.Musics);

        busMock.Verify(item => item.Send(It.IsAny<MidiFileAdded>()), Times.Once);
        repertoryRepositoryMock.Verify(item => item.Save(It.IsAny<Repertoire>(), It.IsAny<CancellationToken>()), Times.Once);

        fileServerMock.Verify(item => item.Save(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    [Fact]
    public async Task AssertThatNewRepertoryIsCreatedIfNonExistsWhenAddingNewMusic()
    {
        // Arrange
        var fileMock = new Mock<IFormFile>();
        fileServerMock.Setup(x=>x.Save(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>())).ReturnsAsync(new FileReference("/some/file/reference"));
        
        var cmd = new AddMusic.Command(Guid.NewGuid(), "Some test music", fileMock.Object);
        var handlerToTest = new AddMusic.Handler(fileServerMock.Object, repertoryRepositoryMock.Object, busMock.Object);

        // Act
        Guid result = await handlerToTest.Handle(cmd, CancellationToken.None);

        // Assert
        Assert.NotEqual(result, Guid.Empty);

        busMock.Verify(item => item.Send(It.IsAny<MidiFileAdded>()), Times.Once);
        repertoryRepositoryMock.Verify(item => item.Save(It.IsAny<Repertoire>(), It.IsAny<CancellationToken>()), Times.Once);
        fileServerMock.Verify(item => item.Save(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
