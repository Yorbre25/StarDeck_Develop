using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Models;
using Contracts;
using StarAPI.Constants;

namespace StarAPI.Logic.Mappers
{
    public class PlanetMapper
    {
        private IRepositoryWrapper _context;
        private PlanetTypeHandling _planetTypeHandling;
        private ImageHandling _imageHandling;

        public PlanetMapper(IRepositoryWrapper context)
        {
            _context = context;
            _planetTypeHandling = new PlanetTypeHandling(_context);
            _imageHandling = new ImageHandling(_context);
        }
        
        public OutputPlanet FillOutputPlanet(Planet planet)
        {
            OutputPlanet outputPlanet = new OutputPlanet
            {
                id = planet.id,
                name = planet.name,
                type = _planetTypeHandling.GetPlanetType(planet.typeId),
                description = planet.description,
                image = _imageHandling.GetImage(planet.imageId),
            };
            return outputPlanet;
        }

        public List<OutputPlanet> FillOutputPlanet(List<Planet> planets)
        {
            List<OutputPlanet> outputPlanets = new List<OutputPlanet>();
            foreach (Planet planet in planets)
            {
                outputPlanets.Add(FillOutputPlanet(planet));
            }
            return outputPlanets;
        }
        
        public Planet FillNewPlanet(InputPlanet newPlanet, string id)
        {
            Planet planet = new Planet();
            planet.id = id;
            planet.name = newPlanet.name;
            planet.typeId = newPlanet.typeId;
            planet.activatedPlanet = Const.DefaultActivationState;
            planet.description = newPlanet.description;
            planet.imageId = _imageHandling.GetImageId(newPlanet.image);
            return planet;
        }
    }
}