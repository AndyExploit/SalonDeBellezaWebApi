USE [eliqsv_salonbelleza2]
GO

INSERT INTO [dbo].[Rol]
			([Nombre])
		VALUES
			('ADMINISTRADOR DEL SISTEMA')
GO

SELECT * FROM Rol
GO

--Encriptar la contraseña Admin2021 en MD5 https://www.infranetworking.com/md5
INSERT INTO [dbo].[Usuario]
			([IdRol]
			,[Dui]
			,[Nombre]
			,[Apellido]
			,[Numero]
			,[Login]
			,[Password]
			,[Estado]
			,[FechaRegistro])
		VALUES
			((SELECT TOP 1 Id FROM Rol WHERE Nombre='ADMINISTRADOR DEL SISTEMA'),
			 123456789,
			 'Administrador',
			 'Del Sistema',
			 75209577,
			 'SysAdmin',
			 '1fe57b020cf7bcd8ef4cc23354b214a9',
			 1,
			 SYSDATETIME())

SELECT * FROM Usuario
GO