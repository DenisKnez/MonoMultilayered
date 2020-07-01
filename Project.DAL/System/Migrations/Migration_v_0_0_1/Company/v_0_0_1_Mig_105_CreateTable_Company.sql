DO $$
BEGIN
	IF (NOT EXISTS(SELECT 1 FROM pg_tables t WHERE t.tablename = 'Company')) THEN
			CREATE TABLE "public"."Company"(
				"Id" uuid not null primary key default "public".uuid_generate_v1(),
				"Name" citext not null,
				"Address" citext null,
				"Email" citext null,
				"Phone" citext null,
				"DateFounded" timestamp not null,
				"CompanyTypeId" uuid not null,


				"IsActive" bool not null default true,
				"DateCreated" timestamp not null,
				"DateUpdated" timestamp not null,



				CONSTRAINT FK_Company_CompanyType FOREIGN KEY ("CompanyTypeId") REFERENCES "CompanyType" ("Id")
			);

			RAISE NOTICE 'The company table was created';
		ELSE
			RAISE NOTICE 'The company table already exists';

	END IF;

END $$
