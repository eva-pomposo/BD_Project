USE p5g7
GO
/*
DROP TABLE CruzeirosDB.C_Atraca;
DROP TABLE CruzeirosDB.C_Trabalha;
DROP TABLE CruzeirosDB.C_Tripulante;
DROP TABLE CruzeirosDB.C_IATE;
DROP TABLE CruzeirosDB.C_NavioHotel;
DROP TABLE CruzeirosDB.C_Reserva;
DROP TABLE CruzeirosDB.C_Cliente;
DROP TABLE CruzeirosDB.C_Pessoa;
DROP TABLE CruzeirosDB.C_Chegada;
DROP TABLE CruzeirosDB.C_Partida;
DROP TABLE CruzeirosDB.C_Cais;
DROP TABLE CruzeirosDB.C_Cruzeiro;
DROP TABLE CruzeirosDB.C_Barco;
DROP TABLE CruzeirosDB.C_TipoBarco;
*/

CREATE TABLE CruzeirosDB.C_TipoBarco(
	nomeTipoBarco varchar(100) NOT NULL PRIMARY KEY,
	maxBilhetes int,
	classificacao int
)
GO
CREATE TABLE CruzeirosDB.C_Barco(
	codigoBarco int NOT NULL PRIMARY KEY,
	numTotalBilhetes int,
	nomeBarco varchar(100),
	C_TipoBarco_nomeTipoBarco varchar(100) NOT NULL,
	FOREIGN KEY (C_TipoBarco_nomeTipoBarco) REFERENCES CruzeirosDB.C_TipoBarco(nomeTipoBarco)
)
GO
CREATE TABLE CruzeirosDB.C_Cruzeiro(
	numCruzeiro int NOT NULL PRIMARY KEY, 
	dataEmbarque date NOT NULL,
	dataDesembarque date NOT NULL,
	vagas int,
	C_Barco_codigoBarco int,
	FOREIGN KEY (C_Barco_codigoBarco) REFERENCES CruzeirosDB.C_Barco(codigoBarco)

)
GO
CREATE TABLE CruzeirosDB.C_Cais(
	codigo int NOT NULL PRIMARY KEY,
	localidade varchar(1000),
	nome varchar(100)
)
GO
CREATE TABLE CruzeirosDB.C_Partida(
	id int NOT NULL PRIMARY KEY,
	horaPartida time NOT NULL,
	C_Cruzeiro_numCruzeiro int,
	C_Cais_codigo int NOT NULL,
	FOREIGN KEY (C_Cruzeiro_numCruzeiro) REFERENCES CruzeirosDB.C_Cruzeiro(numCruzeiro),
	FOREIGN KEY (C_Cais_codigo) REFERENCES CruzeirosDB.C_Cais(codigo)
)
GO
CREATE TABLE CruzeirosDB.C_Chegada(
	id int NOT NULL PRIMARY KEY,
	horaChegada time NOT NULL,
	C_Cruzeiro_numCruzeiro int,
	C_Cais_codigo int NOT NULL,
	FOREIGN KEY (C_Cruzeiro_numCruzeiro) REFERENCES CruzeirosDB.C_Cruzeiro(numCruzeiro),
	FOREIGN KEY (C_Cais_codigo) REFERENCES CruzeirosDB.C_Cais(codigo)
)
GO
CREATE TABLE CruzeirosDB.C_Pessoa(
	numTelemovel varchar(100),
	nome varchar(100),
	email varchar(1000),
	numCC int NOT NULL PRIMARY KEY
)
GO
CREATE TABLE CruzeirosDB.C_Cliente(
	C_Pessoa_numCC int NOT NULL PRIMARY KEY,
	numCliente int NOT NULL,
	FOREIGN KEY (C_Pessoa_numCC) REFERENCES CruzeirosDB.C_Pessoa(numCC)

)
GO
CREATE TABLE CruzeirosDB.C_Reserva(
	nomeCliente varchar(100),
	numBilhete int NOT NULL,
	C_Data date,
	C_Cruzeiro_numCruzeiro int not null,
	C_Cliente_C_Pessoa_numCC int NOT NULL,
	FOREIGN KEY (C_Cruzeiro_numCruzeiro) REFERENCES CruzeirosDB.C_Cruzeiro(numCruzeiro),
	FOREIGN KEY (C_Cliente_C_Pessoa_numCC) REFERENCES CruzeirosDB.C_Cliente(C_Pessoa_numCC),
	PRIMARY KEY (numBilhete, C_Cruzeiro_numCruzeiro)
)
GO
CREATE TABLE CruzeirosDB.C_NavioHotel(
	numQuartos int NOT NULL,
	piscina bit NOT NULL,
	C_TipoBarco_nomeTipoBarco varchar(100) NOT NULL PRIMARY KEY,
	FOREIGN KEY (C_TipoBarco_nomeTipoBarco) REFERENCES CruzeirosDB.C_TipoBarco(nomeTipoBarco)
)
GO
CREATE TABLE CruzeirosDB.C_IATE(
	arCondicionado bit NOT NULL,
	heliponto bit NOT NULL,
	C_TipoBarco_nomeTipoBarco varchar(100) NOT NULL PRIMARY KEY,
	FOREIGN KEY (C_TipoBarco_nomeTipoBarco) REFERENCES CruzeirosDB.C_TipoBarco(nomeTipoBarco)
)
GO 
CREATE TABLE CruzeirosDB.C_Tripulante(
	numTripulante int NOT NULL,
	C_Pessoa_numCC int NOT NULL PRIMARY KEY,
	FOREIGN KEY (C_Pessoa_numCC) REFERENCES CruzeirosDB.C_Pessoa(numCC)
)
GO
CREATE TABLE CruzeirosDB.C_Trabalha(
	C_Tripulante_C_Pessoa_numCC  int,
	C_Cruzeiro_numCruzeiro int not null,
	PRIMARY KEY(C_Tripulante_C_Pessoa_numCC, C_Cruzeiro_numCruzeiro),
	FOREIGN KEY (C_Tripulante_C_Pessoa_numCC) REFERENCES CruzeirosDB.C_Tripulante(C_Pessoa_numCC),
	FOREIGN KEY (C_Cruzeiro_numCruzeiro) REFERENCES CruzeirosDB.C_Cruzeiro(numCruzeiro)
	
)
GO
CREATE TABLE CruzeirosDB.C_Atraca(
	C_Cais_codigo int NOT NULL,
	C_TipoBarco_nomeTipoBarco varchar(100) NOT NULL,
	PRIMARY KEY(C_Cais_codigo, C_TipoBarco_nomeTipoBarco),
	FOREIGN KEY (C_Cais_codigo) REFERENCES CruzeirosDB.C_CaiS(codigo),
	FOREIGN KEY (C_TipoBarco_nomeTipoBarco) REFERENCES CruzeirosDB.C_TipoBarco(nomeTipoBarco)
)
GO