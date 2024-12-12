-- Create table for user information (since there's only one user)
CREATE TABLE User (
    user_id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL, -- User's full name
    weight REAL NOT NULL, -- User's weight (in kilograms)
    height REAL, -- Optional: User's height (in centimeters)
    age INTEGER, -- Optional: User's age
    experience_level TEXT, -- Optional: Experience level for backpacking (e.g., Beginner, Intermediate, Expert)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create table for backpacks (still relevant for storing backpack data)
CREATE TABLE Backpacks (
    backpack_id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    weight REAL DEFAULT 0.00, -- Current weight of the backpack
    capacity_liters INTEGER NOT NULL, -- Capacity of the backpack in liters
    description TEXT, -- Optional description of the backpack
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create table for sub-bags within the backpack
CREATE TABLE SubBags (
    subbag_id INTEGER PRIMARY KEY AUTOINCREMENT,
    backpack_id INTEGER NOT NULL,
    name TEXT NOT NULL,
    description TEXT, -- Optional description of the sub-bag
    weight REAL DEFAULT 0.00, -- Current weight of the sub-bag
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (backpack_id) REFERENCES Backpacks(backpack_id) ON DELETE CASCADE
);

-- Create table for items
CREATE TABLE Items (
    item_id INTEGER PRIMARY KEY AUTOINCREMENT,
    subbag_id INTEGER, -- Nullable, as an item may belong directly to the backpack
    backpack_id INTEGER NOT NULL, -- The backpack to which the item ultimately belongs
    name TEXT NOT NULL,
    description TEXT, -- Optional description of the item
    weight REAL NOT NULL, -- Weight of the item in kilograms
    picture_url TEXT, -- Optional URL to a picture of the item
    quantity INTEGER DEFAULT 1, -- Number of such items
    in_bag BOOLEAN DEFAULT 1, -- Tracks if the item is currently in the bag
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (subbag_id) REFERENCES SubBags(subbag_id) ON DELETE SET NULL,
    FOREIGN KEY (backpack_id) REFERENCES Backpacks(backpack_id) ON DELETE CASCADE
);

-- Create table for backpack configurations
CREATE TABLE BackpackConfigurations (
    config_id INTEGER PRIMARY KEY AUTOINCREMENT,
    backpack_id INTEGER NOT NULL,
    name TEXT NOT NULL, -- Name for the configuration
    description TEXT, -- Optional description of the configuration
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (backpack_id) REFERENCES Backpacks(backpack_id) ON DELETE CASCADE
);

-- Create table for items within a specific configuration
CREATE TABLE ConfigurationItems (
    config_item_id INTEGER PRIMARY KEY AUTOINCREMENT,
    config_id INTEGER NOT NULL,
    item_id INTEGER NOT NULL,
    in_bag BOOLEAN DEFAULT 1,
    quantity INTEGER DEFAULT 1, -- Quantity of the item in this configuration
    FOREIGN KEY (config_id) REFERENCES BackpackConfigurations(config_id) ON DELETE CASCADE,
    FOREIGN KEY (item_id) REFERENCES Items(item_id) ON DELETE CASCADE
);

-- Example data insertion for the user table
INSERT INTO User (name, weight, height, age, experience_level)
VALUES 
    ('John Doe', 75.5, 180, 30, 'Intermediate');

-- Insert a backpack
INSERT INTO Backpacks (name, weight, capacity_liters, description)
VALUES ('Hiking Backpack', 0.00, 50, 'A lightweight, durable backpack for long hikes.');

-- Insert a sub-bag
INSERT INTO SubBags (backpack_id, name, description, weight)
VALUES (1, 'Electronics Pouch', 'A small pouch for electronics and accessories.', 0.00);

-- Insert items
INSERT INTO Items (subbag_id, backpack_id, name, description, weight, picture_url, quantity, in_bag)
VALUES
    (1, 1, 'Power Bank', 'A 10,000mAh portable charger.', 0.25, 'http://example.com/powerbank.jpg', 1, 1),
    (1, 1, 'Charging Cable', 'A USB-C charging cable.', 0.05, 'http://example.com/chargingcable.jpg', 2, 1),
    (NULL, 1, 'Sleeping Bag', 'A compact sleeping bag suitable for 3 seasons.', 1.20, 'http://example.com/sleepingbag.jpg', 1, 1);

-- Insert a configuration
INSERT INTO BackpackConfigurations (backpack_id, name, description)
VALUES (1, 'Weekend Trip', 'Configuration for a 2-day hiking trip.');

-- Link items to the configuration
INSERT INTO ConfigurationItems (config_id, item_id, in_bag, quantity)
VALUES
    (1, 1, 1, 1),
    (1, 2, 1, 2),
    (1, 3, 1, 1);
