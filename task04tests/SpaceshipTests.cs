using Xunit;
using Moq;
using task04;

namespace task04tests;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        ISpaceship cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(100, cruiser.FirePower);
    }

    [Fact]
    public void Fighter_ShouldHaveCorrectStats()
    {
        ISpaceship fighter = new Fighter();
        Assert.Equal(100, fighter.Speed);
        Assert.Equal(50, fighter.FirePower);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void Cruiser_ShouldHaveStrongerFirePowerThanFighter()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(cruiser.FirePower > fighter.FirePower);
    }

    [Fact]
    public void Cruiser_Rotate_SetsAngleValueCorrectly()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(45);
        Assert.Equal(45, cruiser.Angle);
    }

    [Fact]
    public void Fighter_Rotate_SetsAngleValueCorrectly()
    {
        var fighter = new Fighter();
        fighter.Rotate(90);
        Assert.Equal(90, fighter.Angle);
    }

    [Fact]
    public void Cruiser_Rotate_FullCircle_SetsAngleValueCorrectly()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(360);
        Assert.Equal(0, cruiser.Angle);
    }

    [Fact]
    public void Fighter_Rotate_FullCircle_SetsAngleValueCorrectly()
    {
        var fighter = new Fighter();
        fighter.Rotate(460);
        Assert.Equal(100, fighter.Angle);
    }

    [Fact]
    public void Cruiser_MoveForward_SetsCoordinatesValuesCorrectly()
    {
        var cruiser = new Cruiser();
        cruiser.MoveForward();
        Assert.Equal(50, cruiser.XCoordinate);
        Assert.Equal(0, cruiser.YCoordinate);
    }

    [Fact]
    public void Fighter_MoveForward_SetsCoordinatesValuesCorrectly()
    {
        var fighter = new Fighter();
        fighter.MoveForward();
        Assert.Equal(100, fighter.XCoordinate);
        Assert.Equal(0, fighter.YCoordinate);
    }

    [Fact]
    public void Cruiser_MoveForward_WithAngle_SetsCoordinatesValuesCorrectly()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(45);
        cruiser.MoveForward();
        Assert.True((35.36 - cruiser.XCoordinate) < 1E-2);
        Assert.True((35.36 - cruiser.YCoordinate) < 1E-2);
    }

    [Fact]
    public void Fighter_MoveForward_WithAngle_SetsCoordinatesValuesCorrectly()
    {
        var fighter = new Fighter();
        fighter.Rotate(90);
        fighter.MoveForward();
        Assert.True((0 - fighter.XCoordinate) < 1E-2);
        Assert.True((100 - fighter.YCoordinate) < 1E-2);
    }

    [Fact]
    public void Cruiser_MoveForward_WithNegativeAngle_SetsCoordinatesValuesCorrectly()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(-45);
        cruiser.MoveForward();
        Assert.True((35.36 - cruiser.XCoordinate) < 1E-2);
        Assert.True((-35.36 - cruiser.YCoordinate) < 1E-2);
    }

    [Fact]
    public void Fighter_MoveForward_WithNegativeAngle_SetsCoordinatesValuesCorrectly()
    {
        var fighter = new Fighter();
        fighter.Rotate(-90);
        fighter.MoveForward();
        Assert.True((0 - fighter.XCoordinate) < 1E-2);
        Assert.True((-100 - fighter.YCoordinate) < 1E-2);
    }

    [Fact]
    public void Cruiser_Fire_SetsAmmoValueCorrectly()
    {
        var cruiser = new Cruiser();
        cruiser.Fire();
        Assert.Equal(9, cruiser.Ammo);
    }

    [Fact]
    public void Fighter_Fire_SetsAmmoValueCorrectly()
    {
        var fighter = new Fighter();
        fighter.Fire();
        Assert.Equal(9, fighter.Ammo);
    }
}
