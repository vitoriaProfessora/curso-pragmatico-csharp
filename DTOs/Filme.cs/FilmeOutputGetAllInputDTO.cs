public class FilmeOutputPutGetAllDTO {
    public long Id { get; set; }
    public string Titulo { get; set; }

    public FilmeOutputPutGetAllDTO(long id, string titulo) {
        Id = id;
        Titulo = titulo;
    }
}