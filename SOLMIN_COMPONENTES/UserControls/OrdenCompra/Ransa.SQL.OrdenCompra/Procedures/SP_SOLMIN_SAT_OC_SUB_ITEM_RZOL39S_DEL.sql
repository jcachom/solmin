-- CAMBIAR RNSLIB POR DESLIB
DROP PROCEDURE SP_SOLMIN_SAT_OC_SUB_ITEM_RZOL39S_DEL
GO
CREATE PROCEDURE SP_SOLMIN_SAT_OC_SUB_ITEM_RZOL39S_DEL(
		IN	IN_CCLNT		NUMERIC(6, 0),
		IN	IN_NORCML		VARCHAR(35),
		IN	IN_NRITOC		NUMERIC(6, 0),
		IN	IN_SBITOC		VARCHAR(10),
		IN	IN_USUARI  		VARCHAR(10),
		OUT OU_STULTR		VARCHAR(200),	-- Mensaje de Error
		OUT OU_MSGERR		VARCHAR(200)	-- Mensaje de Error
        )
RESULT SETS 0
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
DECLARE	WK_NORCML	VARCHAR(35)		DEFAULT '';
DECLARE	WK_QCNRCP	NUMERIC(10, 0)	DEFAULT 0;
DECLARE	WK_SESTRG	VARCHAR(1)		DEFAULT '';

-- Obtengo la fecha y hora actual
SET	WK_FECHA = YEAR(CURRENT DATE) * 10000 + MONTH(CURRENT DATE) * 100 + DAY(CURRENT DATE);
SET	WK_HORA  = HOUR(CURRENT TIME) * 10000 + MINUTE(CURRENT TIME) * 100 + SECOND(CURRENT TIME);

SET OU_MSGERR = '';
SET OU_STULTR = 'N';

SELECT	NORCML, QCNRCP, SESTRG
INTO	WK_NORCML, WK_QCNRCP, WK_SESTRG	
FROM	RZOL39S
WHERE	NORCML = IN_NORCML
AND		CCLNT  = IN_CCLNT
AND		NRITOC = IN_NRITOC
AND		SBITOC = IN_SBITOC;

IF WK_NORCML <> '' THEN
	IF WK_QCNRCP = 0 THEN
		IF WK_SESTRG <> '*' THEN
			UPDATE	RZOL39S
			SET		SESTRG = '*',
					CULUSA = IN_USUARI,
					FULTAC = WK_FECHA,
					HULTAC = WK_HORA,
					UPDATE_IDENT = UPDATE_IDENT + 1
			WHERE	NORCML = IN_NORCML
			AND		CCLNT  = IN_CCLNT
			AND		NRITOC = IN_NRITOC
			AND		SBITOC = IN_SBITOC;
		ELSE
			SET OU_MSGERR = 'Item de la Orden de Compra ya se encuentra ANULADO.';
		END IF;
	
	ELSE
		SET OU_MSGERR = 'Item de la Orden de Compra posee MOVIMIENTO.';
	END IF;
	
ELSE
	SET OU_MSGERR = 'Item de la Orden de Compra NO EXISTE.';
END IF;

END

GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_SAT_OC_SUB_ITEM_RZOL39S_DEL TO PUBLIC 