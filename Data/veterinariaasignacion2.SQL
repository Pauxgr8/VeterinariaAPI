CREATE DATABASE Veterinaria;
GO
 
USE Veterinaria;
GO
 
/*=========================================================
    TABLA USUARIOS
=========================================================*/
 
CREATE TABLE Usuarios
(
    Id INT PRIMARY KEY IDENTITY(1,1),
 
    Usuario VARCHAR(15) NOT NULL,
 
    Clave VARCHAR(10) NOT NULL,
 
    Estado CHAR(1) NOT NULL,
 
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
 
    UsuarioCreacion VARCHAR(15) NOT NULL,
 
    FechaModificacion DATETIME NULL,
 
    UsuarioModificacion VARCHAR(15) NULL
);
GO
 
/*=========================================================
    TABLA PROPIETARIOS
=========================================================*/
 
CREATE TABLE Propietarios
(
    Id INT PRIMARY KEY IDENTITY(1,1),
 
    Identificacion VARCHAR(15) NOT NULL,
 
    PrimerNombre VARCHAR(25) NOT NULL,
 
    SegundoNombre VARCHAR(25) NOT NULL,
 
    PrimerApellido VARCHAR(25) NOT NULL,
 
    SegundoApellido VARCHAR(25) NOT NULL,
 
    Telefono VARCHAR(8) NOT NULL,
 
    Correo VARCHAR(150) NOT NULL,
 
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
 
    UsuarioCreacion VARCHAR(15) NOT NULL,
 
    FechaModificacion DATETIME NULL,
 
    UsuarioModificacion VARCHAR(15) NULL
);
GO
 
/*=========================================================
    TABLA MASCOTAS
=========================================================*/
 
CREATE TABLE Mascotas
(
    Id INT PRIMARY KEY IDENTITY(1,1),
 
    Nombre VARCHAR(15) NOT NULL,
 
    FechaNacimiento DATE NOT NULL,
 
    Sexo VARCHAR(1) NOT NULL,
 
    Peso NUMERIC(5,2) NOT NULL,
 
    Alergias VARCHAR(150) NULL,
 
    PropietarioId INT NOT NULL,
 
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
 
    UsuarioCreacion VARCHAR(15) NOT NULL,
 
    FechaModificacion DATETIME NULL,
 
    UsuarioModificacion VARCHAR(15) NULL,
 
    CONSTRAINT FK_Mascotas_Propietarios
        FOREIGN KEY (PropietarioId)
        REFERENCES Propietarios(Id)
);
GO
 
/*=========================================================
    TABLA HOJA CLINICA
=========================================================*/
 
CREATE TABLE HojaClinica
(
    Id INT PRIMARY KEY IDENTITY(1,1),
 
    FechaAtencion DATE NOT NULL,
 
    Sintomas VARCHAR(150) NOT NULL,
 
    Diagnostico VARCHAR(150) NOT NULL,
 
    Tratamiento VARCHAR(150) NOT NULL,
 
    MascotaId INT NOT NULL,
 
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
 
    UsuarioCreacion VARCHAR(15) NOT NULL,
 
    FechaModificacion DATETIME NULL,
 
    UsuarioModificacion VARCHAR(15) NULL,
 
    CONSTRAINT FK_HojaClinica_Mascotas
        FOREIGN KEY (MascotaId)
        REFERENCES Mascotas(Id)
);
GO

--PROCEDIMIENTOS ALMACENADOS


--VT_USUARIOS

 
/*=========================================================
    CREAR USUARIO
=========================================================*/
CREATE OR ALTER PROCEDURE sp_CrearUsuario
(
    @Usuario VARCHAR(15),
    @Clave VARCHAR(10),
    @Estado CHAR(1),
    @UsuarioCreacion VARCHAR(15)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    BEGIN TRY
 
        BEGIN TRANSACTION;
 
        INSERT INTO Usuarios
        (
            Usuario,
            Clave,
            Estado,
            FechaCreacion,
            UsuarioCreacion
        )
        VALUES
        (
            @Usuario,
            @Clave,
            @Estado,
            GETDATE(),
            @UsuarioCreacion
        );
 
        SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
 
        COMMIT TRANSACTION;
 
    END TRY
    BEGIN CATCH
 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
 
        THROW;
 
    END CATCH
END;
GO
 
 
/*=========================================================
    LEER TODOS LOS USUARIOS
=========================================================*/
CREATE OR ALTER PROCEDURE sp_LeerUsuarios
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT
        Id,
        Usuario,
        Clave,
        Estado,
        FechaCreacion,
        UsuarioCreacion,
        FechaModificacion,
        UsuarioModificacion
    FROM Usuarios
    ORDER BY Id;
END;
GO
 
 
/*=========================================================
    BUSCAR USUARIO POR ID
=========================================================*/
CREATE OR ALTER PROCEDURE sp_BuscarUsuarioPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT
        Id,
        Usuario,
        Clave,
        Estado,
        FechaCreacion,
        UsuarioCreacion,
        FechaModificacion,
        UsuarioModificacion
    FROM Usuarios
    WHERE Id = @Id;
END;
GO
 
 
/*=========================================================
    MODIFICAR USUARIO
=========================================================*/
CREATE OR ALTER PROCEDURE sp_ModificarUsuario
(
    @Id INT,
    @Usuario VARCHAR(15),
    @Clave VARCHAR(10),
    @Estado CHAR(1),
    @UsuarioModificacion VARCHAR(15)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    BEGIN TRY
 
        BEGIN TRANSACTION;
 
        UPDATE Usuarios
        SET
            Usuario = @Usuario,
            Clave = @Clave,
            Estado = @Estado,
            FechaModificacion = GETDATE(),
            UsuarioModificacion = @UsuarioModificacion
        WHERE Id = @Id;
 
        COMMIT TRANSACTION;
 
    END TRY
    BEGIN CATCH
 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
 
        THROW;
 
    END CATCH
END;
GO
 
 
/*=========================================================
    ELIMINAR USUARIO
=========================================================*/
CREATE OR ALTER PROCEDURE sp_EliminarUsuario
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    BEGIN TRY
 
        BEGIN TRANSACTION;
 
        DELETE
        FROM Usuarios
        WHERE Id = @Id;
 
        COMMIT TRANSACTION;
 
    END TRY
    BEGIN CATCH
 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
 
        THROW;
 
    END CATCH
END;
GO

/*=========================================================
    CREAR HOJA CLINICA
=========================================================*/
CREATE OR ALTER PROCEDURE sp_CrearHojaClinica
(
    @FechaAtencion DATE,
    @Sintomas VARCHAR(150),
    @Diagnostico VARCHAR(150),
    @Tratamiento VARCHAR(150),
    @MascotaId INT,
    @UsuarioCreacion VARCHAR(15)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    BEGIN TRY
 
        BEGIN TRANSACTION;
 
        INSERT INTO HojaClinica
        (
            FechaAtencion,
            Sintomas,
            Diagnostico,
            Tratamiento,
            MascotaId,
            FechaCreacion,
            UsuarioCreacion
        )
        VALUES
        (
            @FechaAtencion,
            @Sintomas,
            @Diagnostico,
            @Tratamiento,
            @MascotaId,
            GETDATE(),
            @UsuarioCreacion
        );
 
        SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
 
        COMMIT TRANSACTION;
 
    END TRY
    BEGIN CATCH
 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
 
        THROW;
 
    END CATCH
END;
GO
 
/*=========================================================
    LEER TODAS LAS HOJAS CLINICAS
=========================================================*/
CREATE OR ALTER PROCEDURE sp_LeerHojaClinica
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT
        Id,
        FechaAtencion,
        Sintomas,
        Diagnostico,
        Tratamiento,
        MascotaId,
        FechaCreacion,
        UsuarioCreacion,
        FechaModificacion,
        UsuarioModificacion
    FROM HojaClinica
    ORDER BY Id;
END;
GO
 
/*=========================================================
    BUSCAR HOJA CLINICA POR ID
=========================================================*/
CREATE OR ALTER PROCEDURE sp_BuscarHojaClinicaPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT
        Id,
        FechaAtencion,
        Sintomas,
        Diagnostico,
        Tratamiento,
        MascotaId,
        FechaCreacion,
        UsuarioCreacion,
        FechaModificacion,
        UsuarioModificacion
    FROM HojaClinica
    WHERE Id = @Id;
END;
GO
 
/*=========================================================
    MODIFICAR HOJA CLINICA
=========================================================*/
CREATE OR ALTER PROCEDURE sp_ModificarHojaClinica
(
    @Id INT,
    @FechaAtencion DATE,
    @Sintomas VARCHAR(150),
    @Diagnostico VARCHAR(150),
    @Tratamiento VARCHAR(150),
    @MascotaId INT,
    @UsuarioModificacion VARCHAR(15)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    BEGIN TRY
 
        BEGIN TRANSACTION;
 
        UPDATE HojaClinica
        SET
            FechaAtencion = @FechaAtencion,
            Sintomas = @Sintomas,
            Diagnostico = @Diagnostico,
            Tratamiento = @Tratamiento,
            MascotaId = @MascotaId,
            FechaModificacion = GETDATE(),
            UsuarioModificacion = @UsuarioModificacion
        WHERE Id = @Id;
 
        COMMIT TRANSACTION;
 
    END TRY
    BEGIN CATCH
 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
 
        THROW;
 
    END CATCH
END;
GO
 
/*=========================================================
    ELIMINAR HOJA CLINICA
=========================================================*/
CREATE OR ALTER PROCEDURE sp_EliminarHojaClinica
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    BEGIN TRY
 
        BEGIN TRANSACTION;
 
        DELETE
        FROM HojaClinica
        WHERE Id = @Id;
 
        COMMIT TRANSACTION;
 
    END TRY
    BEGIN CATCH
 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
 
        THROW;
 
    END CATCH
END;
GO

/*=========================================================
    CREAR MASCOTA
=========================================================*/
CREATE OR ALTER PROCEDURE sp_CrearMascota
(
    @Nombre VARCHAR(15),
    @FechaNacimiento DATE,
    @Sexo VARCHAR(1),
    @Peso NUMERIC(5,2),
    @Alergias VARCHAR(150),
    @PropietarioId INT,
    @UsuarioCreacion VARCHAR(15)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    BEGIN TRY
 
        BEGIN TRANSACTION;
 
        INSERT INTO Mascotas
        (
            Nombre,
            FechaNacimiento,
            Sexo,
            Peso,
            Alergias,
            PropietarioId,
            FechaCreacion,
            UsuarioCreacion
        )
        VALUES
        (
            @Nombre,
            @FechaNacimiento,
            @Sexo,
            @Peso,
            @Alergias,
            @PropietarioId,
            GETDATE(),
            @UsuarioCreacion
        );
 
        SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
 
        COMMIT TRANSACTION;
 
    END TRY
    BEGIN CATCH
 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
 
        THROW;
 
    END CATCH
END;
GO
 
 
/*=========================================================
    LEER TODAS LAS MASCOTAS
=========================================================*/
CREATE OR ALTER PROCEDURE sp_LeerMascotas
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT
        Id,
        Nombre,
        FechaNacimiento,
        Sexo,
        Peso,
        Alergias,
        PropietarioId,
        FechaCreacion,
        UsuarioCreacion,
        FechaModificacion,
        UsuarioModificacion
    FROM Mascotas
    ORDER BY Id;
END;
GO
 
 
/*=========================================================
    BUSCAR MASCOTA POR ID
=========================================================*/
CREATE OR ALTER PROCEDURE sp_BuscarMascotaPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT
        Id,
        Nombre,
        FechaNacimiento,
        Sexo,
        Peso,
        Alergias,
        PropietarioId,
        FechaCreacion,
        UsuarioCreacion,
        FechaModificacion,
        UsuarioModificacion
    FROM Mascotas
    WHERE Id = @Id;
END;
GO
 
 
/*=========================================================
    MODIFICAR MASCOTA
=========================================================*/
CREATE OR ALTER PROCEDURE sp_ModificarMascota
(
    @Id INT,
    @Nombre VARCHAR(15),
    @FechaNacimiento DATE,
    @Sexo VARCHAR(1),
    @Peso NUMERIC(5,2),
    @Alergias VARCHAR(150),
    @PropietarioId INT,
    @UsuarioModificacion VARCHAR(15)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    BEGIN TRY
 
        BEGIN TRANSACTION;
 
        UPDATE Mascotas
        SET
            Nombre = @Nombre,
            FechaNacimiento = @FechaNacimiento,
            Sexo = @Sexo,
            Peso = @Peso,
            Alergias = @Alergias,
            PropietarioId = @PropietarioId,
            FechaModificacion = GETDATE(),
            UsuarioModificacion = @UsuarioModificacion
        WHERE Id = @Id;
 
        COMMIT TRANSACTION;
 
    END TRY
    BEGIN CATCH
 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
 
        THROW;
 
    END CATCH
END;
GO
 
 
/*=========================================================
    ELIMINAR MASCOTA
=========================================================*/
CREATE OR ALTER PROCEDURE sp_EliminarMascota
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    BEGIN TRY
 
        BEGIN TRANSACTION;
 
        DELETE
        FROM Mascotas
        WHERE Id = @Id;
 
        COMMIT TRANSACTION;
 
    END TRY
    BEGIN CATCH
 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
 
        THROW;
 
    END CATCH
END;
GO

/*=========================================================
    CREAR PROPIETARIO
=========================================================*/
CREATE OR ALTER PROCEDURE sp_CrearPropietario
(
    @Identificacion VARCHAR(15),
    @PrimerNombre VARCHAR(25),
    @SegundoNombre VARCHAR(25),
    @PrimerApellido VARCHAR(25),
    @SegundoApellido VARCHAR(25),
    @Telefono VARCHAR(8),
    @Correo VARCHAR(150),
    @UsuarioCreacion VARCHAR(15)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    BEGIN TRY
 
        BEGIN TRANSACTION;
 
        INSERT INTO Propietarios
        (
            Identificacion,
            PrimerNombre,
            SegundoNombre,
            PrimerApellido,
            SegundoApellido,
            Telefono,
            Correo,
            FechaCreacion,
            UsuarioCreacion
        )
        VALUES
        (
            @Identificacion,
            @PrimerNombre,
            @SegundoNombre,
            @PrimerApellido,
            @SegundoApellido,
            @Telefono,
            @Correo,
            GETDATE(),
            @UsuarioCreacion
        );
 
        SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
 
        COMMIT TRANSACTION;
 
    END TRY
    BEGIN CATCH
 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
 
        THROW;
 
    END CATCH
END;
GO
 
 
/*=========================================================
    LEER TODOS LOS PROPIETARIOS
=========================================================*/
CREATE OR ALTER PROCEDURE sp_LeerPropietarios
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT
        Id,
        Identificacion,
        PrimerNombre,
        SegundoNombre,
        PrimerApellido,
        SegundoApellido,
        Telefono,
        Correo,
        FechaCreacion,
        UsuarioCreacion,
        FechaModificacion,
        UsuarioModificacion
    FROM Propietarios
    ORDER BY Id;
END;
GO
 
 
/*=========================================================
    BUSCAR PROPIETARIO POR ID
=========================================================*/
CREATE OR ALTER PROCEDURE sp_BuscarPropietarioPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT
        Id,
        Identificacion,
        PrimerNombre,
        SegundoNombre,
        PrimerApellido,
        SegundoApellido,
        Telefono,
        Correo,
        FechaCreacion,
        UsuarioCreacion,
        FechaModificacion,
        UsuarioModificacion
    FROM Propietarios
    WHERE Id = @Id;
END;
GO
 
 
/*=========================================================
    MODIFICAR PROPIETARIO
=========================================================*/
CREATE OR ALTER PROCEDURE sp_ModificarPropietario
(
    @Id INT,
    @Identificacion VARCHAR(15),
    @PrimerNombre VARCHAR(25),
    @SegundoNombre VARCHAR(25),
    @PrimerApellido VARCHAR(25),
    @SegundoApellido VARCHAR(25),
    @Telefono VARCHAR(8),
    @Correo VARCHAR(150),
    @UsuarioModificacion VARCHAR(15)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    BEGIN TRY
 
        BEGIN TRANSACTION;
 
        UPDATE Propietarios
        SET
            Identificacion = @Identificacion,
            PrimerNombre = @PrimerNombre,
            SegundoNombre = @SegundoNombre,
            PrimerApellido = @PrimerApellido,
            SegundoApellido = @SegundoApellido,
            Telefono = @Telefono,
            Correo = @Correo,
            FechaModificacion = GETDATE(),
            UsuarioModificacion = @UsuarioModificacion
        WHERE Id = @Id;
 
        COMMIT TRANSACTION;
 
    END TRY
    BEGIN CATCH
 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
 
        THROW;
 
    END CATCH
END;
GO
 
 
/*=========================================================
    ELIMINAR PROPIETARIO
=========================================================*/
CREATE OR ALTER PROCEDURE sp_EliminarPropietario
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    BEGIN TRY
 
        BEGIN TRANSACTION;
 
        DELETE
        FROM Propietarios
        WHERE Id = @Id;
 
        COMMIT TRANSACTION;
 
    END TRY
    BEGIN CATCH
 
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
 
        THROW;
 
    END CATCH
END;
GO


--usuario admin

INSERT INTO Usuarios
(
Usuario,
Clave,
Estado,
FechaCreacion,
UsuarioCreacion
)
VALUES
(
'admin',
'123',
'A',
GETDATE(),
'SISTEMA'
);

--propietario

INSERT INTO Propietarios
(
    Identificacion,
    PrimerNombre,
    SegundoNombre,
    PrimerApellido,
    SegundoApellido,
    Telefono,
    Correo,
    FechaCreacion,
    UsuarioCreacion
)
VALUES
(
    '111111111',
    'María',
    'Gabriela',
    'Garcia',
    'Ruiz',
    '88887777',
    'paula@gmail.com',
    GETDATE(),
    'admin'
);

--Mascota

INSERT INTO Mascotas
(
    Nombre,
    FechaNacimiento,
    Sexo,
    Peso,
    Alergias,
    PropietarioId,
    FechaCreacion,
    UsuarioCreacion
)
VALUES
(
    'Firulais',
    '2022-05-15',
    'M',
    12.50,
    'Ninguna',
    1,
    GETDATE(),
    'admin'
);

--Hoja clinica
INSERT INTO HojaClinica
(
    FechaAtencion,
    Sintomas,
    Diagnostico,
    Tratamiento,
    MascotaId,
    FechaCreacion,
    UsuarioCreacion
)
VALUES
(
    GETDATE(),
    'Fiebre y vómitos',
    'Gastroenteritis',
    'Suero y antibióticos',
    2,
    GETDATE(),
    'admin'
);