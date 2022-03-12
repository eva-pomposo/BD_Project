CREATE PROCEDURE CruzeirosDB.DeleteBarco
    @codigoBarco int
AS
BEGIN
    BEGIN TRANSACTION; 
    UPDATE CruzeirosDB.C_Cruzeiro SET C_Barco_codigoBarco = NULL WHERE C_Barco_codigoBarco = @codigoBarco;
	declare @numeroCruzeiro int;
	declare tmp_cursor cursor for
	Select numCruzeiro FROM CruzeirosDB.C_Cruzeiro WHERE C_Barco_codigoBarco is NULL;
	open tmp_cursor;
	fetch next from tmp_cursor into @numeroCruzeiro;
	while @@fetch_status = 0
	begin
		DELETE from CruzeirosDB.C_Reserva WHERE C_Cruzeiro_numCruzeiro=@numeroCruzeiro
		DELETE from CruzeirosDB.C_Trabalha WHERE C_Cruzeiro_numCruzeiro=@numeroCruzeiro
		UPDATE CruzeirosDB.C_Partida SET C_Cruzeiro_numCruzeiro = NULL WHERE C_Cruzeiro_numCruzeiro=@numeroCruzeiro
		UPDATE CruzeirosDB.C_Chegada SET C_Cruzeiro_numCruzeiro = NULL WHERE C_Cruzeiro_numCruzeiro=@numeroCruzeiro
		fetch next from tmp_cursor into @numeroCruzeiro
	end
	close tmp_cursor
	deallocate tmp_cursor
	DELETE from CruzeirosDB.C_Partida WHERE C_Cruzeiro_numCruzeiro is NULL;
	DELETE from CruzeirosDB.C_Chegada WHERE C_Cruzeiro_numCruzeiro is NULL;
	DELETE from CruzeirosDB.C_Reserva WHERE C_Cruzeiro_numCruzeiro is NULL;
	DELETE from CruzeirosDB.C_Trabalha WHERE C_Cruzeiro_numCruzeiro is NULL;
	DELETE from CruzeirosDB.C_Cruzeiro where C_Barco_codigoBarco is NULL;
    Delete FROM CruzeirosDB.C_Barco WHERE codigoBarco = @codigoBarco;
    COMMIT;
END
GO

go
CREATE PROCEDURE CruzeirosDB.Cliente
AS
SELECT P.numTelemovel, P.nome, P.email, C.C_Pessoa_numCC, C.numCliente
FROM CruzeirosDB.C_Pessoa AS P JOIN CruzeirosDB.C_Cliente AS C ON P.numCC=C.C_Pessoa_numCC
go

go 
CREATE PROCEDURE CruzeirosDB.Tripulante
AS
SELECT P.numTelemovel, P.nome, P.email, C.C_Pessoa_numCC, C.numTripulante
FROM CruzeirosDB.C_Pessoa AS P JOIN CruzeirosDB.C_Tripulante AS C ON P.numCC=C.C_Pessoa_numCC

go

CREATE PROCEDURE CruzeirosDB.AddCliente
    @numCC int,
    @email varchar(100),
    @nome varchar(100),
    @numTelemovel int
AS
BEGIN
    BEGIN TRANSACTION; 
		declare @numCliente int
		set @numCliente = (select CruzeirosDB.getNextNumCliente())
		INSERT INTO CruzeirosDB.C_Pessoa([numCC],[email],[nome],[numTelemovel]) VALUES(@numCC,@email,@nome,@numTelemovel);
		INSERT INTO CruzeirosDB.C_Cliente([numCliente],[C_Pessoa_numCC]) VALUES(@numCliente,@numCC);
		Commit;
END
GO
CREATE PROCEDURE CruzeirosDB.AddTripulante
    @numCC int,
    @email varchar(100),
    @nome varchar(100),
    @numTelemovel int
AS
BEGIN
    BEGIN TRANSACTION; 
		declare @numTripulante int
		set @numTripulante = (select CruzeirosDB.getNextNumTripulante())
		INSERT INTO CruzeirosDB.C_Pessoa([numCC],[email],[nome],[numTelemovel]) VALUES(@numCC,@email,@nome,@numTelemovel);
		INSERT INTO CruzeirosDB.C_Tripulante(numTripulante,[C_Pessoa_numCC]) VALUES(@numTripulante,@numCC);
		Commit;
END
GO

CREATE PROCEDURE CruzeirosDB.showCruzeirodaReserva
    @numeroCruzeiro int
AS
BEGIN
	Select * from CruzeirosDB.listCruzeiro where numCruzeiro = @numeroCruzeiro
END
GO

CREATE PROCEDURE CruzeirosDB.showClientedaReserva
    @numCC int
AS
BEGIN
SELECT P.numTelemovel, P.nome, P.email, C.C_Pessoa_numCC, C.numCliente
FROM CruzeirosDB.C_Pessoa AS P JOIN CruzeirosDB.C_Cliente AS C ON P.numCC=C.C_Pessoa_numCC where C_Pessoa_numCC = @numCC
END
GO


create procedure CruzeirosDB.criarReserva
	@nomeCliente varchar(100),
	@numCCcliente int,
	@numCruzeiro int
AS
BEGIN
    BEGIN TRANSACTION; 
	declare @numbilhete int
	declare @data date
	set @numbilhete = (select CruzeirosDB.getNumbilhete(@numCruzeiro))
    set @data = (select GETDATE())
    UPDATE CruzeirosDB.C_Cruzeiro SET vagas = vagas -1 WHERE numCruzeiro = @numCruzeiro;
	INSERT INTO CruzeirosDB.C_Reserva([C_Cliente_C_Pessoa_numCC],[numBilhete],[C_Data],[C_Cruzeiro_numCruzeiro],[nomeCliente]) VALUES(@numCCcliente, @numbilhete, @data, @numCruzeiro,@nomeCliente);
	COMMIT;
END
GO

create procedure CruzeirosDB.criarCruzeiro
	@codigoBarco int,
	@dataEmbarque date,
	@dataDesembarque date,
	@horaEmbarque time,
	@horaDesembarque time,
	@codigoCaisChegada int,
	@codigoCaisPartida int
as
BEGIN
    BEGIN TRANSACTION; 
	declare @numeroCruzeiro int
	set @numeroCruzeiro = (select CruzeirosDB.getNextNumCruzeiro())
	declare @vagas int
	set @vagas = (select CruzeirosDB.getVagascriarCruzeiro (@codigoBarco))
	declare @idPartida int
	declare @idChegada int
	set @idPartida = (select CruzeirosDB.getNextPartida ())
	set @idChegada = (select CruzeirosDB.getNextChegada ())

	insert into CruzeirosDB.C_Cruzeiro (C_Barco_codigoBarco, dataEmbarque, dataDesembarque, vagas, numCruzeiro) values (@codigoBarco, @dataEmbarque, @dataDesembarque, @vagas, @numeroCruzeiro);
	INSERT INTO CruzeirosDB.C_Partida([C_Cruzeiro_numCruzeiro],[C_Cais_codigo],[horaPartida],[id]) VALUES(@numeroCruzeiro, @codigoCaisPartida,@horaEmbarque,@idPartida);
	INSERT INTO CruzeirosDB.C_Chegada([C_Cruzeiro_numCruzeiro],[C_Cais_codigo],[horaChegada],[id]) VALUES(@numeroCruzeiro, @codigoCaisChegada,@horaDesembarque,@idChegada);
	COMMIT;
END
GO

create procedure CruzeirosDB.editCruzeiro
	@numeroCruzeiro int,
	@vagas int,
	@codigoBarco int,
	@dataEmbarque date,
	@dataDesembarque date,
	@horaEmbarque time,
	@horaDesembarque time,
	@codigoCaisChegada int,
	@codigoCaisPartida int
as
BEGIN
    BEGIN TRANSACTION; 
	declare @idPartida int
	declare @idChegada int
	set @idPartida = (select CruzeirosDB.getIdPartida (@numeroCruzeiro))
	set @idChegada = (select CruzeirosDB.getIdChegada (@numeroCruzeiro))

	UPDATE CruzeirosDB.C_Chegada  Set C_Cruzeiro_numCruzeiro = @numeroCruzeiro, C_Cais_codigo = @codigoCaisChegada,horaChegada = @horaDesembarque where id = @idChegada;
	UPDATE CruzeirosDB.C_Partida  Set C_Cruzeiro_numCruzeiro = @numeroCruzeiro, C_Cais_codigo = @codigoCaisPartida,horaPartida = @horaEmbarque where id = @idPartida;
	UPDATE CruzeirosDB.C_Cruzeiro Set C_Barco_codigoBarco = @codigoBarco, dataEmbarque = @dataEmbarque, dataDesembarque = @dataDesembarque, vagas = @vagas where @numeroCruzeiro = numCruzeiro;
	COMMIT;
END
GO

CREATE PROCEDURE CruzeirosDB.DeleteCruzeiro
    @numCruzeiro int
AS
BEGIN
    BEGIN TRANSACTION;
	DELETE from CruzeirosDB.C_Partida WHERE C_Cruzeiro_numCruzeiro = @numCruzeiro;
	DELETE from CruzeirosDB.C_Chegada WHERE C_Cruzeiro_numCruzeiro = @numCruzeiro;
	DELETE from CruzeirosDB.C_Reserva WHERE C_Cruzeiro_numCruzeiro = @numCruzeiro;
	DELETE from CruzeirosDB.C_Trabalha WHERE C_Cruzeiro_numCruzeiro = @numCruzeiro;
	DELETE from CruzeirosDB.C_Cruzeiro where numCruzeiro = @numCruzeiro;
    COMMIT;
END
GO