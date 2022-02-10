-- CAMBIAR RNSLIB POR DESLIB
DROP FUNCTION DC@DESLIB.TarifaServicioPorDivision
GO
CREATE FUNCTION DC@DESLIB.TarifaServicioPorDivision(
	PSCCMPN		VARCHAR(2),
	PSCDVSN		VARCHAR(1),
	--PNCPLNDV	NUMERIC(3, 0),
	PNCCLNT		NUMERIC(6),
	PNFECSER	NUMERIC(8),
	PNNRTFSV	NUMERIC(10, 0) 
)
RETURNS VARCHAR(250)
LANGUAGE SQL
NOT DETERMINISTIC
READS SQL DATA
RETURNS NULL ON NULL INPUT
BEGIN
	DECLARE RESULTADO VARCHAR(250) DEFAULT '';
	
	SELECT Max(TRIM ( RUBRO.NOMRUB ) CONCAT ' - ' CONCAT TRIM ( SERRUB.NOMSER ) CONCAT ' (' CONCAT TRIM ( TARIFA.DESTAR ) CONCAT ')')
	INTO	RESULTADO
	FROM RZSC04 TARIFA	JOIN RZSC01 CONTRATO	ON	CONTRATO.NRCTSL = TARIFA.NRCTSL 
						JOIN RZSC08 SERRUB		ON	SERRUB.NRSRRB = TARIFA.NRSRRB 
												AND SERRUB.CCMPN = PSCCMPN 
												AND SERRUB.CDVSN = PSCDVSN 
						JOIN RZSC12 CLIENTE		ON	CLIENTE.NRCTCL = CONTRATO.NRCTCL 
												AND CLIENTE.CCLNT = PNCCLNT 
						JOIN RZSC07 RUBRO		ON	RUBRO.CCMPN = TARIFA.CCMPN 
												AND	RUBRO.CDVSN = TARIFA.CDVSN 
												AND RUBRO.NRRUBR = SERRUB.NRRUBR 
												AND RUBRO.CCMPN = PSCCMPN 
												AND RUBRO.CDVSN = PSCDVSN 
						JOIN RZSC09 RANGO		ON	RANGO.NRRNGO = TARIFA.NRRNGO 
												AND RANGO.STPTRA <> 'F' 
		WHERE	TARIFA.NRTFSV = PNNRTFSV
		--AND		TARIFA.CPLNDV = PNCPLNDV 
		AND		TARIFA.SESTRG <> '*' 
		AND		TARIFA.NRCTSL IN (	SELECT	CONT.NRCTSL 
									FROM	RZSC01 CONT , 
											RZSC12 CLIEN 
									WHERE	CLIEN.CCLNT = PNCCLNT 
									AND		CLIEN.NRCTCL = CONT.NRCTCL 
									AND		CLIEN.SESTRG <> '*' 
									AND		CONT.SESTRG = 'A' 
									AND		CONT.FECINI <= PNFECSER 
									AND   ( CONT.FECFIN >= PNFECSER OR CONT.FECFIN = 0 ) ) 
		AND		RANGO.STPTRA <> 'F';
	
	RETURN RESULTADO;
END