CREATE TABLE "ATIOL"."Roles" ( 
  "Id" NVARCHAR2(128) NOT NULL,
  "Name" NVARCHAR2(256) NOT NULL,
  PRIMARY KEY ("Id")
);


CREATE TABLE "ATIOL"."UserRoles" ( 
  "UserId" NVARCHAR2(128) NOT NULL,
  "RoleId" NVARCHAR2(128) NOT NULL,
  PRIMARY KEY ("UserId", "RoleId")
);


CREATE TABLE "ATIOL"."Users" ( 
  "Id" NVARCHAR2(128) NOT NULL,
  "Email" NVARCHAR2(256) NULL,
  "EmailConfirmed" NUMBER(1) NOT NULL,
  "PasswordHash" NVARCHAR2(256) NULL,
  "SecurityStamp" NVARCHAR2(256) NULL,
  "PhoneNumber" NVARCHAR2(256) NULL,
  "PhoneNumberConfirmed" NUMBER(1) NOT NULL,
  "TwoFactorEnabled" NUMBER(1) NOT NULL,
  "LockoutEndDateUtc" TIMESTAMP(7) NULL,
  "LockoutEnabled" NUMBER(1) NOT NULL,
  "AccessFailedCount" NUMBER(10) NOT NULL,
  "UserName" NVARCHAR2(256) NOT NULL,
  PRIMARY KEY ("Id")
);


CREATE TABLE "ATIOL"."UserClaims" ( 
  "Id" NUMBER(10) NOT NULL,
  "UserId" NVARCHAR2(128) NOT NULL,
  "ClaimType" NVARCHAR2(256) NULL,
  "ClaimValue" NVARCHAR2(256) NULL,
  PRIMARY KEY ("Id")
);


CREATE SEQUENCE "ATIOL"."UserClaims_SEQ";


CREATE OR REPLACE TRIGGER "ATIOL"."UserClaims_INS_TRG"
  BEFORE INSERT ON "ATIOL"."UserClaims"
  FOR EACH ROW
BEGIN
  SELECT "ATIOL"."UserClaims_SEQ".NEXTVAL INTO :NEW."Id" FROM DUAL;
END;


CREATE TABLE "ATIOL"."UserLogins" ( 
  "LoginProvider" NVARCHAR2(128) NOT NULL,
  "ProviderKey" NVARCHAR2(128) NOT NULL,
  "UserId" NVARCHAR2(128) NOT NULL,
  PRIMARY KEY ("LoginProvider", "ProviderKey", "UserId")
);


CREATE UNIQUE INDEX "RoleNameIndex" ON "ATIOL"."Roles" ("Name");

CREATE INDEX "IX_UserRoles_UserId" ON "ATIOL"."UserRoles" ("UserId");


CREATE INDEX "IX_UserRoles_RoleId" ON "ATIOL"."UserRoles" ("RoleId");


CREATE UNIQUE INDEX "UserNameIndex" ON "ATIOL"."Users" ("UserName");


CREATE INDEX "IX_UserClaims_UserId" ON "ATIOL"."UserClaims" ("UserId");


CREATE INDEX "IX_UserLogins_UserId" ON "ATIOL"."UserLogins" ("UserId");


ALTER TABLE "ATIOL"."UserRoles"
  ADD CONSTRAINT "FK_UserRoles_Roles" FOREIGN KEY ("RoleId") REFERENCES "ATIOL"."Roles" ("Id")
  ON DELETE CASCADE;

ALTER TABLE "ATIOL"."UserRoles"
  ADD CONSTRAINT "FK_UserRoles_Users" FOREIGN KEY ("UserId") REFERENCES "ATIOL"."Users" ("Id")
  ON DELETE CASCADE;

ALTER TABLE "ATIOL"."UserClaims"
  ADD CONSTRAINT "FK_UserClaims_Users" FOREIGN KEY ("UserId") REFERENCES "ATIOL"."Users" ("Id")
  ON DELETE CASCADE;

ALTER TABLE "ATIOL"."UserLogins"
  ADD CONSTRAINT "FK_UserLogins_Users" FOREIGN KEY ("UserId") REFERENCES "ATIOL"."Users" ("Id")
  ON DELETE CASCADE;