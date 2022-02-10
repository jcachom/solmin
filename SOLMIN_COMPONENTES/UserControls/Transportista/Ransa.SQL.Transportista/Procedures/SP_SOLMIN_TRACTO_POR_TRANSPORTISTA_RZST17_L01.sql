DROP PROCEDURE SP_SOLMIN_TRACTO_POR_TRANSPORTISTA_RZST17_L01
GO
CREATE PROCEDURE SP_SOLMIN_TRACTO_POR_TRANSPORTISTA_RZST17_L01(
		IN	IN_NRUCTR		VARCHAR(15),
		IN	IN_CCMPN		VARCHAR(2),
		IN	IN_CDVSN		VARCHAR(1),
		IN	IN_CPLNDV		NUMERIC(3),
		IN	IN_NPLCUN		VARCHAR(256),
        IN	IN_TMRCTR		VARCHAR(256),
        IN	IN_NRGMT1		VARCHAR(256),
		IN	IN_NROPAG		NUMERIC(6, 0),
		IN	IN_REGPAG		NUMERIC(6, 0),
		OUT	OU_TOTPAG		NUMERIC(6, 0)
        )
RESULT SETS 1
LANGUAGE SQL
BEGIN 
DECLARE WK_TOTPAG			NUMERIC(6,0)		DEFAULT 0 ;	 -- NRO TOTAL DE PAGINAS SOBRE LA CONSULTA 
DECLARE WK_NPLCUN			VARCHAR(6)			DEFAULT '' ; -- NUMERO DE TRACTO
DECLARE WK_CNTREG			NUMERIC(6,0)		DEFAULT 0 ;	 -- CONTADOR DE REGISTROS 
DECLARE WK_NRPAGS			NUMERIC(6,0)		DEFAULT 0 ;	 -- NRO DE PAGINAS EN TOTAL PARA LA CONSULTA ESTABLECIDA 

DECLARE STRSQL				VARCHAR(6000)		DEFAULT '' ; 
DECLARE STRSQL_FILTRO		VARCHAR(1000)		DEFAULT '' ; 
  
DECLARE CU_00 SCROLL CURSOR WITH RETURN FOR S0 ; 
DECLARE CU_01 SCROLL CURSOR WITH RETURN FOR S1 ; 
DECLARE CU_02 SCROLL CURSOR WITH RETURN FOR S2 ; 
  
IF IN_REGPAG <= 0 THEN 
	SET IN_REGPAG = 1 ; 
END IF ; 

-- Filtros seg�n Nro de Tracto
IF IN_NPLCUN <> '' THEN 
	IF POSSTR ( IN_NPLCUN , ',' ) <> 0 THEN 
		SET IN_NPLCUN = REPLACE ( REPLACE ( IN_NPLCUN , ' ' , '' ) , ',' , ''',''' ); 
		SET STRSQL_FILTRO = ' AND NPLCUN IN  ( ''' || IN_NPLCUN || ''') ' ; 
	ELSE 
		IF POSSTR ( IN_NPLCUN , '*' ) <> 0 THEN 
			SET IN_NPLCUN = REPLACE ( IN_NPLCUN , '*' , '%' ) ; 
			SET STRSQL_FILTRO = ' AND Trim(NPLCUN) LIKE ''' || IN_NPLCUN || ''' ' ; 
		ELSE 
			SET STRSQL_FILTRO = ' AND NPLCUN = ''' || IN_NPLCUN || ''' ' ; 
		END IF ; 
	END IF ; 
END IF ; 
  
-- Filtros seg�n Detalle Tracto
IF IN_TMRCTR <> '' THEN 
	IF POSSTR ( IN_TMRCTR , ',' ) <> 0 THEN 
		SET IN_TMRCTR = REPLACE ( REPLACE ( IN_TMRCTR , ' ' , '' ) , ',' , ''',''' ) ; 
		SET STRSQL_FILTRO = STRSQL_FILTRO || ' AND EXISTS( SELECT TMRCTR FROM RZST03 WHERE NPLCUN = RZST17.NPLCUN AND TMRCTR IN  ( ''' || IN_TMRCTR || ''') ) ' ; 
	ELSE 
		IF POSSTR ( IN_TMRCTR , '*' ) <> 0 THEN 
			SET IN_TMRCTR = REPLACE ( IN_TMRCTR , '*' , '%' ) ; 
			SET STRSQL_FILTRO = STRSQL_FILTRO || ' AND EXISTS( SELECT TMRCTR FROM RZST03 WHERE NPLCUN = RZST17.NPLCUN AND TMRCTR LIKE ''' || IN_TMRCTR || ''' ) ' ; 
		ELSE 
			SET STRSQL_FILTRO = STRSQL_FILTRO || ' AND EXISTS( SELECT TMRCTR FROM RZST03 WHERE NPLCUN = RZST17.NPLCUN AND TMRCTR = ''' || IN_TMRCTR || ''' ) ' ; 
		END IF ; 
	END IF ; 
END IF ; 
  
-- Filtros seg�n MTC
IF IN_NRGMT1 <> '' THEN 
	IF POSSTR ( IN_NRGMT1 , ',' ) <> 0 THEN 
		SET IN_NRGMT1 = REPLACE ( REPLACE ( IN_NRGMT1 , ' ' , '' ) , ',' , ''',''' ) ; 
		SET STRSQL_FILTRO = STRSQL_FILTRO || ' AND EXISTS( SELECT TMRCTR FROM RZST03 WHERE NPLCUN = RZST17.NPLCUN AND NRGMT1 IN  ( ''' || IN_NRGMT1 || ''') ) ' ; 
	ELSE 
		IF POSSTR ( IN_NRGMT1 , '*' ) <> 0 THEN 
			SET IN_NRGMT1 = REPLACE ( IN_NRGMT1 , '*' , '%' ) ; 
			SET STRSQL_FILTRO = STRSQL_FILTRO || ' AND EXISTS( SELECT TMRCTR FROM RZST03 WHERE NPLCUN = RZST17.NPLCUN AND Trim(NRGMT1) LIKE ''' || IN_NRGMT1 || ''' ) ' ; 
		ELSE 
			SET STRSQL_FILTRO = STRSQL_FILTRO || ' AND EXISTS( SELECT TMRCTR FROM RZST03 WHERE NPLCUN = RZST17.NPLCUN AND NRGMT1 = ''' || IN_NRGMT1 || ''' ) ' ; 
		END IF ; 
	END IF ; 
END IF ; 

SET STRSQL = ' SELECT Count(NPLCUN) FROM RZST17 WHERE NRUCTR = ? AND CCMPN = ? AND CDVSN = ? AND CPLNDV = ? AND SESTCM <> ''*'' AND SESTRG <> ''*'' ' || STRSQL_FILTRO;	 
-- Preparamos el Macros que contiene la consulta de toda la informaci�n. 
PREPARE S0 FROM STRSQL ; 
OPEN CU_00 USING IN_NRUCTR, IN_CCMPN, IN_CDVSN, IN_CPLNDV; 
FETCH LAST FROM CU_00 INTO WK_TOTPAG ; 
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

SET STRSQL = ' SELECT NPLCUN FROM RZST17 WHERE NRUCTR = ? AND CCMPN = ? AND CDVSN = ? AND CPLNDV = ? AND NPLCUN >= ? AND SESTCM <> ''*'' AND SESTRG <> ''*'' ' || STRSQL_FILTRO || 
			 ' ORDER BY NPLCUN ASC FETCH FIRST ' || ( IN_REGPAG + 1 ) || ' ROWS ONLY ' ;	 
-- Preparamos el Macros que contiene la consulta de toda la informaci�n. 
PREPARE S1 FROM STRSQL ; 
-- Recorremos todas las p�ginas anteriores para obtener la �ltima Orden de Compra 
WHILE WK_CNTREG < ( IN_NROPAG - 1 ) DO 
	OPEN CU_01 USING IN_NRUCTR, IN_CCMPN, IN_CDVSN, IN_CPLNDV, WK_NPLCUN; 
	FETCH LAST FROM CU_01 INTO WK_NPLCUN ; 
	CLOSE CU_01 ; 
	 -- AVANZAMOS EL CONTADOR DE PAGINAS 
	SET WK_CNTREG = WK_CNTREG + 1 ; 
END WHILE ; 
-- Armamos la consulta final con toda la informaci�n 
SET STRSQL = ' SELECT NRUCTR, NPLCUN, ( SELECT TMRCTR FROM RZST03 WHERE NPLCUN = RZST17.NPLCUN ) TMRCTR, ' || 
					' ( SELECT NRGMT1 FROM RZST03 WHERE NPLCUN = RZST17.NPLCUN ) NRGMT1, NPLACP, CBRCND, CCMPN, CDVSN, CPLNDV ' || 
			 ' FROM RZST17 WHERE NRUCTR = ? AND CCMPN = ? AND CDVSN = ? AND CPLNDV = ? AND NPLCUN >= ? AND SESTCM <> ''*'' AND SESTRG <> ''*'' ' || STRSQL_FILTRO || 
			 ' ORDER BY NPLCUN ASC FETCH FIRST ' || IN_REGPAG || ' ROWS ONLY ' ; 
-- Realizamos la preparaci�n de la Consulta para ser ejecutada 
PREPARE S2 FROM STRSQL ; 
OPEN CU_02 USING IN_NRUCTR, IN_CCMPN, IN_CDVSN, IN_CPLNDV, WK_NPLCUN; 

END
GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_TRACTO_POR_TRANSPORTISTA_RZST17_L01 TO PUBLIC