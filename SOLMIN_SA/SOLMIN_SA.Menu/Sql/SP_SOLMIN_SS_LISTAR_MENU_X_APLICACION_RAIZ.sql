DROP PROCEDURE SP_SOLMIN_SS_LISTAR_MENU_X_APLICACION_RAIZ
GO

CREATE PROCEDURE SP_SOLMIN_SS_LISTAR_MENU_X_APLICACION_RAIZ (
  IN PSMMCAPL CHAR(2) )
  DYNAMIC RESULT SETS 1
  LANGUAGE SQL
  BEGIN
DECLARE CR CURSOR FOR

SELECT * FROM MMMENU WHERE
  MMCAPL IN (SELECT DISTINCT MMCAPL from MMOPCN WHERE MMCAIN = PSMMCAPL AND MMCAPL <> PSMMCAPL) AND SESTRG <> '*' ;

OPEN CR ;

END
GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_SS_LISTAR_MENU_X_APLICACION_RAIZ TO PUBLIC
