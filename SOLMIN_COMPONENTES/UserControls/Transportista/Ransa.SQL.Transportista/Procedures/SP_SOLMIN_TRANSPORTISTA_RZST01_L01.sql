DROP PROCEDURE SP_SOLMIN_TRANSPORTISTA_RZST01_L01
GO
CREATE PROCEDURE SP_SOLMIN_TRANSPORTISTA_RZST01_L01(
		IN	IN_CCMPN		VARCHAR(2),
		IN	IN_NRUCTR		VARCHAR(256),
        IN	IN_TCMTRT		VARCHAR(256),
        IN	IN_CTRSPT		VARCHAR(256),
		IN	IN_NROPAG		NUMERIC(6, 0),
		IN	IN_REGPAG		NUMERIC(6, 0),
		OUT OU_TOTPAG		NUMERIC(6, 0)
        )
RESULT SETS 1
LANGUAGE SQL
BEGIN 
DECLARE WK_TOTPAG			NUMERIC ( 6 , 0 )	DEFAULT 0 ;	 -- NRO TOTAL DE PAGINAS SOBRE LA CONSULTA 
DECLARE WK_NRUCTR			VARCHAR(15)			DEFAULT '' ; -- NUMERO DE RUC DE LA EMPRESA DE TRANSPORTE
DECLARE WK_CNTREG			NUMERIC ( 6 , 0 )	DEFAULT 0 ;	 -- CONTADOR DE REGISTROS 
DECLARE WK_NRPAGS			NUMERIC ( 6 , 0 )	DEFAULT 0 ;	 -- NRO DE PAGINAS EN TOTAL PARA LA CONSULTA ESTABLECIDA 
  
DECLARE STRSQL				VARCHAR ( 6000 )	DEFAULT '' ; 
DECLARE STRSQL_NRUCTR		VARCHAR ( 300 )		DEFAULT '' ; 
DECLARE STRSQL_TCMTRT		VARCHAR ( 300 )		DEFAULT '' ; 
DECLARE STRSQL_CTRSPT		VARCHAR ( 300 )		DEFAULT '' ; 
  
DECLARE CU_00 SCROLL CURSOR WITH RETURN FOR S0 ; 
DECLARE CU_01 SCROLL CURSOR WITH RETURN FOR S1 ; 
DECLARE CU_02 SCROLL CURSOR WITH RETURN FOR S2 ; 
  
IF IN_REGPAG <= 0 THEN 
	SET IN_REGPAG = 1 ; 
END IF ; 
  
-- Filtros seg�n RUC de la Empresa  de Transporte
IF IN_NRUCTR <> '' THEN 
	IF POSSTR ( IN_NRUCTR , ',' ) <> 0 THEN 
		SET IN_NRUCTR = REPLACE ( REPLACE ( IN_NRUCTR , ' ' , '' ) , ',' , ''',''' ); 
		SET STRSQL_NRUCTR = ' And NRUCTR IN  ( ''' || IN_NRUCTR || ''') ' ; 
	ELSE 
		IF POSSTR ( IN_NRUCTR , '*' ) <> 0 THEN 
			SET IN_NRUCTR = REPLACE ( IN_NRUCTR , '*' , '%' ) ; 
			SET STRSQL_NRUCTR = ' And Trim(NRUCTR) LIKE ''' || IN_NRUCTR || ''' ' ; 
		ELSE 
			SET STRSQL_NRUCTR = ' And NRUCTR = ''' || IN_NRUCTR || ''' ' ; 
		END IF ; 
	END IF ; 
END IF ; 
  
-- Filtros seg�n Raz�n Social 
IF IN_TCMTRT <> '' THEN 
	IF POSSTR ( IN_TCMTRT , ',' ) <> 0 THEN 
		SET IN_TCMTRT = REPLACE ( REPLACE ( IN_TCMTRT , ' ' , '' ) , ',' , ''',''' ) ; 
		SET STRSQL_TCMTRT = ' And TCMTRT IN  ( ''' || IN_TCMTRT || ''') ' ; 
	ELSE 
		IF POSSTR ( IN_TCMTRT , '*' ) <> 0 THEN 
			SET IN_TCMTRT = REPLACE ( IN_TCMTRT , '*' , '%' ) ; 
			SET STRSQL_TCMTRT = ' And TCMTRT LIKE ''' || IN_TCMTRT || ''' ' ; 
		ELSE 
			SET STRSQL_TCMTRT = ' And TCMTRT = ''' || IN_TCMTRT || ''' ' ; 
		END IF ; 
	END IF ; 
END IF ; 
  
-- Filtros seg�n C�digo de le Empresa de Transporte
IF IN_CTRSPT <> '' THEN 
	IF POSSTR ( IN_CTRSPT , ',' ) <> 0 THEN 
		SET IN_CTRSPT = REPLACE ( IN_CTRSPT , ' ' , '' ) ; 
		SET STRSQL_CTRSPT = ' And CTRSPT IN  ( ' || IN_CTRSPT || ') ' ; 
	ELSE 
		IF POSSTR ( IN_CTRSPT , '*' ) <> 0 THEN 
			SET IN_CTRSPT = REPLACE ( IN_CTRSPT , '*' , '%' ) ; 
			SET STRSQL_CTRSPT = ' And Trim(CTRSPT) LIKE ''' || IN_CTRSPT || ''' ' ; 
		ELSE 
			SET STRSQL_CTRSPT = ' And CTRSPT = ' || IN_CTRSPT || ' ' ; 
		END IF ; 
	END IF ; 
END IF ; 

SET STRSQL = ' SELECT Count(NRUCTR) FROM RZST01 WHERE CCMPN = ? AND SESTRG <> ''*'' ' || STRSQL_NRUCTR || STRSQL_TCMTRT || STRSQL_CTRSPT;	 
-- Preparamos el Macros que contiene la consulta de toda la informaci�n. 
PREPARE S0 FROM STRSQL;
OPEN CU_00 USING IN_CCMPN;
FETCH LAST FROM CU_00 INTO WK_TOTPAG;
CLOSE CU_00 ; 

IF IN_REGPAG <= 0 THEN 
	SET IN_REGPAG = 1;
	IF WK_TOTPAG > 0 THEN
		SET IN_REGPAG = WK_TOTPAG;
	END IF;
END IF;

IF WK_TOTPAG > 0 THEN
	IF MOD(WK_TOTPAG, IN_REGPAG) > 0 THEN
		SET WK_TOTPAG = INTEGER ( WK_TOTPAG / IN_REGPAG ) + 1;
	ELSE
		SET WK_TOTPAG = INTEGER ( WK_TOTPAG / IN_REGPAG );
	END IF;
ELSE
	SET WK_TOTPAG = 1;
END IF;

IF IN_NROPAG > WK_TOTPAG THEN
	SET IN_NROPAG = WK_TOTPAG;
END IF;

SET OU_TOTPAG = WK_TOTPAG;

SET STRSQL = ' SELECT NRUCTR FROM RZST01 WHERE CCMPN = ? AND SESTRG <> ''*'' AND NRUCTR >= ?  ' || STRSQL_NRUCTR || STRSQL_TCMTRT || STRSQL_CTRSPT || 
			 ' ORDER BY NRUCTR ASC FETCH FIRST ' || ( IN_REGPAG + 1 ) || ' ROWS ONLY ' ;	 
-- Preparamos el Macros que contiene la consulta de toda la informaci�n. 
PREPARE S1 FROM STRSQL ; 
-- Recorremos todas las p�ginas anteriores para obtener la �ltima Orden de Compra 
WHILE WK_CNTREG < ( IN_NROPAG - 1 ) DO 
	OPEN CU_01 USING IN_CCMPN, WK_NRUCTR;
	FETCH LAST FROM CU_01 INTO WK_NRUCTR;
	CLOSE CU_01 ; 
	 -- AVANZAMOS EL CONTADOR DE PAGINAS 
	SET WK_CNTREG = WK_CNTREG + 1 ; 
END WHILE ; 
-- Armamos la consulta final con toda la informaci�n 
SET STRSQL = '	SELECT NRUCTR, TCMTRT, CTRSPT FROM RZST01 WHERE CCMPN = ? AND SESTRG <> ''*'' and NRUCTR >= ? ' || STRSQL_NRUCTR || STRSQL_TCMTRT || STRSQL_CTRSPT || 
			 ' ORDER BY NRUCTR ASC FETCH FIRST ' || IN_REGPAG || ' ROWS ONLY ' ; 
-- Realizamos la preparaci�n de la Consulta para ser ejecutada 
PREPARE S2 FROM STRSQL ; 
OPEN CU_02 USING IN_CCMPN, WK_NRUCTR;

END
GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_TRANSPORTISTA_RZST01_L01 TO PUBLIC