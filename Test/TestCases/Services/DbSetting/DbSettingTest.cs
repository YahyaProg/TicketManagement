
using Infrastructure;
using Infrastructure.Repositories.Setting;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.DbSetting;

public class DbSettingTest
{
    private readonly Mock<DBContext> _contextMoq = new();


    //[Fact]
    //public void GetSetting_NotEmpty()
    //{
    //    //Arrange
    //    var data = new List<Core.Entities.DbSetting>(){
    //        new(){Id=1, Key="name", Value="mikaeel"}
    //    };

    //    _contextMoq.Setup(x => x.DbSettings).ReturnsDbSet(data);

    //    //Act
    //    var dbSetting = new DbSettings(_contextMoq.Object);
    //    var settingValue = dbSetting.GetSetting("name");

    //    //Assert
    //    Assert.NotEmpty(settingValue);
    //    Assert.Equal("mikaeel", settingValue);
    //}

    [Fact]
    public void GetAllSettings_NotEmpty()
    {
        //Arrange
        var data = new List<Core.Entities.DbSetting>(){
            new(){Id=1, Key="name", Value="mikaeel"}
        };

        _contextMoq.Setup(x => x.DbSettings).ReturnsDbSet(data);

        //Act
        var dbSetting = new DbSettings(_contextMoq.Object);
        var settingValue = dbSetting.GetAllSettings();

        //Assert
        Assert.NotEmpty(settingValue);
    }
    [Fact]
    public void AddSetting_Success()
    {
        //Arrange
        var data = new List<Core.Entities.DbSetting>(){
            new(){Id=1, Key="name", Value="mikaeel"}
        };

        _ = _contextMoq.Setup(x => x.DbSettings.Add(It.IsAny<Core.Entities.DbSetting>()));
        _ = _contextMoq.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));

        _contextMoq.Setup(x => x.DbSettings).ReturnsDbSet(data);
        // .ReturnsAsync(1);

        //Act
        var dbSetting = new DbSettings(_contextMoq.Object);
        var settingValue = dbSetting.AddSetting("name", "mikaeel", "core");

        //Assert
        Assert.True(settingValue);
    }
}