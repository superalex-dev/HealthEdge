CREATE TABLE doctornew  (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    username VARCHAR(50) NOT NULL,
    specialization VARCHAR(100) NOT NULL,
    contact_number VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL
);


ALTER TABLE doctornew  RENAME COLUMN id TO "Id";
ALTER TABLE doctornew  RENAME COLUMN first_name TO "FirstName";
ALTER TABLE doctornew  RENAME COLUMN last_name TO "LastName";
ALTER TABLE doctornew  RENAME COLUMN username TO "Username";
ALTER TABLE doctornew  RENAME COLUMN specialization TO "Specialization";
ALTER TABLE doctornew  RENAME COLUMN contact_number TO "ContactNumber";
ALTER TABLE doctornew  RENAME COLUMN email TO "Email";


insert into doctornew ("Id", "FirstName", "LastName", "Username", "Specialization", "ContactNumber", "Email")
select "Id" , "FirstName" , "LastName" , "Username" , "Specialization" , "ContactNumber" , "Email" from "Doctors" 

DROP TABLE "Doctors" CASCADE;

ALTER TABLE doctornew  RENAME TO "Doctors";

select *
from "Doctors" d 