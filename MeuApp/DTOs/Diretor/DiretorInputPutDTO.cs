using FluentValidation;
public class DiretorInputPutDTO {
    public string Nome { get; set; }
}

public class DiretorInputPutDtoValidator : AbstractValidator<DiretorInputPutDTO> {
    public DiretorInputPutDtoValidator() {
        RuleFor(x => x.Nome)
        .NotEmpty()
        .WithMessage("O nome do Diretor é Obrigatório");
    }
}