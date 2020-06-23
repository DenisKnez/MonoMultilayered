DO $$
BEGIN
	IF (EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'FirstName')) THEN
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

	-- Last name insert empty values
	IF (EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'LastName')) THEN
		UPDATE  "public"."User" set "LastName" = '';

		RAISE NOTICE 'The LastName column was created';
	ELSE
		RAISE NOTICE 'The LastName column already exists';
	END IF;

	-- Last name change to not null
	IF (EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'LastName')) THEN
		ALTER TABLE "public"."User" ALTER COLUMN "LastName" SET not null;

		RAISE NOTICE 'The LastName column was changed to not null';
	ELSE
		RAISE NOTICE 'The LastName column does not exists';
	END IF;


	-- Email
	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'Email')) THEN
		ALTER TABLE "public"."User" ADD "Email" citext null;

		RAISE NOTICE 'The Email column was created';
	ELSE
		RAISE NOTICE 'The Email column already exists';
	END IF;

	
	-- Email insert empty values
	IF (EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'Email')) THEN
		UPDATE  "public"."User" set "Email" = '';

		RAISE NOTICE 'The Email column was inserted with placeholder values';
	ELSE
		RAISE NOTICE 'The Email column already exists';
	END IF;

	-- Email change to not null
	IF (EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'Email')) THEN
		ALTER TABLE "public"."User" ALTER COLUMN "Email" SET not null;

		RAISE NOTICE 'The Email column was changed to not null';
	ELSE
		RAISE NOTICE 'The Email column does not exists';
	END IF;


	-- Date of birth
	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'DateOfBirth')) THEN
		ALTER TABLE "public"."User" ADD "DateOfBirth" date null ;

		RAISE NOTICE 'The DateOfBirth column was created';
	ELSE
		RAISE NOTICE 'The DateOfBirth column already exists';
	END IF;

	-- Add constranint for date of birth column
	IF (NOT EXISTS(SELECT constraint_name FROM information_schema.constraint_column_usage WHERE table_name = 'User' and constraint_name= 'DateOfBirth_Range')) THEN
		ALTER TABLE "public"."User" ADD CONSTRAINT "DateOfBirth_Range" CHECK ("DateOfBirth" > '1900-01-01') ;

		RAISE NOTICE 'Added constraint to the DateOfBirth column';
	ELSE
		RAISE NOTICE 'The DateOfBirth column already has the constraint';
	END IF;

	-- Date of birth insert placeholder value
	IF (EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'DateOfBirth')) THEN
		UPDATE  "public"."User" set "DateOfBirth" = '2000-01-01';

		RAISE NOTICE 'The DateOfBirth column was inserted with placeholder values';
	ELSE
		RAISE NOTICE 'The DateOfBirth column already exists';
	END IF;

	-- Date of birth set to not null
	IF (EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'DateOfBirth')) THEN
		ALTER TABLE "public"."User" ALTER COLUMN "DateOfBirth" SET not null;

		RAISE NOTICE 'The DateOfBirth column was changed to not null';
	ELSE
		RAISE NOTICE 'The DateOfBirth column does not exists';
	END IF;



	-- Salary
	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'Salary')) THEN
		ALTER TABLE "public"."User" ADD "Salary" numeric(11, 2) null;

		RAISE NOTICE 'The Salary column was created';
	ELSE
		RAISE NOTICE 'The Salary column already exists';
	END IF;

	-- Add constranint for salary range column
	IF (NOT EXISTS(SELECT constraint_name FROM information_schema.constraint_column_usage WHERE table_name = 'User' and constraint_name= 'Salary_Range')) THEN
		ALTER TABLE "public"."User" ADD CONSTRAINT "Salary_Range" CHECK ("Salary" > 0) ;

		RAISE NOTICE 'Added constraint to the Salary column';
	ELSE
		RAISE NOTICE 'The Salary column already has the constraint';
	END IF;


	-- Date Joined
	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'DateJoined')) THEN
		ALTER TABLE "public"."User" ADD "DateJoined" date  null;

		RAISE NOTICE 'The DateJoined column was created';
	ELSE
		RAISE NOTICE 'The DateJoined column already exists';
	END IF;

	-- Add constranint for date joined column
	IF (NOT EXISTS(SELECT constraint_name FROM information_schema.constraint_column_usage WHERE table_name = 'User' and constraint_name= 'DateJoined_Range')) THEN
		ALTER TABLE "public"."User" ADD CONSTRAINT "DateJoined_Range" CHECK ("DateJoined" > '1900-01-01') ;

		RAISE NOTICE 'Added constraint to the DateJoined_Range column';
	ELSE
		RAISE NOTICE 'The DateJoined_Range column already has the constraint';
	END IF;

	-- Date Joined insert placeholder value
	IF (EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'DateJoined')) THEN
		UPDATE  "public"."User" set "DateJoined" = '2000-01-01';

		RAISE NOTICE 'The DateJoined column was inserted with placeholder values';
	ELSE
		RAISE NOTICE 'The DateJoined column already exists';
	END IF;

	-- Date Joined set to not null
	IF (EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'DateJoined')) THEN
		ALTER TABLE "public"."User" ALTER COLUMN "DateJoined" SET not null;

		RAISE NOTICE 'The DateJoined column was changed to not null';
	ELSE
		RAISE NOTICE 'The DateJoined column does not exists';
	END IF;


END $$





