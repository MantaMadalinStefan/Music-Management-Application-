CREATE DATABASE music;
USE music;

CREATE TABLE Albums (
    id INT PRIMARY KEY AUTO_INCREMENT,
    titlu VARCHAR(100),
    an_lansare INT
);

CREATE TABLE Musicians (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nume VARCHAR(100),
    gen_muzical VARCHAR(50)
);

CREATE TABLE Albums_Musicians (
    id_muzician INT,
    id_album INT,
    PRIMARY KEY (id_muzician, id_album),
    FOREIGN KEY (id_muzician) REFERENCES Musicians(id),
    FOREIGN KEY (id_album) REFERENCES Albums(id)
);
SHOW TABLES;
DESCRIBE Musicians;

-- Inserting a musician
INSERT INTO Musicians (nume, gen_muzical) VALUES 
('Eminem', 'Rap'),
('2Pac', 'Rap'),
('El Nino', 'Hip Hop'),
('Noua Unșpe', 'Trap'),
('Aerozen', 'Trap'),
('John Lennon', 'Rock'),
('Queen', 'Rock'),
('Michael Jackson', 'Pop'),
('Maria Tănase', 'Folk');

-- Inserting an album
INSERT INTO Albums (titlu, an_lansare) VALUES 
('The Marshall Mathers LP', 2000),
('Revival', 2017),
('All Eyez on Me', 1996),
('Me Against the World', 1995),
('Onoarea Inainte de Toate', 2015),
('Alin Durerea', 2016),
('Binecuvantat', 2017),
('Arca 11', 2023),
('PartyPackz+', 2023),
('Imagine', 1971),
('Bohemian Rhapsody', 1975),
('Thriller', 1982),
('Dangerous', 1991),
('Cântec de leagăn', 1938),
('Lume, Lume', 1940);
-- Link between musician and album
INSERT INTO Albums_Musicians (id_muzician, id_album) VALUES
(1, 1), (1, 2),   -- Eminem
(2, 3), (2, 4),   -- 2Pac
(3, 5), (3, 6), (3,7),  -- El Nino
(4, 8),  -- Noua Unșpe
(5, 9), -- Aerozen
(6, 10), -- John Lennon
(7, 11), (7, 12), -- Queen
(8,13), -- Michael Jackson
(9, 14), (9, 15); -- Maria Tănase
SELECT * FROM Albums;
SELECT * FROM Albums_Musicians;
DROP DATABASE music;