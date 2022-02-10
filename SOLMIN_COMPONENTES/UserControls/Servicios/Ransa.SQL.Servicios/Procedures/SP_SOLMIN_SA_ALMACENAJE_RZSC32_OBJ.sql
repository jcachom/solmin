-- CAMBIAR RNSLIB POR DESLIB
DROP PROCEDURE DC@DESLIB.SP_SOLMIN_SA_ALMACENAJE_RZSC32_OBJ
GO
CREATE PROCEDURE DC@DESLIB.SP_SOLMIN_SA_ALMACENAJE_RZSC32_OBJ(
		IN	IN_CCLNT		NUMERIC(6, 0),
		IN	IN_NPRALM		NUMERIC(10, 0)
        )
RESULT SETS 1
LANGUAGE SQL
BEGIN 
DECLARE		strSQL			VARCHAR(1000);

DECLARE CU_00 SCROLL CURSOR WITH RETURN FOR S0;

-- Armamos la consulta final con toda la información
SET strSQL =  '	SELECT NPRALM, CCLNT,  NANPRC, NMES, NumberToDate(FECINI) FECINI, NumberToDate(FECFIN) FECFIN, OBSERV, NDIALI, ' ||
					 ' STPOFC, TQAROC, TQPSOB, TQVLBT, TNDPER, TNDAFA, SESTRG ' ||
			  '	FROM RZSC32 ' || 
			  '	WHERE	CCLNT	= ? ' || 
			  '	AND		NPRALM	= ? ' ||
			  ' ORDER BY NPRALM DESC ';
-- Realizamos la preparación de la Consulta para ser ejecutada
PREPARE S0 FROM strSQL;
OPEN CU_00 USING IN_CCLNT, IN_NPRALM;

END

GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_SA_ALMACENAJE_RZSC32_OBJ TO PUBLIC