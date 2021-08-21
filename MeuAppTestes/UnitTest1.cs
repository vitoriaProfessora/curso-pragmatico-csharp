using System;
using Xunit;

namespace MeuAppTestes
{
    public class UnitTest1
    {
        [Fact]
        public void CriaUmDiretor()
        {
            var diretor = new Diretor("Nome Teste");
            Assert.Equal("Nome Teste", diretor.Nome);
        }
    }
}
