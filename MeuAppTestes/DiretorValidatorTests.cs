using System;
using Xunit;
using FluentValidation.TestHelper;

namespace MeuAppTestes
{
    public class DiretorValidatorTests
    {
        [Fact]
        public void NomeDoDiretorDeveApresentarErroSeForNull()
        {
            var validator = new DiretorInputPostDTOValidator();
            var dto = new DiretorInputPostDTO { Nome = null };
            var result = validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(diretor => diretor.Nome);
        }

        [Fact]
        public void NomeDoDiretorDeveApresentarErroSeForVazio()
        {
            var validator = new DiretorInputPostDTOValidator();
            var dto = new DiretorInputPostDTO { Nome = "" };
            var result = validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(diretor => diretor.Nome);
        }

        [Theory]
        [InlineData("Teste")]
        [InlineData("Teste Teste Teste Teste")]
        public void NomeDoDiretorValidoNaoDeveConterErro(string data)
        {
            var validator = new DiretorInputPostDTOValidator();
            var dto = new DiretorInputPostDTO { Nome = data };
            var result = validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(diretor => diretor.Nome);
        }
    }
}
