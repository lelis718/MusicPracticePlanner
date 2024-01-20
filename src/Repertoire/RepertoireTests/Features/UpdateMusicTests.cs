using Microsoft.AspNetCore.Http;
using Moq;
using MusicPracticePlanner.Base.ServicePrimitives;
using MusicPracticePlanner.Base.ServicePrimitives.Integration;
using RepertoireDomain;
using RepertoireDomain.Entities;
using RepertoireService;
using StorageService;

namespace RepertoireTests;

public class UpdateMusicTests
{
    private readonly Mock<IStorageService> fileServerMock;
    private readonly Mock<IRepertoireRepository> repertoryRepositoryMock;
    private readonly Mock<IUnitOfWork> unitOfWorkMock;

    public UpdateMusicTests()
    {
        fileServerMock = new();
        repertoryRepositoryMock = new();
        unitOfWorkMock = new();
    }

    [Fact]
    public async Task AssertThatMusicWasUpdatedWithAMidiFile()
    {
        // Arrange
        var fileMock = new Mock<IFormFile>();

        var studentId = Guid.NewGuid();
        var musicId = Guid.NewGuid();
        var existantRepertory = Repertoire.Create(studentId);
        var music = existantRepertory.AddMusic("some music", "some/file");

        fileServerMock.Setup(x=>x.Save(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>())).ReturnsAsync(new FileReference("/some/file/reference"));
        repertoryRepositoryMock.Setup(x=> x.FindRepertoireByStudent(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(existantRepertory);
        
        var cmd = new UpdateMusic.Command(studentId, music.Id, "Some test music", null, fileMock.Object);
        var handlerToTest = new UpdateMusic.Handler(fileServerMock.Object, repertoryRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        Guid result = await handlerToTest.Handle(cmd, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        Assert.Equal("Some test music", existantRepertory.Musics.First().Name);
        Assert.Equal("/some/file/reference", existantRepertory.Musics.First().MidiFile?.FileUrl);


        repertoryRepositoryMock.Verify(item => item.Save(It.IsAny<Repertoire>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(item => item.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        fileServerMock.Verify(item => item.Save(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    [Fact]
    public async Task AssertThatMusicWasUpdatedWithoutMidiFile()
    {
        // Arrange

        var studentId = Guid.NewGuid();
        var musicId = Guid.NewGuid();
        var existantRepertory = Repertoire.Create(studentId);
        var music = existantRepertory.AddMusic("some music", "some/file");

        repertoryRepositoryMock.Setup(x=> x.FindRepertoireByStudent(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(existantRepertory);
        
        var cmd = new UpdateMusic.Command(studentId, music.Id, "Some test music", "SOME OTHER NOTATION", null);
        var handlerToTest = new UpdateMusic.Handler(fileServerMock.Object, repertoryRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        Guid result = await handlerToTest.Handle(cmd, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        Assert.Equal("Some test music", existantRepertory.Musics.First().Name);
        Assert.Equal("some/file", existantRepertory.Musics.First().MidiFile?.FileUrl);
        Assert.Equal("SOME OTHER NOTATION", existantRepertory.Musics.First().ABCNotation?.NotationString);


        repertoryRepositoryMock.Verify(item => item.Save(It.IsAny<Repertoire>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(item => item.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
