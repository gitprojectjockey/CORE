DECLARE @locationId int = 2;

SELECT 
	LocationId 
	,SUM(COST * NumberOfCopies) AS COSTPERLOCATION 
FROM [LibraryManagementSystem].[dbo].[LibraryAssets]
INNER JOIN LibraryBranches ON LibraryBranches.Id = LibraryAssets.LocationId
where locationid = @locationId
Group By LocationId
