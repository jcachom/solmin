
--SELECT * FROM MMOPCN
DROP PROCEDURE SP_SOLMIN_SA_LISTAR_TOOL_X_APLICACION_USUARIO
GO
CREATE PROCEDURE SP_SOLMIN_SA_LISTAR_TOOL_X_APLICACION_USUARIO (
  IN IN_MMCUSR VARCHAR(10),
  IN IN_MMCAPL CHAR(2),
  IN IN_MMCMNU CHAR(2))
  RESULT SETS 1
  LANGUAGE SQL
  BEGIN
DECLARE CR CURSOR FOR
  SELECT DISTINCT
        MMOPCN.MMNPVB,
        MMOPCN.MMDOPC,
        MMOPCN.URLIMG
        FROM   MMAPLC INNER JOIN  MMMENU ON MMAPLC . MMCAPL = MMMENU . MMCAPL
                    INNER JOIN  MMOPCN ON  MMMENU . MMCAPL = MMOPCN . MMCAPL
                                       AND MMMENU . MMCMNU = MMOPCN . MMCMNU
                    INNER JOIN MMOPUS ON   MMOPCN . MMCAPL = MMOPUS . MMCAPL
                                       AND MMOPCN . MMCMNU = MMOPUS . MMCMNU
                                       AND MMOPCN . MMCOPC = MMOPUS . MMCOPC
                     INNER JOIN MMUSER ON MMOPUS . MMCUSR = MMUSER . MMCUSR
 where    MMUSER . MMCUSR  = IN_MMCUSR
     AND  (MMOPCN.MMCAPL,MMOPCN.MMCMNU)
     IN( SELECT MMOPCN.MMCAIN,MMOPCN.MMCMIN
     FROM MMOPCN
     WHERE
         MMOPCN.MMCAPL  = IN_MMCAPL
     AND MMOPCN.MMCMNU  = IN_MMCMNU
     AND MMOPCN.MMTOPC  = 'B');

OPEN CR ;

END
GO
GRANT ALL PRIVILEGES ON PROCEDURE SP_SOLMIN_SA_LISTAR_TOOL_X_APLICACION_USUARIO  TO PUBLIC
GO
CALL SP_SOLMIN_SA_LISTAR_TOOL_X_APLICACION_USUARIO('AZORRILLAP','SM','01')