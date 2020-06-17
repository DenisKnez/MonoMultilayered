DO $$
BEGIN
	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'FirstName')) THEN
			ALTER TABLE "public"."User" RENAME COLUMN "Name" TO "FirstName";

			RAISE NOTICE 'Column Name renamed to FirstName';
		ELSE
			RAISE NOTICE 'Column FirstName already exists';
	END IF;

	-- Last name
	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'LastName')) THEN
		ALTER TABLE "public"."User" ADD "LastName" citext null;

		RAISE NOTICE 'The LastName column was created';
	ELSE
		RAISE NOTICE 'The LastName column already exists';
	END IF;


	-- Last name change to not null
	IF (EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'LastName')) THEN
		ALTER TABLE "public"."User" ALTER COLUMN "LastName" citext SET not null;

		RAISE NOTICE 'The LastName column was changed to not null';
	ELSE
		RAISE NOTICE 'The LastName column does not exists';
	END IF;

	-- Email
	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'Email')) THEN
		ALTER TABLE "public"."User" ADD "Email" citext not null;

		RAISE NOTICE 'The IsActive column was created';
	ELSE
		RAISE NOTICE 'The IsActive column already exists';
	END IF;


	-- Date of birth
	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'DateOfBirth')) THEN
		ALTER TABLE "public"."User" ADD "DateOfBirth" date CHECK (DateOfBirth > '1900-01-01') not null ;

		RAISE NOTICE 'The DateOfBirth column was created';
	ELSE
		RAISE NOTICE 'The DateOfBirth column already exists';
	END IF;


	-- Salary
	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'Salary')) THEN
		ALTER TABLE "public"."User" ADD "Salary" money CHECK (Salary > 0) null;

		RAISE NOTICE 'The Salary column was created';
	ELSE
		RAISE NOTICE 'The Salary column already exists';
	END IF;

	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'DateJoined')) THEN
		ALTER TABLE "public"."User" ADD "DateJoined" date CHECK (DateJoined > DateOfBirth) not null;

		RAISE NOTICE 'The DateJoined column was created';
	ELSE
		RAISE NOTICE 'The DateJoined column already exists';
	END IF;


END $$





