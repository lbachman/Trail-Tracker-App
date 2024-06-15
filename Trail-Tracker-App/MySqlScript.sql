
CREATE SCHEMA IF NOT EXISTS mountaintrails;


USE mountaintrails;


CREATE TABLE IF NOT EXISTS MountainRanges (
    RangeID INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(255) NOT NULL,
    Location VARCHAR(255),
    Description TEXT
);


CREATE TABLE IF NOT EXISTS Mountains (
    MountainID INT PRIMARY KEY AUTO_INCREMENT,
    RangeID INT,
    Name VARCHAR(255) NOT NULL,
    Location VARCHAR(255),
    Height INT,
    Description TEXT,
    FOREIGN KEY (RangeID) REFERENCES MountainRanges(RangeID)
);


CREATE TABLE IF NOT EXISTS Trails (
    TrailID INT PRIMARY KEY AUTO_INCREMENT,
    MountainID INT,
    Name VARCHAR(255) NOT NULL,
    Difficulty VARCHAR(50),
    Length DECIMAL(5, 2),
    Description TEXT,
    FOREIGN KEY (MountainID) REFERENCES Mountains(MountainID)
);


CREATE TABLE IF NOT EXISTS Pictures (
    PictureID INT PRIMARY KEY AUTO_INCREMENT,
    TrailID INT,
    FilePath VARCHAR(255) NOT NULL,
    UploadedBy VARCHAR(450),
    UploadDate DATETIME,
    FOREIGN KEY (TrailID) REFERENCES Trails(TrailID)
);


CREATE TABLE IF NOT EXISTS Videos (
    VideoID INT PRIMARY KEY AUTO_INCREMENT,
    TrailID INT,
    FilePath VARCHAR(255) NOT NULL,
    UploadedBy VARCHAR(450),
    UploadDate DATETIME,
    FOREIGN KEY (TrailID) REFERENCES Trails(TrailID)
);


CREATE TABLE IF NOT EXISTS Comments (
    CommentID INT PRIMARY KEY AUTO_INCREMENT,
    UserID VARCHAR(255),  -- UserID from ASP.NET Identity
    TrailID INT,
    CommentText TEXT,
    CommentDate DATETIME,
    FOREIGN KEY (TrailID) REFERENCES Trails(TrailID),
    FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id)
);


CREATE TABLE IF NOT EXISTS Reviews (
    ReviewID INT PRIMARY KEY AUTO_INCREMENT,
    UserID VARCHAR(255),  -- UserID from ASP.NET Identity
    TrailID INT,
    Rating INT CHECK (Rating >= 1 AND Rating <= 5),
    ReviewText TEXT,
    ReviewDate DATETIME,
    FOREIGN KEY (TrailID) REFERENCES Trails(TrailID),
    FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id)
);


CREATE TABLE IF NOT EXISTS UserProfiles (
    UserProfileID INT PRIMARY KEY AUTO_INCREMENT,
    UserID VARCHAR(255) UNIQUE,  --  UserID from ASP.NET Identity
    Bio TEXT,
    ProfilePicture VARCHAR(255),
    FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id)
);


CREATE TABLE IF NOT EXISTS UserFavorites (
    FavoriteID INT PRIMARY KEY AUTO_INCREMENT,
    UserID VARCHAR(255),  --  UserID from ASP.NET Identity
    TrailID INT,
    FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id),
    FOREIGN KEY (TrailID) REFERENCES Trails(TrailID)
);

-- Sample data for MountainRanges table
INSERT INTO mountaintrails.MountainRanges (Name, Location, Description) VALUES
('Himalaya', 'Asia', 'The highest mountain range in the world, home to Mount Everest.'),
('Eastern Rift Mountains', 'Africa', 'Mountain range in East Africa, includes Mount Kilimanjaro.'),
('Japanese Alps', 'Japan', 'Mountain range on Honshu island, includes Mount Fuji.');

-- Sample data for Mountains table
INSERT INTO mountaintrails.Mountains (RangeID, Name, Location, Height, Description) VALUES
(1, 'Mount Everest', 'Nepal/Tibet', 8848, 'The highest mountain in the world, part of the Himalaya range.'),
(2, 'Mount Kilimanjaro', 'Tanzania', 5895, 'The highest mountain in Africa, known for its shrinking glaciers.'),
(3, 'Mount Fuji', 'Japan', 3776, 'An active stratovolcano and a prominent cultural symbol of Japan.');

-- Sample data for Trails table
INSERT INTO mountaintrails.Trails (MountainID, Name, Difficulty, Length, Description) VALUES
(1, 'South Col Route', 'Extreme', 20.0, 'The most commonly used route to reach the summit of Mount Everest.'),
(1, 'North Ridge Route', 'Extreme', 22.0, 'A challenging and less frequently used route to the summit of Everest.'),
(2, 'Marangu Route', 'Moderate', 72.0, 'The oldest and most popular route on Mount Kilimanjaro.'),
(2, 'Machame Route', 'High', 62.0, 'Known as the "Whiskey Route," this is a more scenic and difficult path on Kilimanjaro.'),
(3, 'Yoshida Trail', 'Moderate', 18.0, 'The most popular route to the summit of Mount Fuji, used by most climbers.'),
(3, 'Subashiri Trail', 'Moderate', 17.0, 'Runs parallel to the Yoshida Trail but merges with it before reaching the summit of Fuji.');

-- Sample data for Pictures table
INSERT INTO mountaintrails.Pictures (TrailID, FilePath, UploadedBy, UploadDate) VALUES
(1, '/uploads/everest_south_col_view.jpg', 'JohnDoe', '2023-05-01 14:30:00'),
(1, '/uploads/everest_south_col_camp.jpg', 'JaneDoe', '2023-05-02 15:45:00'),
(2, '/uploads/everest_north_ridge.jpg', 'AliceSmith', '2023-05-03 16:00:00'),
(4, '/uploads/kilimanjaro_machame_camp.jpg', 'BobJones', '2023-05-04 17:15:00'),
(5, '/uploads/fuji_yoshida_sunrise.jpg', 'CarolWhite', '2023-05-05 05:20:00'),
(6, '/uploads/fuji_subashiri_trail.jpg', 'DaveBrown', '2023-05-06 06:30:00');

-- Sample data for Videos table
INSERT INTO mountaintrails.Videos (TrailID, FilePath, UploadedBy, UploadDate) VALUES
(1, '/videos/everest_south_col_route.mp4', 'JohnDoe', '2023-05-01 14:30:00'),
(1, '/videos/everest_south_col_camp.mp4', 'JaneDoe', '2023-05-02 15:45:00'),
(2, '/videos/everest_north_ridge.mp4', 'AliceSmith', '2023-05-03 16:00:00'),
(4, '/videos/kilimanjaro_machame_camp.mp4', 'BobJones', '2023-05-04 17:15:00'),
(5, '/videos/fuji_yoshida_sunrise.mp4', 'CarolWhite', '2023-05-05 05:20:00'),
(6, '/videos/fuji_subashiri_trail.mp4', 'DaveBrown', '2023-05-06 06:30:00');

-- -- Sample data for Comments table
-- INSERT INTO mountaintrails.Comments (UserID, TrailID, CommentText, CommentDate) VALUES
-- ('1', 1, 'Amazing view from the top!', '2023-05-01 14:30:00'),
-- ('2', 2, 'Challenging but worth it.', '2023-05-02 15:45:00'),
-- ('3', 4, 'Great experience on this trail.', '2023-05-03 16:00:00');

-- -- Sample data for Reviews table
-- INSERT INTO mountaintrails.Reviews (UserID, TrailID, Rating, ReviewText, ReviewDate) VALUES
-- ('1', 1, 5, 'Incredible route, highly recommend!', '2023-05-01 14:30:00'),
-- ('2', 2, 4, 'Difficult but rewarding.', '2023-05-02 15:45:00'),
-- ('3', 4, 5, 'Fantastic trail, well maintained.', '2023-05-03 16:00:00');

-- -- Sample data for UserProfiles table
-- INSERT INTO mountaintrails.UserProfiles (UserID, Bio, ProfilePicture) VALUES
-- ('1', 'Avid hiker and mountain climber.', '/uploads/profile1.jpg'),
-- ('2', 'Nature enthusiast and photographer.', '/uploads/profile2.jpg'),
-- ('3', 'Mountain guide and adventurer.', '/uploads/profile3.jpg');

-- -- Sample data for UserFavorites table
-- INSERT INTO mountaintrails.UserFavorites (UserID, TrailID) VALUES
-- ('1', 1),
-- ('1', 3),
-- ('2', 2),
-- ('3', 4);
