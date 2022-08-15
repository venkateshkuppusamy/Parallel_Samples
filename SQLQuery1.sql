select * from Product

--truncate table Product

CREATE NONCLUSTERED INDEX IDX_Product_name ON Product (Name) 
INCLUDE (Id)

Drop INDEX IDX_Product_name ON Product 

SELECT 
    DB_NAME(dbid) as DBName, 
    COUNT(dbid) as NoOfConnections,
    loginame as LoginName
FROM
    sys.sysprocesses
WHERE 
    dbid > 0
GROUP BY 
    dbid, loginame



SELECT RAND()*(10-5)+5;

SELECT FLOOR(RAND()*(20-15+1)+15);


set nocount on

Declare @Id int
Set @Id = 1



While @Id <= 10000000
Begin 
   Insert Into Product values ('Name - ' + CAST(@Id as nvarchar(10)),
              'Description - ' + CAST(@Id as nvarchar(10)),
              'Category - ' + CAST(@Id as nvarchar(10)),
              FLOOR(RAND()*(10-5+1)+5),
              FLOOR(RAND()*(20000-15000+1)+15000)
              )
   Print @Id
   Set @Id = @Id + 1
End