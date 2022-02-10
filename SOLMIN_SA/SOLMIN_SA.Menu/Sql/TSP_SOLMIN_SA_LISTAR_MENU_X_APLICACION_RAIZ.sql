DROP PROCEDURE SP_SOLMIN_SA_LISTAR_MENU_X_APLICACION_RAIZ
GO
CREATE PROCEDURE  SP_SOLMIN_SA_LISTAR_MENU_X_APLICACION_RAIZ (
        IN IN_MMCUSR VARCHAR(10) ,
        IN IN_MMCAPL CHAR(2) )
        DYNAMIC RESULT SETS 1
        LANGUAGE SQL

        BEGIN
DECLARE CR CURSOR FOR

SELECT DISTINCT MMMENU . MMCMNU , MMMENU . MMTMNU
        FROM MMAPLC , MMMENU , MMOPCN , MMOPUS , MMUSER
        WHERE MMAPLC . MMCAPL = MMMENU . MMCAPL
        AND MMMENU . MMCAPL = MMOPCN . MMCAPL
        AND MMMENU . MMCMNU = MMOPCN . MMCMNU
        AND MMOPCN . MMCAPL = MMOPUS . MMCAPL
        AND MMOPCN . MMCMNU = MMOPUS . MMCMNU
        AND MMOPCN . MMCOPC = MMOPUS . MMCOPC
        AND MMOPUS . MMCUSR = MMUSER . MMCUSR
        AND MMMENU . MMCAPL = IN_MMCAPL
        AND MMMENU . SESTRG <> '*'
        AND MMUSER . MMCUSR = IN_MMCUSR
                AND MMMENU . MMCMNU <> '00' ;

OPEN CR ;
END
go
grant all privileges on procedure SP_SOLMIN_SA_LISTAR_MENU_X_APLICACION_RAIZ to public