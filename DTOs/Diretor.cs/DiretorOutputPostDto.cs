public class DiretorOutputPostDto {
    public long Id { get; set; }

    public string Nome { get; set; }

    public DiretorOutputPostDto(long id, string nome) {
        Id = id;
        Nome = nome;
    }
}