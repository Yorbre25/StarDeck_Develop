
-- IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'StarDeckDB')
-- BEGIN
--   CREATE DATABASE StarDeckDB;
-- END

-- USE StarDeckDB;

-- DROP TABLE IF EXISTS



-- CREATE TABLES
CREATE TABLE Card_Type (
    type VARCHAR(30) UNIQUE NOT NULL,
    PRIMARY KEY (type)
)

CREATE TABLE Race(
    race VARCHAR(30) UNIQUE NOT NULL,
    PRIMARY KEY (race)
)

Create Table Card (
    id VARCHAR(15) NOT NULL,
    name VARCHAR(30) NOT NULL,
    energy INT NOT NULL,
    cost INT NOT NULL,
    type VARCHAR(30) NOT NULL,
    race VARCHAR(30) NOT NULL,
    activated_card BIT NOT NULL DEFAULT 1,
    description VARCHAR(1000), 
	image VARCHAR(MAX),
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
	email VARCHAR(30) NOT NULL,
	f_name VARCHAR(15) NOT NULL,
	l_name VARCHAR(15) NOT NULL,
	nickname VARCHAR(15) NOT NULL,
	p_hash VARCHAR(1000) NOT NULL,
	lvl INT NOT NULL,
	ranking VARCHAR(15) NOT NULL,
	in_game BIT NOT NULL,
	active BIT NOT NULL,
	country VARCHAR(15) Not NULL,
	coins INT NOT NULL,
	avatar VARCHAR(MAX),
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
	c_name VARCHAR(30) NOT NULL,
	PRIMARY KEY(id)
)

-- CREATE FOREIGN KEYS
ALTER TABLE Card
ADD CONSTRAINT fk_Card_Race
FOREIGN KEY (race)
REFERENCES Race(race);

ALTER TABLE Card
ADD CONSTRAINT fk_Card_Type
FOREIGN KEY (type)
REFERENCES Card_Type(type);

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

