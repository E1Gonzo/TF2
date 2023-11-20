USE MASTER
GO

IF EXISTS (SELECT NAME FROM SYS.databases WHERE NAME = 'PetHome')
BEGIN
	DROP DATABASE PetHome
END
GO

CREATE DATABASE PetHome
GO

USE PetHome
GO

CREATE TABLE perro(
	id_perro		INT IDENTITY(1,1),
	nombre			VARCHAR(100)	NOT NULL,
	edad			INT				NOT NULL,
	peso			INT				NOT NULL,
	raza			VARCHAR(70)		NOT NULL,
	color			VARCHAR(50)		NULL,
	estadoSalud		VARCHAR(120)	NOT NULL,
	lugarRescate	VARCHAR(100)	NULL,
	PRIMARY KEY (id_perro)
)
CREATE TABLE persona_interesada(
	id_INT			INT IDENTITY(1,1),
	DNI				INT				NOT NULL,
	Nombre			VARCHAR(100)	NOT NULL,
	apellidoPat		VARCHAR(100)	NOT NULL,
	apellidoMat		VARCHAR(100)	NULL,
	edad			INT				NOT NULL,
	celular			INT				NOT NULL,
	domicilio		VARCHAR(100)	NOT NULL,
	correo			VARCHAR(100)	NOT NULL,
	id_perro		INT				,
	PRIMARY KEY(id_int),
	FOREIGN KEY (id_perro) REFERENCES Perro(id_perro)
)
GO

ALTER TABLE perro				ADD CONSTRAINT CK_EDADPERRO	CHECK(0 < edad)
ALTER TABLE perro				ADD CONSTRAINT CK_PESO		CHECK(0 < peso)
ALTER TABLE persona_interesada	ADD CONSTRAINT CK_DNI			CHECK(10000000 <= DNI AND DNI < 90000000)
ALTER TABLE persona_interesada	ADD CONSTRAINT CK_CELL			CHECK(900000000 <= celular AND celular < 1000000000)
ALTER TABLE persona_interesada	ADD CONSTRAINT CK_EDADINTERESAO	CHECK(edad >= 18)
ALTER TABLE persona_interesada	ADD CONSTRAINT UQ_ID_PERRO		UNIQUE(id_perro)
ALTER TABLE persona_interesada	ADD CONSTRAINT UQ_DNI			UNIQUE(DNI)
ALTER TABLE persona_interesada	ADD CONSTRAINT UQ_celular		UNIQUE(celular)
ALTER TABLE persona_interesada	ADD CONSTRAINT UQ_correo		UNIQUE(correo)
GO

INSERT INTO perro (nombre,edad,peso,raza,color,estadoSalud,lugarRescate) VALUES 
	('Max',2,70,'presa canario','marron','óptimo','gambeta callao'),
	('Yaku',1,40,'cooker','amarillo','en observación','gambeta callao'),
	('Bella',4,60,'Pitbull','blanco','enferma','centro de lima'),
	('diablo',1,20,'chihuahua','azul','óptimo','lince'),
	('Robert',3,90,'dogo argentino','blanco','óptimo','villa el salvador'),
	('Charlie', 2, 45, 'Beagle', 'tricolor', 'óptimo', 'Miraflores'),
	('Luna', 3, 55, 'Labrador', 'negro', 'óptimo', 'San Isidro'),
	('Rocky', 4, 70, 'Bulldog', 'atigrado', 'enferma', 'Lince'),
	('Coco', 2, 30, 'Chihuahua', 'café', 'óptimo', 'San Borja'),
	('Buddy', 5, 80, 'Golden Retriever', 'dorado', 'enferma', 'Surco'),
	('Rex', 3, 50, 'German Shepherd', 'Negro y gris', 'óptimo', 'San Juan de Lurigancho'),
    ('Daisy', 1, 25, 'Dachshund', 'marrón', 'enferma', 'Barranco'),
    ('Maximus', 4, 60, 'Rottweiler', 'Negro y marrón', 'óptimo', 'Magdalena')
GO

INSERT INTO persona_interesada (DNI, Nombre, apellidoPat,apellidoMat, edad, celular, domicilio, correo, id_perro) VALUES 
	(76543210,'Marcelo','Carrasco','Garcia',19,'987654321','santa rosa callao','marcelitopkmz@gmail.com',1),
	(73005607,'Jefferson','Panta','Ruiz',19,'930889076','santa rosa callao','panta173@gmail.com',5),
	(72131234,'Harold','Chavez','Hau',30,'932432433','santa rosa callao','pelao@gmail.com',2),
	(54234344,'Mauricio','Menacho','Idk',50,'945643342','chorrillos','mauri@gmail.com',4),
	(11823255,'Cielo','Ruiz','Murillo',20,'923454510','villa el salvador','cielito@gmail.com',3),
	(87654321, 'Laura', 'Gomez', 'Perez', 22, 987654322, 'San Miguel', 'laura@gmail.com', 10),
	(71005608, 'Carlos', 'Vargas', 'Lopez', 23, 930889077, 'Magdalena', 'carlos@gmail.com', 8),
	(72131235, 'Luis', 'Mendoza', 'Diaz', 35, 932432434, 'Miraflores', 'luis@gmail.com', 7),
	(54234345, 'Ana', 'Gutierrez', 'Castro', 45, 945643343, 'Barranco', 'ana@gmail.com', 9),
	(11823256, 'Diego', 'Vega', 'Torres', 21, 923454511, 'Chorrillos', 'diego@gmail.com', 6)
GO

-----<<<<SCRIPTS DE INTERESADOS>>>>-----
CREATE OR ALTER PROC usp_interesados
AS 
SELECT pe_i.id_int
		,pe_i.DNI
		,pe_i.Nombre
		,pe_i.apellidoPat
		,pe_i.apellidoMat
		, pe_i.edad
		, pe_i.celular
		, pe_i.domicilio
		, pe_i.correo
		, CASE 
			WHEN pe_i.id_perro is NULL THEN 0
			ELSE pe_i.id_perro
			END AS idPerro
	FROM persona_interesada pe_i 
GO
/**MANTENIMIENTO INTERESADOS**/
CREATE OR ALTER PROC USP_INSERTA_INTERESADOS
	@DNI			INT,
	@Nombre			VARCHAR(100),
	@apellidoPat	VARCHAR(100),
	@apellidoMat	VARCHAR(100),
	@edad			INT,
	@celular		INT,
	@domicilio		VARCHAR(100),
	@correo			VARCHAR(100),
	@id_perro		INT
AS
BEGIN
	IF (@apellidoMat LIKE 'Indefinido')
	BEGIN
		SET @apellidoMat = ''
	END
	INSERT INTO persona_interesada (DNI,Nombre,apellidoPat,apellidoMat,edad,celular,domicilio,correo, id_perro)
	VALUES (@DNI,@Nombre,@apellidoPat,@apellidoMat,@edad,@celular,@domicilio,@correo,@id_perro)
END
GO
CREATE OR ALTER PROC USP_ACTUALIZA_INTERESADOS
	@CODIGO			INT,
	@DNI			INT,
	@Nombre			VARCHAR(100),
	@apellidoPat	VARCHAR(100),
	@apellidoMat	VARCHAR(100),
	@edad			INT,
	@celular		INT,
	@domicilio		VARCHAR(100),
	@correo			VARCHAR(100),
	@id_perro		INT
AS
	UPDATE persona_interesada SET DNI=@DNI,Nombre=@Nombre,apellidoPat=@apellidoPat,
	apellidoMat=@apellidoMat,edad=@edad,celular=@celular,domicilio=@domicilio,correo=@correo, ID_PERRO=@id_perro
	WHERE id_INT=@CODIGO
GO
CREATE OR ALTER PROC usp_elimina_interesados
@CODIGO INT
AS
	DELETE persona_interesada WHERE id_INT=@CODIGO
GO


-----<<<<SCRIPTS DE PERROS>>>>-----
CREATE OR ALTER   proc usp_perros
AS 
SELECT p.id_perro
		,p.nombre
		,p.edad
		, p.peso
		, p.raza
		, p.color
		, p.estadoSalud
		, p.lugarRescate
		,CASE 
		WHEN pe_i.Nombre IS NULL THEN '>No tiene<'
		ELSE pe_i.Nombre
		END AS "Interesado"
	FROM perro p 
	left join persona_interesada pe_i ON p.id_perro=pe_i.id_perro
GO
/**MANTENIMIENTO PERROS**/
CREATE OR ALTER PROC usp_inserta_perros
	@nombre			VARCHAR(100),
	@edad			INT,				
	@peso			INT,				
	@raza			VARCHAR(70),	
	@color			VARCHAR(50),	
	@estadoSalud	VARCHAR(120),	
	@lugarRescate	VARCHAR(100)
AS
BEGIN 
	IF (@color LIKE 'Indefinido')
	BEGIN
		SET @color = ''
	END
	IF (@lugarRescate LIKE 'Indefinido')
	BEGIN
		SET @lugarRescate = ''
	END
	INSERT INTO perro (nombre,edad,peso,raza,color,estadoSalud,lugarRescate) 
	VALUES (@nombre,@edad,@peso,@raza,@color,@estadoSalud,@lugarRescate)
END
GO
CREATE OR ALTER PROC usp_actualiza_perros
	@id				INT,
	@nombre			VARCHAR(100),
	@edad			INT,				
	@peso			INT,				
	@raza			VARCHAR(70),	
	@color			VARCHAR(50),	
	@estadoSalud	VARCHAR(120),	
	@lugarRescate	VARCHAR(100)
AS
BEGIN
	IF (@lugarRescate LIKE 'Indefinido')
	BEGIN
		SET @lugarRescate = ''
	END
	UPDATE perro SET nombre=@nombre,edad=@edad,peso=@peso,raza=@raza,color=@color,estadoSalud=@estadoSalud,lugarRescate=@lugarRescate
	WHERE id_perro=@id
END
GO
CREATE OR ALTER PROC usp_elimina_perros
@cod INT
AS
	DELETE perro WHERE id_perro=@cod
GO
