

SELECT * FROM ProductOrder AS p WHERE ProductOrderId='PROD000000R2'

SELECT * FROM OrderDetail AS od WHERE od.ProductOrderId='PROD000000R2'

Select ProductOrderId,OrdId,ProductType,ProductOrderNO,CustomerName,Company from ProductOrder where ProductOrderId='PROD000000R2'

Select OrderDetailId,ProductOrderId,ProductDescription,Wide,High,Deth,Qty from OrderDetail where ProductOrderId='PROD000000R2'


--���۳����·���
SELECT * FROM SellOutOfTheWarehouseTable
--���۳����·���Items��
SELECT * from Items

--���������·���
SELECT * FROM Plant_Stock_Table
--�ƶ������·���
SELECT * FROM SAPToMESTable

--���������ݱ�
SELECT * FROM MaterialMasterData
--�ɹ���������ͬ����
SELECT * FROM PurchaseOrderSynchronization
--�ɹ���������ͬ����Items��
SELECT * FROM PurchaseOrderSynchronizationItem


SELECT MBLNR,MJAHR FROM PurchaseOrderSynchronizationItem


--
SELECT VBELN,BWART,kunnr,ZYJCHSJ FROM SellOutOfTheWarehouseTable WHERE SellOutOfTheWarehouseId='F0513B31-972'
SELECT POSNR,MATNR,LGORT,CHARG,LFIMG,MEINS FROM Items WHERE SellOutOfTheWarehouseId='F0513B31-972'


--select distinct VBELN,BWART,SOBKZ,BEDAT from PurchaseOrderSynchronization join PurchaseOrderSynchronizationItem on PurchaseOrderSynchronization.PurchaseOrderSynchronizationId=PurchaseOrderSynchronizationItem.PurchaseOrderSynchronizationId 


SELECT VBELN,BEDAT,ZSSHENG FROM PurchaseOrderSynchronization












