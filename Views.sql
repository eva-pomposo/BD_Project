go
CREATE VIEW CruzeirosDB.listCruzeiro AS
	Select C.numCruzeiro, C.dataEmbarque, C.dataDesembarque, C.vagas, C.C_Barco_codigoBarco, P.horaPartida, Che.horaChegada, CaisP.localidade as localidadepartida, CaisP.nome as nomePartida, CaisC.localidade as localidadeChegada, CaisC.nome as nomeChegada, CaisC.codigo as idChegada, CaisP.codigo as idPartida
	FROM CruzeirosDB.C_Cruzeiro AS C Join CruzeirosDB.C_Partida AS P ON P.C_Cruzeiro_numCruzeiro=C.numCruzeiro 
		JOIN CruzeirosDB.C_Chegada AS Che ON Che.C_Cruzeiro_numCruzeiro=C.numCruzeiro JOIN CruzeirosDB.C_Cais AS CaisC ON CaisC.codigo=Che.C_Cais_codigo JOIN CruzeirosDB.C_Cais AS CaisP ON CaisP.codigo=P.C_Cais_codigo ;
go

go
CREATE VIEW CruzeirosDB.listReserva AS
	SELECT * FROM CruzeirosDB.C_Reserva
GO
select * from CruzeirosDB.C_Cais
