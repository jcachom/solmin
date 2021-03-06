-- CAMBIAR RNSLIB POR DESLIB
DROP PROCEDURE DC@DESLIB.SP_SOLMIN_OC_ITEM_RZOL39_R01
GO
CREATE PROCEDURE DC@DESLIB.SP_SOLMIN_OC_ITEM_RZOL39_R01(
	IN	IN_CCLNT	VARCHAR(256),
	IN	IN_FMOVI	NUMERIC(8, 0),
	IN	IN_FMOVF	NUMERIC(8, 0)
)
RESULT SETS 1
LANGUAGE SQL
BEGIN ATOMIC
-- Variables Generales
DECLARE WK_CCLNT	VARCHAR(300)	DEFAULT '';
DECLARE strSQL  	VARCHAR(6000)	DEFAULT '';
-- Cursores
DECLARE CU_02 SCROLL CURSOR WITH RETURN FOR S2;

-- Filtros seg?n C?digos de Mercader?a
IF IN_CCLNT <> '' THEN
	IF POSSTR( IN_CCLNT, ',') <> 0 THEN
		SET WK_CCLNT = ' TABLA.CCLNT IN (' || IN_CCLNT || ') And ' ;
	ELSE
		IF POSSTR( IN_CCLNT, '*') <> 0 THEN
			SET IN_CCLNT = REPLACE( IN_CCLNT, '*', '%');
			SET WK_CCLNT = ' TABLA.CCLNT LIKE '''  || IN_CCLNT || ''' And ' ;
		ELSE
			SET WK_CCLNT = ' TABLA.CCLNT = ' || IN_CCLNT || ' And ' ;
		END IF;
	END IF;
END IF;

-- Armamos la consulta final con toda la informaci?n
SET strSQL = '	SELECT  RZOL39.CCLNT, (Select TCMPCL From RZZM01 Where CCLNT = RZOL39.CCLNT ) TCMPCL, RZOL39.TCITCL,
						MAX( RZOL39.TDITES ) TDITES, MAX( RZOL39.TUNDIT ) TUNDIT,
						SUM( RZOL39.QSTKIT - IFNULL( RZ66.QTAINGI, 0) + IFNULL( RZ66.QTASALI, 0) ) QSLINI,
						SUM((RZOL39.QSTKIT - IFNULL( RZ66.QTAINGI, 0) + IFNULL( RZ66.QTASALI, 0))*IVUNIT ) VSLINI,
						SUM( IFNULL( RZ66.QTAINGI, 0) - IFNULL( RZ66.QTAINGF, 0) ) QMVING,
						SUM((IFNULL( RZ66.QTAINGI, 0) - IFNULL( RZ66.QTAINGF, 0))*IVUNIT ) VMVING,
						SUM( IFNULL( RZ66.QTASALI, 0) - IFNULL( RZ66.QTASALF, 0) ) QMVSAL,
						SUM((IFNULL( RZ66.QTASALI, 0) - IFNULL( RZ66.QTASALF, 0))*IVUNIT ) VMVSAL,
						SUM( QSTKIT - IFNULL( RZ66.QTAINGF, 0) + IFNULL( RZ66.QTASALF, 0) ) QSLFIN,
						SUM((QSTKIT - IFNULL( RZ66.QTAINGF, 0) + IFNULL( RZ66.QTASALF, 0))*IVUNIT ) VSLFIN
				FROM    RZOL39 LEFT OUTER JOIN( SELECT  RZOL65.CCLNT, RZOL66.NORCML, RZOL66.NRITOC,
														SUM( CASE WHEN RZOL65.FINGAL >= ' || IN_FMOVI || ' THEN RZOL66.QBLTSR ELSE 0 END ) QTAINGI,
														SUM( CASE WHEN RZOL65.FSLDAL >= ' || IN_FMOVI || ' THEN RZOL66.QBLTSR ELSE 0 END ) QTASALI,
														SUM( CASE WHEN RZOL65.FINGAL >  ' || IN_FMOVF || ' THEN RZOL66.QBLTSR ELSE 0 END ) QTAINGF,
														SUM( CASE WHEN RZOL65.FSLDAL >  ' || IN_FMOVF || ' THEN RZOL66.QBLTSR ELSE 0 END ) QTASALF
												FROM    RZOL65 INNER JOIN RZOL66 
														ON ' || REPLACE( WK_CCLNT, 'TABLA.CCLNT', 'RZOL65.CCLNT') || ' (  RZOL65.FINGAL >= ' || IN_FMOVI || ' OR FSLDAL >= ' || IN_FMOVI || ' )
														AND    RZOL65.SESTRG <> ''*''
														AND    RZOL66.CREFFW  = RZOL65.CREFFW
														AND    RZOL66.CCLNT   = RZOL65.CCLNT
														AND    RZOL66.SESTRG <> ''*''
												GROUP BY RZOL65.CCLNT, RZOL66.NORCML, RZOL66.NRITOC
											  ) RZ66 ON RZOL39.CCLNT = RZ66.CCLNT AND RZOL39.NORCML = RZ66.NORCML AND RZOL39.NRITOC = RZ66.NRITOC
				WHERE ' || REPLACE( WK_CCLNT, 'TABLA.CCLNT', 'RZOL39.CCLNT') || ' RZOL39.TCITCL <> ''''
				AND     RZOL39.SESTRG <> ''*''
				GROUP BY RZOL39.CCLNT, RZOL39.TCITCL
				HAVING  SUM( QSTKIT - IFNULL( RZ66.QTAINGI, 0) + IFNULL( RZ66.QTASALI, 0) ) <> 0
					OR  SUM( IFNULL( RZ66.QTAINGI, 0) + IFNULL( RZ66.QTAINGF, 0) )  <> 0
					OR  SUM( IFNULL( RZ66.QTASALI, 0) + IFNULL( RZ66.QTASALF, 0) )  <> 0
					OR  SUM( QSTKIT - IFNULL( RZ66.QTAINGF, 0) + IFNULL( RZ66.QTASALF, 0) ) <> 0
				ORDER BY RZOL39.CCLNT, RZOL39.TCITCL';
-- Realizamos la preparaci?n de la Consulta para ser ejecutada
PREPARE S2 FROM strSQL;
OPEN CU_02;

END

GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_OC_ITEM_RZOL39_R01 TO PUBLIC