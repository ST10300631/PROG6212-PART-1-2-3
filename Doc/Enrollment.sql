-- 1. If using MSSQL, we create a Check Constraint instead of an ENUM
CREATE TABLE Enrollment (
    enrollment_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    participant_id INT NOT NULL,
    event_id INT NOT NULL,
    enrollment_date DATETIME DEFAULT GETDATE() NOT NULL,
    status VARCHAR(15) NOT NULL DEFAULT 'Active',
    
    -- Foreign Key Constraints
    CONSTRAINT FK_Enrollment_Participant FOREIGN KEY (participant_id) REFERENCES Participant(participant_id),
    CONSTRAINT FK_Enrollment_Event FOREIGN KEY (event_id) REFERENCES Event(event_id),
    
    -- Status validation replacing the ENUM
    CONSTRAINT CHK_Enrollment_Status CHECK (status IN ('Active', 'Completed', 'Cancelled'))
);
