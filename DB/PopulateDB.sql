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

SET IDENTITY_INSERT dbo.Image ON;
INSERT INTO IMAGE (id, imageString)
VALUES (1, 'Default Card Image'),
       (2, 'Default Planet Image'),
       (3, 'Default Profile Image');
SET IDENTITY_INSERT dbo.Image OFF;


INSERT INTO Card (
    id, 
    name, 
    energy, 
    cost, 
    typeId, 
    raceId, 
    description, 
    imageId)
VALUES  ('C-000000000001', 'Carta A', 10, 10, 1, 1, 'Descripción A', 1),
        ('C-000000000002', 'Carta B', 10, 10, 1, 1, 'Description B', 1),
        ('C-000000000003', 'Carta C', 10, 10, 1, 1, 'Description C', 1),
        ('C-000000000004', 'Carta D', 10, 10, 1, 1, 'Description D', 1),
        ('C-000000000005', 'Carta E', 10, 10, 1, 1, 'Description E', 1),
        ('C-000000000006', 'Carta F', 10, 10, 1, 1, 'Description F', 1),
        ('C-000000000007', 'Carta G', 10, 10, 1, 1, 'Description G', 1),
        ('C-000000000008', 'Carta H', 10, 10, 1, 1, 'Description H', 1),
        ('C-000000000009', 'Carta I', 10, 10, 1, 1, 'Description I', 1),
        ('C-000000000010', 'Carta J', 10, 10, 1, 1, 'Description J', 1),
        ('C-000000000011', 'Carta K', 10, 10, 1, 1, 'Description K', 1),
        ('C-000000000012', 'Carta M', 10, 10, 1, 1, 'Description L', 1),
        ('C-000000000013', 'Carta M', 10, 10, 1, 1, 'Description M', 1),
        ('C-000000000014', 'Carta N', 10, 10, 1, 1, 'Description N', 1),
        ('C-000000000015', 'Carta O', 10, 10, 1, 1, 'Description O', 1),
        ('C-000000000016', 'Carta P', 10, 10, 1, 1, 'Description P', 1),
        ('C-000000000017', 'Carta Q', 10, 10, 1, 1, 'Description Q', 1),

        ('C-000000000018', 'Carta R', 10, 10, 2, 1, 'Description R', 1),
        ('C-000000000019', 'Carta S', 10, 10, 2, 1, 'Description S', 1),
        ('C-000000000020', 'Carta T', 10, 10, 2, 1, 'Description T', 1),
        ('C-000000000021', 'Carta U', 10, 10, 2, 1, 'Description U', 1),
        ('C-000000000022', 'Carta V', 10, 10, 2, 1, 'Description V', 1),
        ('C-000000000023', 'Carta 1', 10, 10, 3, 1, 'Description 1', 1),
        ('C-000000000024', 'Carta 2', 10, 10, 3, 1, 'Description 2', 1),
        ('C-000000000025', 'Carta 3', 10, 10, 3, 1, 'Description 3', 1),
        ('C-000000000026', 'Carta 4', 10, 10, 3, 1, 'Description 4', 1),
        ('C-000000000027', 'Carta 5', 10, 10, 3, 1, 'Description 5', 1),
        ('C-000000000028', 'Carta 6', 10, 10, 3, 1, 'Description 6', 1);


INSERT INTO Planet (
    id, 
    name, 
    description, 
    TypeId,  
    imageId)
VALUES 
    ('P-01a1zyys4aph','Planeta A','Descripción A',1,1),
    ('P-01a1zjlg4aph','Planeta B','Descripción B',1,1),
    ('P-02a1zjlg4aph','Planeta C','Descripción C',1,1),
    ('P-03a1zjlg4aph','Planeta D','Descripción D',2,1),
    ('P-04a1zjlg4aph','Planeta E','Descripción E',2,1),
    ('P-05a1zjlg4aph','Planeta F','Descripción F',3,1),
    ('P-06a1zjlg4aph','Planeta G','Descripción G',3,1);

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
    ('U-01a1zjlg4aph','yraulbr25@gmail.com', 'Yordi', 'Brenes', 'sadKaladin', '123ABC', 0, 0, 0, 1, 1, 0, 1),
    ('U-02a1zjlg4aph','adriana@gmail.com', 'Adriana', 'Calderon', 'quadriante', '123ABC', 0, 0, 0, 1, 1, 0, 1),
    ('U-03a1zjlg4aph','nasser@gmail,com', 'Nasser', 'Nasser', 'bigNass', '123ABC', 0, 0, 0, 1, 1, 0, 1),
    ('U-04a1zjlg4aph','marcelo@gmail.com', 'Marcelo', 'Truque', 'Marce', '123ABC', 0, 0, 0, 1, 1, 0, 1),
    ('U-05a1zjlg4aph','nuevo1@gmail.com', 'Nuevo', 'Nuevo', 'nuevo1', '123ABC', 0, 0, 0, 1, 1,0, 1),
    ('U-06a1zjlg4aph','nuevo2@gmail.com', 'Nuevo', 'Nuevo', 'nuevo2', '123ABC', 0, 0, 0, 1, 1, 0, 1),
    ('U-07a1zjlg4aph','nuevo3@gmail.com', 'Nuevo', 'Nuevo', 'nuevo3', '123ABC', 0, 0, 0, 1, 1, 0, 1);

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
    ('D-000000000002', 'C-000000000011'),
    ('D-000000000002', 'C-000000000010'),

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