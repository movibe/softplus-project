USE [Softplus_Testing]
GO

-- =============================================
-- Author:		<Rasel Ahmmed>
-- Create date: <2013-07-08>
-- Description:	<Create User Type for Details with param value type>
-- =============================================
CREATE TYPE dbo.tbl_DetailInfoType2 AS TABLE 
( 
	CustomerID int NULL,
	ItemSizeID int NOT NULL,
	Quantity real NULL 
) 
GO
