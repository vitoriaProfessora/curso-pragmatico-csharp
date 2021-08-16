using FluentValidation;

public class FilmeInputPutDTO {
    public long Id { get; set; }
    public string Titulo { get; set; }
    public long DiretorId { get; set; }
    
    public class FilmeInputPuttDTOValidator : AbstractValidator<FilmeInputPutDTO>
    {
        public FilmeInputPuttDTOValidator()
        {
            RuleFor(filme => filme.Id).NotNull().NotEmpty();
            RuleFor(filme => filme.Titulo).NotNull().NotEmpty();
            RuleFor(filme => filme.Titulo).Length(2, 250).WithMessage("Tamanho {TotalLength} inválido.");
            RuleFor(filme => filme.DiretorId).NotNull().NotEmpty();
        }
    }
}