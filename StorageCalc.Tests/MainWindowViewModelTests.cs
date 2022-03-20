using StorageCalc.ViewModels;
using System;
using Xunit;

namespace StorageCalc.Tests
{
    public class MainWindowViewModelTests
    {
        MainWindowViewModel _sut = new MainWindowViewModel();

        [Theory]
        [InlineData("3", "2", null, null, true, null, null, "3.64 TB", "1 Platte")]
        public void Calculate_ReturnsExpectedStrings_WhenGoodDataIsPassed(string txtDiskCount, string txtDiskSpace, bool? raid0, bool? raid1, bool? raid5, bool? raid6, bool? raid10, string expectedTotalSpace, string expectedFaultTolerance)
        {
            (string totalSpace, string faultTolerance) = _sut.Calculate(txtDiskCount, txtDiskSpace, raid0, raid1, raid5, raid6, raid10);

            Assert.Equal(expectedTotalSpace, totalSpace);
            Assert.Equal(expectedFaultTolerance, faultTolerance);
        }
    }
}
