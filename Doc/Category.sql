CREATE TABLE Category(
  category_id integer IDENTITY(1,1) primary key  not null, 
  event_id integer,
  name VARCHAR (50),
  entry_fee DECIMAL,
  distance_km DECIMAL,
  start_time TIME
  FOREIGN KEY(event_id) REFERENCES Event(event_id))
