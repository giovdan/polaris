USE accounting;

DELIMITER //

CREATE OR REPLACE PROCEDURE ResetAdminPassword()
BEGIN
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
 	     ROLLBACK;
	     SHOW ERRORS;  
	 END; 

	START TRANSACTION;
	
	INSERT IGNORE INTO USER
	(Id, FirstName, LastName, UserName, Password, Culture, GroupId, UserStatusId, IsSystemUser)		
	VALUES
	(2, 'Admin', 'FICEP', 'admin', SHA1('ficep2022'), 'it-IT', 1, 1, 0);
	
	INSERT IGNORE INTO USER
	(Id, FirstName, LastName, UserName, Password, Culture, GroupId, UserStatusId, IsSystemUser)		
	VALUES
	(3, 'SuperUser', 'FICEP', 'super', SHA1('ficep2022'), 'it-IT', 2, 1, 0);
	
	INSERT IGNORE INTO USER
	(Id, FirstName, LastName, UserName, Password, Culture, GroupId, UserStatusId, IsSystemUser)		
	VALUES
	(4, 'User', 'FICEP', 'user', SHA1('ficep2022'), 'it-IT', 3, 1, 0);	
	
	UPDATE user SET Password = SHA1('ficep2022') where UserName = 'admin';
	UPDATE user SET Password = SHA1('ficep2022') where UserName = 'super';
	UPDATE user SET Password = SHA1('ficep2022') where UserName = 'user';
	
	COMMIT;

END; //

DELIMITER ;

CALL ResetAdminPassword();

