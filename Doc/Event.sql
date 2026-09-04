CREATE TABLE Event(
  event_id integer primary key IDENTITY(1,1) not null,
  organiser_id integer ,
  title VARCHAR (100) not null,
  location VARCHAR(50) not null,
  start_date DATE not null,
  end_date DATE not null,
  description TEXT not null,
FOREIGN KEY(organiser_id) REFERENCES Organiser(organiser_id));

INSERT INTO Event (organiser_id, title, location, start_date, end_date, description) VALUES
(1, 'Soweto 10K Classic', 'Soweto, Johannesburg', '2026-04-12', '2026-04-12', 'An annual 10km road running event touring iconic heritage sites.'),
(2, 'Cape Town Cycle Tour Mini', 'Green Point, Cape Town', '2026-05-24', '2026-05-25', 'A weekend community cycling event for local clubs and amateur riders.'),
(3, 'Durban Night Run', 'Beachfront, Durban', '2026-06-15', '2026-06-15', 'A scenic 5km and 10km evening race along the Durban promenade.'),
(101, 'Johannesburg Autumn Trail', 'Modderfontein Reserve', '2026-07-04', '2026-07-05', 'A two-day multi-stage trail running challenge covering 15km daily.'),
(102, 'Gauteng Criterium Series', 'Kyalami Grand Prix Circuit', '2026-08-20', '2026-08-20', 'Fast-paced closed-circuit road bike racing across multiple skill tiers.');

