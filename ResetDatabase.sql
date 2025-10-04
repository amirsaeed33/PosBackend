-- Clear all users to allow reseeding with correct password hashes
DELETE FROM Users;

-- Reseed identity column
DBCC CHECKIDENT ('Users', RESEED, 0);

-- The application will automatically reseed on next startup
PRINT 'Database cleared. Restart the API to reseed with correct password hashes.';

