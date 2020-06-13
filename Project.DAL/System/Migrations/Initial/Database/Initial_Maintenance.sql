do $$
DECLARE
  _db TEXT := @databaseName ;
  _user TEXT := @userName ;
  _password TEXT := @password ;
  _host TEXT := @host ;
BEGIN
  CREATE EXTENSION IF NOT EXISTS dblink; -- enable extension
  IF EXISTS (SELECT 1 FROM pg_database WHERE datname = _db) THEN
    RAISE NOTICE 'Database already exists';
  ELSE
    PERFORM dblink_connect('host=' || _host || ' user=' || _user || ' password=' || _password || ' dbname=' || current_database());
    PERFORM dblink_exec('CREATE DATABASE ' || _db);
  END IF;
end $$
