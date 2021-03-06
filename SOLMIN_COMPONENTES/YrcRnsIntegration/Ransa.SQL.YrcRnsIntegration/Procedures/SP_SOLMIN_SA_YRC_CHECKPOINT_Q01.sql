-- CAMBIAR RNSLIB POR RNSLIB
DROP PROCEDURE DC@RNSLIB.SP_SOLMIN_SA_YRC_CHECKPOINT_Q01
GO
CREATE PROCEDURE DC@RNSLIB.SP_SOLMIN_SA_YRC_CHECKPOINT_Q01( 
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
	DECLARE WK_CCLNT_YRC	NUMERIC(6, 0)	DEFAULT 0; 
	DECLARE WK_SIDYRC		VARCHAR(22)		DEFAULT ''; 
	DECLARE WK_NNROPAR		INTEGER			DEFAULT 0;
	DECLARE WK_STRSQL		VARCHAR(1000)	DEFAULT '';
	 
	-- Cursor para mostrar el resultado 
	DECLARE CU_02 SCROLL CURSOR WITH RETURN FOR S2 ; 
	-- Creamos la tabla temporal que contiene los datos del Reporte 
	
	DECLARE GLOBAL TEMPORARY TABLE SESSION.T008_CHECKPOINT ( 
	CCLIENT		INTEGER,	SNROOC		VARCHAR(20),	SNROITE		VARCHAR(15),	NNROPAR		INTEGER,
	CCODCP		VARCHAR(6),	NSEC		INTEGER,		DFECCP		DATE,			SCOMENT		VARCHAR(800),
	NORDEN		INTEGER,	CUSUCRE		VARCHAR(20),	DFECCRE		DATE,			CUSUACT		VARCHAR(20), 
	DFECACT		DATE  ) WITH REPLACE NOT LOGGED ;

	-- Obtengo la fecha y hora actual 
	SET	WK_FECHA = YEAR ( CURRENT DATE ) * 10000 + MONTH ( CURRENT DATE ) * 100 + DAY ( CURRENT DATE ) ; 
	SET	WK_HORA = HOUR ( CURRENT TIME ) * 10000 + MINUTE ( CURRENT TIME ) * 100 + SECOND ( CURRENT TIME ) ; 
  
	SELECT	T0.CCLNT, Trim(T0.NOREMB)
	INTO	WK_CCLNT, WK_SIDYRC
	FROM	RZOL41 T0 
	WHERE	T0.NORSCI = IN_NORSCI;
	
	IF WK_CCLNT = 2287 THEN
		SET WK_CCLNT_YRC = 29;
	ELSE
	    IF WK_CCLNT = 48916 THEN
			SET WK_CCLNT_YRC = 370;
		ELSE
		    IF WK_CCLNT = 46550 THEN
				SET WK_CCLNT_YRC = 371;
			ELSE
			    IF WK_CCLNT = 48623 THEN
					SET WK_CCLNT_YRC = 372;
				ELSE
				    IF WK_CCLNT = 48622 THEN
						SET WK_CCLNT_YRC = 373;
					END IF;
				END IF;
			END IF;
		END IF;
	END IF;
	
	FOR_LOOP:
	FOR FILA AS CU_01 CURSOR FOR
	SELECT  Trim(RZOL40.NORCML) NORCML, RZOL40.NRITEM, RZOL40.NRPEMB,
			RZOT01.NESTDO, NumberToDate(RZOT01.DFECREA) DFECREA, Trim(RZOT01.TOBS) TOBEST
	FROM    RZOL40,
			RZOT01
	WHERE   RZOL40.NORSCI = IN_NORSCI
	AND     RZOT01.CCLNT = RZOL40.CCLNT
	AND     RZOT01.NORCML = RZOL40.NORCML
	AND     RZOT01.NRITOC = RZOL40.NRITEM
	AND     RZOT01.NRPEMB = RZOL40.NRPEMB
	AND		RZOT01.SESTRG <> '*'
	AND		RZOT01.NESTDO BETWEEN 108 AND 126
	DO
		SET WK_NNROPAR = 1;
		
		IF FILA.NRPEMB <> '' THEN
			SELECT	RZOL39P.NRPARC 
			INTO	WK_NNROPAR
			FROM	RZOL39P 
			WHERE	RZOL39P.NRPEMB = FILA.NRPEMB
			AND		RZOL39P.CCLNT = WK_CCLNT;
		END IF;
		
		INSERT INTO SESSION.T008_CHECKPOINT ( CCLIENT, SNROOC, SNROITE, NNROPAR, CCODCP, NSEC, DFECCP, SCOMENT, CUSUACT, CUSUCRE )
		VALUES(WK_CCLNT_YRC, FILA.NORCML, FILA.NRITEM, WK_NNROPAR, 'RNS' || RIGHT(DIGITS(FILA.NESTDO),3), 1, FILA.DFECREA, FILA.TOBEST, 0, 0 );
	END FOR;
	
	SET WK_STRSQL = 'Select	* From SESSION.T008_CHECKPOINT Order By 2, 3, 4, 5 ';
	-- Realizamos la preparaci?n de la Consulta para ser ejecutada 
	PREPARE S2 FROM WK_STRSQL ; 
	OPEN CU_02 ; 
END
GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_SA_YRC_CHECKPOINT_Q01 TO PUBLIC