using DataRetriever.DataStorage;
using DataRetriever.Dtos;
using DataRetriever.Factory;
using DataRetriever.Services.Concrete;
using DataRetriever.Services.Interfaces;
using Moq;

namespace DataRetriever.Tests
{
    public class DataRetrieverServiceTests
    {
        private Mock<IDataStorageFactory> _factoryMock;
        private Mock<IDataStorage> _cacheMock;
        private Mock<IDataStorage> _fileMock;
        private Mock<IDataStorage> _dbMock;
        private IDataRetrieverService _service;

        [SetUp]
        public void Setup()
        {
            _factoryMock = new Mock<IDataStorageFactory>();

            _cacheMock = new Mock<IDataStorage>();
            _cacheMock.Setup(s => s.StorageType).Returns(DataStorageType.Cache);

            _fileMock = new Mock<IDataStorage>();
            _fileMock.Setup(s => s.StorageType).Returns(DataStorageType.File);

            _dbMock = new Mock<IDataStorage>();
            _dbMock.Setup(s => s.StorageType).Returns(DataStorageType.Database);

            var factory = new DataStorageFactory(new[] {
                _cacheMock.Object,
                _fileMock.Object,
                _dbMock.Object
            });

            _service = new DataRetrieverService(factory);
        }

        [Test]
        public async Task CreateDataAndThenGetIt()
        {
            var inputDto = new CreateDataDto { Value = "My Test Value" };
            DataItem? savedItem = null;

            _dbMock.Setup(s => s.SaveDataAsync(It.IsAny<DataItem>()))
           .Callback<DataItem>(d => savedItem = d)
           .Returns(Task.CompletedTask);

            _dbMock.Setup(s => s.GetDataAsync(It.IsAny<string>()))
           .ReturnsAsync(() => savedItem);
            
            var createdItem = await _service.CreateDataAsync(inputDto);
            var retrievedItem = await _service.GetDataAsync(createdItem.Id);

            Assert.That(retrievedItem, Is.Not.Null);
            Assert.That(retrievedItem!.Id, Is.EqualTo(createdItem.Id));
            Assert.That(retrievedItem.Value, Is.EqualTo(createdItem.Value));
        }

        [Test]
        public async Task GetDataFromCache()
        {
            var expected = new DataItem { Id = "123", Value = "Test" };
            _cacheMock.Setup(s => s.GetDataAsync(expected.Id)).ReturnsAsync(expected);

            var result = await _service.GetDataAsync(expected.Id);

            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public async Task GetDataReturnsDataFromDBAndUpdatesCacheAndFile()
        {
            var expected = new DataItem { Id = "123", Value = "db" };

            _dbMock.Setup(s => s.GetDataAsync(expected.Id)).ReturnsAsync(expected);

            var result = await _service.GetDataAsync(expected.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo(expected.Id));
            Assert.That(result.Value, Is.EqualTo(expected.Value));

            _cacheMock.Verify(s => s.SaveDataAsync(It.Is<DataItem>(d => d.Id == expected.Id)), Times.Once);
            _fileMock.Verify(s => s.SaveDataAsync(It.Is<DataItem>(d => d.Id == expected.Id)), Times.Once);
        }

        [Test]
        public async Task GetDataReturnsDataFromFileAndUpdatesCache()
        {
            var expected = new DataItem { Id = "123", Value = "db" };

            _fileMock.Setup(s => s.GetDataAsync(expected.Id)).ReturnsAsync(expected);

            var result = await _service.GetDataAsync(expected.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo(expected.Id));
            Assert.That(result.Value, Is.EqualTo(expected.Value));

            _cacheMock.Verify(s => s.SaveDataAsync(It.Is<DataItem>(d => d.Id == expected.Id)), Times.Once);
        }

        [Test]
        public async Task GetDataReturnsNullIfNotFound()
        {
            var result = await _service.GetDataAsync("notFound");

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task UpdateDataInAllStorages()
        {
            var id = "123";
            var existingItem = new DataItem { Id = id, Value = "old" };
            var updateDto = new UpdateDataDto { Value = "new" };

            _cacheMock.Setup(s => s.GetDataAsync(id)).ReturnsAsync(existingItem);
            _fileMock.Setup(s => s.GetDataAsync(id)).ReturnsAsync(existingItem);
            _dbMock.Setup(s => s.GetDataAsync(id)).ReturnsAsync(existingItem);

            await _service.UpdateDataAsync(id, updateDto);

            _cacheMock.Verify(s => s.UpdateDataAsync(It.Is<DataItem>(d => d.Id == id && d.Value == updateDto.Value)), Times.Once);
            _fileMock.Verify(s => s.UpdateDataAsync(It.Is<DataItem>(d => d.Id == id && d.Value == updateDto.Value)), Times.Once);
            _dbMock.Verify(s => s.UpdateDataAsync(It.Is<DataItem>(d => d.Id == id && d.Value == updateDto.Value)), Times.Once);
        }

    }
}
