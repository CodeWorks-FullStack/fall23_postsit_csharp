CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS albums(
  -- always make sure id is first column in table
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title CHAR(50) NOT NULL,
  category ENUM("misc", "dogs", "cats", "gachamon", "animals", "games") NOT NULL,
  archived BOOLEAN NOT NULL DEFAULT false,
  coverImg VARCHAR(1000) NOT NULL,
  creatorId VARCHAR(255) NOT NULL, -- data type should match the type for our accounts table id column
  FOREIGN KEY (creatorId) REFERENCES accounts(id) -- Sets up a foreign key constraint for our creatorId. When you try to create an album, it will make sure the creatorId we are using matches one of the ids in our account table
)default charset utf8 COMMENT '';

DROP TABLE albums;

INSERT INTO 
albums(title, category, coverImg, creatorId)
VALUES("doggy dogs", "dogs", "https://images.unsplash.com/photo-1530281700549-e82e7bf110d6?q=80&w=2188&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "65677a22619a399a33aeef33");

SELECT 
alb.*,
acc.* 
FROM albums alb -- alb is an alias for album
JOIN accounts acc ON alb.creatorId = acc.id -- only join account data when our on statement is true
WHERE alb.id = 1
;