CREATE TABLE RouteDetail(
  route_id integer IDENTITY(1,1) primary key not null,
  category_id  integer ,
  gpx_file_url VARCHAR (50),
  start_coordinates VARCHAR (50),
  elevation_gain_meters integer,
  FOREIGN KEY(category_id) REFERENCES Category(category_id))