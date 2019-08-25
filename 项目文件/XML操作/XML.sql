

--采购订单下发
select * from PurchaseOrderSynchronization
select * from PurchaseOrderSynchronizationItem

--truncate table PurchaseOrderSynchronization
--truncate table PurchaseOrderSynchronizationItem
--truncate table Dump_The_Order_Outbound_ReturnTable
--truncate table Dump_The_Order_Outbound_ReturnekpoTable
--truncate table Purchase_RequisitionTable
--truncate table Purchase_RequisitionTablekpoTable
--351出库单
select * from Dump_The_Order_Outbound_ReturnTable
select * from Dump_The_Order_Outbound_ReturnekpoTable
--101入库单
select * from Purchase_RequisitionTable
select * from Purchase_RequisitionTablekpoTable
--update SellOutOfTheWarehouseTable set ZCHANGE_IND='Q' where VBELN='0080015356' and ZCHANGE_IND='I'

delete from Purchase_RequisitionTablekpoTable where VBELN=''

select * from SellOutOfTheWarehouseTable
select * from Items

SELECT BEDAT AS DATA,VBELN AS MBLNR,2017 AS MJAHR FROM PurchaseOrderSynchronization WHERE PurchaseOrderSynchronizationId='{0}'

select BS,DATA,MBLNR,MJAHR,VBELN,VSTEL FROM Items where VBELN='0080015356'


select SellOutOfTheWarehouseTable.SellOutOfTheWarehouseId from Items join  SellOutOfTheWarehouseTable 
on Items.SellOutOfTheWarehouseId=SellOutOfTheWarehouseTable.SellOutOfTheWarehouseId where SellOutOfTheWarehouseTable.VBELN='{0}'


--库内转移表
select * from Transfer_The_Rolls
select * from Transfer_The_Rolls_KNZYLN


--其它出库下发表
select * from Other_Outbound_Shipments_Table
select * from Other_Outbound_Shipments_Item











select PurchaseOrderSynchronization.PurchaseOrderSynchronizationId from PurchaseOrderSynchronization join PurchaseOrderSynchronizationItem 
on PurchaseOrderSynchronization.PurchaseOrderSynchronizationId=PurchaseOrderSynchronizationItem.PurchaseOrderSynchronizationId
where PurchaseOrderSynchronization.VBELN='4500588753' and PurchaseOrderSynchronization.ZCHANGE_IND='I'

select SellOutOfTheWarehouseTable.SellOutOfTheWarehouseId from Items join  SellOutOfTheWarehouseTable 
on Items.SellOutOfTheWarehouseId=SellOutOfTheWarehouseTable.SellOutOfTheWarehouseId 
where SellOutOfTheWarehouseTable.VBELN='0080015356'

select * from ProductOrder where ProductOrderNo='S300134170'

select * from GetMaterialbills where ProductOrderId='PROD000002QC' 


select distinct '' as EBELP,GetMaterialbills.ProductName as MATNR,GetMaterialbills.ProductDes,'' AS CHARG,'1001' AS LGORT,SUM(QTY) AS MENGE,'6006' AS WERKS,UNIT AS MEINS FROM GetMaterialbills JOIN ProductOrder
ON GetMaterialbills.ProductOrderiD=ProductOrder.ProductOrderId where ProductOrder.ProductOrderNO='S300134170'
group by GetMaterialbills.ProductName,GetMaterialbills.Qty,GetMaterialbills.Unit,GetMaterialbills.ProductDes

SELECT DISTINCT'' AS EBELP,ProductName AS MATNR,'' AS CHARG,'2001' AS LGORT,20 AS MENGE,'6006' AS WERKS,'张' AS MEINS 
FROM GetMaterialBills WHERE ProductOrderId='PROD000002SW' GROUP BY ProductName,Unit,ProductName


select * from ProductOrder where ProductOrderNo='S300134179'
select * from GetMaterialbills where ProductOrderId='PROD000002QE'
--订单信息
select ProductOrderNo as AUFNR,'261' AS BWART,'' AS SOBKZ,'' AS BUDAT,'' AS BLDAT,'CC60062001' as KOSTL,Outbound_Credentials from ProductOrder where ProductOrderNo='S300134170'

--去重查询领料单
select distinct '' as EBELP, GetMaterialbills.ProductName as MATNR,'' AS CHARG,'1001' AS LGORT, SUM(QTY) AS MENGE,'6006' AS WERKS,UNIT AS MEINS,Outbound_Credentials FROM GetMaterialbills join ProductOrder
on GetMaterialbills.ProductOrderId=ProductOrder.ProductOrderId
where ProductOrder.ProductOrderNO='S300134170' and  GetMaterialbills.ProductName not in( 'JJJ-XS-TJB002',
'JJL-XS000006',   'JWJ-LS000343',
'JJL-XS000007',
'JJJ-XS-HX002',
'JWJ-JC000406',
'JJL-BL000007',
'JWJ-JC000559',
'JWJ-JC000225',
'JWJ-JC000134',
'JWJ-JC000258',
'JBC-BH250029',
'JSM-PM000008',
'JJL-QT000009',
'JJJ-XS-ZJ003',
'JFB-PC102239',
'JFB-PC102920',
'JWJ-JC000233',
'JBC-MD250009',
'JJJ-XS-FK001',
'JWJ-JC000382',
'JLC-YG000006',
'JBC-BH120028',
'JWJ-JC000264',
'JWJ-JC000392'

)
group by GetMaterialbills.ProductName,GetMaterialbills.Unit,Outbound_Credentials

select * from GetMaterialbills Where ProductName='JBC-MD220008' and ProductOrderId='PROD000002QC'


select * from ProductOrder where ProductOrderId='PROD000000O0'



select * from SellOutOfTheWarehouseTable

select * from SellOutOfTheWarehouseTable
select * from Items

--delete from SellOutOfTheWarehouseTable where SellOutOfTheWarehouseId='746A6641-A6D'
--delete from Items where SellOutOfTheWarehouseId='746A6641-A6D'



SELECT DISTINCT'' AS EBELP,ProductName AS MATNR,'' AS CHARG,'1000' AS LGORT,sum(Qty) AS MENGE,'6006' AS WERKS,Unit AS MEINS FROM GetMaterialBills
 WHERE ProductOrderId='PROD000002QC' GROUP BY ProductName,Unit,ProductName






--销售出库下发
select * from SellOutOfTheWarehouseTable
select * from Items




select * from GetMaterialBills where OrdId='83468'


ZCHANGE_IND
i






0080015357
--delete from SellOutOfTheWarehouseTable where SellOutOfTheWarehouseId='2C4152FB-EB4'
--delete from Items where SellOutOfTheWarehouseId='2C4152FB-EB4'

select SellOutOfTheWarehouseTable.SellOutOfTheWarehouseId from Items join  SellOutOfTheWarehouseTable 
on Items.SellOutOfTheWarehouseId=SellOutOfTheWarehouseTable.SellOutOfTheWarehouseId 
where SellOutOfTheWarehouseTable.VBELN='0080015357'



select ProductOrderNO,ProductName,'套'as 单位,1 as 数量 from productorder  where ProductOrderNO='S400368499'


select 'S400368499' as AUFNR,101 as BWART,'E' AS SOBKZ,CONVERT (nvarchar(12),GETDATE(),112) as BUDAT,CONVERT (nvarchar(12),GETDATE(),112) as BLDAT
from ProductOrder WHERE ProductOrderId='PROD000000GK'

SELECT DISTINCT'' AS EBELP,ProductName AS MATNR,'' AS CHARG,'2001' AS LGORT,20 AS MENGE,'6006' AS WERKS,'张' AS MEINS FROM GetMaterialBills
WHERE ProductOrderId='PROD000000GK' AND OrdId='66666' GROUP BY ProductName,Unit,ProductName

select * from GetMaterialBills where  ProductOrderId='PROD000000GK'



--销售出库下发
select * from SellOutOfTheWarehouseTable
select * from Items

--truncate table SellOutOfTheWarehouseTable
--truncate table Items
--delete from SellOutOfTheWarehouseTable where SellOutOfTheWarehouseId='0FC3E889-F97'
--delete from Items where SellOutOfTheWarehouseId='0FC3E889-F97'

--采购订单数据
select * from PurchaseOrderSynchronization where VBELN='4500588752'
select * from PurchaseOrderSynchronizationItem where VBELN='4500588752'

--update PurchaseOrderSynchronization set ZCHANGE_IND='Q' where VBELN='' and ZCHANGE_IND='I'

--351出库单
select * from Dump_The_Order_Outbound_ReturnTable where EBELN='4500588752'
select * from Dump_The_Order_Outbound_ReturnekpoTable where VBELN='4500588752'
--101采购入库单
select * from Purchase_RequisitionTable where EBELN='4500588752'
select * from Purchase_RequisitionTablekpoTable where VBELN='4500588752'

delete from Dump_The_Order_Outbound_ReturnTable where EBELN='4500588752'
delete from Dump_The_Order_Outbound_ReturnekpoTable where VBELN='4500588752'
delete from Purchase_RequisitionTable where EBELN='4500588752'
delete from Purchase_RequisitionTablekpoTable where VBELN='4500588752'
delete from PurchaseOrderSynchronization where VBELN='4500588752' 
delete from PurchaseOrderSynchronizationItem where VBELN='4500588752'


select * from Transfer_The_Rolls
select * from Transfer_The_Rolls_KNZYLN


select * from ProductOrder  where ProductOrderNo='S300134170'
select * from GetMaterialbills where ProductOrderId='PROD000002SW'

where PurchaseApplyId='PURC000000TH' where  PurchaseApplyId='PURC000000TH'
--采购申请数据表
select * from PurchaseApply 
select * from PurchaseDetails


select * from MaterialMasterData
--物料表
select * from Product order by CreateDate desc

select BS,DATA,MBLNR,MJAHR,VBELN,VSTEL FROM Items where VBELN='{0}'

select * from Items


select * from ProductOrder

SELECT Bsart AS BSART,TEXT AS ITEXT1 FROM PurchaseApply WHERE PurchaseApplyId='PURC000000TH'

SELECT '' AS BNFPO,PRODUCTNAME AS MATNR,PURCHASEQTY AS MENGE,PURCHASEUNIT AS MEINS,FACTORY AS WERKS,
STORELOCATION AS LGORT,VENDORNO AS LIFNR,FIXEDVENDOR AS FLIEF,REQUESTARRIVEDATE AS LFDAT,PURCHASETISSUE AS EKORG 
FROM purchasedetails WHERE PurchaseApplyId='{0}'


select * from ProductOrder where ProductOrderNO='S300134170'


select ProductOrderNO as 生产订单号,101 as 移动类型代码,'' as 特殊库存,
case when  DatePart (MM,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end as BUDAT,
case when  DatePart (MM,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end 
from productorder where productorderid=''
select ProductOrderNO as AUFNR,101 as BWART,'' as SOBKZ,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end as BUDAT
,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end 
 as BLDAT  from productorder where productorderid='PROD000002SW'


SELECT DISTINCT'' AS EBELP,ProductName AS MATNR,'' AS CHARG,'2001' AS LGORT,20 AS MENGE,'6006' AS WERKS,'张' AS MEINS 
FROM GetMaterialBills WHERE ProductOrderId='{0}' AND OrdId='66666' GROUP BY ProductName,Unit,ProductName
select '' as EBELP,productname as MATNR,''as CHARG,3001 as LGORT,1 as MENGE,6006 as WERKS,'套' as MEINS from ProductOrder where productorderid='PROD000002SW'

--其它出库下发
select * from Other_Outbound_Shipments_Table
select * from Other_Outbound_Shipments_Item






select dbo.InvokeWebService('http://192.168.124.66:8080/SAP_Docking_MES.asmx/Production_Order_Receiving 

','Id=PROD000002SW')








select dbo.InvokeWebService('http://192.168.124.66:8080/SAP_Docking_MES.asmx/Production_Order_Receiving','12345')















































