-- CAMBIAR RNSLIB POR DESLIB
DROP PROCEDURE DC@DESLIB.SP_SOLMIN_SA_SERVICIOS_RZSC30_L02
GO
CREATE PROCEDURE DC@DESLIB.SP_SOLMIN_SA_SERVICIOS_RZSC30_L02(
		IN	IN_CCMPN		VARCHAR(2),
		IN	IN_CDVSN		VARCHAR(1),
		IN	IN_CCLNT		NUMERIC(6, 0),
		IN	IN_NOPRCN		NUMERIC(10, 0)
        )
RESULT SETS 1
LANGUAGE SQL
BEGIN 
DECLARE strSQL  			VARCHAR(6000)	DEFAULT '';

DECLARE CU_02 SCROLL CURSOR WITH RETURN FOR S2;


-- Armamos la consulta final con toda la información
SET strSQL = '	SELECT	NRTFSV, TarifaServicioPorDivision(CCMPN, CDVSN, CCLNT, FOPRCN, NRTFSV) NOMSER, QCNESP, TUNDIT, QATNAN
				FROM	RZSC30
				WHERE	CCLNT = ? And NOPRCN = ? And SESTRG <> ''*''
				AND		CCMPN = ? AND CDVSN = ? 
				ORDER BY NOPRCN ';
				
-- Realizamos la preparación de la Consulta para ser ejecutada
PREPARE S2 FROM strSQL;
OPEN CU_02 USING IN_CCLNT, IN_NOPRCN, IN_CCMPN, IN_CDVSN;

END

GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_SA_SERVICIOS_RZSC30_L02 TO PUBLIC