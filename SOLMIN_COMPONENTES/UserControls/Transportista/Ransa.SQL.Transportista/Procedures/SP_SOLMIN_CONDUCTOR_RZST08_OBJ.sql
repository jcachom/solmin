DROP PROCEDURE SP_SOLMIN_CONDUCTOR_RZST08_OBJ
GO
CREATE PROCEDURE SP_SOLMIN_CONDUCTOR_RZST08_OBJ(
		IN	IN_CCMPN		VARCHAR(2),
		IN	IN_CBRCNT		VARCHAR(15),
		OUT OU_MSGERR		VARCHAR(100)
        )
RESULT SETS 1
LANGUAGE SQL
BEGIN ATOMIC
--------------------------
-- Variables de Trabajo --
--------------------------
DECLARE strSQL  		VARCHAR(6000) DEFAULT '';
DECLARE	WK_SESTRG		VARCHAR(1) DEFAULT '';
DECLARE CU_01 SCROLL CURSOR WITH RETURN FOR S1;

SET OU_MSGERR = '';

SELECT	IFNULL(MAX(SESTRG), '') INTO WK_SESTRG 
FROM	RZST08 WHERE CBRCNT = IN_CBRCNT;

-- Verificamos 	si existe el Conductor con las caracterÝsticas proporcionadas
IF WK_SESTRG <> '' THEN
	IF WK_SESTRG <> '*' THEN
		SET strSQL = 'SELECT CCMPN, CBRCNT, TNMCMC, APEPAT, APEMAT, NCLICO, NLELCH, CSGRSC, TGRPSN, NumberToDate( FVEDNI ) FVEDNI, ' ||
						   ' NumberToDate( FEMLIC ) FEMLIC, NumberToDate( FVELIC ) FVELIC, CODSAP, FECING, TDRCC, NTERPM, TOBS, SINDRC, SESTRG ' ||
					 'FROM	RZST08 WHERE CCMPN = ? AND CBRCNT = ? ';
		PREPARE S1 FROM strSQL;
		OPEN CU_01 USING IN_CCMPN, IN_CBRCNT;
	ELSE
		SET OU_MSGERR = 'Conductor esta Anulado.';
	END IF;
ELSE
	SET OU_MSGERR = 'Conductor  no existe.';
END IF;

END
GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_CONDUCTOR_RZST08_OBJ TO PUBLIC