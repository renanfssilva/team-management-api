CREATE PROCEDURE [dbo].[spMembers_Insert]
    @Name NVARCHAR(150),
    @SalaryPerYear DECIMAL(18, 2),
    @Type NVARCHAR(10),
    @ContractDurationMonths INT = NULL,
    @EmployeeRole NVARCHAR(50) = NULL,
    @CountryName NVARCHAR(100),
    @Currency NVARCHAR(3),
    @TagNames NVARCHAR(MAX) = NULL
AS
BEGIN
    BEGIN TRANSACTION;
        DECLARE @MemberId INT;

        INSERT INTO dbo.members (name, salary_per_year, type, contract_duration_months, employee_role, country_name, currency)
        OUTPUT INSERTED.id
        VALUES (@Name, @SalaryPerYear, @Type, @ContractDurationMonths, @EmployeeRole, @CountryName, @Currency);

        SET @MemberId = SCOPE_IDENTITY();

        IF @TagNames IS NOT NULL
        BEGIN
            CREATE TABLE #TempTags (name NVARCHAR(MAX));
    
            INSERT INTO #TempTags (name)
            SELECT value
            FROM STRING_SPLIT(@TagNames, ',');

            INSERT INTO dbo.tags (name)
            SELECT DISTINCT tt.name
            FROM #TempTags tt
            WHERE NOT EXISTS (SELECT 1 FROM tags WHERE name = tt.name);

            INSERT INTO member_tags (member_id, tag_id)
            SELECT @MemberId, t.id
            FROM #TempTags tt
            JOIN tags t ON t.name = tt.name;

            DROP TABLE #TempTags;
        END
    COMMIT;
END;
