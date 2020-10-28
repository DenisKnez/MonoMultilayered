DO $$
BEGIN

	-- Date Joined set to nullable
	IF (EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'DateJoined')) THEN
		ALTER TABLE "public"."User" ALTER COLUMN "DateJoined" drop not null;

		RAISE NOTICE 'The DateJoined column was changed to null';
	ELSE
		RAISE NOTICE 'The DateJoined column does not exists';
	END IF;

	-- Add constranint for date joined and salary column
	IF (NOT EXISTS(SELECT constraint_name FROM information_schema.constraint_column_usage WHERE table_name = 'User' and constraint_name= 'Company_Has_Relevant_Information_In_User_Table')) THEN
		ALTER TABLE "public"."User"  ADD CONSTRAINT "Company_Has_Relevant_Information_In_User_Table" 
			CHECK ( 
				("User"."CompanyId" IS null) AND ("User"."Salary" IS NULL and "User"."DateJoined" IS null) 
				or
				("User"."CompanyId" IS not null) and ("User"."Salary" is null and "User"."DateJoined" is not null)
				or
				("User"."CompanyId" IS not null) and ("User"."Salary" is not null and "User"."DateJoined" is not null)
			);
		RAISE NOTICE 'Added constraint Company_Has_Relevant_Information_In_User_Table to the User table';
	ELSE
		RAISE NOTICE 'The user table already has the constraint Company_Has_Relevant_Information_In_User_Table';
	END IF;
END $$





