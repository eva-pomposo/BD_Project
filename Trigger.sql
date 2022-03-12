go
CREATE TRIGGER CruzeirosDB.triggerAddBraco ON CruzeirosDB.C_Barco
AFTER INSERT, UPDATE 
AS
	SET NOCOUNT ON;
	DECLARE @maxBilhetesTP AS INT;
	DECLARE @numTotalBilhetesB AS INT;
	DECLARE @tipoBarco AS varchar(100);
	SELECT @numTotalBilhetesB=numTotalBilhetes FROM INSERTED;
	SElECT @tipoBarco = C_TipoBarco_nomeTipoBarco FROM INSERTED;
	SELECT  @maxBilhetesTP = maxBilhetes FROM CruzeirosDB.C_TipoBarco Where nomeTipoBarco = @tipoBarco

	IF @numTotalBilhetesB> @maxBilhetesTP
	BEGIN
		RAISERROR('Numero Total de bilhetes tem que ser inferiror ao Maximo de bilhetes desse Tipo de barco' ,16,1);
		ROLLBACK TRAN;
	END
go

CREATE TRIGGER CruzeirosDB.TriggerAddPessoa ON CruzeirosDB.C_Pessoa 
AFTER INSERT, UPDATE
AS 
	SET NOCOUNT ON;
	DECLARE @numCartao AS INT;
	SELECT @numCartao = numCC FROM INSERTED;

	declare @count as int
	SELECT @count = count(*) FROM  CruzeirosDB.C_Pessoa where numCC = @numCartao
	print(@count)
	IF @count > 0
	BEGIN
		RAISERROR('Numero de cartão de cidadão já existente' ,16,1);
	END
go

CREATE TRIGGER CruzeirosDB.triggercheckVagas ON CruzeirosDB.C_Cruzeiro
AFTER INSERT, UPDATE 
AS
	SET NOCOUNT ON;
	DECLARE @numVagas AS INT;
	SELECT @numVagas= vagas FROM INSERTED;

	IF @numVagas < 0
	BEGIN
		RAISERROR('Não pode fazer reserva neste cruzeiro, não há mais vagas!' ,16,1);
		ROLLBACK TRAN;
	END
go


CREATE TRIGGER CruzeirosDB.TriggerAddCruzeiroData ON CruzeirosDB.C_Cruzeiro
AFTER INSERT, UPDATE
AS 
	SET NOCOUNT ON;
	DECLARE @dEmbarque AS DATE;
	DECLARE @dDesembarque AS DATE;
	SELECT @dEmbarque = dataEmbarque FROM INSERTED;
	SELECT @dDesembarque = dataDesembarque FROM INSERTED;
	declare @horaChegada as time;
	declare @horaPartida as time;
	declare @numCruzeiro as int;
	Select @numCruzeiro = numCruzeiro FROM INSERTED;
	set @horaPartida = (select CruzeirosDB.gethorapartida(@numCruzeiro)); 
	set	@horaChegada = (select CruzeirosDB.gethorachegada(@numCruzeiro));
	IF @dDesembarque < @dEmbarque
	BEGIN
		RAISERROR('Data de Desembarque não pode ser anteiror à data de Embarque' ,16,1);
		ROLLBACK TRAN;
	END

	IF @dDesembarque = @dEmbarque and @horaPartida > @horaChegada
	BEGIN
		RAISERROR('Hora de Desembarque não pode ser anteiror à hora de Embarque' ,16,1);
		ROLLBACK TRAN;
	END

GO


CREATE TRIGGER CruzeirosDB.TriggerAddBarcoTipoBarco ON CruzeirosDB.C_Barco
AFTER INSERT, UPDATE
AS 
	SET NOCOUNT ON;
	DECLARE @tipoBarcoTP AS varchar(100);
	DECLARE @tipoBarcoB AS varchar(100);
	SELECT @tipoBarcoB = C_TipoBarco_nomeTipoBarco FROM INSERTED;

	declare @countTipoBarco as int
	SELECT @countTipoBarco = count(*) FROM  CruzeirosDB.C_TipoBarco where nomeTipoBarco = @tipoBarcoB
	IF @countTipoBarco = 0
	BEGIN
		RAISERROR('Só é possivel inserir um tipo de Barco' ,16,1);
		ROLLBACK TRAN;
	END
go


