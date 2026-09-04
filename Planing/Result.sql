

CREATE TABLE Result(
  result_id  integer IDENTITY(1,1) primary key not null,
  entry_id  integer,
  finish_time TIME,
  overall_rank integer, 
  category_rank integer,
  status VARCHAR (50),
  FOREIGN KEY(entry_id) REFERENCES Entry (entry_id)
  );