-- CAMBIAR RNSLIB POR RNSLIB
DROP PROCEDURE DC@RNSLIB.SP_SOLMIN_SA_YRC_EMBARQUE_SUSTENTO_Q01
GO
CREATE PROCEDURE DC@RNSLIB.SP_SOLMIN_SA_YRC_EMBARQUE_SUSTENTO_Q01( 
	IN	IN_NORSCI	NUMERIC(10, 0) 
) 
RESULT SETS 2
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
	DECLARE CU_01 SCROLL CURSOR WITH RETURN FOR S1 ; 
	DECLARE CU_02 SCROLL CURSOR WITH RETURN FOR S2 ; 
	-- Creamos la tabla temporal que contiene los datos del Reporte 
	
	DECLARE GLOBAL TEMPORARY TABLE SESSION.T011_SUSTENTO( 
	CIDSUST		INTEGER,		NYRCRNS		VARCHAR(3),		CTIPDOC		INTEGER,		SRUTA		VARCHAR(800),
	CUSUCRE		VARCHAR(20) ,	DFECCRE		DATE,			CUSUACT		VARCHAR(20),	DFECACT		DATE ) WITH REPLACE NOT LOGGED ; 
	
	DECLARE GLOBAL TEMPORARY TABLE SESSION.T012_EMB_SUSTENTO( 
	CIDSUST		INTEGER,		NYRCRNS		VARCHAR(3),		SIDYRC		VARCHAR(22),	CUSUCRE		VARCHAR(20),
	DFECCRE		DATE,			CUSUACT		VARCHAR(20),	DFECACT		DATE ) WITH REPLACE NOT LOGGED ; 
	
	-- Obtengo la fecha y hora actual 
	SET	WK_FECHA = YEAR ( CURRENT DATE ) * 10000 + MONTH ( CURRENT DATE ) * 100 + DAY ( CURRENT DATE ) ; 
	SET	WK_HORA = HOUR ( CURRENT TIME ) * 10000 + MINUTE ( CURRENT TIME ) * 100 + SECOND ( CURRENT TIME ) ; 
  
	SELECT	T0.CCLNT, Trim(T0.NOREMB)
	INTO	WK_CCLNT, WK_SIDYRC
	FROM	RZOL41 T0 
	WHERE	T0.NORSCI = IN_NORSCI;
	
	FOR_LOOP:
	FOR FILA AS CU_01 CURSOR FOR
	SELECT	SECSUS, Trim(URLARC) URLARC
	FROM	RZOL53 
	WHERE	NORSCI = IN_NORSCI 
	AND		NDOCIN = 2
	DO
		INSERT INTO SESSION.T011_SUSTENTO(CIDSUST, NYRCRNS, CTIPDOC, SRUTA, CUSUCRE, CUSUACT)
		VALUES(FILA.SECSUS, 'RNS', 1, 'http://asp.ransa.net/wapmineria/imagenes/solmin/SOLMIN_SC/6240_2_2.tif', 0, 0 );
		-- FILA.URLARC
		
		INSERT INTO SESSION.T012_EMB_SUSTENTO(CIDSUST, NYRCRNS, SIDYRC, CUSUCRE, CUSUACT)
		VALUES(FILA.SECSUS, 'RNS', WK_SIDYRC, 0, 0 );
	END FOR;
	
	SET WK_STRSQL = 'Select	* From SESSION.T011_SUSTENTO Order By CIDSUST' ;	 
	-- Realizamos la preparaci�n de la Consulta para ser ejecutada 
	PREPARE S1 FROM WK_STRSQL ; 
	OPEN CU_01 ; 
  
	SET WK_STRSQL = 'Select	* From SESSION.T012_EMB_SUSTENTO Order By CIDSUST' ;	 
	-- Realizamos la preparaci�n de la Consulta para ser ejecutada 
	PREPARE S2 FROM WK_STRSQL ; 
	OPEN CU_02 ; 
END
GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_SA_YRC_EMBARQUE_SUSTENTO_Q01 TO PUBLIC