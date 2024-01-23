CREATE TABLE [dbo].[members]
(
	[id] INT IDENTITY(1,1),
    [name] NVARCHAR(150) NOT NULL,
    [salary_per_year] DECIMAL(18, 2) NOT NULL,
    [type] NVARCHAR(10) NOT NULL,
    [contract_duration_months] INT,
    [employee_role] NVARCHAR(50),
    [country_name] NVARCHAR(100) NOT NULL,
    [currency] NVARCHAR(3) NOT NULL,
    CONSTRAINT PK_members PRIMARY KEY CLUSTERED ([id]),
    CONSTRAINT CK_type CHECK ([type] IN ('Contractor', 'Employee')),
	CONSTRAINT CK_employee_role CHECK (
		([type] <> 'Employee' AND employee_role IS NULL) OR
		([type] = 'Employee' AND employee_role IS NOT NULL)
	)
)
