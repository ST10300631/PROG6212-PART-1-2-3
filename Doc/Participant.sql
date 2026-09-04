CREATE TABLE Participant(
  participant_id  integer IDENTITY(1,1) primary key not null,
  first_name varchar(50) not null,
  Last_name varchar(50) not null,
  email varchar(225) unique,
  phone_number integer not null,
  date_of_birth date not null,
  club_name varchar(50) null,
  role varchar(50) null)
