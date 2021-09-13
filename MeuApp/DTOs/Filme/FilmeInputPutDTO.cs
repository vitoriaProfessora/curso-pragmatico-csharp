using FluentValidation;

public class FilmeInputPutDTO {
    public string Titulo { get; set; }
    public long DiretorId { get; set; }
    public FilmeInputPutDTO(string titulo, long diretorId) {
        Titulo = titulo;
        DiretorId = diretorId;
    }
}

public class FilmeInputPutDTOValidator : AbstractValidator<FilmeInputPutDTO> {
  public FilmeInputPutDTOValidator() {
    RuleFor(x => x.Titulo)
      .NotEmpty()
      .WithMessage("O titulo do filme é obrigatorio");
  }
}