DECLARE @statusId INT, @taskId INT, @userId INT;

IF NOT EXISTS (SELECT * FROM [User] WHERE Username = 'bhogg')  
	INSERT INTO [User] (Firstname, Lastname, Username) VALUES ('Boss','Hogg', 'bhogg')

IF NOT EXISTS (SELECT * FROM [User] WHERE Username = 'jbob')  
	INSERT INTO [User] (Firstname, Lastname, Username) VALUES ('Jim','Bob', 'jbob')

IF NOT EXISTS (SELECT * FROM [User] WHERE Username = 'jdoe')  
	INSERT INTO [User] (Firstname, Lastname, Username) VALUES ('John','Doe', 'jdoe')

IF NOT EXISTS (SELECT * FROM Task WHERE Subject = 'Test Task') BEGIN
	SELECT TOP 1 @statusId = StatusId FROM Status ORDER BY StatusId;
	SELECT TOP 1 @userId = UserId FROM [User] ORDER BY UserId;

	INSERT INTO Task (Subject, StartDate, StatusId, CreatedDate, CreatedUserId)
		VALUES ('Test Task', GETDATE(), @statusId, GETDATE(), @userId);

	SET @taskId = SCOPE_IDENTITY();

	INSERT TaskUser (TaskId, UserId) VALUES (@taskId, @userId);
END
