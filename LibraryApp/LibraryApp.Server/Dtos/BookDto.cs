namespace LibraryApp.Server.Dtos;

public record BookDto(
    int Id,
    string Title,
    int AuthorId);
