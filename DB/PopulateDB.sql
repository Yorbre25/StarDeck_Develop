Use StarDeck

SET IDENTITY_INSERT dbo.CardType ON
INSERT INTO CardType (id, typeName)
VALUES  (1, 'Básica'),
        (2, 'Normal'),
        (3, 'Rara'),
        (4, 'Muy Rara'),
        (5, 'Ultra Rara');
SET IDENTITY_INSERT dbo.CardType OFF


Use StarDeck
SET IDENTITY_INSERT dbo.Race ON
INSERT INTO Race (id, name)
VALUES (1,'Humano'),
       (2, 'Trisolariano'),
       (3, 'Robot'),
       (4, 'Marciano'),
       (5, 'Ciborg');
SET IDENTITY_INSERT dbo.Race OFF

SET IDENTITY_INSERT dbo.PlanetType ON;
INSERT INTO PlanetType (id, typeName)
VALUES  (1, 'Popular'),
        (2, 'Basico'),
        (3, 'Raro');
SET IDENTITY_INSERT dbo.PlanetType OFF;


SET IDENTITY_INSERT dbo.Country ON;
INSERT INTO Country (id, countryName)
VALUES (1, 'Costa Rica'),
       (2, 'Estados Unidos'),
       (3, 'Mexico');
SET IDENTITY_INSERT dbo.Country OFF;


-- DATA FOR TESTS

INSERT INTO Card (
    id, 
    name, 
    energy, 
    cost, 
    typeId, 
    raceId, 
    description, 
    imageId)
VALUES  ('C-000000000001', 'Carta A', 1, 2, 1, 1, 'Descripción A', 1),
        ('C-000000000002', 'Carta B', 4, 4, 1, 1, 'Description B', 1),
        ('C-000000000003', 'Carta C', 7, 5, 1, 1, 'Description C', 1),
        ('C-000000000004', 'Carta D', 8, 2, 1, 1, 'Description D', 1),
        ('C-000000000005', 'Carta E', 3, 6, 1, 1, 'Description E', 1),
        ('C-000000000006', 'Carta F', 10, 7, 1, 1, 'Description F', 1),
        ('C-000000000007', 'Carta G', 15, 8, 1, 1, 'Description G', 1),
        ('C-000000000008', 'Carta H', 9, 2, 1, 1, 'Description H', 1),
        ('C-000000000009', 'Carta I', 5, 7, 1, 1, 'Description I', 1),
        ('C-000000000010', 'Carta J', 6, 1, 1, 1, 'Description J', 1),
        ('C-000000000011', 'Carta K', 1, 7, 1, 1, 'Description K', 1),
        ('C-000000000012', 'Carta M', 7, 2, 1, 1, 'Description L', 1),
        ('C-000000000013', 'Carta M2', 6, 2, 1, 1, 'Description M', 1),
        ('C-000000000014', 'Carta N', 10, 9, 1, 1, 'Description N', 1),
        ('C-000000000015', 'Carta O', 1, 3, 1, 1, 'Description O', 1),
        ('C-000000000016', 'Carta P', 1, 5, 1, 1, 'Description P', 1),
        ('C-000000000017', 'Carta Q', 6, 8, 1, 1, 'Description Q', 1),

        ('C-000000000018', 'Carta R', 10, 10, 2, 1, 'Description R', 3),
        ('C-000000000019', 'Carta S', 10, 10, 2, 1, 'Description S', 3),
        ('C-000000000020', 'Carta T', 5, 10, 2, 1, 'Description T', 3),
        ('C-000000000021', 'Carta U', 5, 10, 2, 1, 'Description U', 3),
        ('C-000000000022', 'Carta V', 1, 10, 2, 1, 'Description V', 3),
        ('C-000000000023', 'Carta 1', 1, 10, 3, 1, 'Description 1', 4),
        ('C-000000000024', 'Carta 2', 3, 10, 3, 1, 'Description 2', 4),
        ('C-000000000025', 'Carta 3', 3, 10, 3, 1, 'Description 3', 4),
        ('C-000000000026', 'Carta 4', 6, 10, 3, 1, 'Description 4', 4),
        ('C-000000000027', 'Carta 5', 1, 10, 3, 1, 'Description 5', 4),
        ('C-000000000028', 'Carta 6', 7, 10, 3, 1, 'Description 6', 4);


INSERT INTO Planet (
    id, 
    name, 
    description, 
    TypeId,  
    imageId)
VALUES 
    ('P-01a1zyys4aph','Planeta A','Descripción A',1,5),
    ('P-01a1zjlg4aph','Planeta B','Descripción B',1,6),
    ('P-02a1zjlg4aph','Planeta C','Descripción C',1,11),
    ('P-03a1zjlg4aph','Planeta D','Descripción D',2,10),
    ('P-04a1zjlg4aph','Planeta E','Descripción E',2,9);

INSERT INTO Player (
    id, 
    email,
    firstName, 
    lastName,
    username,
    pHash,
    ranking,
    xp,
    inGame,
    activatedAccount,
    countryId, 
    coins,
    avatarId)
VALUES
    -- 1111qqqq
    ('U-01a1zjlg4aph','yraulbr25@gmail.com', 'Yordi', 'Brenes', 'sadKaladin', 'c57ESO2vf02R60Q0Yu0yAHSGKTXSx5m3bG7ULsbt14w=', 0, 0, 0, 1, 1, 0, 1),
    ('U-02a1zjlg4aph','adriana@gmail.com', 'Adriana', 'Calderon', 'quadriante', 'c57ESO2vf02R60Q0Yu0yAHSGKTXSx5m3bG7ULsbt14w=', 0, 0, 0, 1, 1, 0, 1),
    ('U-03a1zjlg4aph','nasser@gmail,com', 'Nasser', 'Nasser', 'bigNass', 'c57ESO2vf02R60Q0Yu0yAHSGKTXSx5m3bG7ULsbt14w=', 0, 0, 0, 1, 1, 0, 1),
    ('U-04a1zjlg4aph','marcelo@gmail.com', 'Marcelo', 'Truque', 'Marce', 'c57ESO2vf02R60Q0Yu0yAHSGKTXSx5m3bG7ULsbt14w=', 0, 0, 0, 1, 1, 0, 1),
    ('U-05a1zjlg4aph','nuevo1@gmail.com', 'Nuevo', 'Nuevo', 'nuevo1', 'c57ESO2vf02R60Q0Yu0yAHSGKTXSx5m3bG7ULsbt14w=', 0, 0, 0, 1, 1,0, 1),
    ('U-06a1zjlg4aph','nuevo2@gmail.com', 'Nuevo', 'Nuevo', 'nuevo2', 'c57ESO2vf02R60Q0Yu0yAHSGKTXSx5m3bG7ULsbt14w=', 0, 0, 0, 1, 1, 0, 1),
    ('U-07a1zjlg4aph','nuevo3@gmail.com', 'Nuevo', 'Nuevo', 'nuevo3', 'c57ESO2vf02R60Q0Yu0yAHSGKTXSx5m3bG7ULsbt14w=', 0, 0, 0, 1, 1, 0, 1);

INSERT INTO Player_Card (
    playerId, 
    cardId)
VALUES
    ('U-01a1zjlg4aph', 'C-000000000001'),
    ('U-01a1zjlg4aph', 'C-000000000002'),
    ('U-01a1zjlg4aph', 'C-000000000003'),
    ('U-01a1zjlg4aph', 'C-000000000004'),
    ('U-01a1zjlg4aph', 'C-000000000005'),
    ('U-01a1zjlg4aph', 'C-000000000006'),
    ('U-01a1zjlg4aph', 'C-000000000007'),
    ('U-01a1zjlg4aph', 'C-000000000008'),
    ('U-01a1zjlg4aph', 'C-000000000009'),
    ('U-01a1zjlg4aph', 'C-000000000010'),
    ('U-01a1zjlg4aph', 'C-000000000011'),
    ('U-01a1zjlg4aph', 'C-000000000012'),
    ('U-01a1zjlg4aph', 'C-000000000013'),
    ('U-01a1zjlg4aph', 'C-000000000014'),
    ('U-01a1zjlg4aph', 'C-000000000015'),
    ('U-01a1zjlg4aph', 'C-000000000016'),
    ('U-01a1zjlg4aph', 'C-000000000017'),
    ('U-01a1zjlg4aph', 'C-000000000018'),
    ('U-01a1zjlg4aph', 'C-000000000019'),

    ('U-02a1zjlg4aph', 'C-000000000028'),
    ('U-02a1zjlg4aph', 'C-000000000027'),
    ('U-02a1zjlg4aph', 'C-000000000026'),
    ('U-02a1zjlg4aph', 'C-000000000025'),
    ('U-02a1zjlg4aph', 'C-000000000024'),
    ('U-02a1zjlg4aph', 'C-000000000023'),
    ('U-02a1zjlg4aph', 'C-000000000022'),
    ('U-02a1zjlg4aph', 'C-000000000021'),
    ('U-02a1zjlg4aph', 'C-000000000020'),
    ('U-02a1zjlg4aph', 'C-000000000019'),
    ('U-02a1zjlg4aph', 'C-000000000018'),
    ('U-02a1zjlg4aph', 'C-000000000017'),
    ('U-02a1zjlg4aph', 'C-000000000016'),
    ('U-02a1zjlg4aph', 'C-000000000015'),
    ('U-02a1zjlg4aph', 'C-000000000014'),
    ('U-02a1zjlg4aph', 'C-000000000013'),
    ('U-02a1zjlg4aph', 'C-000000000011'),
    ('U-02a1zjlg4aph', 'C-000000000010'),
    ('U-02a1zjlg4aph', 'C-000000000009'),
    ('U-02a1zjlg4aph', 'C-000000000008'),
    ('U-02a1zjlg4aph', 'C-000000000007'),
    ('U-02a1zjlg4aph', 'C-000000000006'),
    ('U-02a1zjlg4aph', 'C-000000000005'),
    ('U-02a1zjlg4aph', 'C-000000000004'),
    ('U-02a1zjlg4aph', 'C-000000000003'),
    ('U-02a1zjlg4aph', 'C-000000000002'),
    ('U-02a1zjlg4aph', 'C-000000000001');

Insert into Deck (
    id,
    name,
    playerId)
VALUES
    ('D-000000000001', 'Deck1','U-01a1zjlg4aph'),
    ('D-000000000002', 'Deck2','U-02a1zjlg4aph'),
    ('D-000000000003', 'Deck3','U-02a1zjlg4aph');

INSERT INTO Deck_Card (
    deckId,
    cardId)
VALUES
    ('D-000000000001', 'C-000000000001'),
    ('D-000000000001', 'C-000000000002'),
    ('D-000000000001', 'C-000000000003'),
    ('D-000000000001', 'C-000000000004'),
    ('D-000000000001', 'C-000000000005'),
    ('D-000000000001', 'C-000000000006'),
    ('D-000000000001', 'C-000000000007'),
    ('D-000000000001', 'C-000000000008'),
    ('D-000000000001', 'C-000000000009'),
    ('D-000000000001', 'C-000000000010'),
    ('D-000000000001', 'C-000000000011'),
    ('D-000000000001', 'C-000000000012'),
    ('D-000000000001', 'C-000000000013'),
    ('D-000000000001', 'C-000000000014'),
    ('D-000000000001', 'C-000000000015'),
    ('D-000000000001', 'C-000000000016'),
    ('D-000000000001', 'C-000000000017'),
    ('D-000000000001', 'C-000000000018'),

    ('D-000000000002', 'C-000000000028'),
    ('D-000000000002', 'C-000000000027'),
    ('D-000000000002', 'C-000000000026'),
    ('D-000000000002', 'C-000000000025'),
    ('D-000000000002', 'C-000000000024'),
    ('D-000000000002', 'C-000000000023'),
    ('D-000000000002', 'C-000000000022'),
    ('D-000000000002', 'C-000000000021'),
    ('D-000000000002', 'C-000000000020'),
    ('D-000000000002', 'C-000000000019'),
    ('D-000000000002', 'C-000000000018'),
    ('D-000000000002', 'C-000000000017'),
    ('D-000000000002', 'C-000000000016'),
    ('D-000000000002', 'C-000000000015'),
    ('D-000000000002', 'C-000000000014'),
    ('D-000000000002', 'C-000000000013'),
    ('D-000000000002', 'C-000000000012'),
    ('D-000000000002', 'C-000000000011'),

    ('D-000000000003', 'C-000000000001'),
    ('D-000000000003', 'C-000000000002'),
    ('D-000000000003', 'C-000000000003'),
    ('D-000000000003', 'C-000000000004'),
    ('D-000000000003', 'C-000000000005'),
    ('D-000000000003', 'C-000000000006'),
    ('D-000000000003', 'C-000000000007'),
    ('D-000000000003', 'C-000000000008'),
    ('D-000000000003', 'C-000000000009'),
    ('D-000000000003', 'C-000000000010'),
    ('D-000000000003', 'C-000000000011'),
    ('D-000000000003', 'C-000000000012'),
    ('D-000000000003', 'C-000000000013'),
    ('D-000000000003', 'C-000000000014'),
    ('D-000000000003', 'C-000000000015'),
    ('D-000000000003', 'C-000000000016'),
    ('D-000000000003', 'C-000000000017'),
    ('D-000000000003', 'C-000000000018');

-- INSERT INTO Game (
--     id,
--     maxTurns,
--     turn,
--     player1Id,
--     player2Id,
--     timeStarted)
-- VALUES
--     ('G-000000000001',5,1,'U-01a1zjlg4aph','U-02a1zjlg4aph', '2019-01-01 00:00:00');

-- INSERT INTO Game_Planet(
--     gameId,
--     planetId,
--     show)
-- VALUES
--     ('G-000000000001','P-01a1zjlg4aph', 1),
--     ('G-000000000001','P-02a1zjlg4aph', 1),
--     ('G-000000000001','P-04a1zjlg4aph', 0);

-- INSERT INTO Game_Player(
--     gameId,
--     playerId,
--     deckId)
-- VALUES
--     ('G-000000000001', 'U-01a1zjlg4aph', 'D-000000000001'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'D-000000000002');

-- INSERT INTO Game_Deck(
--     gameId,
--     playerId,
--     cardId)
-- VALUES
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000008'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000009'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000010'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000011'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000012'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000013'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000014'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000015'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000016'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000017'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000018'),

--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000021'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000020'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000019'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000018'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000017'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000016'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000015'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000014'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000013'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000012'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000011');


-- INSERT INTO Hand(
--     gameId,
--     playerId,
--     cardId)
-- VALUES
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000001'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000002'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000003'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000004'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000005'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000006'),
--     ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000007'),

--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000028'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000027'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000026'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000025'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000024'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000023'),
--     ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000022');