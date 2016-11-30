CREATE TABLE SalesLT.Reviews ( 
 id int PRIMARY KEY IDENTITY(1,1),
 ProductID int NOT NULL,
 Name nvarchar(30) DEFAULT 'Anonymous',
 Rating Decimal(2,1) NOT NULL,
 Review varchar(max) NOT NULL,
 FOREIGN KEY (ProductId) REFERENCES SalesLT.Product(ProductId)
  );