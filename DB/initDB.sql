-- CREATE DB
-- IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'StarDeckDB')
-- BEGIN
--   CREATE DATABASE StarDeckDB;
-- END

-- USE StarDeckDB;

-- DROP TABLE IF EXISTS
IF OBJECT_ID('Deck', 'U') IS NOT NULL
BEGIN
  DROP TABLE Deck;
END

IF OBJECT_ID('Card', 'U') IS NOT NULL
BEGIN
  DROP TABLE Card;
END

IF OBJECT_ID('Card_Type', 'U') IS NOT NULL
BEGIN
  DROP TABLE Card_Type;
END

IF OBJECT_ID('Race', 'U') IS NOT NULL
BEGIN
  DROP TABLE Race
END

-- CREATE TABLES
CREATE TABLE Card_Type (
    type_id INT IDENTITY(1,1),
    type_name VARCHAR(30) UNIQUE,
    PRIMARY KEY (type_id)
)

CREATE TABLE Race(
    race_id INT IDENTITY(1,1),
    race_name VARCHAR(30) UNIQUE,
    PRIMARY KEY (race_id)
)

Create Table Card (
    id VARCHAR(15) NOT NULL,
    name VARCHAR(30) NOT NULL,
    energy INT NOT NULL,
    cost INT NOT NULL,
    c_image VARCHAR(2500),
    card_type_id INT NOT NULL,
    card_race_id INT NOT NULL,
    activated_card BIT NOT NULL DEFAULT 1,
    description VARCHAR(1000), 
    PRIMARY KEY (id)
)

CREATE TABLE Deck
(
	deck_id VARCHAR(15) NOT NULL,
	name VARCHAR(15) NOT NULL,
	player_id VARCHAR(15) NOT NULL,
	PRIMARY KEY (deck_id)
)

CREATE TABLE Deck_Card (
    deck_id VARCHAR(15) NOT NULL,
    card_id VARCHAR(15) NOT NULL,
    PRIMARY KEY (deck_id, card_id)
)

CREATE TABLE Player
(
	id VARCHAR(15) NOT NULL,
	f_name VARCHAR(50) NOT NULL,
	p_hash VARCHAR(1000) NOT NULL,
	lvl INT NOT NULL,
	country VARCHAR(15) Not NULL,
	PRIMARY KEY(id)
)

CREATE TABLE Player_Card
(
	player_id VARCHAR(15) NOT NULL,
	card_id VARCHAR(15) NOT NULL,
	PRIMARY KEY(player_id, card_id)
)

CREATE Table Country
(
	id VARCHAR(15) NOT NULL,
	c_name VARCHAR(15) NOT NULL,
	PRIMARY KEY(id)
)

-- CREATE FOREIGN KEYS
ALTER TABLE Card
ADD CONSTRAINT fk_Card_Race
FOREIGN KEY (card_race_id)
REFERENCES Race(race_id);

ALTER TABLE Card
ADD CONSTRAINT fk_Card_Type
FOREIGN KEY (card_type_id)
REFERENCES Card_Type(type_id);

ALTER TABLE Deck_Card
ADD CONSTRAINT fk_Deck_Card
FOREIGN KEY (card_id)
REFERENCES Card(id);

ALTER TABLE Deck_Card
ADD CONSTRAINT fk_Card_Deck
FOREIGN KEY (deck_id)
REFERENCES Deck(deck_id);

ALTER TABLE Deck
ADD CONSTRAINT fk_Deck_Player
FOREIGN KEY (player_id)
REFERENCES Player(id)

ALTER TABLE Player
ADD CONSTRAINT fk_Country_Player
FOREIGN KEY (country)
REFERENCES Country(id)

ALTER TABLE Player_Card
ADD CONSTRAINT fk_Card_Player
FOREIGN KEY (card_id)
REFERENCES Card(id)

ALTER TABLE Player_Card
ADD CONSTRAINT fk_Player_Card
FOREIGN KEY (player_id)
REFERENCES Player(id)