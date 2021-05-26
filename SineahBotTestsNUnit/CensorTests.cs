using NUnit.Framework;
using SineahBot.Tools;

namespace SineahBotTestsNUnit
{
    public class CensorTests
    {
        [Test]
        public void TestUrlFilter()
        {
            string validUrl0 = "Test https://youtube.com/videoIdstuff Test test test";
            string validUrl1 = "Test https://youtube.com/videoIdstuff Test https://discord.com/channelstuff test test";
            string validUrl2 = "Test https://youtube.com/videoIdstuff Test https://discord.com/channelstuff test https://discord.com/otherchannelstuff test";
            string validUrl3 = "Test <https://youtube.com/videoIdstuff> Test test test";

            string invalidUrl0 = "Test https://not.youtube.com/videoIdstuff Test test test";
            string invalidUrl1 = "Test https://not.youtube.com/videoIdstuff Test https://not.discord.com/channelstuff test test";
            string invalidUrl2 = "Test https://not.youtube.com/videoIdstuff Test https://not.discord.com/channelstuff test https://not.discord.com/otherchannelstuff test";

            string mixedUrl0 = "Test https://youtube.com/videoIdstuff Test https://not.discord.com/channelstuff test test";
            string mixedUrl1 = "Test https://not.youtube.com/videoIdstuff Test https://discord.com/channelstuff test test";
            string mixedUrl2 = "Test https://youtube.com/videoIdstuff Test https://not.discord.com/channelstuff test https://discord.com/otherchannelstuff test";
            string mixedUrl3 = "Test https://not.youtube.com/videoIdstuff Test https://discord.com/channelstuff test https://not.discord.com/otherchannelstuff test";

            Assert.AreEqual(validUrl0, CensorManager.FilterMessageUrl(validUrl0), "Shouldn't have filtered.");
            Assert.AreEqual(validUrl1, CensorManager.FilterMessageUrl(validUrl1), "Shouldn't have filtered.");
            Assert.AreEqual(validUrl2, CensorManager.FilterMessageUrl(validUrl2), "Shouldn't have filtered.");
            Assert.AreEqual(validUrl3, CensorManager.FilterMessageUrl(validUrl3), "Shouldn't have filtered.");

            Assert.AreEqual($"Test {CensorManager.urlFilterReplace} Test test test", CensorManager.FilterMessageUrl(invalidUrl0), "Should have filtered.");
            Assert.AreEqual($"Test {CensorManager.urlFilterReplace} Test {CensorManager.urlFilterReplace} test test", CensorManager.FilterMessageUrl(invalidUrl1), "Should have filtered.");
            Assert.AreEqual($"Test {CensorManager.urlFilterReplace} Test {CensorManager.urlFilterReplace} test {CensorManager.urlFilterReplace} test", CensorManager.FilterMessageUrl(invalidUrl2), "Should have filtered.");

            Assert.AreEqual($"Test https://youtube.com/videoIdstuff Test {CensorManager.urlFilterReplace} test test", CensorManager.FilterMessageUrl(mixedUrl0), "Filtered wrong.");
            Assert.AreEqual($"Test {CensorManager.urlFilterReplace} Test https://discord.com/channelstuff test test", CensorManager.FilterMessageUrl(mixedUrl1), "Filtered wrong.");
            Assert.AreEqual($"Test https://youtube.com/videoIdstuff Test {CensorManager.urlFilterReplace} test https://discord.com/otherchannelstuff test", CensorManager.FilterMessageUrl(mixedUrl2), "Filtered wrong.");
            Assert.AreEqual($"Test {CensorManager.urlFilterReplace} Test https://discord.com/channelstuff test {CensorManager.urlFilterReplace} test", CensorManager.FilterMessageUrl(mixedUrl3), "Filtered wrong.");
        }

        [Test]
        public void TestWordCensor()
        {
            string valid0 = "Test Test test test";

            string invalid0 = "Test ass Test test test.";
            string invalid1 = "Test ass Test ass test.";
            string invalid2 = "Test asshole hoe test dick test.";

            Assert.AreEqual(valid0, CensorManager.CensorMessage(valid0), "Shouldn't have censored.");

            Assert.AreEqual($"Test *** Test test test.", CensorManager.CensorMessage(invalid0), "Should have filtered.");
            Assert.AreEqual($"Test *** Test *** test.", CensorManager.CensorMessage(invalid1), "Should have filtered.");
            Assert.AreEqual($"Test ***hole *** test **** test.", CensorManager.CensorMessage(invalid2), "Should have filtered.");
        }
    }
}