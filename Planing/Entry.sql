CREATE TABLE Entry (
    entry_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    participant_id INT NOT NULL,
    category_id INT NOT NULL,
    registration_date DATETIME DEFAULT GETDATE() NOT NULL,
    payment_status VARCHAR(20) NOT NULL DEFAULT 'Pending',
    bib_number INT NULL,
    
    -- Foreign Key Constraints
    CONSTRAINT FK_Entry_Participant FOREIGN KEY (participant_id) REFERENCES Participant(participant_id),
    CONSTRAINT FK_Entry_Category FOREIGN KEY (category_id) REFERENCES Category(category_id),
    
    -- Payment validation safeguard
    CONSTRAINT CHK_Payment_Status CHECK (payment_status IN ('Pending', 'Paid', 'Refunded', 'Failed'))
);
