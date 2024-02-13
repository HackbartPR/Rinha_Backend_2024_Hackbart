SET statement_timeout = 0; --LEMBRAR DE VERIFICAR UM VALOR BOM PARA ESTE CASO
SET lock_timeout = 0; --LEMBRAR DE VERIFICAR UM VALOR BOM PARA ESTE CASO

CREATE TABLE "Clientes" (
    "Id" SERIAL PRIMARY KEY,
    "Limite" BIGINT NOT NULL DEFAULT 0,
    "Saldo" BIGINT NOT NULL DEFAULT 0
);

CREATE TABLE "Transacoes" (
    "Id" SERIAL PRIMARY KEY,
    "ClienteId" INT NOT NULL,
    "Valor" BIGINT NOT NULL,
    "Tipo" CHAR(1) NOT NULL,
    "Descricao" VARCHAR(10),
    "DataCriacao" TIMESTAMP WITH TIME ZONE NOT NULL,
    FOREIGN KEY ("ClienteId") REFERENCES "Clientes"("Id")
);

DO $$
BEGIN
  INSERT INTO "Clientes" ("Limite")
  VALUES
    (100000),
    (80000),
    (1000000),
    (10000000),
    (500000);
END; $$