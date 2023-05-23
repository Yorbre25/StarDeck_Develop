IF DB_ID('StarDeck') IS NULL
   Create database StarDeck
GO
Use StarDeck


CREATE TABLE CardType (
	id INT NOT NULL IDENTITY(1,1),
    typeName VARCHAR(30) UNIQUE NOT NULL,
    PRIMARY KEY (id)
)

CREATE TABLE Race(
	id INT NOT NULL IDENTITY(1,1),
    name VARCHAR(30) UNIQUE NOT NULL,
    PRIMARY KEY (id)
)

CREATE TABLE PlanetType(
	id INT NOT NULL IDENTITY(1,1),
	typeName VARCHAR(30) UNIQUE NOT NULL,
	PRIMARY KEY (id)
)

CREATE Table Country
(
	id INT NOT NULL IDENTITY(1,1),
	countryName VARCHAR(30) UNIQUE NOT NULL,
	PRIMARY KEY(id)
)

CREATE TABLE Image(
	id INT NOT NULL IDENTITY(1,1),
	imageString VARCHAR(MAX) NOT NULL,
	PRIMARY KEY(id)
)

CREATE TABLE Card (
    id VARCHAR(15) NOT NULL,
    name VARCHAR(30) NOT NULL,
    energy INT NOT NULL,
    cost INT NOT NULL,
    typeId INT NOT NULL,
    raceId INT NOT NULL,
    activatedCard BIT NOT NULL DEFAULT 1,
    description VARCHAR(1000), 
	imageId INT ,
    PRIMARY KEY (id)
)

CREATE TABLE Player
(
	id VARCHAR(15) NOT NULL,
	email VARCHAR(30) NOT NULL,
	firstName VARCHAR(15) NOT NULL,
	lastName VARCHAR(15) NOT NULL,
	username VARCHAR(30) NOT NULL,
	pHash VARCHAR(1000) NOT NULL,
	ranking INT NOT NULL,
	xp INT NOT NULL DEFAULT 0,
	inGame BIT NOT NULL DEFAULT 0,
	activatedAccount BIT NOT NULL DEFAULT 1,
	countryId INT Not NULL,
	coins INT NOT NULL Default 0,
	avatarId INT NOT NULL,
	PRIMARY KEY(id)
)

CREATE TABLE Planet
(
	id VARCHAR(15) NOT NULL,
	name VARCHAR(30) NOT NULL,
	typeId int NOT NULL,
	activatedPlanet BIT DEFAULT 1 NOT NULL,
	description VARCHAR(1000),
	imageId INT,
	PRIMARY KEY(id)
)

CREATE TABLE Player_Card
(
	playerId VARCHAR(15) NOT NULL,
	cardId VARCHAR(15) NOT NULL,
	PRIMARY KEY(playerId, cardId)
)


CREATE TABLE Deck(
	id VARCHAR(15) NOT NULL,
	name VARCHAR(30) NOT NULL,
	playerId VARCHAR(15) NOT NULL,
	PRIMARY KEY (id)
)

CREATE TABLE Deck_Card (
    deckId VARCHAR(15) NOT NULL,
    cardId VARCHAR(15) NOT NULL,
    PRIMARY KEY (deckId, cardId)
)

CREATE TABLE Match_Player(
	id VARCHAR(15) NOT NULL,
	waiting_since Datetime NOT NULL,
	deckId VARCHAR(15) NOT NULL,
	PRIMARY KEY (id)
)

CREATE TABLE GameTable(
	gameId VARCHAR(15),
	planetId VARCHAR(15),
	playerId VARCHAR(15),
	cardId VARCHAR(15),
	PRIMARY KEY (gameId, planetId, playerId, cardId)
)

CREATE TABLE Game_Planet(
	gameId VARCHAR(15),
	planetId VARCHAR(15),
	show BIT NOT NULL DEFAULT 1,
	PRIMARY KEY (gameId, planetId)
)

CREATE TABLE Game_Player(
	playerId VARCHAR(15) NOT NULL,
	deckId VARCHAR(15) NULL,
	gameId VARCHAR(15) NOT NULL,
	PRIMARY KEY (playerId)
)

CREATE TABLE Game_Deck(
	id int IDENTITY(1,1),
	playerId VARCHAR(15) NOT NULL,
	gameId VARCHAR(15) NOT NULL,
	cardId VARCHAR(15) NOT NULL,
	PRIMARY KEY (id)
)


CREATE Table Hand(
	id int IDENTITY(1,1),
	playerId  VARCHAR(15) NOT NULL,
	gameId VARCHAR(15) NOT NULL,
	cardId VARCHAR(15) NOT NULL,
	-- PRIMARY KEY (playerId, cardId)
)

CREATE TABLE Game(
	id VARCHAR(15) NOT NULL,
	maxTurns INT NOT NULL,
	timePerTurn int NOT NULL,
	turn int NOT NULL,
	player1Id VARCHAR(15) NULL,
	player2Id VARCHAR(15) NULL,
	timeStarted DATETIME,
	PRIMARY KEY (id)
)


-- CREATE FOREIGN KEYS
ALTER TABLE Card
ADD CONSTRAINT fk_Card_Race
FOREIGN KEY (raceId)
REFERENCES Race(id);

ALTER TABLE Card
ADD CONSTRAINT fk_Card_Type
FOREIGN KEY (typeId)
REFERENCES CardType(id);

ALTER TABLE Card
ADD CONSTRAINT fk_Card_Image
FOREIGN KEY (imageId)
REFERENCES Image(id);

ALTER TABLE Player
ADD CONSTRAINT fk_Country_Player
FOREIGN KEY (countryId)
REFERENCES Country(id)

ALTER TABLE Planet
ADD CONSTRAINT fk_PlanetType_Planet
FOREIGN KEY (typeId)
REFERENCES PlanetType(id)

ALTER TABLE Planet
ADD CONSTRAINT fk_Planet_Image
FOREIGN KEY (imageId)
REFERENCES Image(id)

ALTER TABLE Player
ADD CONSTRAINT fk_Player_Image
FOREIGN KEY (avatarId)
REFERENCES Image(id)

ALTER TABLE Player_Card
ADD CONSTRAINT fk_Card_Player
FOREIGN KEY (cardId)
REFERENCES Card(id)

ALTER TABLE Player_Card
ADD CONSTRAINT fk_Player_Card
FOREIGN KEY (playerId)
REFERENCES Player(id)

ALTER TABLE Deck
ADD CONSTRAINT fk_Deck_Player
FOREIGN KEY (playerId)
REFERENCES Player(id)

ALTER TABLE Deck_Card
ADD CONSTRAINT fk_Deck_Card
FOREIGN KEY (cardId)
REFERENCES Card(id);

ALTER TABLE Deck_Card
ADD CONSTRAINT fk_Card_Deck
FOREIGN KEY (deckId)
REFERENCES Deck(id);

ALTER TABLE Match_Player
ADD CONSTRAINT fk_Player_Match
FOREIGN KEY (id)
REFERENCES Player(id);

ALTER TABLE Match_Player
ADD CONSTRAINT fk_Deck_Match
FOREIGN KEY (deckId)
REFERENCES Deck(id);

ALTER TABLE Game
ADD CONSTRAINT fk_Game_Game_Player1
FOREIGN KEY (player1Id)
REFERENCES Game_Player(playerId);

ALTER TABLE Game
ADD CONSTRAINT fk_Game_Game_Player2
FOREIGN KEY (player2Id)
REFERENCES Game_Player(playerId);

ALTER TABLE Game_Player
ADD CONSTRAINT fk_Game_PlayerDeck
FOREIGN KEY (DeckId)
REFERENCES Deck(id);

ALTER TABLE Hand
ADD CONSTRAINT fk_Hand_Player
FOREIGN KEY (playerId)
REFERENCES Player(id)

ALTER TABLE Hand
ADD CONSTRAINT fk_Hand_Card
FOREIGN KEY (cardId)
REFERENCES Card(id)

-- ALTER TABLE Hand
-- ADD CONSTRAINT fk_Hand_Game
-- FOREIGN KEY (gameId)

ALTER TABLE Game_Deck
ADD CONSTRAINT fk_Game_Deck_Player
FOREIGN KEY (playerId)
REFERENCES Player(id)

ALTER TABLE Game_Deck
ADD CONSTRAINT fk_Game_Deck_Card
FOREIGN KEY (cardId)
REFERENCES Card(id)


-- ALTER TABLE Game_Deck
-- ADD CONSTRAINT fk_Game_Deck_Game
-- FOREIGN KEY (gameId)
-- REFERENCES Game(id)

-- ALTER TABLE Game_Planet
-- ADD CONSTRAINT fk_Game_Planet_Game
-- FOREIGN KEY (gameId)
-- REFERENCES Game(id)

ALTER TABLE Game_Planet
ADD CONSTRAINT fk_Game_Planet_Planet
FOREIGN KEY (planetId)
REFERENCES Planet(id)



