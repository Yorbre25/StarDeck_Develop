Use StarDeck

INSERT INTO Game (
    id,
    maxTurns,
    turn,
    player1Id,
    player2Id,
    timeStarted)
VALUES
    ('G-000000000001',5,1,'U-01a1zjlg4aph','U-02a1zjlg4aph', '2019-01-01 00:00:00');

INSERT INTO Game_Planet(
    gameId,
    planetId,
    show)
VALUES
    ('G-000000000001','P-01a1zjlg4aph', 1),
    ('G-000000000001','P-02a1zjlg4aph', 1),
    ('G-000000000001','P-04a1zjlg4aph', 0);

INSERT INTO Game_Player(
    gameId,
    playerId,
    deckId,
    cardPoints,
    maxCardPoints)
VALUES
    ('G-000000000001', 'U-01a1zjlg4aph', 'D-000000000001', 12, 12),
    ('G-000000000001', 'U-02a1zjlg4aph', 'D-000000000002', 12, 12);

INSERT INTO Game_Deck(
    gameId,
    playerId,
    cardId)
VALUES
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000008'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000009'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000010'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000011'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000012'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000013'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000014'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000015'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000016'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000017'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000018'),

    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000021'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000020'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000019'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000018'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000017'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000016'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000015'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000014'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000013'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000012'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000011');


INSERT INTO Hand(
    gameId,
    playerId,
    cardId)
VALUES
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000001'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000003'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000005'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000006'),
    ('G-000000000001', 'U-01a1zjlg4aph', 'C-000000000007'),

    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000028'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000027'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000026'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000025'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000024'),
    ('G-000000000001', 'U-02a1zjlg4aph', 'C-000000000023');

INSERT INTO GameTable(
    gameId,
    planetId,
    playerId,
    cardId,
    battlePoints)
VALUES
    ('G-000000000001', 'P-01a1zjlg4aph', 'U-01a1zjlg4aph', 'C-000000000002', 10),
    ('G-000000000001', 'P-02a1zjlg4aph', 'U-01a1zjlg4aph', 'C-000000000004', 10),
    ('G-000000000001', 'P-01a1zjlg4aph', 'U-02a1zjlg4aph', 'C-000000000022', 10);