--PRUEBA PARCIAL: PROGRAMACIÓN I
-- Base de datos: ParcialProgramacion1 
-- Sistema de control de productos
-- Autor: Guillermo Antonio Pérez 
--Abigail Alexandra Colocho Flamenco y René Alejandro Guevara Guevara.

--creamos la base de datos--
CREATE DATABASE ParcialProgramacion1;
GO 
USE ParcialProgramacion1; 
GO

--Crear la tabla Productos--
create table Productos ( 
IdProducto INT IDENTITY(1,1) PRIMARY KEY, 
Nombre NVARCHAR(100) NOT NULL, 
Categoria NVARCHAR(50) NOT NULL, 
Precio DECIMAL(10,2) NOT NULL, 
Existencias INT NOT NULL 
);

GO 

Insert into Productos(Nombre, Categoria, Precio, Existencias) values ('Zucaritas','Cereal', 2.0, 25);
Insert into Productos(Nombre, Categoria, Precio, Existencias) values ('Gaseosa','Bebidas', 1.5, 30);
Insert into Productos(Nombre, Categoria, Precio, Existencias) values ('Jugo de naranja','Bebidas', 2.3, 35);
Insert into Productos(Nombre, Categoria, Precio, Existencias) values ('Carne de cerdo','Carnes', 2.0, 35 );

GO

SELECT * FROM Productos

