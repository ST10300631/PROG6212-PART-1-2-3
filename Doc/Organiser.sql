CREATE TABLE Organiser (
    organiser_id INT PRIMARY KEY NOT NULL,
    name VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    contact_number VARCHAR(15) NOT NULL
);

INSERT INTO Organiser (organiser_id, name, email, contact_number) VALUES
(1, 'Alice Johnson', 'alice.j@example.com', 123456789),
(2, 'Bob Smith', 'bob.smith@example.com', 987654321),
(3, 'Charlie Brown', 'charlie.b@example.com', 555123456),
(101, 'Alice Johnson', 'alice.j@example.com', 123456789),
(102, 'Bob Smith', 'bob.smith@example.com', 987654321),
(103, 'Charlie Brown', 'charlie.b@example.com', 555123456);

