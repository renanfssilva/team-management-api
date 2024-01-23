CREATE PROCEDURE [dbo].[spMembers_Update]
    @MemberId INT,
    @Name NVARCHAR(150),
    @SalaryPerYear DECIMAL(18, 2),
    @Type NVARCHAR(10),
    @ContractDurationMonths INT,
    @EmployeeRole NVARCHAR(50),
    @CountryName NVARCHAR(100),
    @Currency NVARCHAR(3),
    @TagNames NVARCHAR(MAX)
AS
BEGIN
    BEGIN TRANSACTION;
        DELETE FROM dbo.member_tags
        WHERE member_id = @MemberId;

        UPDATE dbo.members
        SET
            name = @Name,
            salary_per_year = @SalaryPerYear,
            type = @Type,
            contract_duration_months = @ContractDurationMonths,
            employee_role = @EmployeeRole,
            country_name = @CountryName,
            currency = @Currency
        WHERE id = @MemberId;

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
