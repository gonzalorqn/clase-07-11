/****** Script para el comando SelectTopNRows de SSMS  ******/
USE Productos /*es como un using*/
SELECT  pais  as country /*le ponemos ALIAS al pais*/
FROM televisores WHERE 1=1 /*el 1=1 es lo mismo*/
ORDER  by pulgadas,precio asc  /*criterio de ordenamiento asc o desc dependiendo del campo que queremos, la "," permite agregar mas ordenamientos */
