using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace XUnit.Practice.Tests.Practice
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_TwoNumbers_returnSum()
        {
            //Arrange 
            var calculator = new Calculator();

            //Act
            var result = calculator.Add(2, 3);

            //assert
            
            Assert.Equal(5, result);
        }

        [Fact]
        public void Divide_ByZero_ThrowExceptio()
        {
            var calculator = new Calculator();

            Assert.Throws<ArgumentException>(() =>
                calculator.Divide(10, 0));
        }

        [Theory]
        [InlineData(2, 3, 5)]
        [InlineData(5, 5, 10)]
        public void Add_MultipleInputs_ReturnsCorrectSum(int a, int b, int expected)
        {
            var calculator = new Calculator();

            var result = calculator.Add(a, b);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-5, -5, -10)]
        [InlineData(-10, 5, -5)]
        public void Add_NegativeNumbers_ReturnsCorrectSum(int a, int b, int expected)
        {
            // Arrange 
            var calculator = new Calculator();
            //Act
            var result = calculator.Add(a,b);
            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(100, 0)]
        public void Divide_ByZero_WithTheory_ThrowsException(int a, int b)
        {
            //Arrange
            var calculator = new Calculator();
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                 calculator.Divide(a, b));
        }

        [Theory]
        [InlineData(5, 3, 2)]
        [InlineData(3, 5, -2)]
        [InlineData(0, 0, 0)]
        public void Subtract_TwoNums_ReturnValue(int a, int b, int expected)
        {
            // Arrange 
            var calculator = new Calculator();
            //Act
            var result = calculator.Subtract(a, b);
            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(4, true)]
        [InlineData(7, false)]
        public void IsEven_ReturnTrueOrFalse(int num, bool expected)
        {
            // Arrange 
            var calculator = new Calculator();
            //Act
            var result = calculator.IsEven(num);
            //Assert
            Assert.Equal(expected,result);
        }

        [Theory]
        [InlineData(95,"A")]
        [InlineData(70,"C")]
        public void GetGrade_ReturnGrade(int marks, string expected)
        {
            // Arrange 
            var calculator = new Calculator();
            //Act
            var result = calculator.GetGrade(marks);
            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public void GetGrade_ReturnException(int marks)
        {
            // Arrange 
            var calculator = new Calculator();
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() =>
                calculator.GetGrade(marks));
        }

    }
}
