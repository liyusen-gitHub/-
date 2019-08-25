
SELECT * FROM GetMaterialBills AS gmb


SELECT ProductOrderId FROM ProductOrder AS po WHERE po.ProductOrderId='PROD000000R2'


SELECT CONVERT (nvarchar(12),GETDATE(),112)


select ProductOrderId as AUFNR,101 as BWART,'E' AS SOBKZ,CONVERT (nvarchar(12),GETDATE(),112) as BUDAT,CONVERT (nvarchar(12),GETDATE(),112) as BLDAT from ProductOrder WHERE ProductOrderId='PROD000000R2'


SELECT '10' AS EBELP,ProductName AS MATNR,'999999' AS CHARG,'3001' AS LGORT,Qty AS MENGE,'6006' AS WERKS,Unit AS MEINS FROM GetMaterialBills AS gmb WHERE gmb.ProductOrderId='PROD000000R2'

SELECT * FROM PurchaseOrderSynchronization WHERE PurchaseOrderSynchronizationId='B7244F14-8CE'

SELECT BEDAT AS DATA,VBELN AS MBLNR,2017 AS MJAHR FROM PurchaseOrderSynchronization WHERE PurchaseOrderSynchronizationId='{0}'


select ProductOrderId as AUFNR,261 as BWART,'' AS SOBKZ,CONVERT (nvarchar(12),GETDATE(),112) as BUDAT,CONVERT (nvarchar(12),GETDATE(),112) as BLDAT,'CC6006000001' as KOSTL from ProductOrder 

SELECT '10' AS EBELP,ProductName AS MATNR,'999999' AS CHARG,'1000' AS LGORT,Qty AS MENGE,'6002' AS WERKS,Unit AS MEINS FROM GetMaterialBills AS gmb 


SELECT * FROM ProductOrder AS p WHERE p.ProductOrderNO='S300134141'

SELECT * FROM SellOutOfTheWarehouseTable
SELECT * FROM Items AS i

SELECT * FROM ProductOrder AS po WHERE po.ProductOrderNO='S400368499'

select 'S300134141' as AUFNR,261 as BWART,'' AS SOBKZ,CONVERT (nvarchar(12),GETDATE(),112) as BUDAT,CONVERT (nvarchar(12),GETDATE(),112) as BLDAT,'CC6006000001' as KOSTL from ProductOrder WHERE ProductOrderId='PROD000000GK'

SELECT '10' AS EBELP,ProductName AS MATNR,'999999' AS CHARG,'1000' AS LGORT,sum(Qty) AS MENGE,'6002' AS WERKS,Unit AS MEINS FROM GetMaterialBills AS gmb WHERE ProductOrderId='PROD000000GK'
GROUP BY ProductName,gmb.Qty,gmb.Unit,ProductName

SELECT * FROM GetMaterialBills WHERE ProductOrderId='PROD000000GK'
--Í³¼Æ
SELECT DISTINCT'10' AS EBELP,ProductName AS MATNR,'999999' AS CHARG,'1000' AS LGORT,sum(Qty) AS MENGE,'6002' AS WERKS,Unit AS MEINS FROM GetMaterialBills WHERE ProductOrderId='PROD000000GK'
GROUP BY ProductName,Unit,ProductName

select ProductOrderId as AUFNR,101 as BWART,'E' AS SOBKZ,CONVERT (nvarchar(12),GETDATE(),112) as BUDAT,CONVERT (nvarchar(12),GETDATE(),112) as BLDAT from ProductOrder WHERE ProductOrderId='PROD000000R2'
SELECT '10' AS EBELP,ProductName AS MATNR,'999999' AS CHARG,'3001' AS LGORT,Qty AS MENGE,'6006' AS WERKS,Unit AS MEINS FROM GetMaterialBills AS gmb WHERE gmb.ProductOrderId='PROD000000R2'

SELECT DISTINCT'' AS EBELP,ProductName AS MATNR,'' AS CHARG,'1000' AS LGORT,sum(Qty) AS MENGE,'6006' AS WERKS,Unit AS MEINS FROM GetMaterialBills WHERE ProductOrderId='PROD000000GK' AND OrdId='66666' GROUP BY ProductName,Unit,ProductName

SELECT * FROM GetMaterialBills AS gmb
JBC-BH160027
Å¯°×Ë«ÌùÈý¾ÛÇè°·E0¼¶ÅÙ»¨°å16*1220*2440
JBC-BH180007
´¥¸ÐºìÓ£ÌÒË«ÌùÈý¾ÛÇè°·E0¼¶ÅÙ»¨°å18*1220*2440
JBC-MD030019
Å¯°×Ë«ÌùÈý¾ÛÇè°·E1¼¶ÖÐÃÜ¶È°å3*1220*2440

INSERT INTO GetMaterialBills (ProductOrderId,OrdId,ItmId,ProductName,ProductDes) VALUES ( 'PROD000000GK', '66666', '66666', 'JBC-MD030019', 'Å¯°×Ë«ÌùÈý¾ÛÇè°·E1¼¶ÖÐÃÜ¶È°å3*1220*2440')

SELECT DISTINCT'' AS EBELP,ProductName AS MATNR,'2001' AS CHARG,'1000' AS LGORT,20 AS MENGE,'6006' AS WERKS,'¸ö' AS MEINS FROM GetMaterialBills WHERE ProductOrderId='PROD000000GK' AND OrdId='66666' GROUP BY ProductName,Unit,ProductName










SELECT * FROM GetMaterialBills AS gmb WHERE gmb.OrdId=66666
SELECT * FROM Product

