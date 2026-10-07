-- Database schema for the library app (inferred from the queries in serviskatmani.cs)
CREATE DATABASE KutuphaneDB;
GO
USE KutuphaneDB;
GO

CREATE TABLE Kitaplar (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    KitapAdi  NVARCHAR(200) NOT NULL,
    Yazar     NVARCHAR(150) NOT NULL,
    YayinEvi  NVARCHAR(150) NULL,
    Yil       INT           NULL
);
GO
