using MySql.Data.MySqlClient;

public class Database
{
    private Database() { }

    public static Database Instance { get; } = new Database();
    private string? connectionString;

    public void SetConnectionString(string? connectionString)
    {
        this.connectionString = connectionString;
    }

    private MySqlConnection GetOpenConnection()
    {
        if (connectionString == null)
        {
            throw new InvalidOperationException("Connection string not defined.");
        }

        MySqlConnection databaseConnection = new MySqlConnection(connectionString);
        databaseConnection.Open();
        return databaseConnection;
    }

    // Adds a new book to the book table when given a name and author
    public void AddBook(string name, string author, int copies)
    {
        try
        {
            MySqlConnection conn = GetOpenConnection();

            string bookToAdd = "INSERT INTO book (name, author, copies) VALUES (@name, @author, @copies)";
            MySqlCommand command = new MySqlCommand(bookToAdd, conn);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@author", author);
            command.Parameters.AddWithValue("@copies", copies);
            command.ExecuteNonQuery();

            conn.Close();
        }
        catch (MySqlException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }

    // NEEDS REFACTORING: Make it properly show the book information without throwing an exception
    public List<Book> AddBookConfirmation(string name, string author)
    {
        try
        {
            MySqlConnection conn = GetOpenConnection();

            string query = "SELECT name, author, isbn, library_acquisition_date, copies FROM Book WHERE name = @name AND author = @author";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@author", author);
            MySqlDataReader column = command.ExecuteReader();

            List<Book> books = new List<Book>();
            while (column.Read())
            {
                Book book = new Book();
                book.Name = column[0].ToString();
                book.Author = column[1].ToString();
                book.Isbn = Convert.ToInt32(column[2]);
                book.LibraryAcquisitionDate = DateTime.Parse(column[3].ToString());
                book.Copies = Convert.ToInt32(column[4]);
                books.Add(book);
            }

            column.Close();
            conn.Close();
            return books;
        }
        catch (MySqlException exception)
        {
            return ErrorList(exception.Message);
        }
    }

    /// Deletes a book from the book table when given a name and isbn number
    /// NEEDS REFACTORING: Make it into a button that deletes the book when clicked
    public void DeleteBook(string name, int isbn)
    {
        try
        {
            MySqlConnection conn = GetOpenConnection();

            string bookToDelete = "DELETE FROM Book WHERE name = @name AND isbn = @isbn";
            MySqlCommand command = new MySqlCommand(bookToDelete, conn);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@isbn", isbn);
            command.ExecuteNonQuery();

            conn.Close();
        }
        catch (MySqlException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }

    // NEEDS REFACTORING: Make it properly show the book information without throwing an exception
    public List<Book> DeleteBookConfirmation(string name, int isbn)
    {
        try
        {
            MySqlConnection conn = GetOpenConnection();

            string query = "SELECT name, author, isbn FROM Book WHERE name = @name AND isbn = @isbn";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@isbn", isbn);
            MySqlDataReader column = command.ExecuteReader();

            List<Book> books = new List<Book>();
            while (column.Read())
            {
                Book book = new Book();
                book.Name = column[0].ToString();
                book.Author = column[1].ToString();
                books.Add(book);
            }

            column.Close();
            conn.Close();
            return books;
        }
        catch (MySqlException exception)
        {
            return ErrorList(exception.Message);
        }
    }

    private List<Book> ErrorList(string message)
    {
        Book book = new Book();
        book.Name = message;
        book.Author = message;
        book.Copies = int.Parse(message);
        return new List<Book>() { book };
    }

    // Gets all books in the book table
    public List<Book> GetAllBooks()
    {
        try
        {
            MySqlConnection conn = GetOpenConnection();

            string query = "SELECT name, author, isbn, library_acquisition_date, copies FROM Book";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader column = command.ExecuteReader();

            List<Book> books = new List<Book>();
            while (column.Read())
            {
                Book book = new Book();
                book.Name = column[0].ToString();
                book.Author = column[1].ToString();
                book.Isbn = Convert.ToInt32(column[2]);
                book.LibraryAcquisitionDate = DateTime.Parse(column[3].ToString());
                book.Copies = Convert.ToInt32(column[4]);
                books.Add(book);
            }

            column.Close();
            conn.Close();

            return books;
        }
        catch (MySqlException exception)
        {
            return ErrorList(exception.Message);
        }
    }
}
