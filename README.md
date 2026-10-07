# Library Management (C# Windows Forms + SQL Server)

A desktop app to manage a book collection. Built as a university coursework project to practice object-oriented programming and working with a SQL Server database.

## Features

- List all books in a grid
- Add a book (title and author are required, year must be a number)
- Search books by title
- Edit a selected book
- Delete one or more selected books

## Tech

- C# (.NET Framework 4.7.2), Windows Forms
- SQL Server with `System.Data.SqlClient`
- Layered structure: model classes (`Kitap`, `Uye`, `Odunc`), a data access class (`serviskatmani`) and the form
- All SQL statements use parameters, and the connection string is read from an environment variable, so no credentials are stored in the code

## Run it

1. Create the database: run `schema.sql` in SQL Server Management Studio.
2. Optional: point the app to your server by setting the `LIBRARY_DB_CONNECTION` environment variable, for example
   `Data Source=localhost;Initial Catalog=KutuphaneDB;Integrated Security=True;`
   If it is not set, the app connects to a local SQL Server with Windows authentication.
3. Open `kütüphaneTakipSistemi.sln` in Visual Studio and press F5. Windows only.

## Notes

The `Uye` (member) and `Odunc` (loan) classes are prepared for a lending feature that is not implemented yet.
