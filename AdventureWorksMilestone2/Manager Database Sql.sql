Use "aspnet-AdventureWorksMilestone2-20160921113114";

INSERT INTO AspNetUsers (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName) 
VALUES (1, 'manager@advworks.com', 'False', 'AHldc07kSwr65/BntVgfdiDEvQwjIkoz1CndmekNQPO74TR9Z5sN2jkVDi9wJTI1aQ==', '4f82318e-880d-45e1-930b-e5924cb8e84a', 'False', 'False', 'True', 0, 'manager@advworks.com');
INSERT INTO AspNetRoles (Id, Name) VALUES (1, 'Manager');
INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES (1, 1);
