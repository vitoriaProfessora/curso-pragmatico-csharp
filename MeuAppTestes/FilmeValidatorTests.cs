using System;
using Xunit;
using FluentValidation.TestHelper;

namespace MeuAppTestes
{
    public class FilmeValidatorTests
    {
        [Fact]
        public void TituloDoFilmeDeveApresentarErroSeForNull()
        {
            var validator = new FilmeInputPostDTOValidator();
            var dto = new FilmeInputPostDTO { Titulo = null };
            var result = validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(filme => filme.Titulo);
        }

        [Fact]
        public void TituloDoFilmeDeveApresentarErroSeForVazio()
        {
            var validator = new FilmeInputPostDTOValidator();
            var dto = new FilmeInputPostDTO { Titulo = "" };
            var result = validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(filme => filme.Titulo);
        }

        [Theory]
        [InlineData("Teste")]
        [InlineData("Teste Teste Teste Teste")]
        public void TituloDoFilmeValidoNaoDeveConterErro(string data)
        {
            var validator = new FilmeInputPostDTOValidator();
            var dto = new FilmeInputPostDTO { Titulo = data };
            var result = validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(filme => filme.Titulo);
        }
    }
}