using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Sandbox.Enums
{
    [TestFixture]
    public class EnumTest
    {
        public enum AnEnum
        {
            foo, bar
        }

        public enum ExtraCommaCompiles
        {
            foo, bar,
        }

        public enum CanAssignHexValues
        {
            foo = 0x01, 
            bar = 0x02,
        }

        public enum CanAssignHexValues2
        {
            foo = 0x01,
            bar = 0x02,
        }

        public enum EnumsCanBeExplicitlyCastedToInt
        {
            foo = 1,
            bar,


        }


        [Test]
        public void EnumValuesAssignedWithSameValueShouldNotBeEqual()
        {
            Assert.AreNotEqual(AnEnum.foo, AnEnum.bar);
        }

        [Test]
        public void ComparingEnumsWithSameValueShouldNotBeEqual()
        {
            Assert.AreNotEqual(CanAssignHexValues.foo, CanAssignHexValues2.bar);
        }

        [Test]
        public void LeavingNoValueShouldDefaultStartAt0()
        {
            int wtf = (int)AnEnum.foo;
            Assert.AreEqual(0, wtf);
        }

        [Test]
        public void DefaultingInitialValueIncrementsByOne()
        {
            Assert.AreEqual(2, (int)EnumsCanBeExplicitlyCastedToInt.bar);
        }

        [Test]
        public void EnumsMustBeExplicitlyCastToIntForComparisonToInt()
        {
            Assert.AreNotEqual(2, EnumsCanBeExplicitlyCastedToInt.bar);
            Assert.AreEqual(2, (int)EnumsCanBeExplicitlyCastedToInt.bar);
        }

        [Test]
        public void ShouldParseEnumByName()
        {
            object parse = Enum.Parse(typeof(EnumsCanBeExplicitlyCastedToInt), "Bar");
            IList<string> names = new List<string>(Enum.GetNames(typeof (EnumsCanBeExplicitlyCastedToInt)));
            Assert.IsTrue(names.Contains("bar"));
            
        }
    }
}