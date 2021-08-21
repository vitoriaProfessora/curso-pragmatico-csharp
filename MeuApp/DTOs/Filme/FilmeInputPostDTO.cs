using FluentValidation;

public class FilmeInputPostDTO {
    public string Titulo { get; set; }
    public long DiretorId { get; set; }
}

public class FilmeInputPostDTOValidator : AbstractValidator<FilmeInputPostDTO> {
  public FilmeInputPostDTOValidator() {
    RuleFor(x => x.Titulo)
      .NotEmpty()
      .WithMessage("O titulo do filme é obrigatorio");
  }
}