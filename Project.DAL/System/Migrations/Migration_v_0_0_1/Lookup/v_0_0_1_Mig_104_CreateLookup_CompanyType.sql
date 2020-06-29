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

	IF (NOT EXISTS (SELECT "Abrv" FROM "CompanyType" WHERE "Abrv" = 'LP')) THEN
	INSERT INTO "CompanyType" ("Name", "Abrv", "IsActive", "DateCreated", "DateUpdated") 
		VALUES ('LP', 'LP', true, now(), now());

		RAISE NOTICE 'The LP was inserted into Company type lookup table';
	ELSE
		RAISE NOTICE 'LP already exists in the company tyep lookup table';
	END IF;

	IF (NOT EXISTS (SELECT "Abrv" FROM "CompanyType" WHERE "Abrv" = 'LLLP')) THEN
	INSERT INTO "CompanyType" ("Name", "Abrv", "IsActive", "DateCreated", "DateUpdated") 
		VALUES ('LLLP', 'LLLP', true, now(), now());

		RAISE NOTICE 'The LLLP was inserted into Company type lookup table';
	ELSE
		RAISE NOTICE 'LLLP already exists in the company tyep lookup table';
	END IF;

	IF (NOT EXISTS (SELECT "Abrv" FROM "CompanyType" WHERE "Abrv" = 'PLLC')) THEN
	INSERT INTO "CompanyType" ("Name", "Abrv", "IsActive", "DateCreated", "DateUpdated") 
		VALUES ('PLLC', 'PLLC', true, now(), now());

		RAISE NOTICE 'The PLLC was inserted into Company type lookup table';
	ELSE
		RAISE NOTICE 'PLLC already exists in the company tyep lookup table';
	END IF;

	IF (NOT EXISTS (SELECT "Abrv" FROM "CompanyType" WHERE "Abrv" = 'Corp')) THEN
	INSERT INTO "CompanyType" ("Name", "Abrv", "IsActive", "DateCreated", "DateUpdated") 
		VALUES ('Corp', 'Corp', true, now(), now());

		RAISE NOTICE 'The Corp was inserted into Company type lookup table';
	ELSE
		RAISE NOTICE 'Corp already exists in the company tyep lookup table';
	END IF;

	IF (NOT EXISTS (SELECT "Abrv" FROM "CompanyType" WHERE "Abrv" = 'Inc')) THEN
	INSERT INTO "CompanyType" ("Name", "Abrv", "IsActive", "DateCreated", "DateUpdated") 
		VALUES ('Inc', 'Inc', true, now(), now());

		RAISE NOTICE 'The Inc was inserted into Company type lookup table';
	ELSE
		RAISE NOTICE 'Inc already exists in the company tyep lookup table';
	END IF;
END

$$