Create Database ScalableServiceBookExchange
CREATE TABLE ExchangeRequests (
    RequestId NVARCHAR(50) PRIMARY KEY,
    RequestType NVARCHAR(50) NOT NULL, -- "exchange" or "lend/borrow"
    SenderBookId NVARCHAR(50) NOT NULL,
    ReceiverBookId NVARCHAR(50), -- not required for lend/borrow
    SenderUserId NVARCHAR(50) NOT NULL,
    ReceiverUserId NVARCHAR(50) NOT NULL,
    DeliveryMethod INT NOT NULL,
    ExchangeDuration INT,
    PaymentMethod INT, -- Optional for exchange
    Status INT NOT NULL
);

CREATE TABLE DeliveryMethods (
    Id INT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

INSERT INTO DeliveryMethods (Id, Name) VALUES
(0, 'InPerson'),
(1, 'Courier'),
(2, 'PostalService');

CREATE TABLE PaymentMethods (
    Id INT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

INSERT INTO PaymentMethods (Id, Name) VALUES
(0, 'Online'),
(1, 'OnDelivery');

CREATE TABLE Statuses (
    Id INT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

INSERT INTO Statuses (Id, Name) VALUES
(0, 'Pending'),
(1, 'Accepted'),
(2, 'Rejected'),
(3, 'Modified');

ALTER TABLE ExchangeRequests
ADD CONSTRAINT FK_ExchangeRequests_DeliveryMethod
FOREIGN KEY (DeliveryMethod) REFERENCES DeliveryMethods(Id);

ALTER TABLE ExchangeRequests
ADD CONSTRAINT FK_ExchangeRequests_PaymentMethod
FOREIGN KEY (PaymentMethod) REFERENCES PaymentMethods(Id);

ALTER TABLE ExchangeRequests
ADD CONSTRAINT FK_ExchangeRequests_Status
FOREIGN KEY (Status) REFERENCES Statuses(Id);
