CREATE DATABASE tecass_test

CREATE TABLE `clientes` (
  `cliente_id` int NOT NULL,
  `nombre` varchar(60) NOT NULL,
  `numero_identificacion` varchar(20) NOT NULL,
  PRIMARY KEY (`cliente_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci

CREATE TABLE `cuentas` (
  `cuenta_id` int NOT NULL,
  `cliente_id` int NOT NULL,
  `numero_cuenta` int NOT NULL,
  `saldo_actual` double NOT NULL,
  PRIMARY KEY (`cuenta_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci

CREATE TABLE `tipo_movimiento` (
  `tipo_id` int NOT NULL,
  `descripcion` varchar(60) NOT NULL,
  PRIMARY KEY (`tipo_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci

insert into tipo_movimiento values (1, 'Depósitos')
insert into tipo_movimiento values (2, 'Retiros')

CREATE TABLE `movimientos` (
  `movimiento_id` int NOT NULL,
  `cuenta_id` int NOT NULL,
  `tipo_id` int NOT NULL,
  `monto` double NOT NULL,
  PRIMARY KEY (`movimiento_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
