DO $$
BEGIN
	IF (NOT EXISTS(SELECT 1 FROM pg_tables t WHERE t.tablename = 'CompanyType')) THEN
			CREATE TABLE "public"."CompanyType"(
				"Id" uuid not null primary key default "public".uuid_generate_v1(),
				"Name" citext not null,
				"Abrv" citext not null,
				"IsActive" bool not null default true,
				"DateCreated" timestamp not null,
				"DateUpdated" timestamp not null
			);

			CREATE UNIQUE INDEX Index_CompanyType_Abrv ON "CompanyType" ("Abrv");

			RAISE NOTICE 'The user table was created';
		ELSE
			RAISE NOTICE 'The user table already exists';

	END IF;
END
$$;

DO $$

BEGIN 
	IF (NOT EXISTS (SELECT "Abrv" FROM "CompanyType" WHERE "Abrv" = 'LLC')) THEN
	INSERT INTO "CompanyType" ("Name", "Abrv", "IsActive", "DateCreated", "DateUpdated") 
		VALUES ('LLC', 'LLC', true, now(), now());

		RAISE NOTICE 'The LLC was inserted into Company type lookup table';
	ELSE
		RAISE NOTICE 'LLC already exists in the company tyep lookup table';
	END IF;
END

$$