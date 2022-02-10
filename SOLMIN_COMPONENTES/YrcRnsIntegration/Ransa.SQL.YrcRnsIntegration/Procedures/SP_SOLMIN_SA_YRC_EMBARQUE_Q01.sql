-- CAMBIAR RNSLIB POR DESLIB
DROP PROCEDURE DC@RNSLIB.SP_SOLMIN_SA_YRC_EMBARQUE_Q01
GO
CREATE PROCEDURE DC@RNSLIB.SP_SOLMIN_SA_YRC_EMBARQUE_Q01( 
	IN	IN_NORSCI	NUMERIC(10, 0) 
) 
DYNAMIC RESULT SETS 1 
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
	DECLARE WK_CZNFCC		NUMERIC(3, 0)	DEFAULT 0;
	DECLARE WK_NORDAGE		NUMERIC(10, 0)	DEFAULT 0; 
	DECLARE WK_DORDAGE		DATE; 
	DECLARE WK_STIPREG		VARCHAR(250)	DEFAULT ''; 
	DECLARE WK_SNOMTER		VARCHAR(250)	DEFAULT ''; 
	DECLARE WK_SCANAL		VARCHAR(20)		DEFAULT ''; 
	DECLARE WK_SDUA			VARCHAR(20)		DEFAULT ''; 
	DECLARE WK_STRSQL		VARCHAR(200)	DEFAULT '';
	DECLARE WK_NDIASE 		NUMERIC(3, 0)	DEFAULT 0; 
	DECLARE WK_NDIALB 		NUMERIC(3, 0)	DEFAULT 0;  
	 
	-- Cursor para mostrar el resultado 
	DECLARE CU_02 SCROLL CURSOR WITH RETURN FOR S2 ; 
	-- Creamos la tabla temporal que contiene los datos del Reporte 
	
	DECLARE GLOBAL TEMPORARY TABLE SESSION.T005_EMBARQUE ( 
	SIDYRC		VARCHAR(22 ),		SIDRNS		VARCHAR(10),		SVIAJE		VARCHAR(250) ,		SNROEMB		VARCHAR(20), 
	CTIPTRA		VARCHAR(1),			STRANS		VARCHAR(250),		CCLIENT		NUMERIC(10, 0), 	CPAIORI		VARCHAR(3), 
	CPTOORI		VARCHAR(4),			CPAIDEST	VARCHAR(3),			CPTODEST	VARCHAR(4),			DZARPE		DATE, 
	DARRIBO		DATE,				NORDAGE		NUMERIC(10, 0),		DORDAGE		DATE,				STIPREG		VARCHAR(250), 
	SNOMTER		VARCHAR(250),		SDIALIB		NUMERIC(10, 0),		SSOBEST		NUMERIC(10, 0),		SCANAL		VARCHAR(20), 
	SOBS		VARCHAR(800),		CUSUCRE		VARCHAR(20) ,		DFECCRE		DATE,				CUSUACT		VARCHAR(20), 
	DFECACT		DATE,				CCODCON		VARCHAR(50),		SDUA		VARCHAR(20)	 ) WITH REPLACE NOT LOGGED ; 

	-- Obtengo la fecha y hora actual 
	SET	WK_FECHA = YEAR ( CURRENT DATE ) * 10000 + MONTH ( CURRENT DATE ) * 100 + DAY ( CURRENT DATE ) ; 
	SET	WK_HORA = HOUR ( CURRENT TIME ) * 10000 + MINUTE ( CURRENT TIME ) * 100 + SECOND ( CURRENT TIME ) ; 
  
	SELECT	T0.CCLNT, Trim(T0.NOREMB), NUMBERTODATE(T0.FORSCI) , T0.NDIALB, T0.NDIASE,
			( SELECT Trim(T1.TTRMAL) FROM RZZK35 T1 WHERE T1.CTRMAL = T0.CTRMAL ) , 
			( SELECT T1.PNNMOS FROM RZOL37 T1 WHERE T1.NORSCI = T0.NORSCI ),
			( SELECT T1.CZNFCC FROM RZOL37 T1 WHERE T1.NORSCI = T0.NORSCI ) 
	INTO	WK_CCLNT, WK_SIDYRC, WK_DORDAGE, WK_NDIALB, WK_NDIASE, WK_SNOMTER, WK_NORDAGE, WK_CZNFCC
	FROM	RZOL41 T0 
	WHERE	T0.NORSCI = IN_NORSCI; 
  
	SELECT	Trim(T2.TTPORG), Trim(T2.TCANAL), Trim(TNRODU)
	INTO	WK_STIPREG, WK_SCANAL, WK_SDUA
	FROM	RZOL50 T2 
	WHERE	T2.PNNMOS = WK_NORDAGE
	AND		T2.CZNFCC = WK_CZNFCC;

	INSERT INTO SESSION.T005_EMBARQUE(	SIDYRC, SIDRNS, NORDAGE, DORDAGE, STIPREG, SNOMTER, SCANAL, SDIALIB, SSOBEST, SOBS, SDUA, CCODCON, CUSUCRE, CUSUACT  )
	VALUES( WK_SIDYRC, Trim(IN_NORSCI), WK_NORDAGE, WK_DORDAGE, WK_STIPREG, WK_SNOMTER, WK_SCANAL, WK_NDIALB, WK_NDIASE, '', WK_SDUA, 'RNS000', 0, 0) ; 
  
	SET WK_STRSQL = 'Select	* From SESSION.T005_Embarque' ;	 
	-- Realizamos la preparaci�n de la Consulta para ser ejecutada 
	PREPARE S2 FROM WK_STRSQL ; 
	OPEN CU_02 ; 
END
--GO
--GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_SA_YRC_EMBARQUE_Q01 TO PUBLIC