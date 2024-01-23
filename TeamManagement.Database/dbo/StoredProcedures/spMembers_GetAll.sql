CREATE PROCEDURE [dbo].[spMembers_GetAll]
    @TagNames NVARCHAR(MAX) = NULL
AS
BEGIN
	SELECT
		m.id as MemberId,
		m.name as MemberName,
		m.salary_per_year as SalaryPerYear,
		m.type as Type,
		m.contract_duration_months as ContractDurationMonths,
		m.employee_role as EmployeeRole,
		m.country_name as CountryName,
		m.currency as Currency,
		t.id as TagId,
		t.name as TagName
	FROM dbo.members m
	LEFT JOIN member_tags mt ON m.id = mt.member_id
	LEFT JOIN tags t ON mt.tag_id = t.id
	WHERE
		m.id IN (
            SELECT mt.member_id
            FROM dbo.member_tags mt
            LEFT JOIN tags t ON mt.tag_id = t.id
			WHERE
				(@TagNames IS NULL OR 
					EXISTS (
						SELECT 1
						FROM STRING_SPLIT(REPLACE(@TagNames, ' ', ''), ',') tagList
						WHERE LOWER(REPLACE(tagList.value, ' ', '')) = LOWER(REPLACE(t.name, ' ', ''))
					)
				)
        );
END
