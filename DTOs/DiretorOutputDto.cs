public class DiretorOutputDto {
    public long Id { get; set; }

    public string Nome { get; set; }

    public DiretorOutputDto(long id, string nome) {
        Id = id;
        Nome = nome;
    }
}