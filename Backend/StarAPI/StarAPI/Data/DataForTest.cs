using Microsoft.EntityFrameworkCore;
using StarAPI.Logic.ModelHandling;
using StarAPI.DTOs;
using StarAPI.Context;

namespace StarAPI.Data;


public class DataForTest
{

    public InputCard[] cards = new InputCard[]{

        new InputCard()
        {
            name = "Carta A",
            energy = 10,
            cost = 10,
            typeId = 2,
            raceId = 1,
            description = "Descripción A",
            image = "Imagen A"
        },
        new InputCard()
        {
            name = "Carta B",
            energy = 10,
            cost = 10,
            typeId = 2,
            raceId = 1,
            description = "Descripción B",
            image = "Imagen B"
        },
        new InputCard()
        {
            name = "Carta C",
            energy = 10,
            cost = 10,
            typeId = 2,
            raceId = 1,
            description = "Descripción C",
            image = "Imagen C"
        },
        new InputCard()
        {
            name = "Carta D",
            energy = 10,
            cost = 10,
            typeId = 2,
            raceId = 1,
            description = "Descripción D",
            image = "Imagen D"
        },
        new InputCard()
        {
            name = "Carta E",
            energy = 10,
            cost = 10,
            typeId = 2,
            raceId = 1,
            description = "Descripción E",
            image = "Imagen E"
        },
        new InputCard()
        {
            name = "Carta F",
            energy = 10,
            cost = 10,
            typeId = 3,
            raceId = 1,
            description = "Descripción F",
            image = "Imagen F"
        },
        new InputCard()
        {
            name = "Carta G",
            energy = 10,
            cost = 10,
            typeId = 3,
            raceId = 1,
            description = "Descripción G",
            image = "Imagen G"
        },
        new InputCard()
        {
            name = "Carta H",
            energy = 10,
            cost = 10,
            typeId = 3,
            raceId = 1,
            description = "Descripción H",
            image = "Imagen H"
        },
        new InputCard()
        {
            name = "Carta I",
            energy = 10,
            cost = 10,
            typeId = 3,
            raceId = 1,
            description = "Descripción I",
            image = "Imagen I"
        },
        new InputCard()
        {
            name = "Carta J",
            energy = 10,
            cost = 10,
            typeId = 3,
            raceId = 1,
            description = "Descripción J",
            image = "Imagen J"
        }
    };

    public string[] races = new string[]
        {
            "Human",
            "Trisolariano",
            "Robot",
            "Marciano",
            "Ciborg"
        };
    
    public string[] cardTypes = new string[]
        {
            "Basica",
            "Normal",
            "Rara",
            "Muy Rara",
            "Ultra Rara"
        };

    public int numero = 3;


}









