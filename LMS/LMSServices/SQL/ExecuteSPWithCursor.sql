USE [LibraryManagementSystem]
GO

DECLARE	@return_value int,
        @MyCursor CURSOR

EXEC dbo.GetPatronFeesOwed @patron_fee_cursor = @MyCursor OUTPUT;
WHILE (@@FETCH_STATUS = 0)
BEGIN;
     FETCH NEXT FROM @MyCursor;
END;
CLOSE @MyCursor;
DEALLOCATE @MyCursor;
GO
