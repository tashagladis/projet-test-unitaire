using FluentAssertions;
using Xunit;

namespace WebApp.XUnit.Test
{

    public class Pitest
    {

        [Fact]
        public void TestAdd()
        {
            //Given
            Calculatrice calc = new Calculatrice();

            //When
            int result = calc.Add(0, 0);

            //Then
            result.Should().Be(0);
        }
    }

    public class Calculatrice
    {
        internal int Add(int a, int b)
        {
            return a + b;
        }
    }
}
