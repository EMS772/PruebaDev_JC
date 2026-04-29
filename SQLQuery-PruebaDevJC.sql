USE CalculadoraDB;
GO

IF OBJECT_ID('dbo.SP_CalcularCuota', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SP_CalcularCuota;
GO

CREATE PROCEDURE dbo.SP_CalcularCuota
    @FechaNacimiento DATE,
    @Monto           DECIMAL(18,2),
    @Meses           INT,
    @IpConsulta      VARCHAR(45),
    @ValorCuota      DECIMAL(18,2) OUTPUT,
    @Mensaje         VARCHAR(500)  OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Edad INT;
    SET @Edad = DATEDIFF(YEAR, @FechaNacimiento, GETDATE())
              - CASE
                    WHEN (MONTH(@FechaNacimiento) > MONTH(GETDATE()))
                      OR (MONTH(@FechaNacimiento) = MONTH(GETDATE())
                          AND DAY(@FechaNacimiento) > DAY(GETDATE()))
                    THEN 1
                    ELSE 0
                END;

    DECLARE @EdadMin INT = (SELECT MIN(Edad) FROM dbo.TasasEdad);
    DECLARE @EdadMax INT = (SELECT MAX(Edad) FROM dbo.TasasEdad);
    DECLARE @Tasa    DECIMAL(5,2);

    IF @Edad < @EdadMin
    BEGIN
        SET @Mensaje    = 'Lo Sentimos aun no cuenta con la edad para solicitar esta producto.';
        SET @ValorCuota = NULL;

        INSERT INTO dbo.ConsultaLog (FechaConsulta, Edad, Monto, Meses, ValorCuota, IP_de_Consulta, MensajeError)
        VALUES (GETDATE(), @Edad, @Monto, @Meses, NULL, @IpConsulta, @Mensaje);

        RETURN;
    END

    IF @Edad > @EdadMax
    BEGIN
        SET @Mensaje    = 'Favor pasar por una de nuestras sucursales para evaluar su caso.';
        SET @ValorCuota = NULL;

        INSERT INTO dbo.ConsultaLog (FechaConsulta, Edad, Monto, Meses, ValorCuota, IP_de_Consulta, MensajeError)
        VALUES (GETDATE(), @Edad, @Monto, @Meses, NULL, @IpConsulta, @Mensaje);

        RETURN;
    END

    SELECT @Tasa = Tasa FROM dbo.TasasEdad WHERE Edad = @Edad;

    SET @ValorCuota = (@Monto * @Tasa) / @Meses;
    SET @Mensaje    = '';

    INSERT INTO dbo.ConsultaLog (FechaConsulta, Edad, Monto, Meses, ValorCuota, IP_de_Consulta, MensajeError)
    VALUES (GETDATE(), @Edad, @Monto, @Meses, @ValorCuota, @IpConsulta, NULL);
END
GO

IF OBJECT_ID('dbo.SP_ObtenerConsultasLog', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SP_ObtenerConsultasLog;
GO

CREATE PROCEDURE dbo.SP_ObtenerConsultasLog
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdConsulta,
        FechaConsulta,
        Edad,
        Monto,
        Meses,
        ValorCuota,
        IP_de_Consulta,
        MensajeError
    FROM  dbo.ConsultaLog
    ORDER BY FechaConsulta DESC;
END
GO

