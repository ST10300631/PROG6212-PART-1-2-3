CREATE TABLE Weather_Cast(
  weather_id integer primary key  not null,
   category_id integer,
   temperature_celsius DECIMAL(4,1),
   wind_speed_kph DECIMAL(4,1),
   conditions_description VARCHAR (50),
   last_updated TIMESTAMP,
   FOREIGN KEY(category_id) REFERENCES Category(category_id))