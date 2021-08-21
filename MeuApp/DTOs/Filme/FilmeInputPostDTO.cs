using FluentValidation;

public class FilmeInputPostDTO {
    public string Titulo { get; set; }
    public long DiretorId { get; set; }
    public FilmeInputPostDTO(string titulo, long diretorId) {
        Titulo = titulo;
        DiretorId = diretorId;
    }
}

public class FilmeInputPostDTOValidator : AbstractValidator<FilmeInputPostDTO> {
  public FilmeInputPostDTOValidator() {
    RuleFor(x => x.Titulo)
      .NotEmpty()
      .WithMessage("O titulo do filme é obrigatorio");
  }
}