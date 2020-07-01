DO $$
BEGIN
	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'CompanyId')) THEN
			ALTER TABLE "public"."User" ADD COLUMN "CompanyId" uuid null;

			RAISE NOTICE 'CopmanyId column has been created';
		ELSE
			RAISE NOTICE 'CopmanyId column already exists';
	END IF;

	IF (NOT EXISTS(SELECT 1 FROM information_schema.constraint_column_usage info WHERE info.table_name = 'User' and info.constraint_name = 'FK_User_Company')) THEN
			ALTER TABLE "public"."User" ADD CONSTRAINT FK_User_Company FOREIGN KEY ("CompanyId") REFERENCES "Company" ("Id");

			RAISE NOTICE 'FK_User_Company constraint was added to the user table';
		ELSE
			RAISE NOTICE 'FK_User_Company already exists on the user table';
	END IF;

END $$





