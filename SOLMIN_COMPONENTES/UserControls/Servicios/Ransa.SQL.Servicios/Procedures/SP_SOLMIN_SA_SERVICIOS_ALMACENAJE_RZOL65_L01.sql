-- CAMBIAR RNSLIB POR DESLIB
DROP PROCEDURE DC@DESLIB.SP_SOLMIN_SA_SERVICIOS_ALMACENAJE_RZOL65_L01
GO
CREATE PROCEDURE DC@DESLIB.SP_SOLMIN_SA_SERVICIOS_ALMACENAJE_RZOL65_L01(
		IN	IN_CCLNT		NUMERIC(6, 0),
		IN	IN_CREFFW		VARCHAR(256),
		IN	IN_NROPLT		VARCHAR(256),
		IN	IN_FREFFW_INI	NUMERIC(8, 0),
		IN	IN_FREFFW_FIN	NUMERIC(8, 0),
		IN	IN_FSLFFW_INI	NUMERIC(8, 0),
		IN	IN_FSLFFW_FIN	NUMERIC(8, 0),
		IN	IN_TTINTC		VARCHAR(6),
		IN	IN_TUBRFR		VARCHAR(17),
		IN	IN_STPING		VARCHAR(1),
		IN	IN_CRTLTE		VARCHAR(256)
        )
RESULT SETS 1
LANGUAGE SQL
BEGIN 
DECLARE WK_SESTRG			VARCHAR(1)		DEFAULT '*';	-- SITUACION - ANULADOS

DECLARE strSQL  			VARCHAR(6000)	DEFAULT '';
DECLARE strSQL_CREFFW		VARCHAR(300)	DEFAULT '';
DECLARE strSQL_NROPLT		VARCHAR(300)	DEFAULT '';
DECLARE strSQL_FREFFW_INI	VARCHAR(50)		DEFAULT '';
DECLARE strSQL_FREFFW_FIN	VARCHAR(50)		DEFAULT '';
DECLARE strSQL_FSLFFW_INI	VARCHAR(50)		DEFAULT '';
DECLARE strSQL_FSLFFW_FIN	VARCHAR(50)		DEFAULT '';
DECLARE strSQL_TTINTC		VARCHAR(300)	DEFAULT '';
DECLARE strSQL_TUBRFR		VARCHAR(100)	DEFAULT '';
DECLARE strSQL_STPING		VARCHAR(100)	DEFAULT '';
DECLARE strSQL_CRTLTE		VARCHAR(300)	DEFAULT '';

DECLARE CU_00 SCROLL CURSOR WITH RETURN FOR S0;

-- Filtros seg?n C?digos de Bultos
IF IN_CREFFW <> '' THEN
	IF POSSTR( IN_CREFFW, ',') <> 0 THEN
		SET IN_CREFFW = REPLACE( REPLACE( IN_CREFFW, ' ', '' ), ',', ''',''');
		SET strSQL_CREFFW = ' And RZOL65.CREFFW IN  ( '''  || IN_CREFFW || ''') ' ;
	ELSE
		SET IN_CREFFW = REPLACE( IN_CREFFW, '*', '%');
		SET strSQL_CREFFW = ' And Trim(RZOL65.CREFFW) LIKE '''  || IN_CREFFW || ''' ' ;
	END IF;
END IF;
-- Filtros seg?n Nro de Paleta
IF IN_NROPLT <> '' THEN
	IF POSSTR( IN_NROPLT, ',') <> 0 THEN
		SET strSQL_NROPLT = ' And CREFFW IN ( SELECT CREFFW FROM RZOL65Q WHERE CCLNT = RZOL65.CCLNT And NROPLT IN  ( '  || IN_NROPLT || ') ) ' ;
	ELSE
		SET IN_NROPLT = REPLACE( REPLACE( IN_NROPLT, ' ', '' ), '*', '%');
		SET strSQL_NROPLT = ' And CREFFW IN ( SELECT CREFFW FROM RZOL65Q WHERE CCLNT = RZOL65.CCLNT And Trim(NROPLT) LIKE '''  || IN_NROPLT || ''' ) ' ;
	END IF;
END IF;
-- Filtros seg?n Fecha de Recepci?n 
IF IN_FREFFW_INI > 0 THEN
	SET strSQL_FREFFW_INI = ' And RZOL65.FREFFW >= ' || IN_FREFFW_INI ;
END IF;
IF IN_FREFFW_FIN > 0 THEN
	SET strSQL_FREFFW_FIN = ' And RZOL65.FREFFW <= ' || IN_FREFFW_FIN ;
END IF;
-- Filtros seg?n Fecha de Despacho
IF IN_FSLFFW_INI > 0 THEN
	SET strSQL_FSLFFW_INI = ' And RZOL65.FSLFFW >= ' || IN_FSLFFW_INI ;
END IF;
IF IN_FSLFFW_FIN > 0 THEN
	SET strSQL_FSLFFW_FIN = ' And RZOL65.FSLFFW <= ' || IN_FSLFFW_FIN ;
END IF;
-- Filtros seg?n el Termino Internacional de la Orden de Compra
IF IN_TTINTC <> '' THEN
	SET strSQL_TTINTC = ' And Exists(	SELECT	CREFFW 
										FROM	RZOL66 WHERE RZOL66.CCLNT = RZOL65.CCLNT AND RZOL66.CREFFW = RZOL65.CREFFW
										AND		EXISTS( SELECT	NORCML 
														FROM	RZOL38 WHERE RZOL38.CCLNT = RZOL66.CCLNT AND RZOL38.NORCML = RZOL66.NORCML 
														AND		TTINTC = ''' || IN_TTINTC || ''' ))';
END IF;
-- Filtros seg?n Ubicaci?n en Almac?n
IF IN_TUBRFR <> '' THEN
	SET strSQL_TUBRFR = ' And RZOL65.TUBRFR = '''  || IN_TUBRFR || ''' ' ;
END IF;
-- Filtros seg?n Tipo de Movimiento
IF IN_STPING <> '' THEN
	SET strSQL_STPING = ' And RZOL65.STPING = '''  || IN_STPING || ''' ' ;
END IF;
-- Filtros seg?n Criterio
IF IN_CRTLTE <> '' THEN
	IF POSSTR( IN_CRTLTE, ',') <> 0 THEN
		SET IN_CRTLTE = REPLACE( REPLACE( IN_CRTLTE, ' ', '' ), ',', ''',''');
		SET strSQL_CRTLTE = ' And RZOL65.CRTLTE IN  ( '''  || IN_CRTLTE || ''') ' ;
	ELSE
		SET IN_CRTLTE = REPLACE( REPLACE( IN_CRTLTE, ' ', '' ), '*', '%');
		SET strSQL_CRTLTE = ' And Trim(RZOL65.CRTLTE) LIKE '''  || IN_CRTLTE || ''' ' ;
	END IF;
END IF;

-- Armamos la consulta final con toda la informaci?n
SET strSQL = '	SELECT	CREFFW,	DESCWB,	TUBRFR, NumberToDate( FREFFW ) AS FREFFW,
					  ( SELECT MAX(NROPLT) FROM RZOL65Q WHERE CCLNT = RZOL65.CCLNT AND CREFFW = RZOL65.CREFFW ) NROPLT,
					  ( SELECT Trim( TDSDES ) FROM RZO103 WHERE CODVAR = ''MOTREC'' AND CCMPRN = RZOL65.SMTRCP ) AS SMTRCP,
						QREFFW, TIPBTO, QPSOBL, TUNPSO, QVLBTO, TUNVOL, 
					  ( SELECT Trim( TDSDES ) FROM RZO103 WHERE CODVAR = ''SITCAR'' AND CCMPRN = RZOL65.SSNCRG ) AS SSNCRG,
						QAROCP,	NORAGN,	NTCKPS, CRTLTE, 
					  ( SELECT Trim( TDSDES ) FROM RZO103 WHERE CODVAR = ''SITUAC'' AND CCMPRN = RZOL65.SESTRG ) AS SESTRG
				FROM	RZOL65
				WHERE	CCLNT = ? ' || strSQL_CREFFW || ' And SESTRG <> ? ' || strSQL_NROPLT || 
				strSQL_FREFFW_INI || strSQL_FREFFW_FIN || strSQL_FSLFFW_INI || strSQL_FSLFFW_FIN || strSQL_TTINTC || strSQL_TUBRFR || 
				strSQL_STPING || strSQL_CRTLTE || ' ORDER BY CREFFW ';

-- Realizamos la preparaci?n de la Consulta para ser ejecutada
PREPARE S0 FROM strSQL;
OPEN CU_00 USING IN_CCLNT, WK_SESTRG;




END
GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_SA_SERVICIOS_ALMACENAJE_RZOL65_L01 TO PUBLIC