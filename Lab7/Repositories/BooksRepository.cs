using Lab7.Context;
using Lab7.Entities;
using Lab7.Utils;

namespace Lab7.Repositories;

public class BooksRepository
{
    private LibraryDbContext _dbContext;

    public BooksRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Book? GetByIsbn(string isbn)
    {
        return _dbContext.Books.Find(isbn);
    }

    public void Create(string isbn, string title, string authors, string publishers, string publicationYear)
    {
        if (ValidateFields.IsbnExists(_dbContext, isbn))
        {
            throw new ArgumentException("ISBN already exists");
        }
        
        var publishersSet = _dbContext.Publishers;
        
        var publisher = publishersSet.FirstOrDefault(p => p.Name.Equals(publishers));
        if (publisher is null)
        {
            var publisherRepo = new PublishersRepository(_dbContext);
            publisherRepo.Create(publishers);
            
            publisher = publishersSet.FirstOrDefault(p => p.Name.Equals(publisher));
        }

        var publisherId = publisher!.Id;
        var publicationYearShort = short.Parse(publicationYear);

        _dbContext.Add(new Book
        {
            Isbn = isbn,
            Title = title,
            Authors = authors,
            PublisherCode = publisherId,
            PublicationYear = publicationYearShort
        });
        
        Save();
    }

    public void Delete(string isbn)
    {
        var element = _dbContext.Books.Find(isbn);

        if (element is null) throw new ArgumentException("There's no books with this ISBN");
        
        _dbContext.Remove(element);
        
        Save();
    }

    public void Update(string isbnInitial, string isbn, string title, string authors, string publishers, string publicationYear)
    {
        var isInitialIsbnExist = ValidateFields.IsbnExists(_dbContext, isbnInitial);
        
        var isIsbnExist = ValidateFields.IsbnExists(_dbContext, isbn) && !isbnInitial.Equals(isbn);
        if (!isInitialIsbnExist || isIsbnExist)
        {
            throw new ArgumentException("""
                                        There are two conditions under which this error can occur:
                                        1. Initial ISBN does not exist;
                                        2. You are trying to change ISBN, and new ISBN already exist.
                                        """);
        }
        
        if (!isbnInitial.Equals(isbn))
        {
            UpdateIsbn(isbnInitial, isbn);
        }
        
        var publishersSet = _dbContext.Publishers;
        
        var publisher = publishersSet.FirstOrDefault(p => p.Name.Equals(publishers));
        if (publisher is null)
        {
            var publisherRepo = new PublishersRepository(_dbContext);
            publisherRepo.Create(publishers);
            
            publisher = publishersSet.FirstOrDefault(p => p.Name.Equals(publisher));
        }

        var publisherId = publisher!.Id;
        var publicationYearShort = short.Parse(publicationYear);

        var initObject = _dbContext.Books.Find(isbn)!;
        
        initObject.Title = title;
        initObject.Authors = authors;
        initObject.PublisherCode = publisherId;
        initObject.PublicationYear = publicationYearShort;
        
        Save();
    }

    private void UpdateIsbn(string isbnInitial, string isbn)
    {
        var initObject = _dbContext.Books.Find(isbnInitial)!;

        var copy = new Book
        {
            Isbn = isbn,
            Title = initObject.Title,
            Authors = initObject.Title,
            PublisherCode = initObject.PublisherCode,
            PublicationYear = initObject.PublicationYear
        };
        
        Delete(isbnInitial);
        Save();
        
        _dbContext.Add(copy);
        
        Save();
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}