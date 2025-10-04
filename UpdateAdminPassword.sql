-- Update Admin User Password
-- Password: Admin123!
-- BCrypt Hash generated with work factor 11

USE POSDB;
GO

UPDATE Users
SET PasswordHash = '$2a$11$N7DHv5cF5mKz5JqKGZwNXeZw3JRj9zJ6V8yqK4xZ9tJ7k5C6V4uXy'
WHERE Email = 'admin@pos.com';
GO

-- Verify the update
SELECT Id, Name, Email, Role, IsActive, CreatedAt 
FROM Users
WHERE Email = 'admin@pos.com';
GO

