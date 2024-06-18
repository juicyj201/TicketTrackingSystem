CREATE DATABASE TicketTracking; 

CREATE TABLE Users (
    EMP_ID INTEGER PRIMARY KEY, 
    Name varchar(50), 
    Emp_Type varchar(20)
); 

CREATE TABLE Tickets (
    Ticket_ID INTEGER PRIMARY KEY,
    Summary varchar(300) NOT NULL,
    Description varchar(500) NOT NULL,
    Ticket_Type varhcar(50) NOT NULL,
    Severity varchar(50),
    Priority varchar(20),
    Status varchar(20)
); 