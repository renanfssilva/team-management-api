IF NOT EXISTS (SELECT 1 FROM dbo.[members]) AND NOT EXISTS (SELECT 1 FROM dbo.[tags])
BEGIN
	INSERT INTO dbo.tags (name)
	VALUES
		('C#'),
		('Angular'),
		('JavaScript'),
		('SQL'),
		('Project Management');

	INSERT INTO dbo.members (name, salary_per_year, type, contract_duration_months, country_name, currency)
	VALUES
		('Contractor 1', 50000.00, 'Contractor', 12, 'Egypt', 'EGP'),
		('Contractor 2', 55000.00, 'Contractor', 6, 'Andorra', 'EUR'),
		('Contractor 3', 60000.00, 'Contractor', 9, 'United States', 'USD');

	INSERT INTO dbo.members (name, salary_per_year, type, employee_role, country_name, currency)
	VALUES
		('Employee 1', 75000.00, 'Employee', 'Software Engineer', 'Japan', 'JPY'),
		('Employee 2', 80000.00, 'Employee', 'Project Manager', 'Macau', 'MOP');

	INSERT INTO dbo.member_tags (member_id, tag_id)
	VALUES
		(1, 1),
		(1, 2),
		(2, 3),
		(3, 4),
		(4, 5);
END;
