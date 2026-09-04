CREATE TABLE Participant(
  participant_id  integer IDENTITY(1,1) primary key not null,
  first_name varchar(50) not null,
  Last_name varchar(50) not null,
  email varchar(225) unique,
  phone_number integer not null,
  date_of_birth date not null,
  club_name varchar(50) null,
  role varchar(50) null)

  
INSERT INTO Participant (first_name, Last_name, email, phone_number, date_of_birth, club_name, role) VALUES
('Sipho', 'Dlamini', 'sipho.dlamini@email.com', 821234567, '1995-03-12', 'Soweto Harriers', 'Participant'),
('Sarah', 'Smith', 'sarah.smith@email.com', 719876543, '1988-07-24', 'Cape Town Striders', 'Organiser'),
('Tendai', 'Moyo', 'tendai.moyo@email.com', 635551234, '2001-11-05', NULL, 'Participant'),
('Jessica', 'Botha', 'jess.botha@email.com', 844449876, '1992-01-30', 'Durban Athletic Club', 'Participant'),
('David', 'Ndlovu', 'd.ndlovu@email.com', 761112222, '1985-09-18', NULL, 'Organiser');
