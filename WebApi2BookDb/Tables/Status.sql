﻿CREATE TABLE dbo.Status (
	StatusId	BIGINT	Identity(1,1) NOT NULL,
	Name		NVARCHAR(100)	NOT NULL,
	Ordinal		INT NOT NULL,
	ts			ROWVERSION	NOT NULL,
	PRIMARY KEY CLUSTERED (StatusId ASC)
);