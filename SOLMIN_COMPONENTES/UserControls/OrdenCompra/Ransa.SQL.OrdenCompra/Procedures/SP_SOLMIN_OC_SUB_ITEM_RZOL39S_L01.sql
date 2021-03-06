 -- CAMBIAR RNSLIB POR DESLIB
DROP PROCEDURE SP_SOLMIN_OC_SUB_ITEM_RZOL39S_L01
GO
CREATE PROCEDURE SP_SOLMIN_OC_SUB_ITEM_RZOL39S_L01(
		IN	IN_CCLNT		NUMERIC(6, 0),
		IN	IN_NORCML		VARCHAR(35),
		IN	IN_NRITOC		NUMERIC(6, 0)
        )
RESULT SETS 1
LANGUAGE SQL
BEGIN
DECLARE strSQL  			VARCHAR(6000)	DEFAULT '';
DECLARE CU_02 SCROLL CURSOR WITH RETURN FOR S2;


-- Armamos la consulta final con toda la información
SET strSQL = '	SELECT	NORCML, NRITOC,SBITOC, TCITCL, TCITPR, TDITES, CPTDAR, QCNTIT, TUNDIT, IVUNIT, IVTOIT, QCNTIT - QCNRCP QCNPEN
				FROM	RZOL39S
				WHERE	NORCML = ? And CCLNT = ? AND NRITOC=? AND SESTRG <> ''*'' 
				ORDER BY NRITOC ';
-- Realizamos la preparación de la Consulta para ser ejecutada
PREPARE S2 FROM strSQL;
OPEN CU_02 USING IN_NORCML, IN_CCLNT,IN_NRITOC;

END

GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_OC_SUB_ITEM_RZOL39S_L01 TO PUBLIC