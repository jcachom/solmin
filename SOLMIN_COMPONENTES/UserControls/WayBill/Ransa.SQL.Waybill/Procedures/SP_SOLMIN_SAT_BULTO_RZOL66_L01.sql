-- CAMBIAR RNSLIB POR DESLIB
DROP PROCEDURE DC@DESLIB.SP_SOLMIN_SAT_BULTO_RZOL66_L01
GO
CREATE PROCEDURE DC@DESLIB.SP_SOLMIN_SAT_BULTO_RZOL66_L01(
		IN	IN_CCLNT		NUMERIC(6, 0),
		IN	IN_CREFFW		VARCHAR(20)
        )
RESULT SETS 1
LANGUAGE SQL
BEGIN 
DECLARE strSQL  			VARCHAR(6000)	DEFAULT '';

DECLARE CU_02 SCROLL CURSOR WITH RETURN FOR S2;

SET strSQL = '	Select  RZOL66.CREFFW, RZOL66.NORCML, RZOL66.NRITOC, RZOL66.CIDPAQ, 
							  (	Select RZOL39.TDITES From RZOL39 
								Where RZOL39.CCLNT = RZOL66.CCLNT AND RZOL66.NORCML = RZOL39.NORCML AND RZOL66.NRITOC = RZOL39.NRITOC ) TDITES,
								RZOL66.QBLTSR, RZOL66.QPEPQT, RZOL66.TUNPSO, RZOL66.QVOPQT, RZOL66.TUNVOL,
							  (	Select RZOL39.TLUGEN From RZOL39 
								Where RZOL39.CCLNT = RZOL66.CCLNT AND RZOL66.NORCML = RZOL39.NORCML AND RZOL66.NRITOC = RZOL39.NRITOC ) TLUGEN,
								RZOL66.NGRPRV
						From	RZOL66
						Where   RZOL66.CCLNT =  ? 
						And		RZOL66.CREFFW =  ? 
						And		RZOL66.SESTRG <> ''*'' 
						Order by RZOL66.NORCML, RZOL66.NRITOC ';	
-- Realizamos la preparación de la Consulta para ser ejecutada
PREPARE S2 FROM strSQL;
OPEN CU_02 USING IN_CCLNT, IN_CREFFW;

END

GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_SAT_BULTO_RZOL66_L01 TO PUBLIC