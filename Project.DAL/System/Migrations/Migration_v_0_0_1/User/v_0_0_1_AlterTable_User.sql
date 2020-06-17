DO $$
BEGIN
	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'DateCreated')) THEN
			ALTER TABLE "public"."User" ADD "DateCreated" timestamp not null default NOW();

			RAISE NOTICE 'The DateCreated column was created';
		ELSE
			RAISE NOTICE 'The DateCreated column already exists';
	END IF;

	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'DateUpdated')) THEN
		ALTER TABLE "public"."User" ADD "DateUpdated" timestamp not null default NOW();

		RAISE NOTICE 'The DateUpdated column was created';
	ELSE
		RAISE NOTICE 'The DateUpdated  column already exists';
	END IF;

	IF (NOT EXISTS(SELECT 1 FROM information_schema."columns" info WHERE info.table_name = 'User' and info.column_name= 'IsActive')) THEN
		ALTER TABLE "public"."User" ADD "IsActive" boolean not null default true;

		RAISE NOTICE 'The IsActive column was created';
	ELSE
		RAISE NOTICE 'The IsActive column already exists';
	END IF;

END $$





