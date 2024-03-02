-- CREATE UNLOGGED TABLE "Clientes" (
--     "Id" SERIAL PRIMARY KEY,
--     "Limite" BIGINT NOT NULL DEFAULT 0,
--     "Saldo" BIGINT NOT NULL DEFAULT 0
-- );

-- CREATE UNLOGGED TABLE "Transacoes" (
--     "Id" SERIAL PRIMARY KEY,
--     "ClienteId" INT NOT NULL,
--     "Valor" BIGINT NOT NULL,
--     "Tipo" VARCHAR(1) NOT NULL,
--     "Descricao" VARCHAR(30),
--     "Realizada_Em" TIMESTAMP WITH TIME ZONE NOT NULL,
--     FOREIGN KEY ("ClienteId") REFERENCES "Clientes"("Id")
-- );

CREATE TABLE "Clientes" (
    "Id" SERIAL PRIMARY KEY,
    "Limite" BIGINT NOT NULL DEFAULT 0,
    "Saldo" BIGINT NOT NULL DEFAULT 0
);

CREATE TABLE "Transacoes" (
    "Id" SERIAL PRIMARY KEY,
    "ClienteId" INT NOT NULL,
    "Valor" BIGINT NOT NULL,
    "Tipo" VARCHAR(1) NOT NULL,
    "Descricao" VARCHAR(30),
    "Realizada_Em" TIMESTAMP WITH TIME ZONE NOT NULL,
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