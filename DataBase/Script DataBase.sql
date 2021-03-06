USE [SpeedSolutions]
GO
/****** Object:  StoredProcedure [dbo].[procDispensadoresGuardar]    Script Date: 24/07/2021 11:41:39 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[procDispensadoresGuardar] (@pEstacionClave INT,
@pDispensadorClave INT,
@pDispensadorNombre VARCHAR(20))
AS

	DECLARE @RESULTADO VARCHAR(MAX)

	BEGIN
		SET NOCOUNT ON;
		BEGIN TRAN DISPENSADORES_INSERT
			BEGIN TRY

				IF @pDispensadorClave = 0
				BEGIN
					SELECT
						@pDispensadorClave = ISNULL(MAX(d.DispensadorClave) + 1, 1)
					FROM catDispensadores d
					WHERE d.EstacionClave = @pEstacionClave

					INSERT INTO catDispensadores (EstacionClave, DispensadorClave, DispensadorNombre)
						VALUES (@pEstacionClave, @pDispensadorClave, @pDispensadorNombre);
				END
				ELSE
				BEGIN
					UPDATE catDispensadores
					SET DispensadorNombre = @pDispensadorNombre
					WHERE EstacionClave = @pEstacionClave
					AND DispensadorClave = @pDispensadorClave
				END

				SET @RESULTADO = 'EXITO'
			COMMIT TRAN DISPENSADORES_INSERT
		END TRY
		BEGIN CATCH
			SET @RESULTADO = 'Ocurrio un Error: ' + ERROR_MESSAGE() + ' en la línea ' + CONVERT(NVARCHAR(255), ERROR_LINE()) + '.'
			ROLLBACK TRAN DISPENSADORES_INSERT
		END CATCH
		SELECT
			@RESULTADO AS RESULTADO
	END
GO
/****** Object:  StoredProcedure [dbo].[procEstacionesCon]    Script Date: 24/07/2021 11:41:39 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		VICTOR MENDOZA
-- Create date: 22/07/2021
-- Description:	Obtiene el listado de estaciones
-- =============================================
CREATE PROCEDURE [dbo].[procEstacionesCon] @pEstacionClave INT
AS
BEGIN

	SET NOCOUNT ON;

	IF @pEstacionClave = 0
	BEGIN
		SET @pEstacionClave = NULL
	END

	SELECT
		e.EstacionClave,
		e.EstacionNombre,
		e.EstacionDomicilio
	FROM catEstaciones e
	WHERE (@pEstacionClave IS NULL
	OR @pEstacionClave = e.EstacionClave)

END
GO
/****** Object:  StoredProcedure [dbo].[procProductosCon]    Script Date: 24/07/2021 11:41:39 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		VICTOR MENDOZA
-- Create date: 21/07/2021
-- Description:	Obtiene el listado de productos con los precios actuales
-- =============================================
CREATE PROCEDURE [dbo].[procProductosCon] @pEstacionClave INT,
@pProductoClave INT
AS
BEGIN

	SET NOCOUNT ON;

	IF @pProductoClave = 0
	BEGIN
		SET @pProductoClave = NULL
	END

	SELECT
		p.EstacionClave,
		p.ProductoClave,
		p.ProductoNombre,
		p.ProductoPrecio,
		CONVERT(VARCHAR(19),p.FechaActualizacion) AS FechaActualizacion,
		e.EstacionNombre
	FROM catProductos p
	INNER JOIN catEstaciones e ON p.EstacionClave = e.EstacionClave
	WHERE (@pProductoClave IS NULL
	OR @pProductoClave = p.ProductoClave)
	ORDER BY p.ProductoPrecio DESC
END
GO
/****** Object:  StoredProcedure [dbo].[procProductosGuardar]    Script Date: 24/07/2021 11:41:39 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procProductosGuardar] (
@pEstacionClave INT,
@pProductoClave INT,
@pProductoNombre VARCHAR(20),
@pProuctoPrecio NUMERIC(10, 2))
AS

	DECLARE @RESULTADO VARCHAR(MAX)

	BEGIN
		SET NOCOUNT ON;
		BEGIN TRAN PRODUCTOS_INSERT
			BEGIN TRY

				IF @pProductoClave = 0
				BEGIN
					SELECT
						@pProductoClave = ISNULL(MAX(p.ProductoClave) + 1, 1)
					FROM catProductos p
					WHERE p.EstacionClave = @pEstacionClave

					INSERT INTO catProductos (EstacionClave, ProductoClave, ProductoNombre, ProductoPrecio, FechaActualizacion)
						VALUES (@pEstacionClave, @pProductoClave, @pProductoNombre, @pProuctoPrecio, GETDATE());

					SET @RESULTADO = 'EXITO'
				END
				ELSE
				BEGIN

					IF @pProuctoPrecio != (SELECT TOP (1)
							p.ProductoPrecio
						FROM catProductos p
						WHERE p.EstacionClave = @pEstacionClave
						AND p.ProductoClave = p.ProductoClave)
					BEGIN
						INSERT INTO catProductos (EstacionClave, ProductoClave, ProductoNombre, ProductoPrecio, FechaActualizacion)
							VALUES (@pEstacionClave, @pProductoClave, @pProductoNombre, @pProuctoPrecio, GETDATE());
					END
					ELSE
					BEGIN
						UPDATE catProductos
						SET ProductoNombre = @pProductoNombre
						WHERE EstacionClave = @pEstacionClave
						AND ProductoClave = @pProductoClave
					END

					SET @RESULTADO = 'EXITO'
				END
			COMMIT TRAN PRODUCTOS_INSERT
		END TRY
		BEGIN CATCH
			SET @RESULTADO = 'Ocurrio un Error: ' + ERROR_MESSAGE() + ' en la línea ' + CONVERT(NVARCHAR(255), ERROR_LINE()) + '.'
			ROLLBACK TRAN PRODUCTOS_INSERT
		END CATCH
		SELECT
			@RESULTADO AS RESULTADO
	END
GO
/****** Object:  StoredProcedure [dbo].[procUsuariosAutentificar]    Script Date: 24/07/2021 11:41:39 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		 VICTOR MENDOZA
-- Create date: 21/07/2021
-- Description:	AUTENTIFICACION DE USUARIOS
-- =============================================
CREATE PROCEDURE [dbo].[procUsuariosAutentificar] (
@pUsuarioClave VARCHAR(20),
@pUsuarioContraseña VARCHAR(MAX))
AS
BEGIN

	DECLARE @EXISTE INT = 0;

	SET NOCOUNT ON;

	SET @EXISTE = (SELECT COUNT(*)
	FROM catUsuarios cu
	WHERE cu.UsuarioClave = @pUsuarioClave
	AND cu.UsuarioContraseña = @pUsuarioContraseña
	AND UsuarioEstatus = 1)
	IF @EXISTE <> 0
	BEGIN
		SELECT
			UsuarioContraseña
		FROM catUsuarios cu
		WHERE cu.UsuarioClave = @pUsuarioClave
		AND cu.UsuarioContraseña = @pUsuarioContraseña
		AND UsuarioEstatus = 1
	END
	ELSE
	BEGIN
		SELECT
			'ERROR' AS 'UsuarioContraseña'
	END

END

GO
/****** Object:  StoredProcedure [dbo].[procUsuariosCon]    Script Date: 24/07/2021 11:41:39 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		VICTOR MENDOZA
-- Create date: 21/07/2021
-- Description:	Obtiene el listado de usuarios, recibe como parametro el ID del usuario
-- =============================================
CREATE PROCEDURE [dbo].[procUsuariosCon] @pUsuarioID INT
AS
BEGIN

	SET NOCOUNT ON;

	IF @pUsuarioID = 0
	BEGIN
		SET @pUsuarioID = NULL
	END


	SELECT
		u.UsuarioID,
		u.UsuarioClave,
		u.UsuarioNombre,
		u.UsuarioApellido,
		u.UsuarioEstatus
	FROM catUsuarios u
	WHERE (@pUsuarioID IS NULL
	OR @pUsuarioID = u.UsuarioID)

END

GO
/****** Object:  StoredProcedure [dbo].[procUsuariosGuardar]    Script Date: 24/07/2021 11:41:39 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		 VICTOR MENDOZA
-- Create date: 21/07/2021
-- Description:	Guarda y actualiza los usuarios
-- =============================================
CREATE PROCEDURE [dbo].[procUsuariosGuardar] (@pUsuarioID INT,
@pUsuarioClave VARCHAR(20),
@pUsuarioNombre VARCHAR(30),
@pUsuarioApellido VARCHAR(30),
@pUsuarioContraseña VARCHAR(MAX),
@pUsuarioEstatus BIT)
AS
	DECLARE @RESULTADO VARCHAR(MAX)

	BEGIN
		SET NOCOUNT ON;
		BEGIN TRAN USUARIOS_INSERT
			BEGIN TRY

				IF @pUsuarioID = 0
				BEGIN

					SELECT
						@pUsuarioID = ISNULL(MAX(UsuarioID) + 1, 1)
					FROM catUsuarios

					INSERT INTO catUsuarios (UsuarioID, UsuarioClave, UsuarioNombre, UsuarioContraseña, UsuarioApellido, UsuarioEstatus)
						VALUES (@pUsuarioID, @pUsuarioClave, @pUsuarioNombre, @pUsuarioContraseña, @pUsuarioApellido, @pUsuarioEstatus);

					SET @RESULTADO = 'EXITO'
				END
			COMMIT TRAN USUARIOS_INSERT
		END TRY
		BEGIN CATCH
			SET @RESULTADO = 'Ocurrio un Error: ' + ERROR_MESSAGE() + ' en la línea ' + CONVERT(NVARCHAR(255), ERROR_LINE()) + '.'
			ROLLBACK TRAN USUARIOS_INSERT
		END CATCH
		SELECT
			@RESULTADO AS RESULTADO
	END
GO
/****** Object:  Table [dbo].[catDispensadores]    Script Date: 24/07/2021 11:41:39 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[catDispensadores](
	[EstacionClave] [int] NULL,
	[DispensadorClave] [int] NULL,
	[DispensadorNombre] [varchar](20) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[catDispensadoresDetalle]    Script Date: 24/07/2021 11:41:39 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[catDispensadoresDetalle](
	[DispensadorClave] [int] NULL,
	[ProductoClave] [int] NULL,
	[ProductoUltimoPrecio] [float] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[catEstaciones]    Script Date: 24/07/2021 11:41:39 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[catEstaciones](
	[EstacionClave] [int] NULL,
	[EstacionNombre] [varchar](50) NULL,
	[EstacionDomicilio] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[catProductos]    Script Date: 24/07/2021 11:41:39 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[catProductos](
	[EstacionClave] [int] NULL,
	[ProductoClave] [int] NULL,
	[ProductoNombre] [varchar](30) NULL,
	[ProductoPrecio] [float] NULL,
	[FechaActualizacion] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[catUsuarios]    Script Date: 24/07/2021 11:41:39 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[catUsuarios](
	[UsuarioID] [int] NULL,
	[UsuarioClave] [varchar](20) NULL,
	[UsuarioNombre] [varchar](30) NULL,
	[UsuarioContraseña] [varchar](max) NULL,
	[UsuarioApellido] [varchar](30) NULL,
	[UsuarioEstatus] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[catEstaciones] ([EstacionClave], [EstacionNombre], [EstacionDomicilio]) VALUES (1, N'ESTCLN 1', N'Culiacan')
GO
INSERT [dbo].[catEstaciones] ([EstacionClave], [EstacionNombre], [EstacionDomicilio]) VALUES (2, N'ESTCLN 2', N'Culiacan')
GO
INSERT [dbo].[catEstaciones] ([EstacionClave], [EstacionNombre], [EstacionDomicilio]) VALUES (3, N'ESTGDL 1', N'Guadalajara')
GO
INSERT [dbo].[catProductos] ([EstacionClave], [ProductoClave], [ProductoNombre], [ProductoPrecio], [FechaActualizacion]) VALUES (1, 1, N'GASOLINA 96 OCTANOS', 22.99, CAST(0x0000AD6F009A0406 AS DateTime))
GO
INSERT [dbo].[catProductos] ([EstacionClave], [ProductoClave], [ProductoNombre], [ProductoPrecio], [FechaActualizacion]) VALUES (1, 1, N'GASOLINA 96 OCTANOS', 22.3, CAST(0x0000AD6F00A9060F AS DateTime))
GO
INSERT [dbo].[catProductos] ([EstacionClave], [ProductoClave], [ProductoNombre], [ProductoPrecio], [FechaActualizacion]) VALUES (1, 1, N'GASOLINA 96 OCTANOS', 22.31, CAST(0x0000AD6F00A9DCA0 AS DateTime))
GO
INSERT [dbo].[catProductos] ([EstacionClave], [ProductoClave], [ProductoNombre], [ProductoPrecio], [FechaActualizacion]) VALUES (3, 1, N'GASOLINA 96 OCTANOS', 24.52, CAST(0x0000AD6F00AB8F92 AS DateTime))
GO
INSERT [dbo].[catProductos] ([EstacionClave], [ProductoClave], [ProductoNombre], [ProductoPrecio], [FechaActualizacion]) VALUES (2, 1, N'GASOLINA 96 OCTANOS', 22.81, CAST(0x0000AD6F00AC4138 AS DateTime))
GO
INSERT [dbo].[catProductos] ([EstacionClave], [ProductoClave], [ProductoNombre], [ProductoPrecio], [FechaActualizacion]) VALUES (2, 1, N'GASOLINA 96 OCTANOS', 22.82, CAST(0x0000AD6F00AEC79C AS DateTime))
GO
INSERT [dbo].[catUsuarios] ([UsuarioID], [UsuarioClave], [UsuarioNombre], [UsuarioContraseña], [UsuarioApellido], [UsuarioEstatus]) VALUES (1, N'VICTORM0', N'VICTOR', N'M8ODiHeTF9HWDIGNI0/wLHgmA++l1fq7AxJB6udY3N+GePJMrGrgGm67SNwCbP6SBGnAYIuo07mD/B58eZJgbg==', N'MENDOZA', 1)
GO
