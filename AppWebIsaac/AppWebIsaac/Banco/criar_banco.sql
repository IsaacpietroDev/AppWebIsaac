CREATE DATABASE IF NOT EXISTS app_web_bd
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_general_ci;

USE app_web_bd;

CREATE TABLE IF NOT EXISTS processos (
  id_pro          INT AUTO_INCREMENT PRIMARY KEY,
  numero_pro      VARCHAR(200)  NOT NULL,
  data_pro        DATE          NOT NULL,
  interessado_pro VARCHAR(200)  NOT NULL,
  assunto_pro     VARCHAR(300)  NOT NULL,
  descricao_pro   VARCHAR(2000) NULL,
  situacao_pro    VARCHAR(50)   NOT NULL DEFAULT 'Aberto'
);
