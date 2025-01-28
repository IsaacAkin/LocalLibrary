CREATE TABLE User (
    id INT PRIMARY KEY AUTO_INCREMENT,
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    date_of_birth DATE NOT NULL,
    role TEXT NOT NULL,
    password TEXT NOT NULL
);
--@block
INSERT INTO User (
        first_name,
        last_name,
        date_of_birth,
        role,
        password
    )
VALUES (
        "Arthur",
        "Nightingale",
        "2000-05-12",
        "Admin",
        "Bookw0rm"
    ),
    (
        "John",
        "Doe",
        "1990-01-01",
        "Customer",
        "password"
    ),
    (
        "Jane",
        "Doe",
        "1995-02-12",
        "Customer",
        "specificnumber"
    ),
    (
        "Alice",
        "Smith",
        "1985-03-14",
        "Customer",
        "something"
    ),
    (
        "Bob",
        "Smith",
        "1980-07-05",
        "Customer",
        "special"
    ),
    (
        "Charlie",
        "Brown",
        "1975-11-04",
        "Customer",
        "mybirthday"
    ),
    (
        "Daisy",
        "Johnson",
        "1970-12-09",
        "Customer",
        "bestfriend"
    );
--@block
SELECT *
FROM User;
--@block
CREATE TABLE Book (
    name TEXT NOT NULL,
    author TEXT NOT NULL,
    isbn INT PRIMARY KEY AUTO_INCREMENT,
    library_acquisition_date DATE NOT NULL,
    copies INT NOT NULL
);
--@block
INSERT INTO Book (name, author, library_acquisition_date, copies)
VALUES (
        "The Great Gatsby",
        "F. Scott Fitzgerald",
        "1990-05-10",
        5
    ),
    (
        "To Kill a Mockingbird",
        "Harper Lee",
        "1992-07-11",
        3
    ),
    ("1984", "George Orwell", "1995-08-15", 4),
    (
        "Pride and Prejudice",
        "Jane Austen",
        "1998-09-20",
        2
    ),
    (
        "The Catcher in the Rye",
        "J.D. Salinger",
        "2000-10-25",
        6
    ),
    ("The Hobbit", "J.R.R. Tolkien", "2003-11-30", 7),
    ("Moby Dick", "Herman Melville", "2005-12-05", 1),
    ("War and Peace", "Leo Tolstoy", "2008-01-10", 3);
--@block
SELECT *
FROM Book;
--@block
CREATE TABLE Loan (
    id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT NOT NULL,
    book_isbn INT NOT NULL,
    due_date DATE NOT NULL,
    FOREIGN KEY (user_id) REFERENCES User(id),
    FOREIGN KEY (book_isbn) REFERENCES Book(isbn)
);
--@block
SELECT *
FROM loan;