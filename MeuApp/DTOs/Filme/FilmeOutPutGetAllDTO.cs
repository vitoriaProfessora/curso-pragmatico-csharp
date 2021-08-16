using System.Collections.Generic;

public class FilmeListOutputGetAllDTO {
    public int CurrentPage { get; init; }

    public int TotalItems { get; init; }

    public int TotalPages { get; init; }

    public List<FilmeOutPutGetAllDTO> Items { get; init; }
}

public class FilmeOutPutGetAllDTO {
    public long Id { get; set; }
    public string Titulo { get; set; }

    public FilmeOutPutGetAllDTO(long id, string titulo) {
        Id = id;
        Titulo = titulo;
    }
}