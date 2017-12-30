CREATE PROCEDURE GetAssetValueByLocation
    @LocationId int = 1,
    @TotalAssetValue int OUTPUT 
AS
 set @TotalAssetValue = (select
	SUM(COST * NumberOfCopies) AS COSTPERLOCATION 
	FROM [LibraryManagementSystem].[dbo].[LibraryAssets]
		INNER JOIN LibraryBranches ON LibraryBranches.Id = LibraryAssets.LocationId
		where locationid = 1
		Group By locationId)

RETURN  @TotalAssetValue 