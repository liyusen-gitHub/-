

SELECT * FROM ProductOrder AS p WHERE ProductOrderId='PROD000000R2'

SELECT * FROM OrderDetail AS od WHERE od.ProductOrderId='PROD000000R2'

Select ProductOrderId,OrdId,ProductType,ProductOrderNO,CustomerName,Company from ProductOrder where ProductOrderId='PROD000000R2'

Select OrderDetailId,ProductOrderId,ProductDescription,Wide,High,Deth,Qty from OrderDetail where ProductOrderId='PROD000000R2'


--销售出库下发表
SELECT * FROM SellOutOfTheWarehouseTable
--销售出库下发表Items表
SELECT * from Items

--工厂库存地下发表
SELECT * FROM Plant_Stock_Table
--移动类型下发表
SELECT * FROM SAPToMESTable

--物料主数据表
SELECT * FROM MaterialMasterData
--采购订单数据同步表
SELECT * FROM PurchaseOrderSynchronization
--采购订单数据同步表Items表
SELECT * FROM PurchaseOrderSynchronizationItem


SELECT MBLNR,MJAHR FROM PurchaseOrderSynchronizationItem


--
SELECT VBELN,BWART,kunnr,ZYJCHSJ FROM SellOutOfTheWarehouseTable WHERE SellOutOfTheWarehouseId='F0513B31-972'
SELECT POSNR,MATNR,LGORT,CHARG,LFIMG,MEINS FROM Items WHERE SellOutOfTheWarehouseId='F0513B31-972'


--select distinct VBELN,BWART,SOBKZ,BEDAT from PurchaseOrderSynchronization join PurchaseOrderSynchronizationItem on PurchaseOrderSynchronization.PurchaseOrderSynchronizationId=PurchaseOrderSynchronizationItem.PurchaseOrderSynchronizationId 


SELECT VBELN,BEDAT,ZSSHENG FROM PurchaseOrderSynchronization












