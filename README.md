branch : master
Para rodar o projeto, precisará ter a sdk do .NET 8,e Docker na maquina que o Clonar!
Para Rodar em docker, contendo os requisitos acima, basta debugar ou rodar o Container pelo proprio Vs 2022
![image](https://github.com/Ga-Bicudo/Desafio-BackEnd/assets/66627874/23ee5a74-82d2-42e4-8da2-82bbfffa0bb4)
Pode tambem Rodar pelo ISS normalmente!
Para rodar o porjeto tambem precisará configurar o banco :
-Conectar ao PostgreSQL: Use uma ferramenta como pgAdmin, DBeaver ou o terminal psql.
Criar um Banco de Dados:
CREATE DATABASE MotorcycleRental;

Após Criado , criar as tabelas e tambem ja insira os dados:

CREATE TABLE Deliverymen (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    CNPJ VARCHAR(14) UNIQUE NOT NULL,
    BirthDate DATE NOT NULL,
    CNHNumber VARCHAR(20) UNIQUE NOT NULL,
    CNHType VARCHAR(10) NOT NULL CHECK (CNHType IN ('A', 'B', 'A+B')),
    CNHImagePath VARCHAR(255)
);

CREATE TABLE tbMotorcycles (
    Id SERIAL PRIMARY KEY,
    Year INT NOT NULL,
    Model VARCHAR(255) NOT NULL,
    Plate VARCHAR(7) UNIQUE NOT NULL
);

CREATE TABLE RentalPlans (
    Id SERIAL PRIMARY KEY,
    DurationDays INT NOT NULL,
    CostPerDay DECIMAL(10, 2) NOT NULL,
    FinePercentage DECIMAL(5, 2) NOT NULL
);

INSERT INTO RentalPlans (DurationDays, CostPerDay, FinePercentage) VALUES
(7, 30.00, 20.00),
(15, 28.00, 40.00),
(30, 22.00, 0.00),
(45, 20.00, 0.00),
(50, 18.00, 0.00);

CREATE TABLE Rentals (
    Id SERIAL PRIMARY KEY,
    DeliverymanId INT NOT NULL REFERENCES Deliverymen(Id),
    MotorcycleId INT NOT NULL REFERENCES Motorcycles(Id),
    RentalPlanId INT NOT NULL REFERENCES RentalPlans(Id),
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    ExpectedEndDate DATE NOT NULL
);

CREATE TABLE RentalReturns (
    Id SERIAL PRIMARY KEY,
    RentalId INT NOT NULL REFERENCES Rentals(Id),
    ActualEndDate DATE NOT NULL,
    TotalCost DECIMAL(10, 2) NOT NULL
);

Após,configurar o usuario no appsettings.json da aplicação que sera utlizado, segue como exemplo a imagem a seguir:
![image](https://github.com/Ga-Bicudo/Desafio-BackEnd/assets/66627874/ddee8855-7a72-4b8a-8a95-432a809a3d07)
O memso serve para a criação da Fila, depois de tudo acima,o programa irá Rodar!
