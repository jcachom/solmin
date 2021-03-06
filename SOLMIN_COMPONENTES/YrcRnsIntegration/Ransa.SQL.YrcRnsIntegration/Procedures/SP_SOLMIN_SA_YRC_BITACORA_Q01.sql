-- CAMBIAR RNSLIB POR RNSLIB
DROP PROCEDURE DC@RNSLIB.SP_SOLMIN_SA_YRC_BITACORA_Q01
GO
CREATE PROCEDURE DC@RNSLIB.SP_SOLMIN_SA_YRC_BITACORA_Q01( 
	IN	IN_NORSCI	NUMERIC(10, 0) 
) 
RESULT SETS 1 
LANGUAGE SQL 
BEGIN ATOMIC 
	---------------------------------------
	-- Variables de Trabajo - Seguridad  -- 
	---------------------------------------
	DECLARE	WK_FECHA	NUMERIC (10 , 0)		DEFAULT 0; 
	DECLARE	WK_HORA		NUMERIC (10 , 0)		DEFAULT 0; 
	 ---------------------------
	 -- Variables de Trabajo  -- 
	 ---------------------------
	DECLARE WK_CCLNT 		NUMERIC(6, 0)	DEFAULT 0; 
	DECLARE WK_SIDYRC		VARCHAR(22)		DEFAULT ''; 
	DECLARE WK_STRSQL		VARCHAR(1000)	DEFAULT '';
	 
	-- Cursor para mostrar el resultado 
	DECLARE CU_02 SCROLL CURSOR WITH RETURN FOR S2 ; 
	-- Creamos la tabla temporal que contiene los datos del Reporte 
	
	DECLARE GLOBAL TEMPORARY TABLE SESSION.T013_BITACORA ( 
	SIDYRC		VARCHAR(22),		NSEC		NUMERIC(6, 0),		DFECBIT		DATE,		SCOMBIT		VARCHAR(800),
	NYRCRNS		VARCHAR(3),			CUSUCRE		VARCHAR(20) ,		DFECCRE		DATE,		CUSUACT		VARCHAR(20), 
	DFECACT		DATE ) WITH REPLACE NOT LOGGED ; 
	
	-- Obtengo la fecha y hora actual 
	SET	WK_FECHA = YEAR ( CURRENT DATE ) * 10000 + MONTH ( CURRENT DATE ) * 100 + DAY ( CURRENT DATE ) ; 
	SET	WK_HORA = HOUR ( CURRENT TIME ) * 10000 + MINUTE ( CURRENT TIME ) * 100 + SECOND ( CURRENT TIME ) ; 
  
	SELECT	T0.CCLNT, Trim(T0.NOREMB)
	INTO	WK_CCLNT, WK_SIDYRC
	FROM	RZOL41 T0 
	WHERE	T0.NORSCI = IN_NORSCI;
	
	FOR_LOOP:
	FOR FILA AS CU_01 CURSOR FOR
	SELECT	NRITEM, NumberToDate(TFCOBS) TFCOBS, Trim(TOBS) TOBS
	FROM	RZOL43
	WHERE	NORSCI = IN_NORSCI
	DO
		INSERT INTO SESSION.T013_BITACORA( SIDYRC, NSEC, DFECBIT, SCOMBIT, CUSUCRE, CUSUACT )
		VALUES(WK_SIDYRC, FILA.NRITEM, FILA.TFCOBS, FILA.TOBS, 0, 0 );
	END FOR;
  
	SET WK_STRSQL = 'Select	* From SESSION.T013_BITACORA Order By NSEC' ;	 
	-- Realizamos la preparación de la Consulta para ser ejecutada 
	PREPARE S2 FROM WK_STRSQL ; 
	OPEN CU_02 ; 
END
GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_SA_YRC_BITACORA_Q01 TO PUBLIC