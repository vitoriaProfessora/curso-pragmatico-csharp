using System.Collections.Generic;

public class DiretorListOutputGetAllDTO {
        public int CurrentPage { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }

        public List<DiretorOutputGetAllDTO> Items { get; init; }
}

public class DiretorOutputGetAllDTO {
    public long Id { get; set; }
    public string Nome { get; set; }

    public DiretorOutputGetAllDTO(long id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}