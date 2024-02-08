use AdventureWorks2016

select Color, SUM(ListPrice) "List Price", sum(StandardCost) "Standard Cost" from
Production.Product
group by Color;

select * from Production.Product;

select Color, sum(ListPrice) ListPrice, sum(StandardCost) "Standard Cost" from
Production.Product where Name Like 'Mountain%' and ListPrice>0 group by Color order by Color asc;

select * from Sales.SalesOrderDetail;
select SalesOrderID, count(ProductID) from Sales.SalesOrderDetail group by SalesOrderID

select * from humanresources.employee
select * from HumanResources.employeedepartmenthistory
select DepartmentID, count(BusinessEntityID) as EMP_NO from HumanResources.employeedepartmenthistory
group by DepartmentID having count(BusinessEntityID)>15


select * from Sales.SalesPerson
select BusinessEntityID, AVG(Bonus) "SalesQouta" from Sales.SalesPerson
group by BusinessEntityID
having AVG(SalesQuota)>25000

select * from HumanResources.employeepayhistory
select * from HumanResources.employeedepartmenthistory
select * from HumanResources.Department

select d.GroupName, min(Rate) MIN_SAL, max(Rate) MAX_SAL, avg(Rate) AVG_SAL, count(p.BusinessEntityID) No_OF_EMP 
from HumanResources.Department d,
HumanResources.employeedepartmenthistory dh,HumanResources.employeepayhistory p
where p.BusinessEntityID=
dh.BusinessEntityID and dh.DepartmentID=d.DepartmentID group by GroupName