CREATE TABLE ExchangeRequests (
    RequestId NVARCHAR(50) PRIMARY KEY,
    RequestType NVARCHAR(50) NOT NULL, -- "exchange" or "lend/borrow"
    SenderBookId NVARCHAR(50) NOT NULL,
    ReceiverBookId NVARCHAR(50), -- not required for lend/borrow
    SenderUserId NVARCHAR(50) NOT NULL,
    ReceiverUserId NVARCHAR(50) NOT NULL,
    DeliveryMethod NVARCHAR(50) NOT NULL,
    ExchangeDuration INT,
    PaymentMethod NVARCHAR(50), -- Optional for exchange
    Status NVARCHAR(50) NOT NULL
);