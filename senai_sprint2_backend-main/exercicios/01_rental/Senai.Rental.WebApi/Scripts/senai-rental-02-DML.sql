USE M_Rental

INSERT INTO EMPRESA (nomeEmpresa)
VALUES('ALUGUEL 1'), ('ALUGUEL 2')

INSERT INTO MARCA (nomeMarca)
VALUES('BMW'), ('AUDI')

INSERT INTO MODELO (nomeModelo, idMarca)
VALUES('M3', 1), ('320I', 1), ('R8', 2)

INSERT INTO CLIENTE (nomeCliente, sobrenomeCliente, cpfCliente)
VALUES('RAYSSA', 'Besse', 333), ('ARMANDO', 'Besse', 666)

INSERT INTO ALUGUEL (idCliente, dataRetirada, dataDevolucao)
VALUES(1, '02/09/2021', '12/09/2021'), (2, '03/09/2021', '13/09/2021')

INSERT INTO VEICULO (idEmpresa, idModelo, idAluguel, placaVeiculo)
VALUES(1, 2, 1, '012938'), (2, 1, 2, '47U898')
