
CREATE DATABASE ChatDB;
USE ChatDB;

CREATE TABLE users(
	id INT IDENTITY(1,1) NOT NULL,
	username VARCHAR(32) NOT NULL,
	pass VARCHAR(32) NOT NULL,
	role VARCHAR(32) NOT NULL,
	salt INT NOT NULL,
	deleted BIT NOT NULL
                PRIMARY KEY (id)
);



--CREATE TABLE [messages](
--	id INT IDENTITY(1,1) NOT NULL,
--	dateOfSubmission DATETIME NOT NULL,
--	senderId INT NOT NULL,
--	receiverId INT NOT NULL,
--	messageData VARCHAR(250) NULL,
--	deleted BIT NOT NULL
--                PRIMARY KEY (id)
--);


CREATE TABLE [messages](
	id INT IDENTITY(1,1) NOT NULL,
	dateOfSubmission DATETIME NOT NULL,
	senderId INT NOT NULL,
	receiverId INT NOT NULL,
	messageData VARCHAR(250) NULL,
	deleted BIT NOT NULL,
	readed BIT NOT NULL
                PRIMARY KEY (id)
				CONSTRAINT fk1_messages FOREIGN KEY (senderId) REFERENCES users(id),
                CONSTRAINT fk2_messages FOREIGN KEY (receiverId) REFERENCES users (id)
);


--insert users
GO
CREATE PROCEDURE Insert_Users
    @username VARCHAR(32), 
    @pass VARCHAR(32),
	@role VARCHAR(32)
AS
BEGIN
		DECLARE @salt INT
		SET @salt = 10000*RAND()
        INSERT INTO users (username, pass, role, salt, deleted)
        VALUES(@username, CONVERT(VARCHAR(MAX), HASHBYTES('MD5', CAST(@salt AS VARCHAR(MAX)) + @pass) , 2), @role, @salt, 0)
END

GO

--delete users
CREATE PROCEDURE Delete_Users
    @username VARCHAR(32)
AS
BEGIN
		UPDATE users
		SET deleted = 1
		WHERE username=@username
END

GO

--update username
CREATE PROCEDURE Update_UserName
    @username VARCHAR(32),
	@newUsername VARCHAR(32)
AS
BEGIN
		UPDATE users
		SET username=@newUsername
		WHERE username=@username AND deleted=0
END

GO

--update users password
CREATE PROCEDURE Update_Users_By_Password
    @username VARCHAR(32),
	@newPassword VARCHAR(32)
AS
BEGIN
		DECLARE @salt INT
		UPDATE users
		SET @salt = 10000*RAND(),
		salt=@salt,
        pass = CONVERT(VARCHAR(MAX), HASHBYTES('MD5', CAST(@salt AS VARCHAR(MAX)) + @newPassword) , 2)
		WHERE username=@username AND deleted=0
END

GO


--update users role
CREATE PROCEDURE Update_Users_By_Role
    @username VARCHAR(32),
	@newRole VARCHAR(32)
AS
BEGIN
		UPDATE users
		SET role = @newRole
		WHERE username=@username AND deleted=0
END

GO
--MESSAGES
--insert message
CREATE PROCEDURE Insert_messages
    @senderId INT,
	@receiverId INT,
	@messageData VARCHAR(250)
AS
BEGIN
		INSERT INTO [messages] (dateOfSubmission, senderId, receiverId, messageData, deleted)
        VALUES(GETDATE(), @senderId, @receiverId, @messageData, 0)
END
GO

--delete message
CREATE PROCEDURE Delete_messages
    @id INT
AS
BEGIN
		UPDATE [messages]
		SET deleted = 1
		WHERE id=@id
END
GO

EXECUTE Insert_Users @username='admin', @pass='admin', @role='Super Admin';

--check password
CREATE PROCEDURE check_Password
    @username VARCHAR(32), 
    @pass VARCHAR(32),
	@user VARCHAR(32) OUT
AS
BEGIN
	SELECT @user=username FROM users
	WHERE username = @username AND pass = CONVERT(VARCHAR(MAX),HASHBYTES('MD5', CAST(salt AS VARCHAR(MAX)) + @pass) ,2)
END

DECLARE @user VARCHAR(100) 
EXECUTE check_Password  @username = 'admin', @pass = 'dmin' , @user = @user OUTPUT
SELECT @user 
