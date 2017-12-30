use [LibraryManagementSystem]
Go
ALTER PROCEDURE dbo.GetPatronFeesOwed
    @patron_fee_cursor CURSOR VARYING OUTPUT
AS
    SET @patron_fee_cursor = CURSOR FORWARD_ONLY STATIC FOR 
       SELECT [Patrons].[Id]
      ,[FirstName]
	  ,[LastName]
      ,[Gender]
	  ,[Address]
      ,[Telephone]
      ,[LibraryCardId]
	  ,[Fees]
  FROM [LibraryManagementSystem].[dbo].[Patrons]
  inner join LibraryCards on LibraryCards.Id = Patrons.LibraryCardId
  ORDER BY Fees Desc;
    OPEN @patron_fee_cursor 
	GO
