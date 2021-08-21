using FluentValidation;

public class FilmeInputPutDTO {
    public long Id { get; set; }
    public string Titulo { get; set; }
    public long DiretorId { get; set; }
    public FilmeInputPutDTO(long id, string titulo, long diretorId) {
        Id = id;
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