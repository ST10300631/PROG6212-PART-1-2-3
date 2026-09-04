CREATE TABLE Event(
  event_id integer primary key IDENTITY(1,1) not null,
  organiser_id integer ,
  title VARCHAR (100) not null,
  location VARCHAR(50) not null,
  start_date DATE not null,
  end_date DATE not null,
  description TEXT not null,
FOREIGN KEY(organiser_id) REFERENCES Organiser(organiser_id));
