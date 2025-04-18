
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases 
		  WHERE name = 'SmallBussinessHubDB')
BEGIN
	DROP DATABASE [SmallBussinessHubDB]
	print '' print '*** dropping SmallBussinessHubDB'
END
GO

print '' print '*** creating SmallBussinessHubDB'
GO
CREATE DATABASE [SmallBussinessHubDB]
GO

print '' print '*** SmallBussinessHubDB'
GO
USE [SmallBussinessHubDB]
GO

print '' print '*** creating employee table'
GO
CREATE TABLE [dbo].[employee] (
	[EmployeeID] 		[int] IDENTITY(10000,1) 	NOT NULL,
	[FirstName]			[nvarchar](50) 				NOT NULL,
	[LastName]			[nvarchar](50)			 	NOT NULL,
	[PhoneNumber]		[nvarchar](11) 				NOT NULL,
	[Email]				[nvarchar](250) 			NOT NULL,
	[PasswordHash]		[nvarchar](100)		NOT NULL DEFAULT
	'9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E',
	[Active]			[bit]				NOT NULL DEFAULT 1,
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY([EmployeeID] ASC),
	CONSTRAINT [ak_Email] UNIQUE([Email] ASC)
)
GO

print '' print '*** Adding Index for lastName Employee Table'
GO
CREATE NONCLUSTERED INDEX [ix_lastName]
		ON [Employee]([lastName] ASC)		
GO	

print '' print '*** creating role table'
GO
CREATE TABLE [dbo].[role](
	[RoleID]			[nvarchar](50) 				NOT NULL,
	[Description]		[nvarchar](50) 			    ,
	CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID] ASC)
)
GO



print '' print '*** creating empolyeeRole table'
GO

CREATE TABLE [dbo].[employeeRole](
	[EmployeeID] 		[int] 					 	NOT NULL,
	[RoleID]			[nvarchar](50) 				NOT NULL,
	CONSTRAINT [pk_EmployeeID_RoleID] 
		PRIMARY KEY([EmployeeID] ASC, [RoleID] ASC),
	CONSTRAINT [fk_employee_employeeID] FOREIGN KEY([EmployeeID])
		REFERENCES [Employee]([EmployeeID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_role_roleID] FOREIGN KEY([RoleID])
		REFERENCES [Role]([RoleID]) ON UPDATE CASCADE
)



print '' print '*** creating zipCode table'
GO
CREATE TABLE [dbo].[zipCode](
	[zipCodeID]			[nvarchar](10) 				NOT NULL,
	[state]		        [nvarchar](2) 			    ,
	[city]		        [nvarchar](50) 			    ,
	CONSTRAINT [pk_zipCodeID] PRIMARY KEY([zipCodeID] ASC)
)
GO

print '' print '*** inserting zipCode sample data'
GO
INSERT INTO [dbo].[zipCode] 
	([zipCodeID],[state],[city]) 
VALUES
	("43328","PA","Pittsburgh"),
	("73186","ID","Pocatello"),
	("37630","WI","Kenosha"),
	("94652","CA","Fresno"),
	("33862","MD","Columbia"),
	("21839","Co","Aurora"),
	("12517","MS","Gulfport"),
	("26569","Ia","Des Moines"),
	("52404","VT","South Burlington"),
	("11111","FA","Fake place"),
	("12345","FA","Fake place")
GO

print '' print '*** creating addressType table'
GO
CREATE TABLE [dbo].[addressType](
	[addressTypeID]			[nvarchar](20) 				NOT NULL,
	[Description]		    [nvarchar](50) 			    ,	
	CONSTRAINT [pk_addressTypeID] PRIMARY KEY([addressTypeID] ASC)
)
GO

print '' print '*** inserting address type sample data'
GO
INSERT INTO [dbo].[addressType] 
	([addressTypeID], [Description])
 VALUES 
	("shipping address","This is the shiping address"),
	("mailing address","This is the mailing address")
GO


print '' print '*** creating sp_select_all_address_types'
GO
CREATE PROCEDURE [sp_select_all_address_types]
AS
BEGIN
	SELECT [addressTypeID]
	FROM [addressType]
END
GO

print '' print '*** Creating sp_select_supplier_by_id'
GO
CREATE PROCEDURE [sp_select_supplier_by_id]
(
	@SupplierID		[int]
)
AS
BEGIN

	SELECT 	[SupplierID], [name], [phoneNumber],[Email],[addressID],[active]
	FROM 	[dbo].[Supplier]
	WHERE 	[supplierID] = @SupplierID
END
GO

print '' print '*** creating address table'
GO
CREATE TABLE [dbo].[address](
	[addressID]				[int] IDENTITY(10000,1)		NOT NULL,
	[addressLineOne]		[nvarchar](100)		DEFAULT ""		,
	[addressLineTwo]		[nvarchar](100)	DEFAULT ""			,
	[addressTypeID]			[nvarchar](20) 				NOT NULL,
	[zipCodeID]			    [nvarchar](10) 			    NOT NULL,
	[active]				[bit]		 DEFAULT 1      NOT NULL,
	CONSTRAINT [pk_addressID] PRIMARY KEY([addressID] ASC),
	CONSTRAINT [fk_address_addressTypeID] FOREIGN KEY ([addressTypeID])
		REFERENCES [addressType] ([addressTypeID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_address_zipCodeID] FOREIGN KEY ([zipCodeID])
		REFERENCES [zipCode] ([zipCodeID]) ON UPDATE CASCADE
)
GO

print '' print '*** inserting address sample data'
GO
INSERT INTO [dbo].[address]
	([addressLineOne],[addressTypeID], [zipCodeID])
VALUES
	("4357 k street SW", "mailing address", "43328"),
	("4357 pk street NE", "shipping address", "37630")
GO


print '' print '*** Creating sp_select_address_by_address_id'
GO
CREATE PROCEDURE sp_select_address_by_address_id(
	@addressID [int]
)
AS
BEGIN		
	SELECT [addressLineOne], [addressLineTwo], [addressTypeID], [zipCodeID], [active]
	FROM [dbo].[address]
	WHERE @addressID = [addressID]
END
GO

print '' print '*** Creating sp_insert_address'
GO
CREATE PROCEDURE sp_insert_address(
	@addressLineOne [nvarchar](100),
	@addressLineTwo [nvarchar](100),
	@addressTypeID [nvarchar](20),
	@zipCodeID [nvarchar](10)
)
AS
BEGIN		
	INSERT INTO [dbo].[address] 
		([addressLineOne], [addressLineTwo], [addressTypeID], [zipCodeID])
	VALUES
		(@addressLineOne, @addressLineTwo, @addressTypeID, @zipCodeID)
	SELECT SCOPE_IDENTITY()
END
GO

print '' print '*** Creating sp_update_address'
GO
CREATE PROCEDURE [sp_update_address]
(
	@AddressID		  [int],
	@NewAddressLineOne[nvarchar]   (100)	, 
	
	@NewAddressLineTwo[nvarchar]  (100) , 
	@NewAddressTypeID [nvarchar]  (20)  , 
	@NewZipCodeID     [nvarchar]  (10)  , 
	@NewActive		  [bit],
	@OldAddressLineOne[nvarchar]  (100)	, 
	@OldAddressLineTwo[nvarchar]  (100) , 
	@OldAddressTypeID [nvarchar]  (20)  , 
	@OldZipCodeID     [nvarchar]  (10)  , 
	@OldActive		  [bit]	
)
AS
BEGIN
	UPDATE [dbo].[Address]
	SET 
		[AddressLineOne]  = @NewAddressLineOne,
		[AddressLineTwo]  = @NewAddressLineTwo ,
		[AddressTypeID] = @NewAddressTypeID,
		[ZipCodeID]   =     @NewZipCodeID,
		[Active]	=	@NewActive
	WHERE [AddressID] = @AddressID
		AND [AddressLineOne] = @OldAddressLineOne  
		AND [AddressLineTwo]  = @OldAddressLineTwo
		AND [AddressTypeID] =@OldAddressTypeID 
		AND [ZipCodeID]  = @OldZipCodeID
		AND [Active]	= @OldActive
	RETURN @@ROWCOUNT
END		
GO

print'' print'*** creating customer table'
GO
CREATE TABLE [dbo].[customer](
	[customerID]		[int] IDENTITY(10000,1)		NOT NULL,
	[firstName]			[nvarchar](50) 				NOT NULL,
	[lastName]			[nvarchar](50)			 	NOT NULL,
	[phoneNumber]		[nvarchar](11) 				NOT NULL,
	[Email]				[nvarchar](250) 			NOT NULL,
	[active]            [bit]		DEFAULT 1       NOT NULL,
	CONSTRAINT [pk_customerID] PRIMARY KEY([customerID] ASC),
	CONSTRAINT [ak_phoneNumber] UNIQUE([phoneNumber] ASC)
	
)
GO

print'' print'*** creating potentialCustomer table'
GO
CREATE TABLE [dbo].[potentialCustomer](
	[potentialCustomerID]		[int] IDENTITY(10000,1)		NOT NULL,
	[firstName]			[nvarchar](50) 				NOT NULL,
	[lastName]			[nvarchar](50)			 	NOT NULL,
	[phoneNumber]		[nvarchar](11) 				NOT NULL,
	[email]				[nvarchar](250) 			NOT NULL,
		CONSTRAINT [pk_potentialCustomerID] PRIMARY KEY([potentialCustomerID] ASC),
)
GO

print '' print '*** Inserting Sample potentialCustomer data'
GO
INSERT INTO [dbo].[potentialCustomer]
	([firstName], [lastName], [phoneNumber], [email])
VALUES
	("Elamin", "Mohamed", "12345678901", "mo@gmail.com")

GO


print'' print'*** creating customerAddress table'
GO
CREATE TABLE [dbo].[customerAddress](
	[customerID]			[int]						NOT NULL,
	[addressID]				[int]		 				NOT NULL,
	[active]				[bit]						DEFAULT 1 NOT NULL,
	CONSTRAINT [pk_customerAddressID] PRIMARY KEY([customerID] ASC, [addressID] ASC),
	CONSTRAINT [fk_customerAddress_customerID] FOREIGN KEY ([customerID])
		REFERENCES [customer] ([customerID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_customerAddress_addressID] FOREIGN KEY ([addressID])
		REFERENCES [address] ([addressID]) ON UPDATE CASCADE,
)
GO

print'' print'*** creating salesOrder table'
GO
CREATE TABLE [dbo].[salesOrder](
	[salesOrderID]			[int] IDENTITY(10000,1)		NOT NULL,
	[customerID]			[int]						NOT NULL,
	[salesOrderDate]		[datetime]					NOT NULL,
	[employeeID]			[int]		 				NOT NULL,
	[subTotal]			    [decimal](10, 2) 		    NOT NULL,
	[taxAmount]				[decimal](10, 2)			NOT NULL,
	[discountAmount]		[decimal](10, 2)			NULL,
	[totalAmount]			[decimal](10, 2)			NOT NULL,	
	CONSTRAINT [pk_salesOrderID] PRIMARY KEY([salesOrderID] ASC),
	CONSTRAINT [fk_salesOrder_customerID] FOREIGN KEY ([customerID])
		REFERENCES [customer] ([customerID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_salesOrder_employeeID] FOREIGN KEY ([employeeID])
		REFERENCES [employee] ([employeeID]) ON UPDATE CASCADE
)
GO




print '' print '*** creating productType table'
GO
CREATE TABLE [dbo].[productType](
	[productTypeID]			[nvarchar](50) 				NOT NULL,
	[Description]		    [nvarchar](50) 			    ,
	
	CONSTRAINT [pk_productTypeID] PRIMARY KEY([productTypeID] ASC)
)
GO

print '' print '*** Creating Sample Product Type Records'
GO
INSERT INTO [dbo].[ProductType]
	([productTypeID], [description])
	
VALUES

	('Beef', 'Meat Products'),
	('Eggs', 'Dairy  Products')
	


GO	

print'' print'*** creating supplier table'
GO
CREATE TABLE [dbo].[supplier](
	[supplierID]		[int] IDENTITY(10000,1)		NOT NULL,
	[name]				[nvarchar](500) 			NOT NULL,
	[phoneNumber]		[nvarchar](11) 				NOT NULL,
	[email]				[nvarchar](250) 			NOT NULL,
	[addressID]			[int]						NOT NULL,
	[Active]			[bit]				NOT NULL DEFAULT 1,
	CONSTRAINT [pk_supplierID] PRIMARY KEY([supplierID] ASC),
	CONSTRAINT [fk_supplier_addressID] FOREIGN KEY ([addressID])
		REFERENCES [address] ([addressID]) ON UPDATE CASCADE,
		CONSTRAINT [kk_Email] UNIQUE([Email] ASC)
		
)
GO

print '' print '*** inserting supplier sample data'
GO
INSERT INTO supplier 
	([name],[phoneNumber],[email],[addressId]) 
VALUES
	("Chicago Store","3193782882","hsbc@gmail.com",10000),
	("Walmart","3199999999","Walmart@hotmail.com",10001)
;

print'' print'*** creating purchaseOrder table'
GO
CREATE TABLE [dbo].[purchaseOrder](
	[purchaseOrderID]		[int] IDENTITY(10000,1)		NOT NULL,
	[supplierID]			[int]						NOT NULL,
	[salesOrderDate]		[datetime]					NOT NULL,
	[employeeID]			[int]		 				NOT NULL,
	[subTotal]			    [decimal](10, 2) 		    NOT NULL,
	[taxAmount]				[decimal](10, 2)			NOT NULL,
	[discountAmount]		[decimal](10, 2)			NULL,
	[totalAmount]			[decimal](10, 2)			NOT NULL,
	
	
	CONSTRAINT [pk_purchaseOrderID] PRIMARY KEY([purchaseOrderID] ASC),
	CONSTRAINT [fk_purchaseOrder_supplierID] FOREIGN KEY ([supplierID])
		REFERENCES [supplier] ([supplierID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_purchaseOrder_employeeID] FOREIGN KEY ([employeeID])
		REFERENCES [employee] ([employeeID]) ON UPDATE CASCADE
)
GO

print'' print'*** creating product table'
GO
CREATE TABLE [dbo].[product](
	[productID]				[int] IDENTITY(10000,1)		NOT NULL,
	[cost]			    	[decimal](10, 2) 		    NOT NULL,
	[description]			[nvarchar](500)				NOT NULL,
	[dateReceived]			[datetime]		            NOT NULL,
	[productTypeID]			[nvarchar](50)				NOT NULL,
	[manufacturerName]		[nvarchar](500)				NOT NULL,
	[supplierID]			[int]						NOT NULL,
	[price]					[decimal](10, 2)			NOT NULL,
	[purchaseUnit]			[nvarchar](50)				NOT NULL,
	[saleUnit]				[nvarchar](50)				NOT NULL,
	[qoh]					[int]						NOT NULL,
	[reorderLevel]			[int]						NOT NULL,
	[Active]			    [bit]				NOT NULL DEFAULT 1
	
	CONSTRAINT [pk_productID] PRIMARY KEY([productID] ASC),
	CONSTRAINT [fk_product_productTypeID] FOREIGN KEY ([productTypeID])
		REFERENCES [productType] ([productTypeID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_product_supplierID] FOREIGN KEY ([supplierID])
		REFERENCES [supplier] ([supplierID]) ON UPDATE CASCADE
	
)
GO


print'' print'*** creating inventory table'
GO
CREATE TABLE [dbo].[inventory](
	[inventoryID]			[int] IDENTITY(10000,1)		NOT NULL,
	[productID]				[int]						NOT NULL,
	[arrivalDate]			[datetime]					NOT NULL,
	[count]					[int]						NOT NULL,
	[cost]					[decimal](10, 2)			NOT NULL,
	[employeeID]			[int]						NOT NULL,
	[description]			[nvarchar](500)				NOT NULL,
	
	CONSTRAINT [pk_inventoryID] PRIMARY KEY([inventoryID] ASC),
	CONSTRAINT [fk_inventory_productID] FOREIGN KEY ([productID])
		REFERENCES [product] ([productID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_inventory_employeeID] FOREIGN KEY ([employeeID])
		REFERENCES [employee] ([employeeID]) ON UPDATE CASCADE
)
GO

print'' print'*** creating salesOrderLineItem table'
GO
CREATE TABLE [dbo].[salesOrderLineItem](
	[salesOrderID]			[int] 						NOT NULL,
	[productID]				[int]						NOT NULL,
	[quantity]				[int]						NOT NULL,
	[unit]					[nvarchar](50)		 		NOT NULL,
	[price]				    [decimal](10, 2) 		    NOT NULL,
	[itemTotal]				[decimal](10, 2)			NOT NULL,
	
	
	CONSTRAINT [pk_salesOrderLineItemID] PRIMARY KEY([salesOrderID] ASC, [productID] ASC),
	CONSTRAINT [fk_salesOrderLineItem_salesOrderID] FOREIGN KEY ([salesOrderID])
		REFERENCES [salesOrder] ([salesOrderID]) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT [fk_salesOrderLineItem_productID] FOREIGN KEY ([productID])
		REFERENCES [product] ([productID]) ON UPDATE CASCADE
)
GO



print'' print'*** creating purchaseOrderLineItem table'
GO
CREATE TABLE [dbo].[purchaseOrderLineItem](
	[purchaseOrderID]		[int] 						NOT NULL,
	[productID]				[int]						NOT NULL,
	[quantity]				[int]						NOT NULL,
	[unit]					[nvarchar](50)		 		NOT NULL,
	[price]				    [decimal](10, 2) 		    NOT NULL,
	[itemDiscount]			[decimal](10, 2)			NULL,
	[itemTotal]				[decimal](10, 2)			NOT NULL,
	
	
	CONSTRAINT [pk_purchaseOrderLineItemID] PRIMARY KEY([purchaseOrderID] ASC, [productID] ASC),
	CONSTRAINT [fk_purchaseOrderLineItem_purchaseOrderID] FOREIGN KEY ([purchaseOrderID])
		REFERENCES [purchaseOrder] ([purchaseOrderID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_purchaseOrderLineItem_productID] FOREIGN KEY ([productID])
		REFERENCES [product] ([productID])
)
GO


print'' print'*** creating receivingRecord table'
GO
CREATE TABLE [dbo].[receivingRecord](
	[purchaseOrderID]		[int] 			NOT NULL,
	[employeeID]			[int]			NOT NULL,
	[quantity]				[int]			NOT NULL,
	[unit]					[nvarchar](50)	NOT NULL,
	[productID]				[int]			NOT NULL,
	[dateReceived]			[datetime]		NOT NULL,
	[passedInspection]		[bit]			NOT NULL,
	CONSTRAINT [pk_receivingRecord] PRIMARY KEY([purchaseOrderID] ASC, [productID] ASC),
	CONSTRAINT [fk_receivingRecord_purchaseOrderID] FOREIGN KEY ([purchaseOrderID])
		REFERENCES [purchaseOrder] ([purchaseOrderID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_receivingRecord_productID] FOREIGN KEY ([productID])
		REFERENCES [product] ([productID]),
	CONSTRAINT [fk_receivingRecord_employeeID] FOREIGN KEY ([employeeID])
		REFERENCES [employee] ([employeeID])
)
GO





print '' print '*** Creating Sample Product Records'
GO
INSERT INTO [dbo].[Product]
	([cost],[description],[dateReceived], [productTypeID],
	[manufacturerName], [supplierID], [price],[purchaseUnit]
	,[saleUnit],[qoh],[reorderLevel])
	
VALUES
	('2000', 'Meat Prodcuts','10/10/2025','Beef', 'Farm', 10000
	,'100','pound','unity',100,20),
	('20', 'Dairy  Products','10/11/2024','Eggs', 'Farm', 10000
	,'200','box','unity',100,20)

GO	

print '' print '*** Creating Sample Employee Records'
GO
INSERT INTO [dbo].[Employee]
	([FirstName], [LastName], [PhoneNumber], [Email])
	VALUES
	('System', 'Admin', '13191230000','ad@gmail.com'),
	('Mohamed', 'Elamin', '13191232222','mo@gmail.com')
	
GO

print '' print '*** Creating Sample Deactivated Employee'
GO
INSERT INTO [dbo].[Employee]
	([FirstName], [LastName], [PhoneNumber], [Email], [Active])
	VALUES
	('Boris', 'Badworker', '13191239999','boris@company.com', 0)
GO

print '' print '*** Creating Sample Role Records'
GO
INSERT INTO [dbo].[Role]
	([RoleID])
	VALUES
	('Administrator'),
	('Manager')
	
GO

print '' print '*** Inserting Sample EmployeeRole Records'
GO
INSERT INTO [dbo].[EmployeeRole]
	([EmployeeID], [RoleID])
	VALUES
	(10000, 'Administrator'),
	(10001, 'Manager')



GO

print '' print '*** Creating sp_authenticate_user'
GO
CREATE PROCEDURE [sp_authenticate_user]
(
	@Email 				[nvarchar](250),
	@PasswordHash		[nvarchar](100)
)
AS
BEGIN
	SELECT 	COUNT([EmployeeID])
	FROM 	[dbo].[Employee]
	WHERE	[Email] = LOWER(@Email)
	  AND	[PasswordHash] = @PasswordHash
	  AND	[Active] = 1
END
GO

print '' print '*** Creating sp_update_email'
GO
CREATE PROCEDURE [sp_update_email]
(
	@OldEmail 			[nvarchar](250),
	@NewEmail			[nvarchar](250),
	@PasswordHash		[nvarchar](100)
)
AS
BEGIN
	UPDATE 	[dbo].[Employee]
	SET 	[Email] = LOWER(@NewEmail)
	WHERE	[Email] = LOWER(@OldEmail)
	  AND	[PasswordHash] = @PasswordHash
	  AND	[Active] = 1
	RETURN @@ROWCOUNT
END
GO

print '' print '*** Creating sp_select_user_by_email'
GO
CREATE PROCEDURE [sp_select_user_by_email]
(
	@Email 			[nvarchar](250)
)
AS
BEGIN
	SELECT 	[EmployeeID], [FirstName], [LastName], [PhoneNumber]
	FROM 	[Employee]
	WHERE	[Email] = @Email
END
GO

print '' print '*** Creating sp_select_roles_by_userid'
GO
CREATE PROCEDURE [sp_select_roles_by_userid]
(
	@EmployeeID 		[int]
)
AS
BEGIN
	SELECT 	[RoleID]
	FROM 	[EmployeeRole]
	WHERE	[EmployeeID] = @EmployeeID
END
GO


print '' print '*** Creating sp_update_password'
GO
CREATE PROCEDURE [sp_update_password]
(
	@EmployeeID			[int],
	@OldPasswordHash	[nvarchar](100),
	@NewPasswordHash	[nvarchar](100)
)
AS
BEGIN
	UPDATE 	[dbo].[Employee]
	SET 	[PasswordHash] 	= @NewPasswordHash
	WHERE	[EmployeeID] 	= @EmployeeID
	  AND	[PasswordHash] 	= @OldPasswordHash
	  AND	[Active] = 1
	RETURN @@ROWCOUNT
END
GO

print '' print '*** Creating sp_update_password_by_email'
GO
CREATE PROCEDURE [sp_update_password_by_email]
(
	@Email			[nvarchar](250),
	@PasswordHash	[nvarchar](100)
)
AS
BEGIN
	UPDATE 	[dbo].[Employee]
	SET 	[PasswordHash] 	= @PasswordHash
	WHERE	[Email] 		= @Email
	  AND	[Active] = 1
	RETURN @@ROWCOUNT
END
GO

print '' print '*** Creating sp_insert_employee'
GO
CREATE PROCEDURE [sp_insert_employee]
(
	@FirstName		[nvarchar](50),
	@LastName		[nvarchar](50),
	@PhoneNumber 	[nvarchar](11),
	@Email 			[nvarchar](250)
)
AS
BEGIN
	INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email])
	VALUES
		(@FirstName, @LastName, @PhoneNumber, LOWER(@Email))
	SELECT SCOPE_IDENTITY()
END
GO


print '' print '*** Creating sp_insert_salesOrder'
GO
CREATE PROCEDURE [sp_insert_salesOrder]
(
	@customerID			[int],
	@salesOrderDate		[datetime],
	@employeeID			[int],
	@subTotal		    [decimal](10, 2),
	@taxAmount			[decimal](10, 2),
	@discountAmount		[decimal](10, 2),
	@totalAmount		[decimal](10, 2)
)
AS
BEGIN
	INSERT INTO [dbo].[salesOrder]
		([customerID], [salesOrderDate], [employeeID], [subTotal], [taxAmount], [discountAmount], [totalAmount])
	VALUES
		(@customerID, @salesOrderDate, @employeeID, @subTotal, @taxAmount, @discountAmount, @totalAmount)
	SELECT SCOPE_IDENTITY()
END
GO

/*	
print '' print '*** Creating sp_insert_salesOrderLineItem'
GO
CREATE PROCEDURE [sp_insert_salesOrderLineItem]
(
	@salesOrderID		[int],
	@productID			[int],
	@quantity			[int],
	@unit		    	[nvarchar](50),
	@price				[decimal](10, 2),
	@itemTotal			[decimal](10, 2)
)
AS
BEGIN
	INSERT INTO [dbo].[salesOrderLineItem]
		([salesOrderID], [productID], [quantity], [unit], [price], [itemTOtal])
	VALUES
		(@salesOrderID, @productID, @quantity, @unit, @price, @itemTotal)
	SELECT @@ROWCOUNT
END
GO
*/

print '' print '*** Creating sp_insert_product'
GO
CREATE PROCEDURE [sp_insert_product]
(
	@cost		       [decimal](10, 2),
	@description	   [nvarchar](500),	
    @dateReceived	   [datetime],
    @productTypeID	   [nvarchar](50),
	@manufacturerName  [nvarchar](500),
	@price 	           [decimal](10, 2),
	@purchaseUnit 	   [nvarchar](50),
	@saleUnit 	       [nvarchar](50),
	@supplierID 	   [int],
	@qoh                [int],
	@reorderLevel       [int],
	@active			[bit]
	

)
AS
BEGIN
	INSERT INTO [dbo].[product]	
		([cost],[description],[dateReceived],[productTypeID],[manufacturerName],
		 [price],[purchaseUnit],[saleUnit],[supplierID],[qoh],[reorderLevel],[active])
		
	VALUES
		(@cost,@description,@dateReceived,@productTypeID,@manufacturerName, @price, @purchaseUnit,
		@saleUnit, @supplierID,@qoh,@reorderLevel,@active)
	RETURN SCOPE_IDENTITY()
END
GO

print '' print '*** Creating sp_select_user_by_active'
GO
CREATE PROCEDURE [sp_select_user_by_active]
(
	@Active			[bit]
)
AS
BEGIN
	SELECT 	[EmployeeID],[FirstName], [LastName], [PhoneNumber], [Email]
	FROM 	[dbo].[Employee]
	WHERE 	[Active] = @Active
END
GO


print '' print '*** Inserting Sample customer data'
GO
INSERT INTO [dbo].[Customer]
	([firstName], [lastName], [phoneNumber], [email])
VALUES
	("Romaine", "Stillings", "12345678901", "Stillings@gmail.com"),
	("Jarvis", "Lout", "19345174905", "Lout@hotmail.com"),
	("Jane", "Smith", "12345674701", "Smith@yahoo.com"),
	("Benita", "Andrews", "87945674901", "Benita@yahoo.com"),
	("Brittanie", "Rosenberg", "72363678529", "Rosenberg@yahoo.com")
GO











print '' print '*** Creating sp_select_supplier_by_active'
GO
CREATE PROCEDURE [sp_select_supplier_by_active]
(
	@Active			[bit]
)
AS
BEGIN
	SELECT 	[supplierID],[name], [phoneNumber], [email], [addressID], [active]
	FROM 	[dbo].[supplier]
	WHERE 	[Active] = @Active
END
GO


print '' print '*** Creating sp_select_product_by_active'
GO
CREATE PROCEDURE [sp_select_product_by_active]
(
	@Active			[bit]
)
AS
BEGIN
	SELECT 	[productID], [cost], [description], [dateReceived], [productTypeID], 
	[manufacturerName], [supplierID], [price],[purchaseUnit], [saleUnit], [qoh],
	[reorderLevel], [active]
	FROM 	[dbo].[Product]
	WHERE 	[Active] = @Active
END
GO


print '' print '*** Creating sp_select_products'
GO
CREATE PROCEDURE [sp_select_products]
AS
BEGIN
	SELECT 	[productID], [cost], [description], [dateReceived], [productTypeID], 
	[manufacturerName], [supplierID], [price],[purchaseUnit], [saleUnit], [qoh],
	[reorderLevel], [active]
	FROM 	[dbo].[Product]
END
GO

print '' print '*** Creating sp_select_all_product_types'
GO
CREATE PROCEDURE [sp_select_all_product_types]
AS
BEGIN
	SELECT 	[productTypeID],[description] 
	FROM 	[dbo].[ProductType]
END
GO

print '' print '*** Creating sp_select_employees'
GO
CREATE PROCEDURE [sp_select_employees]
AS
BEGIN
	SELECT 	[EmployeeID],[FirstName], [LastName], [PhoneNumber], [Email],[Active]
	FROM 	[dbo].[Employee]
END
GO
 
 
 
 
print '' print '*** Creating sp_select_employee_by_id'
GO
CREATE PROCEDURE [sp_select_employee_by_id]
(
	@EmployeeID		[int]
)
AS
BEGIN
	SELECT 	[EmployeeID],[FirstName], [LastName], [PhoneNumber], [Email],[Active]
	FROM 	[dbo].[Employee]
	WHERE 	[EmployeeID] = @EmployeeID
END
GO


print '' print '*** Creating sp_select_product_by_id'
GO
CREATE PROCEDURE [sp_select_product_by_id]
(
	@ProductID		[int]
)
AS
BEGIN
	SELECT 	[productID], [cost], [description], [dateReceived], [productTypeID], 
	[manufacturerName], [supplierID], [price],[purchaseUnit], [saleUnit], [qoh],
	[reorderLevel], [active]
	FROM 	[dbo].[Product]
	WHERE 	[ProductID] = @ProductID
END
GO


print '' print '*** Creating sp_activate_customer'
GO
CREATE PROCEDURE [sp_activate_customer]
(
	@CustomerID		[int]
)
AS
BEGIN
	
	UPDATE  	[dbo].[Customer]
	SET [Active] = 1
	WHERE 	[CustomerID] = @CustomerID
	RETURN @@ROWCOUNT
END
GO


print '' print '*** Creating sp_deactivate_customer'
GO
CREATE PROCEDURE [sp_deactivate_customer]
(
	@CustomerID		[int]
)
AS
BEGIN
	
	UPDATE  	[dbo].[Customer]
	SET [Active] = 0
	WHERE 	[CustomerID] = @CustomerID
	RETURN @@ROWCOUNT
END
GO


print '' print '*** Creating sp_deactivate_customer'
GO
CREATE PROCEDURE [sp_reactivate_customer]
(
	@CustomerID		[int]
)
AS
BEGIN
	
	UPDATE  	[dbo].[Customer]
	SET [Active] = 1
	WHERE 	[CustomerID] = @CustomerID
	RETURN @@ROWCOUNT
END
GO



print '' print '*** Creating sp_delete_customer_by_id'
GO
CREATE PROCEDURE sp_delete_customer_by_id
	(
		@CustomerID				[int]
	)
AS
	BEGIN
	
		DELETE  
		FROM 	[salesOrder]
		WHERE 	[CustomerID] = @CustomerID	;
		
		  
		DELETE  
		FROM 	[Customer]
		WHERE 	[CustomerID] = @CustomerID	
		  AND	[Active] = 0
	  
		RETURN @@ROWCOUNT
	END
GO










print '' print '*** Creating sp_insert_customer'
GO
CREATE PROCEDURE [sp_insert_customer]
(
	@FirstName		[nvarchar](50),
	@LastName		[nvarchar](50),
	@PhoneNumber 	[nvarchar](11),
	@Email 			[nvarchar](250)
)
AS
BEGIN
	INSERT INTO [dbo].[Customer]
		([FirstName], [LastName], [PhoneNumber], [Email])
	VALUES
		(@FirstName, @LastName, @PhoneNumber, LOWER(@Email))
	SELECT SCOPE_IDENTITY()
END
GO



print '' print '*** Creating sp_select_customer_by_active'
GO
CREATE PROCEDURE [sp_select_customer_by_active]
(
	@Active			[bit]
)
AS
BEGIN
	SELECT 	[customerID],[FirstName], [LastName], [PhoneNumber], [Email]
	FROM 	[dbo].[Customer]
	WHERE 	[Active] = @Active
END
GO











print '' print '*** Creating sp_update_customer'
GO
CREATE PROCEDURE [sp_update_customer]
(
	@CustomerID		   [int],
	@NewFirstName      [nvarchar]  (50) , 
	@NewLastName       [nvarchar]  (50) , 
	@NewPhoneNumber    [nvarchar]  (11) , 
	@NewEmail          [nvarchar]  (250) , 
	@oldFirstName      [nvarchar] (50),
	@oldLastName       [nvarchar] (50),
	@oldPhoneNumber    [nvarchar] (50),
	@oldEmail          [nvarchar] (50)	
)
AS
BEGIN
	UPDATE [dbo].[Customer]
	SET 
	[FirstName]  = @NewFirstName,
	[LastName]  = @NewLastName ,
	[PhoneNumber] = @NewPhoneNumber,
	[Email]   =     @NewEmail
WHERE [CustomerID] = @CustomerID
 AND [FirstName] = @oldFirstName  
 AND [LastName]  = @oldLastName
 AND [PhoneNumber] =@oldPhoneNumber 
 AND [Email]  = @oldEmail
	RETURN @@ROWCOUNT
END		
GO





print '' print '*** Creating sp_insert_supplier'
GO
CREATE PROCEDURE [sp_insert_supplier]
(
	@name		    [nvarchar](50),
	@PhoneNumber 	[nvarchar](11),
	@Email 			[nvarchar](250),
	@AddressID		[int]
)
AS
BEGIN
	INSERT INTO [dbo].[Supplier]
		([name],[PhoneNumber],[Email], [addressID])
	VALUES
		(@name,@PhoneNumber, LOWER(@Email), @AddressID)
	SELECT SCOPE_IDENTITY()
END
GO

print '' print '*** Creating sp_update_supplier'
GO
CREATE PROCEDURE [sp_update_supplier]
(
	@supplierID		   [int],
	@NewName           [nvarchar]  (50) ,
	@NewPhoneNumber    [nvarchar]  (11) , 
	@NewEmail          [nvarchar]  (250) , 
	@oldName              [nvarchar] (50),
	@oldPhoneNumber    [nvarchar] (50),
	@oldEmail          [nvarchar] (50)
)
AS
BEGIN
	UPDATE [dbo].[Supplier]
	SET 
	[Name]  = @NewName,
	[PhoneNumber] = @NewPhoneNumber,
	[Email]   =     @NewEmail
WHERE [supplierID] = @supplierID
 AND [Name] = @oldName  
 AND [PhoneNumber] =@oldPhoneNumber 
 AND [Email]  = @oldEmail
	RETURN @@ROWCOUNT
END		
GO














print '' print '*** Creating sp_update_employee'
GO
CREATE PROCEDURE [sp_update_employee]
(
	@EmployeeID		[int],
	@NewFirstName   [nvarchar]  (50) , 
	@NewLastName   [nvarchar]  (50) , 
	@NewPhoneNumber  [nvarchar]  (11) , 
	@NewEmail   [nvarchar]  (250) , 
	@oldFirstName      [nvarchar] (50),
	@oldLastName      [nvarchar] (50),
	@oldPhoneNumber     [nvarchar] (50),
	@oldEmail      [nvarchar] (50)	
)
AS
BEGIN
	UPDATE [dbo].[Employee]
	SET 
	[FirstName]  = @NewFirstName,
	[LastName]  = @NewLastName ,
	[PhoneNumber] = @NewPhoneNumber,
	[Email]   =     @NewEmail
WHERE [EmployeeID] = @EmployeeID
 AND [FirstName] = @oldFirstName  
 AND [LastName]  = @oldLastName
 AND [PhoneNumber] =@oldPhoneNumber 
 AND [Email]  = @oldEmail
	RETURN @@ROWCOUNT
END		
GO




print '' print '*** Creating sp_update_product'
GO
CREATE PROCEDURE [sp_update_product]
(
	@ProductID				[int]				,
	@NewCost   				[nvarchar](50) 		, 
	@NewDescription			[nvarchar](500)		,			
	@NewDateReceived		[datetime]			,	           
	@NewProductTypeID		[nvarchar](50)		,		
	@NewManufacturerName	[nvarchar](500)		,		
	@NewSupplierID			[int]				,		
	@NewPrice				[decimal](10, 2)	,		
	@NewPurchaseUnit		[nvarchar](50)		,		
	@NewSaleUnit			[nvarchar](50)		,		
	@NewQOH					[int]				,		
	@NewReorderLevel		[int]				,		
	@NewActive				[bit] 				,
	@OldCost   				[nvarchar](50) 		, 
	@OldDescription			[nvarchar](500)		,			
	@OldDateReceived		[datetime]			,	           
	@OldproductTypeID		[nvarchar](50)		,		
	@OldmanufacturerName	[nvarchar](500)		,		
	@OldsupplierID			[int]				,		
	@OldPrice				[decimal](10, 2)	,		
	@OldPurchaseUnit		[nvarchar](50)		,		
	@OldSaleUnit			[nvarchar](50)		,		
	@OldQOH					[int]				,		
	@OldReorderLevel		[int]				,		
	@OldActive				[bit] 				
)
AS
BEGIN
	UPDATE [dbo].[Product]
	SET 
	[Cost]  = @NewCost,
	[Description]  = @NewDescription ,
	[DateReceived] = @NewDateReceived,
	[ProductTypeID]   = @NewProductTypeID,
	[ManufacturerName] = @NewManufacturerName,
	[SupplierID] = @NewSupplierID,
	[Price]	= @NewPrice,
	[PurchaseUnit] = @NewPurchaseUnit,
	[SaleUnit] = @NewSaleUnit,
	[QOH] = @NewQOH,
	[ReorderLevel] = @NewReorderLevel,
	[Active] = @NewActive
WHERE [Cost]  = @OldCost AND
	[Description]  = @OldDescription  AND
	[DateReceived] = @OldDateReceived AND
	[ProductTypeID]   = @OldProductTypeID AND
	[ManufacturerName] = @OldManufacturerName AND
	[SupplierID] = @OldSupplierID AND
	[Price]	= @OldPrice AND
	[PurchaseUnit] = @OldPurchaseUnit AND
	[SaleUnit] = @OldSaleUnit AND
	[QOH] = @OldQOH AND
	[ReorderLevel] = @OldReorderLevel AND
	[Active] = @OldActive AND
	[ProductID] = @ProductID
	RETURN @@ROWCOUNT
END		
GO

print '' print '*** Creating sp_deactivate_supplier'
GO
CREATE PROCEDURE [sp_deactivate_supplier]
(
	@SupplierID		[int]
)
AS
BEGIN
	
	UPDATE  	[dbo].[Supplier]
	SET [Active] = 0
	WHERE 	[SupplierID] = @SupplierID
	RETURN @@ROWCOUNT
END
GO




print '' print '*** Creating sp_deactivate_employee'
GO
CREATE PROCEDURE [sp_deactivate_employee]
(
	@EmployeeID		[int]
)
AS
BEGIN
	
	UPDATE  	[dbo].[Employee]
	SET [Active] = 0
	WHERE 	[EmployeeID] = @EmployeeID
	RETURN @@ROWCOUNT
END
GO

print '' print '*** Creating sp_deactivate_product'
GO
CREATE PROCEDURE [sp_deactivate_product]
(
	@ProductID		[int]
)
AS
BEGIN
	
	UPDATE  	[dbo].[Product]
	SET [Active] = 0
	WHERE 	[ProductID] = @ProductID
	RETURN @@ROWCOUNT
END
GO


print '' print '*** Creating sp_reactivate_employee'
GO
CREATE PROCEDURE [sp_reactivate_employee]
(
	@EmployeeID		[int]
)
AS
BEGIN
	
	UPDATE  	[dbo].[Employee]
	SET [Active] = 1
	WHERE 	[EmployeeID] = @EmployeeID
	RETURN @@ROWCOUNT
END
GO


print '' print '*** Creating sp_deactivate_product'
GO
CREATE PROCEDURE [sp_reactivate_product]
(
	@ProductID		[int]
)
AS
BEGIN
	
	UPDATE  	[dbo].[Product]
	SET [Active] = 1
	WHERE 	[ProductID] = @ProductID
	RETURN @@ROWCOUNT
END
GO




print '' print '*** Creating sp_insert_employee_role'
GO
CREATE PROCEDURE [sp_insert_employee_role]

(
	@EmployeeID		[int],
	@RoleID         [nvarchar]   (50)
)
AS
BEGIN
	INSERT INTO  [dbo].[EmployeeRole]
	([EmployeeID],[RoleID])

	VALUES
		(@EmployeeID,@RoleID)
END
GO



print '' print '*** Creating sp_delete_employee_role'
GO
CREATE PROCEDURE [sp_delete_employee_role]
(
	@EmployeeID		[int],
	@RoleID         [nvarchar]   (50)
)
AS
BEGIN
	DELETE FROM [dbo].[EmployeeRole]
	WHERE [EmployeeID]	= @EmployeeID
	AND [RoleID] =  @RoleID
END
GO

print '' print '*** Creating sp_delete_salesOrder'
GO
CREATE PROCEDURE [sp_delete_salesOrder]
(
	@SalesOrderID		[int]
)
AS
BEGIN
	DELETE FROM [dbo].[SalesOrder]
	WHERE [SalesOrderID]	= @SalesOrderID
END
GO

print '' print '*** Creating sp_select_all_roles'
GO
CREATE PROCEDURE [sp_select_all_roles]
AS
BEGIN
	SELECT [RoleID]
	FROM [dbo].[Role]
	ORDER BY [RoleID]
END
GO
   

 print '' print '*** Creating sp_select_all_salesOrders'
GO
CREATE PROCEDURE [sp_select_all_salesOrders]
AS
BEGIN
	SELECT [salesOrderID], [customerID], [salesOrderDate], 
	[employeeID], [subTotal], [taxAmount], [discountAmount], [totalAmount]
	FROM [dbo].[SalesOrder]
	ORDER BY [salesOrderID]
END
GO

	 print '' print '*** Creating sp_select_all_salesOrderLinesById'
GO
CREATE PROCEDURE [sp_select_all_salesOrderLinesById]
(
	@salesOrderID [int]
)
AS
BEGIN
	SELECT [salesOrderID], [productID], [quantity], 
	[unit], [price], [itemTotal]
	FROM [dbo].[SalesOrderLineItem]
	WHERE @salesOrderID = salesOrderID
	ORDER BY [salesOrderID]
END
GO

print'' print'*** inserting salesOrder sample data'
GO
INSERT INTO [dbo].[salesOrder]
	([customerID], [salesOrderDate],[employeeID], [subTotal], [taxAmount], [discountAmount], [totalAmount])
VALUES
	(10000, GETDATE(), 10000, 10.00, .70, 0.00, 10.70)
GO

/*
print'' print'*** inserting salesOrderLineItem sample data'
GO
INSERT INTO [dbo].[salesOrderLineItem]
	([salesOrderID], [productID], [quantity], [unit], [price], [itemTotal])
VALUES
	(10000, 10000, 1, "unit", 10.00, 10.00)
GO
*/
print '' print '*** Creating sp_update_salesOrder'
GO
CREATE PROCEDURE [sp_update_salesOrder]
(
	@salesOrderID			[int],

	@oldCustomerID			[int],
	@oldSalesOrderDate		[datetime],
	@oldEmployeeID			[int],
	@oldSubTotal		    [decimal](10, 2),
	@oldTaxAmount			[decimal](10, 2),
	@oldDiscountAmount		[decimal](10, 2),
	@oldTotalAmount			[decimal](10, 2),
	
	@newCustomerID			[int],
	@newSalesOrderDate		[datetime],
	@newEmployeeID			[int],
	@newSubTotal		    [decimal](10, 2),
	@newTaxAmount			[decimal](10, 2),
	@newDiscountAmount		[decimal](10, 2),
	@newTotalAmount			[decimal](10, 2)
)
AS
BEGIN
	DECLARE @updateRowCount [int]
	BEGIN TRANSACTION
	
	UPDATE [dbo].[SalesOrder]
	SET 
	[customerID] = @newCustomerID,
	[salesOrderDate] = @newSalesOrderDate,
	[employeeID] = @newEmployeeID,
	[subTotal] = @newSubTotal,
	[taxAmount] = @newTaxAmount,
	[discountAmount] = @newDiscountAmount,
	[totalAmount] = @newTotalAmount
	WHERE
	[salesOrderID] = @salesOrderID
	AND [customerID] = @oldCustomerID
	AND [salesOrderDate] = @oldSalesOrderDate
	AND [employeeID] = @oldEmployeeID
	AND [subTotal] = @oldSubTotal
	AND [taxAmount] = @oldTaxAmount
	AND [discountAmount] = @oldDiscountAmount
	AND [totalAmount] = @oldTotalAmount
	
	SET @updateRowCount = @@ROWCOUNT
	
	IF @updateRowCount > 0
		DELETE FROM [dbo].[salesOrderLineItem]
		WHERE [salesOrderID] = @salesOrderID
	
	
	COMMIT TRANSACTION
	RETURN @updateRowCount
END
GO

print '' print 'creating sp_delete_salesOrderLines'
GO
CREATE PROCEDURE [sp_delete_salesOrderLines]
(
	@salesOrderID	[int]
)
AS
BEGIN
	DELETE FROM [dbo].[salesOrderLineItem]
	WHERE [salesOrderID] = @salesOrderID
END
GO




print '' print '*** Creating sp_select_customer_by_id'
GO
CREATE PROCEDURE [sp_select_customer_by_id]
(
	@CustomerID		[int]
)
AS

BEGIN
	SELECT 	[customerID], [firstName], [lastName], [phoneNumber], [email], 
	[active]
	
	FROM 	[dbo].[Customer]
	WHERE 	[CustomerID] = @CustomerID
END
GO


print '' print '*** Creating sp_insert_potential_customer'
GO
CREATE PROCEDURE [sp_insert_potential_customer]
(
	@FirstName		[nvarchar](50),
	@LastName		[nvarchar](50),
	@PhoneNumber 	[nvarchar](11),
	@Email 			[nvarchar](250)
)
AS
BEGIN
	INSERT INTO [dbo].[potentialCustomer]
		([FirstName], [LastName], [PhoneNumber], [Email])
	VALUES
		(@FirstName, @LastName, @PhoneNumber, LOWER(@Email))
	SELECT SCOPE_IDENTITY()
END
GO

/*
print '' print '*** Creating sp_select_supplier_by_id'
GO
CREATE PROCEDURE [sp_select_supplier_by_id]
(ProductType
	@SupplierID		[int]
)
AS
BEGIN

	SELECT 	[SupplierID], [name], [phoneNumber],[Email],[addressID],[active]
	FROM 	[dbo].[Supplier]
	WHERE 	[supplierID] = @SupplierID
END
GO
*/
/*
print '' print '*** Creating sp_select_all_product_types'
GO
CREATE PROCEDURE [sp_select_all_product_types]
AS
BEGIN
	SELECT		[productTypeID], 
				[description] 
	FROM		[productType] 

END
GO
*/