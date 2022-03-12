CREATE FUNCTION CruzeirosDB.getNumbilhete (@numCruzeiro INT) RETURNS INT
AS
	BEGIN 
		declare @numbilhete as int
		SELECT @numbilhete = MAX(numBilhete)+1 FROM  CruzeirosDB.C_Reserva where C_Cruzeiro_numCruzeiro = @numCruzeiro
		return @numbilhete
	end
go

CREATE FUNCTION CruzeirosDB.getNextNumCliente () RETURNS INT
AS
	BEGIN 
		declare @numeroCliente as int
		SELECT @numeroCliente = numCliente + 1 FROM  CruzeirosDB.C_Cliente
		return @numeroCliente
	end
go

CREATE FUNCTION CruzeirosDB.getNextNumTripulante () RETURNS INT
AS
	BEGIN 
		declare @numeroTripulante as int
		SELECT @numeroTripulante = numTripulante + 1 FROM  CruzeirosDB.C_Tripulante
		return @numeroTripulante
	end
go

CREATE FUNCTION CruzeirosDB.getNextNumCruzeiro () RETURNS INT
AS
	BEGIN 
		declare @numeroCruzeiro as int
		SELECT @numeroCruzeiro = MAX(numCruzeiro)+1 FROM  CruzeirosDB.C_Cruzeiro
		return @numeroCruzeiro
	end
go

CREATE FUNCTION CruzeirosDB.getNextPartida () RETURNS INT
AS
	BEGIN 
		declare @numero as int
		SELECT @numero= MAX(id)+1 FROM  CruzeirosDB.C_Partida
		return @numero
	end
go

CREATE FUNCTION CruzeirosDB.getNextChegada () RETURNS INT
AS
	BEGIN 
		declare @numero as int
		SELECT @numero= MAX(id)+1 FROM  CruzeirosDB.C_Chegada
		return @numero
	end
go

CREATE FUNCTION CruzeirosDB.getVagascriarCruzeiro (@codigoBarco INT) RETURNS INT
AS
	BEGIN 
		declare @vagas as int
		SELECT @vagas = numTotalBilhetes FROM  CruzeirosDB.C_Barco where @codigoBarco = codigoBarco
		return @vagas
	end
go


CREATE FUNCTION CruzeirosDB.gethorapartida (@numCruzeiro INT) RETURNS time
AS
	BEGIN 
		declare @horaPartida as time
		SELECT @horaPartida = horapartida FROM  CruzeirosDB.C_Partida where C_Cruzeiro_numCruzeiro = @numCruzeiro
		return @horaPartida
	end
go

CREATE FUNCTION CruzeirosDB.gethorachegada (@numCruzeiro INT) RETURNS time
AS
	BEGIN 
		declare @horaChegada as time
		SELECT @horaChegada= horaChegada FROM  CruzeirosDB.C_Chegada where C_Cruzeiro_numCruzeiro = @numCruzeiro
		return @horaChegada
	end
go

CREATE FUNCTION CruzeirosDB.getIdPartida (@numCruzeiro INT) RETURNS int
AS
	BEGIN 
		declare @idPartida as int
		SELECT @idPartida = id FROM  CruzeirosDB.C_Partida where C_Cruzeiro_numCruzeiro = @numCruzeiro
		return @idPartida
	end
go

CREATE FUNCTION CruzeirosDB.getIdChegada (@numCruzeiro INT) RETURNS int
AS
	BEGIN 
		declare @idChegada as int
		SELECT @idChegada= id FROM  CruzeirosDB.C_Chegada where C_Cruzeiro_numCruzeiro = @numCruzeiro
		return @idChegada
	end
go





	