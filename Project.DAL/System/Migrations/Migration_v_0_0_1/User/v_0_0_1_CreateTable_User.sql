DO $$
BEGIN
	IF (NOT EXISTS(SELECT 1 FROM pg_tables t WHERE t.tablename = 'User')) THEN
			CREATE TABLE "public"."User"(
				"Id" uuid not null primary key default "public".uuid_generate_v1(),
				"Name" text not null
			);

			RAISE NOTICE 'The user table was created';
		ELSE
			RAISE NOTICE 'The user table already exists';

	END IF;

END $$




