-- CAMBIAR RNSLIB POR DESLIB
DROP PROCEDURE DC@DESLIB.SP_SOLMIN_SA_SERVICIOS_DETALLE_RZSC31_L04
GO
CREATE PROCEDURE DC@DESLIB.SP_SOLMIN_SA_SERVICIOS_DETALLE_RZSC31_L04(
		IN	IN_CCLNT		NUMERIC(6, 0),
		IN	IN_NOPRCN		NUMERIC(10, 0),
		IN	IN_NORDSR		NUMERIC(10, 0),
		IN	IN_NSLCSR		NUMERIC(4, 0),
		IN	IN_NGUIRN		NUMERIC(10, 0),
		OUT	OU_MSGERR		VARCHAR(200)	-- Mensaje de Error
        )
RESULT SETS 1
LANGUAGE SQL
BEGIN ATOMIC
	--------------------------------------
	-- Variables de Trabajo - Seguridad --
	--------------------------------------
	DECLARE	WK_FECHA	NUMERIC(10, 0)	DEFAULT 0;
	DECLARE	WK_HORA		NUMERIC(10, 0)	DEFAULT 0;
	--------------------------
	-- Variables de Trabajo --
	--------------------------
	DECLARE WK_CMRCLR	VARCHAR(30)		DEFAULT '';
	DECLARE WK_TMRCD2	VARCHAR(50)		DEFAULT '';
	DECLARE WK_CMRCD	VARCHAR(10)		DEFAULT '';
	DECLARE WK_CUNCN5	VARCHAR(3)		DEFAULT '';
	DECLARE WK_CUNPS5	VARCHAR(3)		DEFAULT '';
	DECLARE WK_STRSQL	VARCHAR(200)	DEFAULT '';
	-- Cursor para mostrar el resultado
	DECLARE CU_02 SCROLL CURSOR WITH RETURN FOR S2;
	-- Creamos la tabla temporal que contiene los datos del Reporte
	Declare Global Temporary Table SESSION.tmpMercaderia(
	CMRCLR	VARCHAR(30),	TMRCD2	VARCHAR(50),	CMRCD	VARCHAR(10),	CTPOAT	VARCHAR(1),			NORDSR	NUMERIC(10, 0),
	NSLCSR	NUMERIC(4, 0),	CTPALM	VARCHAR(2),		CALMC	VARCHAR(2),		QTRMC	DECIMAL(15, 5),		CUNCN5	VARCHAR(3),
	QTRMP	DECIMAL(15, 5),	CUNPS5	VARCHAR(3),		NGUIRN	NUMERIC(10, 0),	SESTRG	VARCHAR(1) ) WITH REPLACE Not Logged;

	-- Obtengo la fecha y hora actual
	SET	WK_FECHA = YEAR(CURRENT DATE) * 10000 + MONTH(CURRENT DATE) * 100 + DAY(CURRENT DATE);
	SET	WK_HORA  = HOUR(CURRENT TIME) * 10000 + MINUTE(CURRENT TIME) * 100 + SECOND(CURRENT TIME);
	SET OU_MSGERR = '';
	
	-- Iniciamos el recorrido de los Bultos a ser insertados en la tabla de bultos
	FOR_LOOP:
	FOR FILA AS CU_01 CURSOR FOR
	SELECT  NORDSR, NSLCSR
	FROM    RZIT06
	WHERE   CCLNT3  = IN_CCLNT
	And 	NORDSR = IN_NORDSR
	And		NSLCSR = IN_NSLCSR
	UNION ALL
	SELECT  NORDSR, NSLCSR
	FROM    RZIT06
	WHERE   CCLNT3 = IN_CCLNT
	And 	NGUIRN = IN_NGUIRN
	And		NGUIRN <> 0
	ORDER BY 1, 2
	DO
		SELECT	CMRCLR, CMRCD, CUNCN5, CUNPS5
		INTO	WK_CMRCLR, WK_CMRCD, WK_CUNCN5, WK_CUNPS5
		FROM	RZIT05 
		WHERE	RZIT05.NORDSR = FILA.NORDSR;
		
		SELECT	TMRCD2 
		INTO	WK_TMRCD2
		FROM	RZOL11 
		WHERE	CCLNT  = IN_CCLNT
		AND		CMRCLR = WK_CMRCLR;

		Insert Into SESSION.tmpMercaderia(CMRCLR, TMRCD2, CMRCD, CTPOAT, NORDSR, NSLCSR, CTPALM, CALMC, QTRMC, CUNCN5, QTRMP, CUNPS5, NGUIRN, SESTRG)
		select	WK_CMRCLR, WK_TMRCD2, WK_CMRCD, RZ06.CTPOAT, RZ06.NORDSR, RZ06.NSLCSR, RZ06.CTPALM, RZ06.CALMC, RZ06.QTRMC, WK_CUNCN5, RZ06.QTRMP, 
				WK_CUNPS5, RZ06.NGUIRN, RZ06.SESTRG
		From	RZIT06 RZ06
		Where	RZ06.NORDSR = FILA.NORDSR
		And		RZ06.NSLCSR = FILA.NSLCSR;
	END FOR;
	
	SET WK_STRSQL = 'Select	CMRCLR, TMRCD2, CMRCD, CTPOAT, NORDSR, NSLCSR, CTPALM, CALMC, QTRMC, CUNCN5, QTRMP, CUNPS5, NGUIRN, SESTRG
					 From	SESSION.tmpMercaderia';	
	-- Realizamos la preparación de la Consulta para ser ejecutada
	PREPARE S2 FROM WK_STRSQL;
	OPEN CU_02;
END
GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_SA_SERVICIOS_DETALLE_RZSC31_L04 TO PUBLIC