-- Crear la base de datos
CREATE DATABASE db_inventario;
GO

-- Usar la base de datos creada
USE db_inventario;
GO

-- Crear la tabla inventario
CREATE TABLE inventario
(
    Codigo INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Cantidad INT NOT NULL
);
GO

-- Insertar datos iniciales en la tabla inventario
INSERT INTO inventario (Nombre, Precio, Cantidad) VALUES 
('Mouse', 89990.00, 11),
('Teclado', 69990.00, 5),
('Monitor', 289990.00, 3),
('Gabinete', 19990.00, 22);
GO

-- Procedimiento para ingresar un producto
CREATE PROCEDURE IngresarProducto
    @msn VARCHAR(50) OUTPUT,
    @nombre VARCHAR(50),
    @precio DECIMAL(10, 2),
    @cantidad INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM inventario WHERE Nombre = @nombre)
    BEGIN
        INSERT INTO inventario (Nombre, Precio, Cantidad)
        VALUES (@nombre, @precio, @cantidad);
        SET @msn = 'Producto ingresado correctamente.';
    END
    ELSE
    BEGIN
        SET @msn = 'El producto ya existe.';
    END
END
GO

-- Procedimiento para insertar y verificar un producto
CREATE PROCEDURE InsertarVerificarInventario
    @resp VARCHAR(150) OUTPUT,
    @nombre VARCHAR(50),
    @precio DECIMAL(10, 2),
    @cantidad INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inventario WHERE Nombre = @nombre)
    BEGIN
        SET @resp = 'El producto ' + @nombre + ' ya existe.';
    END
    ELSE
    BEGIN
        INSERT INTO inventario (Nombre, Precio, Cantidad)
        VALUES (@nombre, @precio, @cantidad);

        IF @@ROWCOUNT = 0
        BEGIN
            SET @resp = 'Problema: No se pudo insertar.';
        END
        ELSE
        BEGIN
            SET @resp = 'El producto ' + @nombre + ' ha sido guardado en nuestra base de datos.';
        END
    END
END
GO

-- Consultar todos los productos en la tabla inventario
SELECT * FROM inventario;
GO
