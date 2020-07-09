DO $$
BEGIN
	IF (NOT EXISTS(SELECT 1 FROM information_schema."constraint_column_usage" info WHERE info.table_name = 'Company' and info.column_name= 'Name')) THEN
			ALTER TABLE "public"."Company" ADD CONSTRAINT UC_Company_Name UNIQUE ("Name");

			RAISE NOTICE 'Added UNIQUE constraint to the name column';
		ELSE
			RAISE NOTICE 'The UNIQUE constraint already exists on the Name column';
	END IF;

END $$
