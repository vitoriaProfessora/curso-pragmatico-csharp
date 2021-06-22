public class DiretorOutputPutDto {
    public long Id { get; set; }
    public string Nome { get; set; }

    public DiretorOutputPutDto(long id, string nome) {
        Id = id;
        Nome = nome;
    }
}